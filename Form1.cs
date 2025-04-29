namespace testy_psycho
{
    public partial class Form1 : Form
    {
        private Form2 okno_wizualne;
        private Form3 okno_dzwiekowe;

        public Form1()
        {
            InitializeComponent();
        }

        private void wizualny_Click(object sender, EventArgs e)
        {
            if (okno_wizualne == null || okno_wizualne.IsDisposed)
            {
                okno_wizualne = new Form2();
                okno_wizualne.FormClosed += (s, args) => okno_wizualne = null;
                okno_wizualne.Show();
            }
            else
            {
                okno_wizualne.BringToFront();
            }
        }

        private void dzwiekowy_Click(object sender, EventArgs e)
        {
            if (okno_dzwiekowe == null || okno_dzwiekowe.IsDisposed)
            {
                okno_dzwiekowe = new Form3();
                okno_dzwiekowe.FormClosed += (s, args) => okno_dzwiekowe = null;
                okno_dzwiekowe.Show();
            }
            else
            {
                okno_dzwiekowe.BringToFront();
            }
        }

        private void main_etykieta_Click(object sender, EventArgs e)
        {
            // Nieobs³ugiwane zdarzenie – mo¿na usun¹æ lub dodaæ funkcjonalnoœæ
        }
    }
}
