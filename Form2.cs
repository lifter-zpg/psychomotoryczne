using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace testy_psycho
{
    public partial class Form2 : Form
    {
        private void StartButton_Click_1(object sender, EventArgs e)
        {

        }
        private System.Windows.Forms.Timer waitTimer;
        private Random rand;
        private Stopwatch stopwatch;
        private bool testActive = false;
        private bool isTrainingMode = true; // Domyślnie tryb szkoleniowy

        private List<long> testResults = new List<long>();
        private Label resultsLabel;

        public Form2()
        {
            InitializeComponent();
            InitializeReactionTest();
        }

        private void InitializeReactionTest()
        {
            rand = new Random();
            stopwatch = new Stopwatch();

            waitTimer = new System.Windows.Forms.Timer();
            waitTimer.Tick += WaitTimer_Tick;

            this.MouseDown += Form2_MouseDown;

            this.BackColor = Color.White;
            this.Text = "Test czasu reakcji";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Przypięcie przycisków z Designera
            this.StartButton.Click += StartButton_Click;
            this.testowy_wiz.Click += testowy_wiz_Click;
            this.Tryb_szkoleniowy.Click += Tryb_szkoleniowy_Click;

            // Etykieta do wyświetlania wyników
            resultsLabel = new Label();
            resultsLabel.AutoSize = true;
            resultsLabel.Font = new Font("Arial", 10);
            resultsLabel.Location = new Point(10, 10);
            this.Controls.Add(resultsLabel);

            this.avg_time.Text = "Średni czas: -";

            UpdateResultsDisplay();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartButton.Enabled = false;
            this.BackColor = Color.White;
            testActive = false;

            int delay = rand.Next(2000, 5000);
            waitTimer.Interval = delay;
            waitTimer.Start();
        }

        private void WaitTimer_Tick(object sender, EventArgs e)
        {
            waitTimer.Stop();
            this.BackColor = Color.Red;
            stopwatch.Restart();
            testActive = true;
        }

        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            if (testActive)
            {
                stopwatch.Stop();
                testActive = false;
                this.BackColor = Color.White;

                long reactionTime = stopwatch.ElapsedMilliseconds;
                MessageBox.Show($"Twój czas reakcji: {reactionTime} ms", "Wynik");

                if (!isTrainingMode)
                {
                    testResults.Add(reactionTime);
                    if (testResults.Count > 5)
                        testResults.RemoveAt(0); // Zachowujemy tylko ostatnie 5
                    UpdateResultsDisplay();
                }

                StartButton.Enabled = true;
            }
            else if (!StartButton.Enabled)
            {
                waitTimer.Stop();
                this.BackColor = Color.White;
                MessageBox.Show("Za wcześnie! Poczekaj na czerwony ekran.", "Błąd");
                StartButton.Enabled = true;
            }
        }

        private void UpdateResultsDisplay()
        {
            if (testResults.Count == 0)
            {
                resultsLabel.Text = "Brak wyników testowych.";
                this.avg_time.Text = "Średni czas: -";

            }
            else
            {
                resultsLabel.Text = "Ostatnie wyniki:\n" +
                    string.Join("\n", testResults.Select((t, i) => $"{i + 1}. {t} ms"));

                long avg = (long)testResults.Average();
                avg_time.Text = $"Średni czas: {avg} ms";
            }
        }

        private void Tryb_szkoleniowy_Click(object sender, EventArgs e)
        {
            isTrainingMode = true;
            MessageBox.Show("Tryb szkoleniowy aktywny.\nWyniki nie będą zapisywane.");
        }

        private void testowy_wiz_Click(object sender, EventArgs e)
        {
            isTrainingMode = false;
            MessageBox.Show("Tryb testowy aktywny.\nWyniki będą zapisywane.");
        }

        private void avg_time_Click(object sender, EventArgs e)
        {

        }
    }
}
