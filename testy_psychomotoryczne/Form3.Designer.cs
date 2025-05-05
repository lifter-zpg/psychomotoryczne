namespace testy_psychomotoryczne
{
    partial class Form3
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            button1 = new Button();
            button2 = new Button();
            resultLabel1 = new Label();
            resultLabel2 = new Label();
            resultLabel3 = new Label();
            resultLabel4 = new Label();
            resultLabel5 = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(1, 1);
            button1.Name = "button1";
            button1.Size = new Size(170, 35);
            button1.TabIndex = 0;
            button1.Text = "Tryb szkoleniowy";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(1, 38);
            button2.Name = "button2";
            button2.Size = new Size(170, 35);
            button2.TabIndex = 1;
            button2.Text = "Tryb testowy";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // resultLabel1
            // 
            resultLabel1.Location = new Point(180, 10);
            resultLabel1.Name = "resultLabel1";
            resultLabel1.Size = new Size(300, 25);
            resultLabel1.TabIndex = 3;
            // 
            // resultLabel2
            // 
            resultLabel2.Location = new Point(180, 40);
            resultLabel2.Name = "resultLabel2";
            resultLabel2.Size = new Size(300, 25);
            resultLabel2.TabIndex = 4;
            // 
            // resultLabel3
            // 
            resultLabel3.Location = new Point(180, 70);
            resultLabel3.Name = "resultLabel3";
            resultLabel3.Size = new Size(300, 25);
            resultLabel3.TabIndex = 5;
            // 
            // resultLabel4
            // 
            resultLabel4.Location = new Point(180, 100);
            resultLabel4.Name = "resultLabel4";
            resultLabel4.Size = new Size(300, 25);
            resultLabel4.TabIndex = 6;
            // 
            // resultLabel5
            // 
            resultLabel5.Location = new Point(180, 130);
            resultLabel5.Name = "resultLabel5";
            resultLabel5.Size = new Size(300, 25);
            resultLabel5.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1, 416);
            label1.Name = "label1";
            label1.Size = new Size(453, 25);
            label1.TabIndex = 8;
            label1.Text = "Kliknij w podświetlone przyciski gdy się pojawią.";
            label1.Click += label1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(470, 10);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(300, 150);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1280, 720);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(resultLabel1);
            Controls.Add(resultLabel2);
            Controls.Add(resultLabel3);
            Controls.Add(resultLabel4);
            Controls.Add(resultLabel5);
            Name = "Form3";
            Text = "Test reakcji";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private System.Windows.Forms.Label resultLabel1;
        private System.Windows.Forms.Label resultLabel2;
        private System.Windows.Forms.Label resultLabel3;
        private System.Windows.Forms.Label resultLabel4;
        private System.Windows.Forms.Label resultLabel5;
        private System.Windows.Forms.Label label1;
        private PictureBox pictureBox1;
    }
}