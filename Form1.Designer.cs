namespace testy_psycho
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            wizualny = new Button();
            dzwiekowy = new Button();
            main_etykieta = new Label();
            SuspendLayout();
            // 
            // wizualny
            // 
            wizualny.Location = new Point(132, 190);
            wizualny.Name = "wizualny";
            wizualny.Size = new Size(170, 29);
            wizualny.TabIndex = 0;
            wizualny.Text = "Test wizualny";
            wizualny.UseVisualStyleBackColor = true;
            wizualny.Click += wizualny_Click;
            // 
            // dzwiekowy
            // 
            dzwiekowy.Location = new Point(395, 194);
            dzwiekowy.Name = "dzwiekowy";
            dzwiekowy.Size = new Size(217, 29);
            dzwiekowy.TabIndex = 1;
            dzwiekowy.Text = "Test dźwiękowy";
            dzwiekowy.UseVisualStyleBackColor = true;
            dzwiekowy.Click += dzwiekowy_Click;
            // 
            // main_etykieta
            // 
            main_etykieta.AutoSize = true;
            main_etykieta.Font = new Font("Franklin Gothic Medium", 15F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 238);
            main_etykieta.Location = new Point(182, 36);
            main_etykieta.Name = "main_etykieta";
            main_etykieta.Size = new Size(466, 32);
            main_etykieta.TabIndex = 2;
            main_etykieta.Text = "Testy psychomotoryczne dla kierowców";
            main_etykieta.Click += main_etykieta_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(main_etykieta);
            Controls.Add(dzwiekowy);
            Controls.Add(wizualny);
            Name = "Form1";
            Text = "Testy psychomotoryczne";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button wizualny;
        private Button dzwiekowy;
        private Label main_etykieta;
    }
}
