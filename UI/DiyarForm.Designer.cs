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
            // txtPoddId
            this.txtPoddId = new TextBox();
            this.txtPoddId.Location = new Point(20, 160);
            this.txtPoddId.Size = new Size(250, 30);
            this.txtPoddId.PlaceholderText = "Podd ID";
            this.Controls.Add(this.txtPoddId);

            // txtCategoryId
            this.txtCategoryId = new TextBox();
            this.txtCategoryId.Location = new Point(20, 200);
            this.txtCategoryId.Size = new Size(250, 30);
            this.txtCategoryId.PlaceholderText = "Kategori ID";
            this.Controls.Add(this.txtCategoryId);

            // btnSetCategory
            this.btnSetCategory = new Button();
            this.btnSetCategory.Location = new Point(20, 240);
            this.btnSetCategory.Size = new Size(250, 40);
            this.btnSetCategory.Text = "Sätt kategori på podd";
            this.btnSetCategory.Click += btnSetCategory_Click;
            this.Controls.Add(this.btnSetCategory);

            // 
            // DiyarForm
            // 
            ClientSize = new Size(600, 300);
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
