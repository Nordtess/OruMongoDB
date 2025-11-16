using OruMongoDB.UI;

namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void btnTest_Click(object sender, EventArgs e)
        {
            var reader = new PoddDbReader();


            rtbLog.Text = "L�ser fr�n databasen...";

            string result = await reader.ReadAllAsync();


            rtbLog.Text = result;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            JamieForm f = new JamieForm();
            f.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var f = new AlexForm();
            f.Show(); 
        }
        private void btnCategories_Click(object sender, EventArgs e)
        {
            DiyarForm form = new DiyarForm();
            form.Show();
        }
    }

}
