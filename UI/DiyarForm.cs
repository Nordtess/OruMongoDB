using OruMongoDB.BusinessLayer;
using OruMongoDB.BusinessLayer.Rss;
using OruMongoDB.Domain;
using OruMongoDB.Infrastructure;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace UI
{
    public partial class DiyarForm : Form
    {
        private readonly CategoryService _categoryService =
            new CategoryService(new CategoryRepository(MongoConnector.Instance.GetDatabase()));
        private readonly PoddService _poddService =
        new PoddService(
        new PoddflodeRepository(MongoConnector.Instance.GetDatabase()),
        new PoddAvsnittRepository(MongoConnector.Instance.GetDatabase()),
        new RssParser(),
        MongoConnector.Instance);


        public DiyarForm()
        {
            InitializeComponent();
            _ = LoadCategoriesAsync();
        }

        private async void BtnCreate_Click(object sender, EventArgs e)
        {
            string name = txtCategoryName.Text.Trim();

            try
            {
                await _categoryService.CreateCategoryAsync(name);
                MessageBox.Show("Kategori skapad!");
                txtCategoryName.Clear();
                await LoadCategoriesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel: " + ex.Message);
            }
        }

        private async Task LoadCategoriesAsync()
        {
            lstCategories.Items.Clear();
            var categories = await _categoryService.GetAllCategoriesAsync();

            foreach (var c in categories)
                lstCategories.Items.Add($"{c.Id} - {c.Namn}");
        }

        private void txtCategoryName_TextChanged(object sender, EventArgs e)
        {

        }
        private async void btnSetCategory_Click(object sender, EventArgs e)
        {
            string poddId = txtPoddId.Text.Trim();
            string categoryId = txtCategoryId.Text.Trim();

            try
            {
                await _poddService.AssignCategoryAsync(poddId, categoryId);
                MessageBox.Show("Kategori satt på podden!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fel: " + ex.Message);
            }
        }

    }
}
