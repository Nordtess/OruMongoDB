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
        private DataGridViewTextBoxColumn colDuration;
        private Label lblEpisodeTitle;
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

        private TextBox txtLog;
        private Label lblRssUrl;
        private Label lblCustomName;

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
            lblCategoryFilter = new Label();
            cmbCategoryFilter = new ComboBox();
            lstPodcasts = new ListBox();
            lblCustomName = new Label();
            lblFeedCategory = new Label();
            cmbFeedCategory = new ComboBox();
            btnSetCategory = new Button();
            btnRemoveCategory = new Button();
            btnDelete = new Button();
            btnRename = new Button();
            grpEpisodes = new GroupBox();
            dgvEpisodes = new DataGridView();
            colTitle = new DataGridViewTextBoxColumn();
            colPublishDate = new DataGridViewTextBoxColumn();
            colDuration = new DataGridViewTextBoxColumn();
            lblEpisodeTitle = new Label();
            txtDescription = new TextBox();
            btnOpenExternalLink = new Button();
            grpCategories = new GroupBox();
            lstCategoriesRight = new ListBox();
            lblNewCategory = new Label();
            txtNewCategoryName = new TextBox();
            btnCreateCategory = new Button();
            lblCategoryEdit = new Label();
            cmbCategoryEdit = new ComboBox();
            lblNewCategoryName = new Label();
            txtEditCategoryName = new TextBox();
            btnRenameCategory = new Button();
            btnDeleteCategory = new Button();
            txtLog = new TextBox();
            lblRssUrl = new Label();
            pictureBox1 = new PictureBox();
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
            txtRssUrl.Size = new Size(316, 23);
            txtRssUrl.TabIndex = 1;
            // 
            // btnFetch
            // 
            btnFetch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFetch.Location = new Point(392, 12);
            btnFetch.Name = "btnFetch";
            btnFetch.Size = new Size(90, 25);
            btnFetch.TabIndex = 2;
            btnFetch.Text = "Fetch feed";
            btnFetch.Click += btnFetch_Click;
            // 
            // txtCustomName
            // 
            txtCustomName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCustomName.Location = new Point(6, 300);
            txtCustomName.Name = "txtCustomName";
            txtCustomName.Size = new Size(235, 23);
            txtCustomName.TabIndex = 4;
            // 
            // btnSaveFeed
            // 
            btnSaveFeed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveFeed.Location = new Point(488, 12);
            btnSaveFeed.Name = "btnSaveFeed";
            btnSaveFeed.Size = new Size(90, 25);
            btnSaveFeed.TabIndex = 5;
            btnSaveFeed.Text = "Save feed";
            btnSaveFeed.Click += btnSaveFeed_Click;
            // 
            // grpMyPodcasts
            // 
            grpMyPodcasts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpMyPodcasts.Controls.Add(lblCategoryFilter);
            grpMyPodcasts.Controls.Add(cmbCategoryFilter);
            grpMyPodcasts.Controls.Add(lstPodcasts);
            grpMyPodcasts.Controls.Add(lblCustomName);
            grpMyPodcasts.Controls.Add(lblFeedCategory);
            grpMyPodcasts.Controls.Add(txtCustomName);
            grpMyPodcasts.Controls.Add(cmbFeedCategory);
            grpMyPodcasts.Controls.Add(btnSetCategory);
            grpMyPodcasts.Controls.Add(btnRemoveCategory);
            grpMyPodcasts.Controls.Add(btnDelete);
            grpMyPodcasts.Controls.Add(btnRename);
            grpMyPodcasts.Location = new Point(12, 80);
            grpMyPodcasts.Name = "grpMyPodcasts";
            grpMyPodcasts.Size = new Size(260, 470);
            grpMyPodcasts.TabIndex = 6;
            grpMyPodcasts.TabStop = false;
            grpMyPodcasts.Text = "My podcasts";
            // 
            // lblCategoryFilter
            // 
            lblCategoryFilter.AutoSize = true;
            lblCategoryFilter.Location = new Point(10, 22);
            lblCategoryFilter.Name = "lblCategoryFilter";
            lblCategoryFilter.Size = new Size(55, 15);
            lblCategoryFilter.TabIndex = 0;
            lblCategoryFilter.Text = "Category";
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
            lblCustomName.Location = new Point(6, 286);
            lblCustomName.Name = "lblCustomName";
            lblCustomName.Size = new Size(107, 15);
            lblCustomName.TabIndex = 3;
            lblCustomName.Text = "Change feed name";
            lblCustomName.Click += lblCustomName_Click;
            // 
            // lblFeedCategory
            // 
            lblFeedCategory.AutoSize = true;
            lblFeedCategory.Location = new Point(6, 361);
            lblFeedCategory.Name = "lblFeedCategory";
            lblFeedCategory.Size = new Size(81, 15);
            lblFeedCategory.TabIndex = 3;
            lblFeedCategory.Text = "Feed category";
            lblFeedCategory.Click += lblFeedCategory_Click;
            // 
            // cmbFeedCategory
            // 
            cmbFeedCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFeedCategory.Location = new Point(6, 379);
            cmbFeedCategory.Name = "cmbFeedCategory";
            cmbFeedCategory.Size = new Size(235, 23);
            cmbFeedCategory.TabIndex = 4;
            // 
            // btnSetCategory
            // 
            btnSetCategory.Location = new Point(6, 408);
            btnSetCategory.Name = "btnSetCategory";
            btnSetCategory.Size = new Size(110, 25);
            btnSetCategory.TabIndex = 5;
            btnSetCategory.Text = "Set category";
            btnSetCategory.Click += btnSetCategory_Click;
            // 
            // btnRemoveCategory
            // 
            btnRemoveCategory.Location = new Point(135, 408);
            btnRemoveCategory.Name = "btnRemoveCategory";
            btnRemoveCategory.Size = new Size(110, 25);
            btnRemoveCategory.TabIndex = 6;
            btnRemoveCategory.Text = "Remove category";
            btnRemoveCategory.Click += btnRemoveCategory_Click;
            // 
            // btnDelete
            // 
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new Point(6, 439);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(110, 25);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "Remove feed";
            btnDelete.Click += btnDelete_Click;
            // 
            // btnRename
            // 
            btnRename.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRename.Location = new Point(10, 329);
            btnRename.Name = "btnRename";
            btnRename.Size = new Size(110, 25);
            btnRename.TabIndex = 8;
            btnRename.Text = "Rename feed";
            btnRename.Click += btnRename_Click;
            // 
            // grpEpisodes
            // 
            grpEpisodes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpEpisodes.Controls.Add(dgvEpisodes);
            grpEpisodes.Controls.Add(lblEpisodeTitle);
            grpEpisodes.Controls.Add(txtDescription);
            grpEpisodes.Controls.Add(btnOpenExternalLink);
            grpEpisodes.Location = new Point(280, 80);
            grpEpisodes.Name = "grpEpisodes";
            grpEpisodes.Size = new Size(530, 470);
            grpEpisodes.TabIndex = 7;
            grpEpisodes.TabStop = false;
            grpEpisodes.Text = "Episode list";
            // 
            // dgvEpisodes
            // 
            dgvEpisodes.AllowUserToAddRows = false;
            dgvEpisodes.AllowUserToDeleteRows = false;
            dgvEpisodes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvEpisodes.Columns.AddRange(new DataGridViewColumn[] { colTitle, colPublishDate, colDuration });
            dgvEpisodes.Location = new Point(10, 22);
            dgvEpisodes.MultiSelect = false;
            dgvEpisodes.Name = "dgvEpisodes";
            dgvEpisodes.ReadOnly = true;
            dgvEpisodes.RowHeadersVisible = false;
            dgvEpisodes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEpisodes.Size = new Size(510, 220);
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
            // colDuration
            // 
            colDuration.HeaderText = "Duration";
            colDuration.Name = "colDuration";
            colDuration.ReadOnly = true;
            colDuration.Width = 80;
            // 
            // lblEpisodeTitle
            // 
            lblEpisodeTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblEpisodeTitle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEpisodeTitle.Location = new Point(10, 250);
            lblEpisodeTitle.Name = "lblEpisodeTitle";
            lblEpisodeTitle.Size = new Size(510, 20);
            lblEpisodeTitle.TabIndex = 1;
            lblEpisodeTitle.Text = "No episodes.";
            // 
            // txtDescription
            // 
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new Point(10, 273);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ReadOnly = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(510, 150);
            txtDescription.TabIndex = 2;
            // 
            // btnOpenExternalLink
            // 
            btnOpenExternalLink.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOpenExternalLink.Location = new Point(380, 430);
            btnOpenExternalLink.Name = "btnOpenExternalLink";
            btnOpenExternalLink.Size = new Size(140, 25);
            btnOpenExternalLink.TabIndex = 3;
            btnOpenExternalLink.Text = "Open link";
            btnOpenExternalLink.Click += btnOpenExternalLink_Click;
            // 
            // grpCategories
            // 
            grpCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpCategories.Controls.Add(lstCategoriesRight);
            grpCategories.Controls.Add(lblNewCategory);
            grpCategories.Controls.Add(txtNewCategoryName);
            grpCategories.Controls.Add(btnCreateCategory);
            grpCategories.Controls.Add(lblCategoryEdit);
            grpCategories.Controls.Add(cmbCategoryEdit);
            grpCategories.Controls.Add(lblNewCategoryName);
            grpCategories.Controls.Add(txtEditCategoryName);
            grpCategories.Controls.Add(btnRenameCategory);
            grpCategories.Controls.Add(btnDeleteCategory);
            grpCategories.Location = new Point(820, 80);
            grpCategories.Name = "grpCategories";
            grpCategories.Size = new Size(360, 470);
            grpCategories.TabIndex = 8;
            grpCategories.TabStop = false;
            grpCategories.Text = "Categories";
            // 
            // lstCategoriesRight
            // 
            lstCategoriesRight.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstCategoriesRight.Location = new Point(10, 22);
            lstCategoriesRight.Name = "lstCategoriesRight";
            lstCategoriesRight.Size = new Size(340, 124);
            lstCategoriesRight.TabIndex = 0;
            // 
            // lblNewCategory
            // 
            lblNewCategory.AutoSize = true;
            lblNewCategory.Location = new Point(10, 155);
            lblNewCategory.Name = "lblNewCategory";
            lblNewCategory.Size = new Size(126, 15);
            lblNewCategory.TabIndex = 1;
            lblNewCategory.Text = "New category (create):";
            // 
            // txtNewCategoryName
            // 
            txtNewCategoryName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNewCategoryName.Location = new Point(10, 173);
            txtNewCategoryName.Name = "txtNewCategoryName";
            txtNewCategoryName.Size = new Size(245, 23);
            txtNewCategoryName.TabIndex = 2;
            // 
            // btnCreateCategory
            // 
            btnCreateCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreateCategory.Location = new Point(270, 172);
            btnCreateCategory.Name = "btnCreateCategory";
            btnCreateCategory.Size = new Size(80, 25);
            btnCreateCategory.TabIndex = 3;
            btnCreateCategory.Text = "Create";
            btnCreateCategory.Click += btnCreateCategory_Click;
            // 
            // lblCategoryEdit
            // 
            lblCategoryEdit.AutoSize = true;
            lblCategoryEdit.Location = new Point(10, 210);
            lblCategoryEdit.Name = "lblCategoryEdit";
            lblCategoryEdit.Size = new Size(95, 15);
            lblCategoryEdit.TabIndex = 4;
            lblCategoryEdit.Text = "Category to edit:";
            // 
            // cmbCategoryEdit
            // 
            cmbCategoryEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbCategoryEdit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryEdit.Location = new Point(10, 228);
            cmbCategoryEdit.Name = "cmbCategoryEdit";
            cmbCategoryEdit.Size = new Size(340, 23);
            cmbCategoryEdit.TabIndex = 5;
            // 
            // lblNewCategoryName
            // 
            lblNewCategoryName.AutoSize = true;
            lblNewCategoryName.Location = new Point(10, 260);
            lblNewCategoryName.Name = "lblNewCategoryName";
            lblNewCategoryName.Size = new Size(67, 15);
            lblNewCategoryName.TabIndex = 6;
            lblNewCategoryName.Text = "New name:";
            // 
            // txtEditCategoryName
            // 
            txtEditCategoryName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtEditCategoryName.Location = new Point(10, 278);
            txtEditCategoryName.Name = "txtEditCategoryName";
            txtEditCategoryName.Size = new Size(245, 23);
            txtEditCategoryName.TabIndex = 7;
            // 
            // btnRenameCategory
            // 
            btnRenameCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRenameCategory.Location = new Point(270, 277);
            btnRenameCategory.Name = "btnRenameCategory";
            btnRenameCategory.Size = new Size(80, 25);
            btnRenameCategory.TabIndex = 8;
            btnRenameCategory.Text = "Rename";
            btnRenameCategory.Click += btnRenameCategory_Click;
            // 
            // btnDeleteCategory
            // 
            btnDeleteCategory.Location = new Point(10, 315);
            btnDeleteCategory.Name = "btnDeleteCategory";
            btnDeleteCategory.Size = new Size(120, 25);
            btnDeleteCategory.TabIndex = 9;
            btnDeleteCategory.Text = "Delete category";
            btnDeleteCategory.Click += btnDeleteCategory_Click;
            // 
            // txtLog
            // 
            txtLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.Location = new Point(12, 560);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(1168, 80);
            txtLog.TabIndex = 9;
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
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources.pmlogo;
            pictureBox1.Location = new Point(917, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(263, 84);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // MainUiForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 650);
            Controls.Add(pictureBox1);
            Controls.Add(lblRssUrl);
            Controls.Add(txtRssUrl);
            Controls.Add(btnFetch);
            Controls.Add(btnSaveFeed);
            Controls.Add(grpMyPodcasts);
            Controls.Add(grpEpisodes);
            Controls.Add(grpCategories);
            Controls.Add(txtLog);
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
        private PictureBox pictureBox1;
    }
}
