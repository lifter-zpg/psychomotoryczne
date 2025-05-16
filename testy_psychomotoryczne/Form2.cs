using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testy_psychomotoryczne
{
    public partial class Form2 : Form
    {
        private bool isVisualTest;
        private Random random = new Random();
        private Stopwatch stopwatch = new Stopwatch();
        private bool trainingMode = false;
        private bool testMode = false;
        private int testCount = 0;
        private List<Label> resultLabels = new List<Label>();
        private List<long> reactionTimes = new List<long>();
        private Bitmap? chartBitmap;
        private bool waitingForSignal = false;
        private bool testActive = false;
        private CancellationTokenSource? cancelTokenSource;


        public Form2(bool visualTest)
        {
            InitializeComponent();
            this.isVisualTest = visualTest;
            resultLabels.Add(resultLabel1);
            resultLabels.Add(resultLabel2);
            resultLabels.Add(resultLabel3);
            resultLabels.Add(resultLabel4);
            resultLabels.Add(resultLabel5);

            this.KeyPreview = true;
            this.KeyDown += Form2_KeyDown;
            this.Resize += Form2_Resize;

            pictureBox1.Visible = false;
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.Controls.Add(pictureBox1);

            button3.Anchor = AnchorStyles.None;

            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            button1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            foreach (var label in resultLabels)
            {
                label.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }

            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    ((Button)control).TabStop = false;
                }
            }

            ArrangeControls();
        }

        private void Form2_Resize(object? sender, EventArgs e)
        {
            ArrangeControls();
        }

        private void ArrangeControls()
        {
            pictureBox1.Location = new Point(this.ClientSize.Width - pictureBox1.Width - 30, 20);
            pictureBox1.Size = new Size(this.ClientSize.Width / 4, this.ClientSize.Height / 4);

            button3.Location = new Point(
                (this.ClientSize.Width - button3.Width) / 2,
                (this.ClientSize.Height - button3.Height) / 2);

            label1.Location = new Point(20, this.ClientSize.Height - label1.Height - 20);
            if (isVisualTest == false)
            {
                label1.Text = "Po usłyszeniu dźwięku wciśnij spację lub przycisk reaguj.";
            }
            else
            {
                label1.Text = "Po zmianie koloru tła wciśnij spację lub przycisk reaguj.";
            }

            button1.Location = new Point(20, 20);
            button2.Location = new Point(20, 60);

            int resultsTop = button2.Bottom + 20;
            for (int i = 0; i < resultLabels.Count; i++)
            {
                resultLabels[i].Location = new Point(20, resultsTop + i * 25);
            }
        }

        private void Form2_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (waitingForSignal && testActive)
                {
                    stopwatch.Stop();
                    testActive = false;
                    waitingForSignal = false;
                    cancelTokenSource?.Cancel();

                    MessageBox.Show("FALSTART! Test został przerwany.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    if (isVisualTest)
                    {
                        this.BackColor = SystemColors.Control;
                    }
                }

                else if (button3.Enabled && testActive)
                {
                    button3.PerformClick();
                    e.Handled = true;
                }
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            trainingMode = true;
            testMode = false;
            reactionTimes.Clear();
            pictureBox1.Visible = false;
            testActive = true;
            await StartReactionTestAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            trainingMode = false;
            testMode = true;
            testCount = 0;
            reactionTimes.Clear();

            foreach (var label in resultLabels)
            {
                label.Text = "";
            }

            pictureBox1.Visible = false;
            testActive = true;
            await StartReactionTestAsync();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            if (waitingForSignal && testActive)
            {
                stopwatch.Stop();
                testActive = false;
                waitingForSignal = false;
                cancelTokenSource?.Cancel();

                MessageBox.Show("FALSTART! Test został przerwany.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (isVisualTest)
                {
                    this.BackColor = SystemColors.Control;
                }

                return;
            }


            if (stopwatch.IsRunning)
            {
                stopwatch.Stop();
                if (isVisualTest)
                {
                    this.BackColor = SystemColors.Control;
                }
                var reactionTime = stopwatch.ElapsedMilliseconds;
                reactionTimes.Add(reactionTime);

                if (trainingMode)
                {
                    MessageBox.Show($"Czas reakcji: {reactionTime} ms", "Wynik");
                }
                else if (testMode)
                {
                    if (testCount < 5)
                    {
                        resultLabels[testCount].Text = $"Test {testCount + 1}: {reactionTime} ms";
                        testCount++;
                        if (testCount < 5)
                        {
                            await StartReactionTestAsync();
                        }
                        else
                        {
                            ShowTestResults();
                            testActive = false;
                        }
                    }
                }
            }
        }


        private void ShowTestResults()
        {
            double averageTime = reactionTimes.Average();
            string resultMessage = $"Średni czas reakcji: {averageTime:F2} ms\n";

            if (averageTime > 500)
            {
                resultMessage += "Kierowca nie przeszedł testu.";
            }
            else
            {
                resultMessage += "Kierowca przeszedł test.";
            }

            GenerateChart();
            MessageBox.Show(resultMessage, "Wynik testu");
        }

        private void GenerateChart()
        {
            if (reactionTimes.Count == 0) return;

            int width = pictureBox1.Width;
            int height = pictureBox1.Height;
            chartBitmap = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(chartBitmap))
            {
                g.Clear(Color.White);

                long maxValue = reactionTimes.Max();
                float scaleY = (height - 40) / (float)maxValue;
                float scaleX = (width - 40) / reactionTimes.Count;

                g.DrawLine(Pens.Black, 30, height - 30, width - 10, height - 30);
                g.DrawLine(Pens.Black, 30, height - 30, 30, 10);

                Font font = new Font("Arial", 8);
                g.DrawString("Numer testu", font, Brushes.Black, width / 2 - 30, height - 20);

                for (int i = 0; i <= 10; i++)
                {
                    int y = height - 30 - (int)(i * (height - 40) / 10);
                    g.DrawLine(Pens.Gray, 25, y, 30, y);
                    g.DrawString((i * maxValue / 10).ToString(), font, Brushes.Black, 5, y - 7);
                }

                for (int i = 0; i < reactionTimes.Count; i++)
                {
                    float x = 40 + i * scaleX;
                    float barHeight = reactionTimes[i] * scaleY;
                    float y = height - 30 - barHeight;
                    float barWidth = scaleX * 0.7f;

                    g.FillRectangle(Brushes.Blue, x, y, barWidth, barHeight);
                    g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight);

                    g.DrawString(reactionTimes[i].ToString(), font, Brushes.Black, x, y - 15);
                    g.DrawString((i + 1).ToString(), font, Brushes.Black, x + barWidth / 2 - 5, height - 25);
                }

                float average = (float)reactionTimes.Average();
                float yAverage = height - 30 - average * scaleY;
                g.DrawLine(new Pen(Color.Red, 2) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash },
                            30, yAverage, width - 10, yAverage);
                g.DrawString($"Średnia: {average:F1}", font, Brushes.Red, width - 120, yAverage - 15);
            }

            pictureBox1.Image = chartBitmap;
            pictureBox1.Visible = true;
        }

        private async Task StartReactionTestAsync()
        {
            cancelTokenSource?.Cancel();
            cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;

            button3.Enabled = true;
            waitingForSignal = true;

            int delay = random.Next(2000, 5001);

            try
            {
                await Task.Delay(delay, token);
            }
            catch (TaskCanceledException)
            {
                return;
            }

            if (!testActive || token.IsCancellationRequested) return;

            waitingForSignal = false;

            if (isVisualTest)
            {
                this.BackColor = Color.Yellow;
            }
            else
            {
                Console.Beep();
            }

            stopwatch.Restart();
            button3.Enabled = true;
        }


        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
