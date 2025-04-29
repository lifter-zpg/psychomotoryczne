namespace testy_psycho
{
    partial class Form3
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
            testowy_dz = new Button();
            SuspendLayout();
            // 
            // testowy_dz
            // 
            testowy_dz.Location = new Point(273, 343);
            testowy_dz.Name = "testowy_dz";
            testowy_dz.Size = new Size(227, 29);
            testowy_dz.TabIndex = 2;
            testowy_dz.Text = "Tryb testowy";
            testowy_dz.UseVisualStyleBackColor = true;
            testowy_dz.Click += testowy_dz_Click;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(testowy_dz);
            Name = "Form3";
            Text = "Test dźwiękowy";
            ResumeLayout(false);
        }

        #endregion

        private Button testowy_dz;
    }
}