using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    partial class MainUiForm
    {
        private System.ComponentModel.IContainer components = null;

        private TextBox txtRssUrl;
        private Button btnFetch;
        private TextBox txtCustomName;
        private Button btnSaveFeed;

        private GroupBox grpMyPodcasts;
        private Label lblCategoryFilter;
        private ComboBox cmbCategoryFilter;
        private ListBox lstPodcasts;
        private Label lblFeedCategory;
        private ComboBox cmbFeedCategory;
        private Button btnSetCategory;
        private Button btnRemoveCategory;
        private Button btnDelete;
        private Button btnRename;

        private GroupBox grpEpisodes;
        private DataGridView dgvEpisodes;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colPublishDate;
        private Label lblEpisodeTitle;
        private Label lblEpisodeCount;
        private TextBox txtDescription;
        private Button btnOpenExternalLink;

        private GroupBox grpCategories;
        private ListBox lstCategoriesRight;
        private Label lblNewCategory;
        private TextBox txtNewCategoryName;
        private Button btnCreateCategory;
        private Label lblCategoryEdit;
        private ComboBox cmbCategoryEdit;
        private Label lblNewCategoryName;
        private TextBox txtEditCategoryName;
        private Button btnRenameCategory;
        private Button btnDeleteCategory;

        private Label lblRssUrl;
        private Label lblCustomName;
        private PictureBox pictureBox1;
        private TextBox textBox1;
        private Label label2; // ensure field exists for runtime/designer

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtRssUrl = new TextBox();
            btnFetch = new Button();
            txtCustomName = new TextBox();
            btnSaveFeed = new Button();
            grpMyPodcasts = new GroupBox();
            lblSelectedFeed = new Label();
            lblCategoryFilter = new Label();
            cmbCategoryFilter = new ComboBox();
            lstPodcasts = new ListBox();
            lblCustomName = new Label();
            lblFeedCategory = new Label();
            btnRename = new Button();
            btnSetCategory = new Button();
            btnRemoveCategory = new Button();
            btnDelete = new Button();
            cmbFeedCategory = new ComboBox();
            grpEpisodes = new GroupBox();
            dgvEpisodes = new DataGridView();
            colTitle = new DataGridViewTextBoxColumn();
            colPublishDate = new DataGridViewTextBoxColumn();
            lblEpisodeTitle = new Label();
            lblEpisodeCount = new Label();
            txtDescription = new TextBox();
            btnOpenExternalLink = new Button();
            btnDeleteCategory = new Button();
            btnRenameCategory = new Button();
            grpCategories = new GroupBox();
            lstCategoriesRight = new ListBox();
            lblNewCategoryName = new Label();
            btnCreateCategory = new Button();
            txtEditCategoryName = new TextBox();
            lblCategoryEdit = new Label();
            txtNewCategoryName = new TextBox();
            lblNewCategory = new Label();
            cmbCategoryEdit = new ComboBox();
            lblRssUrl = new Label();
            pictureBox1 = new PictureBox();
            label2 = new Label();
            textBox1 = new TextBox();
            label3 = new Label();
            grpMyPodcasts.SuspendLayout();
            grpEpisodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEpisodes).BeginInit();
            grpCategories.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // txtRssUrl
            // 
            txtRssUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRssUrl.Location = new Point(70, 12);
            txtRssUrl.Name = "txtRssUrl";
            txtRssUrl.Size = new Size(262, 23);
            txtRssUrl.TabIndex = 1;
            txtRssUrl.TextChanged += txtRssUrl_TextChanged;
            // 
            // btnFetch
            // 
            btnFetch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFetch.Location = new Point(338, 12);
            btnFetch.Name = "btnFetch";
            btnFetch.Size = new Size(90, 25);
            btnFetch.TabIndex = 2;
            btnFetch.Text = "Fetch feed";
            btnFetch.Click += btnFetch_Click;
            // 
            // txtCustomName
            // 
            txtCustomName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCustomName.Location = new Point(10, 333);
            txtCustomName.Name = "txtCustomName";
            txtCustomName.Size = new Size(235, 23);
            txtCustomName.TabIndex = 4;
            txtCustomName.TextChanged += txtCustomName_TextChanged;
            // 
            // btnSaveFeed
            // 
            btnSaveFeed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveFeed.Location = new Point(434, 12);
            btnSaveFeed.Name = "btnSaveFeed";
            btnSaveFeed.Size = new Size(90, 25);
            btnSaveFeed.TabIndex = 5;
            btnSaveFeed.Text = "Save feed";
            btnSaveFeed.Click += btnSaveFeed_Click;
            // 
            // grpMyPodcasts
            // 
            grpMyPodcasts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpMyPodcasts.Controls.Add(lblSelectedFeed);
            grpMyPodcasts.Controls.Add(lblCategoryFilter);
            grpMyPodcasts.Controls.Add(cmbCategoryFilter);
            grpMyPodcasts.Controls.Add(lstPodcasts);
            grpMyPodcasts.Controls.Add(lblCustomName);
            grpMyPodcasts.Controls.Add(lblFeedCategory);
            grpMyPodcasts.Controls.Add(txtCustomName);
            grpMyPodcasts.Controls.Add(btnRename);
            grpMyPodcasts.Controls.Add(btnSetCategory);
            grpMyPodcasts.Controls.Add(btnRemoveCategory);
            grpMyPodcasts.Controls.Add(btnDelete);
            grpMyPodcasts.Controls.Add(cmbFeedCategory);
            grpMyPodcasts.Location = new Point(12, 80);
            grpMyPodcasts.Name = "grpMyPodcasts";
            grpMyPodcasts.Size = new Size(260, 598);
            grpMyPodcasts.TabIndex = 6;
            grpMyPodcasts.TabStop = false;
            grpMyPodcasts.Text = "My podcasts";
            // 
            // lblSelectedFeed
            // 
            lblSelectedFeed.AutoSize = true;
            lblSelectedFeed.Location = new Point(10, 289);
            lblSelectedFeed.Name = "lblSelectedFeed";
            lblSelectedFeed.Size = new Size(38, 15);
            lblSelectedFeed.TabIndex = 9;
            lblSelectedFeed.Text = "label1";
            lblSelectedFeed.Click += lblSelectedFeed_Click;
            // 
            // lblCategoryFilter
            // 
            lblCategoryFilter.AutoSize = true;
            lblCategoryFilter.Location = new Point(10, 22);
            lblCategoryFilter.Name = "lblCategoryFilter";
            lblCategoryFilter.Size = new Size(93, 15);
            lblCategoryFilter.TabIndex = 0;
            lblCategoryFilter.Text = "Sort by category";
            lblCategoryFilter.Click += lblCategoryFilter_Click;
            // 
            // cmbCategoryFilter
            // 
            cmbCategoryFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryFilter.Location = new Point(10, 40);
            cmbCategoryFilter.Name = "cmbCategoryFilter";
            cmbCategoryFilter.Size = new Size(235, 23);
            cmbCategoryFilter.TabIndex = 1;
            cmbCategoryFilter.SelectedIndexChanged += cmbCategoryFilter_SelectedIndexChanged;
            // 
            // lstPodcasts
            // 
            lstPodcasts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstPodcasts.Location = new Point(10, 70);
            lstPodcasts.Name = "lstPodcasts";
            lstPodcasts.Size = new Size(235, 214);
            lstPodcasts.TabIndex = 2;
            lstPodcasts.SelectedIndexChanged += lstPodcasts_SelectedIndexChanged;
            // 
            // lblCustomName
            // 
            lblCustomName.AutoSize = true;
            lblCustomName.Location = new Point(10, 315);
            lblCustomName.Name = "lblCustomName";
            lblCustomName.Size = new Size(107, 15);
            lblCustomName.TabIndex = 3;
            lblCustomName.Text = "Change feed name";
            lblCustomName.Click += lblCustomName_Click;
            // 
            // lblFeedCategory
            // 
            lblFeedCategory.AutoSize = true;
            lblFeedCategory.Location = new Point(10, 410);
            lblFeedCategory.Name = "lblFeedCategory";
            lblFeedCategory.Size = new Size(96, 15);
            lblFeedCategory.TabIndex = 3;
            lblFeedCategory.Text = "Current category";
            lblFeedCategory.Click += lblFeedCategory_Click;
            // 
            // btnRename
            // 
            btnRename.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRename.Location = new Point(10, 371);
            btnRename.Name = "btnRename";
            btnRename.Size = new Size(110, 25);
            btnRename.TabIndex = 8;
            btnRename.Text = "Rename feed";
            btnRename.Click += btnRename_Click;
            // 
            // btnSetCategory
            // 
            btnSetCategory.Location = new Point(10, 469);
            btnSetCategory.Name = "btnSetCategory";
            btnSetCategory.Size = new Size(110, 25);
            btnSetCategory.TabIndex = 5;
            btnSetCategory.Text = "Set category";
            btnSetCategory.Click += btnSetCategory_Click;
            // 
            // btnRemoveCategory
            // 
            btnRemoveCategory.Location = new Point(135, 469);
            btnRemoveCategory.Name = "btnRemoveCategory";
            btnRemoveCategory.Size = new Size(110, 25);
            btnRemoveCategory.TabIndex = 6;
            btnRemoveCategory.Text = "Remove category";
            btnRemoveCategory.Click += btnRemoveCategory_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(10, 514);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(110, 25);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Remove feed";
            btnDelete.Click += btnDelete_Click;
            // 
            // cmbFeedCategory
            // 
            cmbFeedCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFeedCategory.Location = new Point(10, 428);
            cmbFeedCategory.Name = "cmbFeedCategory";
            cmbFeedCategory.Size = new Size(235, 23);
            cmbFeedCategory.TabIndex = 4;
            // 
            // grpEpisodes
            // 
            grpEpisodes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpEpisodes.Controls.Add(dgvEpisodes);
            grpEpisodes.Controls.Add(lblEpisodeTitle);
            grpEpisodes.Controls.Add(lblEpisodeCount);
            grpEpisodes.Controls.Add(txtDescription);
            grpEpisodes.Controls.Add(btnOpenExternalLink);
            grpEpisodes.Location = new Point(280, 80);
            grpEpisodes.Name = "grpEpisodes";
            grpEpisodes.Size = new Size(582, 598);
            grpEpisodes.TabIndex = 7;
            grpEpisodes.TabStop = false;
            grpEpisodes.Text = "Episode list";
            // 
            // dgvEpisodes
            // 
            dgvEpisodes.AllowUserToAddRows = false;
            dgvEpisodes.AllowUserToDeleteRows = false;
            dgvEpisodes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvEpisodes.Columns.AddRange(new DataGridViewColumn[] { colTitle, colPublishDate });
            dgvEpisodes.Location = new Point(10, 22);
            dgvEpisodes.MultiSelect = false;
            dgvEpisodes.Name = "dgvEpisodes";
            dgvEpisodes.ReadOnly = true;
            dgvEpisodes.RowHeadersVisible = false;
            dgvEpisodes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEpisodes.Size = new Size(562, 220);
            dgvEpisodes.TabIndex = 0;
            dgvEpisodes.SelectionChanged += dgvEpisodes_SelectionChanged;
            // 
            // colTitle
            // 
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTitle.HeaderText = "Title";
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            // 
            // colPublishDate
            // 
            colPublishDate.HeaderText = "Publish date";
            colPublishDate.Name = "colPublishDate";
            colPublishDate.ReadOnly = true;
            colPublishDate.Width = 110;
            // 
            // lblEpisodeTitle
            // 
            lblEpisodeTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblEpisodeTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEpisodeTitle.Location = new Point(10, 250);
            lblEpisodeTitle.Name = "lblEpisodeTitle";
            lblEpisodeTitle.Size = new Size(442, 20);
            lblEpisodeTitle.TabIndex = 1;
            lblEpisodeTitle.Text = "No episodes.";
            lblEpisodeTitle.Click += lblEpisodeTitle_Click;
            // 
            // lblEpisodeCount
            // 
            lblEpisodeCount.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblEpisodeCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEpisodeCount.Location = new Point(452, 250);
            lblEpisodeCount.Name = "lblEpisodeCount";
            lblEpisodeCount.Size = new Size(120, 20);
            lblEpisodeCount.TabIndex = 4;
            lblEpisodeCount.Text = "Episodes: 0";
            lblEpisodeCount.TextAlign = ContentAlignment.MiddleRight;
            lblEpisodeCount.Click += lblEpisodeCount_Click;
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new Point(10, 273);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(562, 221);
            txtDescription.TabIndex = 2;
            // 
            // btnOpenExternalLink
            // 
            btnOpenExternalLink.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOpenExternalLink.Location = new Point(432, 514);
            btnOpenExternalLink.Name = "btnOpenExternalLink";
            btnOpenExternalLink.Size = new Size(140, 25);
            btnOpenExternalLink.TabIndex = 3;
            btnOpenExternalLink.Text = "Open link";
            btnOpenExternalLink.Click += btnOpenExternalLink_Click;
            // 
            // btnDeleteCategory
            // 
            btnDeleteCategory.Location = new Point(10, 400);
            btnDeleteCategory.Name = "btnDeleteCategory";
            btnDeleteCategory.Size = new Size(120, 25);
            btnDeleteCategory.TabIndex = 9;
            btnDeleteCategory.Text = "Delete category";
            btnDeleteCategory.Click += btnDeleteCategory_Click;
            // 
            // btnRenameCategory
            // 
            btnRenameCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRenameCategory.Location = new Point(270, 354);
            btnRenameCategory.Name = "btnRenameCategory";
            btnRenameCategory.Size = new Size(80, 25);
            btnRenameCategory.TabIndex = 8;
            btnRenameCategory.Text = "Rename";
            btnRenameCategory.Click += btnRenameCategory_Click;
            // 
            // grpCategories
            // 
            grpCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpCategories.Controls.Add(lstCategoriesRight);
            grpCategories.Controls.Add(btnDeleteCategory);
            grpCategories.Controls.Add(lblNewCategoryName);
            grpCategories.Controls.Add(btnRenameCategory);
            grpCategories.Controls.Add(btnCreateCategory);
            grpCategories.Controls.Add(txtEditCategoryName);
            grpCategories.Controls.Add(lblCategoryEdit);
            grpCategories.Controls.Add(txtNewCategoryName);
            grpCategories.Controls.Add(lblNewCategory);
            grpCategories.Controls.Add(cmbCategoryEdit);
            grpCategories.Location = new Point(868, 80);
            grpCategories.Name = "grpCategories";
            grpCategories.Size = new Size(360, 598);
            grpCategories.TabIndex = 8;
            grpCategories.TabStop = false;
            grpCategories.Text = "Category manager";
            // 
            // lstCategoriesRight
            // 
            lstCategoriesRight.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstCategoriesRight.Location = new Point(10, 22);
            lstCategoriesRight.Name = "lstCategoriesRight";
            lstCategoriesRight.Size = new Size(340, 154);
            lstCategoriesRight.TabIndex = 0;
            // 
            // lblNewCategoryName
            // 
            lblNewCategoryName.AutoSize = true;
            lblNewCategoryName.Location = new Point(10, 336);
            lblNewCategoryName.Name = "lblNewCategoryName";
            lblNewCategoryName.Size = new Size(67, 15);
            lblNewCategoryName.TabIndex = 6;
            lblNewCategoryName.Text = "New name:";
            // 
            // btnCreateCategory
            // 
            btnCreateCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreateCategory.Location = new Point(270, 219);
            btnCreateCategory.Name = "btnCreateCategory";
            btnCreateCategory.Size = new Size(80, 25);
            btnCreateCategory.TabIndex = 3;
            btnCreateCategory.Text = "Create";
            btnCreateCategory.Click += btnCreateCategory_Click;
            // 
            // txtEditCategoryName
            // 
            txtEditCategoryName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtEditCategoryName.Location = new Point(10, 354);
            txtEditCategoryName.Name = "txtEditCategoryName";
            txtEditCategoryName.Size = new Size(245, 23);
            txtEditCategoryName.TabIndex = 7;
            // 
            // lblCategoryEdit
            // 
            lblCategoryEdit.AutoSize = true;
            lblCategoryEdit.Location = new Point(10, 271);
            lblCategoryEdit.Name = "lblCategoryEdit";
            lblCategoryEdit.Size = new Size(95, 15);
            lblCategoryEdit.TabIndex = 4;
            lblCategoryEdit.Text = "Category to edit:";
            // 
            // txtNewCategoryName
            // 
            txtNewCategoryName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNewCategoryName.Location = new Point(10, 219);
            txtNewCategoryName.Name = "txtNewCategoryName";
            txtNewCategoryName.Size = new Size(245, 23);
            txtNewCategoryName.TabIndex = 2;
            // 
            // lblNewCategory
            // 
            lblNewCategory.AutoSize = true;
            lblNewCategory.Location = new Point(10, 201);
            lblNewCategory.Name = "lblNewCategory";
            lblNewCategory.Size = new Size(126, 15);
            lblNewCategory.TabIndex = 1;
            lblNewCategory.Text = "New category (create):";
            // 
            // cmbCategoryEdit
            // 
            cmbCategoryEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbCategoryEdit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryEdit.Location = new Point(10, 289);
            cmbCategoryEdit.Name = "cmbCategoryEdit";
            cmbCategoryEdit.Size = new Size(340, 23);
            cmbCategoryEdit.TabIndex = 5;
            // 
            // lblRssUrl
            // 
            lblRssUrl.AutoSize = true;
            lblRssUrl.Location = new Point(12, 15);
            lblRssUrl.Name = "lblRssUrl";
            lblRssUrl.Size = new Size(50, 15);
            lblRssUrl.TabIndex = 0;
            lblRssUrl.Text = "RSS URL";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources.loggo4;
            pictureBox1.Location = new Point(563, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(103, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 44);
            label2.Name = "label2";
            label2.Size = new Size(78, 15);
            label2.TabIndex = 10;
            label2.Text = "Fetched feed:";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.Location = new Point(289, 41);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(235, 23);
            textBox1.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(234, 44);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 12;
            label3.Text = "label3";
            // 
            // MainUiForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1250, 700);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(pictureBox1);
            Controls.Add(lblRssUrl);
            Controls.Add(txtRssUrl);
            Controls.Add(btnFetch);
            Controls.Add(btnSaveFeed);
            Controls.Add(grpMyPodcasts);
            Controls.Add(grpEpisodes);
            Controls.Add(grpCategories);
            Name = "MainUiForm";
            Text = "Podcast Manager";
            Load += MainUiForm_Load;
            grpMyPodcasts.ResumeLayout(false);
            grpMyPodcasts.PerformLayout();
            grpEpisodes.ResumeLayout(false);
            grpEpisodes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEpisodes).EndInit();
            grpCategories.ResumeLayout(false);
            grpCategories.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Label lblSelectedFeed;
        private Label label3;
    }
}
