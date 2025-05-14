using System;
using System.Drawing;
using System.Windows.Forms;

namespace testy_psychomotoryczne
{
    public partial class Form1 : Form
    {
        private bool isVisualTest = false;
        public Form1()
        {
            InitializeComponent();
            this.Resize += (s, e) => CenterButtons();
            CenterButtons();

            foreach (Control control in this.Controls)
            {
                if (control is Button)
                {
                    ((Button)control).TabStop = false;
                }
            }
        }

        private void CenterButtons()
        {
            int spacing = 40;
            int buttonWidth = button1.Width;
            int totalWidth = (3 * buttonWidth) + (2 * spacing);
            int xStart = (ClientSize.Width - totalWidth) / 2;
            int y = ClientSize.Height - 70;

            button1.Location = new Point(xStart, y);
            button2.Location = new Point(xStart + buttonWidth + spacing, y);
            button3.Location = new Point(xStart + 2 * (buttonWidth + spacing), y);

            pictureBox1.Size = new Size(ClientSize.Width / 2, ClientSize.Height / 2);
            pictureBox1.Location = new Point((ClientSize.Width - pictureBox1.Width) / 2, 100);

            label1.Location = new Point((ClientSize.Width - label1.Width) / 2, 30);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            isVisualTest = false;
            Form2 form2 = new Form2(isVisualTest);
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isVisualTest = true;
            Form2 form2 = new Form2(isVisualTest);
            form2.Show();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            isVisualTest = true;
            Form3 form3 = new Form3(isVisualTest);
            form3.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}


