#nullable disable
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace testy_psychomotoryczne
{
    public partial class Form3 : Form
    {
        private bool isVisualTest;
        private Random random = new Random();
        private Stopwatch stopwatch = new Stopwatch();
        private bool trainingMode = false;
        private bool testMode = false;
        private int testCount = 0;
        private int currentTestLevel = 1;
        private List<System.Windows.Forms.Label> resultLabels = new List<System.Windows.Forms.Label>();
        private List<long> reactionTimes = new List<long>();
        private Bitmap chartBitmap;
        private List<Button> reactionButtons = new List<Button>();
        private List<Color> originalButtonColors = new List<Color>();
        private List<int> activeButtons = new List<int>();
        private HashSet<int> clickedButtons = new HashSet<int>();

        public Form3(bool visualTest)
        {
            InitializeComponent();
            this.isVisualTest = visualTest;
            resultLabels.Add(resultLabel1);
            resultLabels.Add(resultLabel2);
            resultLabels.Add(resultLabel3);
            resultLabels.Add(resultLabel4);
            resultLabels.Add(resultLabel5);

            this.KeyPreview = true;
            this.KeyDown += Form3_KeyDown;
            this.Resize += Form3_Resize;

            CreateReactionButtons();

            pictureBox1.Visible = false;
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            this.Controls.Add(pictureBox1);

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

        private void CreateReactionButtons()
        {
            int buttonWidth = 100;
            int buttonHeight = 80;
            int margin = 10;
            int startX = (this.ClientSize.Width - (4 * buttonWidth + 3 * margin)) / 2;
            int startY = (this.ClientSize.Height - (3 * buttonHeight + 2 * margin)) / 2;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    Button btn = new Button();
                    btn.Width = buttonWidth;
                    btn.Height = buttonHeight;
                    btn.Left = startX + col * (buttonWidth + margin);
                    btn.Top = startY + row * (buttonHeight + margin);
                    btn.Text = "";
                    btn.Enabled = false;
                    btn.Tag = row * 4 + col;
                    btn.Click += ReactionButton_Click;
                    reactionButtons.Add(btn);
                    originalButtonColors.Add(btn.BackColor);
                    this.Controls.Add(btn);
                }
            }
        }

        private void Form3_Resize(object sender, EventArgs e)
        {
            ArrangeControls();
        }

        private void ArrangeControls()
        {
            pictureBox1.Location = new Point(this.ClientSize.Width - pictureBox1.Width - 30, 20);
            pictureBox1.Size = new Size(this.ClientSize.Width / 4, this.ClientSize.Height / 4);

            int buttonWidth = 100;
            int buttonHeight = 80;
            int margin = 10;
            int startX = (this.ClientSize.Width - (4 * buttonWidth + 3 * margin)) / 2;
            int startY = (this.ClientSize.Height - (3 * buttonHeight + 2 * margin)) / 2;

            for (int i = 0; i < reactionButtons.Count; i++)
            {
                int row = i / 4;
                int col = i % 4;
                reactionButtons[i].Left = startX + col * (buttonWidth + margin);
                reactionButtons[i].Top = startY + row * (buttonHeight + margin);
            }

            label1.Location = new Point(20, this.ClientSize.Height - label1.Height - 20);

            button1.Location = new Point(20, 20);
            button2.Location = new Point(20, 60);

            int resultsTop = button2.Bottom + 20;
            for (int i = 0; i < resultLabels.Count; i++)
            {
                resultLabels[i].Location = new Point(20, resultsTop + i * 25);
            }
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            trainingMode = true;
            testMode = false;
            reactionTimes.Clear();
            clickedButtons.Clear();
            pictureBox1.Visible = false;
            currentTestLevel = 1;
            await StartReactionTestAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            trainingMode = false;
            testMode = true;
            testCount = 0;
            currentTestLevel = 1;
            reactionTimes.Clear();
            clickedButtons.Clear();

            foreach (var label in resultLabels)
            {
                label.Text = "";
            }

            pictureBox1.Visible = false;
            await StartReactionTestAsync();
        }

        private void ReactionButton_Click(object sender, EventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                Button clickedButton = (Button)sender;
                int buttonIndex = (int)clickedButton.Tag;

                if (activeButtons.Contains(buttonIndex) && !clickedButtons.Contains(buttonIndex))
                {
                    clickedButtons.Add(buttonIndex);
                    clickedButton.BackColor = Color.Green;
                    clickedButton.Enabled = false;

                    if (clickedButtons.Count == activeButtons.Count)
                    {
                        stopwatch.Stop();
                        var reactionTime = stopwatch.ElapsedMilliseconds;
                        reactionTimes.Add(reactionTime);

                        if (trainingMode)
                        {
                            MessageBox.Show($"Czas reakcji: {reactionTime} ms", "Wynik");
                            currentTestLevel++;
                            if (currentTestLevel <= 5)
                            {
                                _ = StartReactionTestAsync();
                            }
                        }
                        else if (testMode)
                        {
                            if (testCount < 5)
                            {
                                resultLabels[testCount].Text = $"Test {testCount + 1}: {reactionTime} ms";
                                testCount++;
                                currentTestLevel++;
                                if (testCount < 5)
                                {
                                    _ = StartReactionTestAsync();
                                }
                                else
                                {
                                    ShowTestResults();
                                }
                            }
                        }
                    }
                }
            }
        }

        private void ShowTestResults()
        {
            double averageTime = reactionTimes.Average();
            string resultMessage = $"Średni czas reakcji: {averageTime:F2} ms\n";

            if (averageTime > 1500)
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

                //osie
                g.DrawLine(Pens.Black, 30, height - 30, width - 10, height - 30); //X
                g.DrawLine(Pens.Black, 30, height - 30, 30, 10); //Y

                //opisy osi
                Font font = new Font("Arial", 8);
                g.DrawString("Numer testu", font, Brushes.Black, width / 2 - 30, height - 20);

                //liczby na osi Y
                for (int i = 0; i <= 10; i++)
                {
                    int y = height - 30 - (int)(i * (height - 40) / 10);
                    g.DrawLine(Pens.Gray, 25, y, 30, y);
                    g.DrawString((i * maxValue / 10).ToString(), font, Brushes.Black, 5, y - 7);
                }

                //slupki
                for (int i = 0; i < reactionTimes.Count; i++)
                {
                    float x = 40 + i * scaleX;
                    float barHeight = reactionTimes[i] * scaleY;
                    float y = height - 30 - barHeight;
                    float barWidth = scaleX * 0.7f;

                    g.FillRectangle(Brushes.Blue, x, y, barWidth, barHeight);
                    g.DrawRectangle(Pens.Black, x, y, barWidth, barHeight);

                    //slupki etykiety
                    g.DrawString(reactionTimes[i].ToString(), font, Brushes.Black, x, y - 15);
                    g.DrawString((i + 1).ToString(), font, Brushes.Black, x + barWidth / 2 - 5, height - 25);
                }

                //srednia
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
            ResetButtons();
            activeButtons.Clear();
            clickedButtons.Clear();

            var availableIndices = Enumerable.Range(0, reactionButtons.Count).ToList();
            for (int i = 0; i < currentTestLevel; i++)
            {
                if (availableIndices.Count == 0) break;

                int randomIndex = random.Next(availableIndices.Count);
                activeButtons.Add(availableIndices[randomIndex]);
                availableIndices.RemoveAt(randomIndex);
            }

            int delay = random.Next(2000, 5001);
            await Task.Delay(delay);

            foreach (int index in activeButtons)
            {
                reactionButtons[index].BackColor = Color.Yellow;
                reactionButtons[index].Enabled = true;
            }

            stopwatch.Restart();
        }

        private void ResetButtons()
        {
            for (int i = 0; i < reactionButtons.Count; i++)
            {
                reactionButtons[i].BackColor = originalButtonColors[i];
                reactionButtons[i].Enabled = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}