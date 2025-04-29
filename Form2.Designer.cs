namespace testy_psycho
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            testowy_wiz = new Button();
            StartButton = new Button();
            Tryb_szkoleniowy = new Button();
            avg_time = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Franklin Gothic Medium", 15F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            label1.Location = new Point(291, 26);
            label1.Name = "label1";
            label1.Size = new Size(243, 32);
            label1.TabIndex = 0;
            label1.Text = "Prosty test wizualny";
            // 
            // testowy_wiz
            // 
            testowy_wiz.Location = new Point(12, 375);
            testowy_wiz.Name = "testowy_wiz";
            testowy_wiz.Size = new Size(227, 29);
            testowy_wiz.TabIndex = 1;
            testowy_wiz.Text = "Tryb testowy";
            testowy_wiz.UseVisualStyleBackColor = true;
            testowy_wiz.Click += testowy_wiz_Click;
            // 
            // StartButton
            // 
            StartButton.Location = new Point(341, 197);
            StartButton.Name = "StartButton";
            StartButton.Size = new Size(94, 29);
            StartButton.TabIndex = 2;
            StartButton.Text = "Start";
            StartButton.UseVisualStyleBackColor = true;
            StartButton.Click += StartButton_Click_1;
            // 
            // Tryb_szkoleniowy
            // 
            Tryb_szkoleniowy.Location = new Point(12, 340);
            Tryb_szkoleniowy.Name = "Tryb_szkoleniowy";
            Tryb_szkoleniowy.Size = new Size(227, 29);
            Tryb_szkoleniowy.TabIndex = 3;
            Tryb_szkoleniowy.Text = "Tryb szkoleniowy";
            Tryb_szkoleniowy.UseVisualStyleBackColor = true;
            Tryb_szkoleniowy.Click += Tryb_szkoleniowy_Click;
            // 
            // avg_time
            // 
            avg_time.AutoSize = true;
            avg_time.Location = new Point(57, 277);
            avg_time.Name = "avg_time";
            avg_time.Size = new Size(83, 20);
            avg_time.TabIndex = 4;
            avg_time.Text = "Średni czas";
            avg_time.Click += avg_time_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(avg_time);
            Controls.Add(Tryb_szkoleniowy);
            Controls.Add(StartButton);
            Controls.Add(testowy_wiz);
            Controls.Add(label1);
            Name = "Form2";
            Text = "Test wizualny";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button testowy_wiz;
        private Button StartButton;
        private Button Tryb_szkoleniowy;
        private Label avg_time;
    }
}