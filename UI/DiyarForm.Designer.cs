namespace UI
{
    partial class DiyarForm
    {
        private System.ComponentModel.IContainer components = null;

        // Fälten ska ligga HÄR, inte utanför namespace eller InitializeComponent
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.ListBox lstCategories;
        private System.Windows.Forms.TextBox txtPoddId;
        private System.Windows.Forms.TextBox txtCategoryId;
        private System.Windows.Forms.Button btnSetCategory;
        private TextBox txtEditCategoryId;
        private TextBox txtEditCategoryName;
        private Button btnEditCategoryName;
        private ListBox lstCategoryPods;
        private Button btnShowPodsByCategory;



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
            lblName = new Label();
            txtCategoryName = new TextBox();
            btnCreate = new Button();
            lstCategories = new ListBox();
            txtPoddId = new TextBox();
            txtCategoryId = new TextBox();
            btnSetCategory = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.Location = new Point(20, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(150, 30);
            lblName.TabIndex = 0;
            lblName.Text = "Kategorinamn:";
            // 
            // txtCategoryName
            // 
            txtCategoryName.Location = new Point(20, 50);
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new Size(250, 23);
            txtCategoryName.TabIndex = 1;
            txtCategoryName.TextChanged += txtCategoryName_TextChanged;
            // 
            // btnCreate
            // 
            btnCreate.Location = new Point(20, 90);
            btnCreate.Name = "btnCreate";
            btnCreate.Size = new Size(250, 40);
            btnCreate.TabIndex = 2;
            btnCreate.Text = "Skapa kategori";
            btnCreate.Click += BtnCreate_Click;
            // 
            // lstCategories
            // 
            lstCategories.Location = new Point(300, 20);
            lstCategories.Name = "lstCategories";
            lstCategories.Size = new Size(260, 214);
            lstCategories.TabIndex = 3;
            // 
            // txtPoddId
            // 
            txtPoddId.Location = new Point(20, 160);
            txtPoddId.Name = "txtPoddId";
            txtPoddId.PlaceholderText = "Podd ID";
            txtPoddId.Size = new Size(250, 23);
            txtPoddId.TabIndex = 0;
            txtPoddId.TextChanged += txtPoddId_TextChanged;
            // 
            // txtCategoryId
            // 
            txtCategoryId.Location = new Point(20, 200);
            txtCategoryId.Name = "txtCategoryId";
            txtCategoryId.PlaceholderText = "Kategori ID";
            txtCategoryId.Size = new Size(250, 23);
            txtCategoryId.TabIndex = 1;
            // 
            // btnSetCategory
            // 
            btnSetCategory.Location = new Point(20, 240);
            btnSetCategory.Name = "btnSetCategory";
            btnSetCategory.Size = new Size(250, 40);
            btnSetCategory.TabIndex = 2;
            btnSetCategory.Text = "Sätt kategori på podd";
            btnSetCategory.Click += btnSetCategory_Click;

            // txtEditCategoryId
            txtEditCategoryId = new TextBox();
            txtEditCategoryId.Location = new Point(300, 250);
            txtEditCategoryId.Size = new Size(260, 23);
            txtEditCategoryId.PlaceholderText = "Kategori ID att ändra";

            // txtEditCategoryName
            txtEditCategoryName = new TextBox();
            txtEditCategoryName.Location = new Point(300, 280);
            txtEditCategoryName.Size = new Size(260, 23);
            txtEditCategoryName.PlaceholderText = "Nytt kategorinamn";

            // btnEditCategoryName
            btnEditCategoryName = new Button();
            btnEditCategoryName.Location = new Point(300, 310);
            btnEditCategoryName.Size = new Size(260, 40);
            btnEditCategoryName.Text = "Ändra kategorinamn";
            btnEditCategoryName.Click += btnEditCategoryName_Click;

            // lstCategoryPods
            lstCategoryPods = new ListBox();
            lstCategoryPods.Location = new Point(300, 360);
            lstCategoryPods.Size = new Size(260, 200);

            // btnShowPodsByCategory
            btnShowPodsByCategory = new Button();
            btnShowPodsByCategory.Location = new Point(300, 570);
            btnShowPodsByCategory.Size = new Size(260, 40);
            btnShowPodsByCategory.Text = "Visa poddar för kategori";
            btnShowPodsByCategory.Click += btnShowPodsByCategory_Click;

            // Add controls
            Controls.Add(txtEditCategoryId);
            Controls.Add(txtEditCategoryName);
            Controls.Add(btnEditCategoryName);
            Controls.Add(lstCategoryPods);
            Controls.Add(btnShowPodsByCategory);

            // 
            // DiyarForm
            // 
            ClientSize = new Size(650, 650);
            Controls.Add(txtPoddId);
            Controls.Add(txtCategoryId);
            Controls.Add(btnSetCategory);
            Controls.Add(lblName);
            Controls.Add(txtCategoryName);
            Controls.Add(btnCreate);
            Controls.Add(lstCategories);
            Name = "DiyarForm";
            Text = "Kategorier";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
