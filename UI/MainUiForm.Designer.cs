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
            components = new System.ComponentModel.Container();
            txtRssUrl = new TextBox();
            btnFetch = new Button();
            txtCustomName = new TextBox();
            btnSaveFeed = new Button();
            grpMyPodcasts = new GroupBox();
            btnRemoveCategory = new Button();
            btnSetCategory = new Button();
            btnRename = new Button();
            btnDelete = new Button();
            cmbFeedCategory = new ComboBox();
            lblFeedCategory = new Label();
            lstPodcasts = new ListBox();
            cmbCategoryFilter = new ComboBox();
            lblCategoryFilter = new Label();
            grpEpisodes = new GroupBox();
            btnOpenExternalLink = new Button();
            txtDescription = new TextBox();
            lblEpisodeTitle = new Label();
            dgvEpisodes = new DataGridView();
            colTitle = new DataGridViewTextBoxColumn();
            colPublishDate = new DataGridViewTextBoxColumn();
            colDuration = new DataGridViewTextBoxColumn();
            grpCategories = new GroupBox();
            btnDeleteCategory = new Button();
            btnRenameCategory = new Button();
            txtEditCategoryName = new TextBox();
            lblNewCategoryName = new Label();
            cmbCategoryEdit = new ComboBox();
            lblCategoryEdit = new Label();
            btnCreateCategory = new Button();
            txtNewCategoryName = new TextBox();
            lblNewCategory = new Label();
            lstCategoriesRight = new ListBox();
            txtLog = new TextBox();
            lblRssUrl = new Label();
            lblCustomName = new Label();
            grpMyPodcasts.SuspendLayout();
            grpEpisodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvEpisodes).BeginInit();
            grpCategories.SuspendLayout();
            SuspendLayout();
            // MainUiForm
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1200, 650);
            Name = "MainUiForm";
            Text = "Podcast Manager";
            Load += MainUiForm_Load;
            // lblRssUrl
            lblRssUrl.AutoSize = true;
            lblRssUrl.Location = new System.Drawing.Point(12, 15);
            lblRssUrl.Text = "RSS URL";
            // txtRssUrl
            txtRssUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRssUrl.Location = new System.Drawing.Point(70, 12);
            txtRssUrl.Size = new System.Drawing.Size(740, 23);
            // btnFetch
            btnFetch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFetch.Location = new System.Drawing.Point(820, 11);
            btnFetch.Size = new System.Drawing.Size(90, 25);
            btnFetch.Text = "Fetch feed";
            btnFetch.Click += btnFetch_Click;
            // lblCustomName
            lblCustomName.AutoSize = true;
            lblCustomName.Location = new System.Drawing.Point(12, 45);
            lblCustomName.Text = "Custom name";
            // txtCustomName
            txtCustomName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCustomName.Location = new System.Drawing.Point(102, 42);
            txtCustomName.Size = new System.Drawing.Size(708, 23);
            // btnSaveFeed
            btnSaveFeed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveFeed.Location = new System.Drawing.Point(820, 41);
            btnSaveFeed.Size = new System.Drawing.Size(90, 25);
            btnSaveFeed.Text = "Save feed";
            btnSaveFeed.Click += btnSaveFeed_Click;
            // grpMyPodcasts
            grpMyPodcasts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpMyPodcasts.Location = new System.Drawing.Point(12, 80);
            grpMyPodcasts.Size = new System.Drawing.Size(260, 470);
            grpMyPodcasts.Text = "My podcasts";
            // lblCategoryFilter
            lblCategoryFilter.AutoSize = true;
            lblCategoryFilter.Location = new System.Drawing.Point(10, 22);
            lblCategoryFilter.Text = "Category";
            // cmbCategoryFilter
            cmbCategoryFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryFilter.Location = new System.Drawing.Point(10, 40);
            cmbCategoryFilter.Size = new System.Drawing.Size(235, 23);
            cmbCategoryFilter.SelectedIndexChanged += cmbCategoryFilter_SelectedIndexChanged;
            // lstPodcasts
            lstPodcasts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstPodcasts.Location = new System.Drawing.Point(10, 70);
            lstPodcasts.Size = new System.Drawing.Size(235, 214);
            lstPodcasts.SelectedIndexChanged += lstPodcasts_SelectedIndexChanged;
            // lblFeedCategory
            lblFeedCategory.AutoSize = true;
            lblFeedCategory.Location = new System.Drawing.Point(10, 290);
            lblFeedCategory.Text = "Feed category";
            // cmbFeedCategory
            cmbFeedCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFeedCategory.Location = new System.Drawing.Point(10, 308);
            cmbFeedCategory.Size = new System.Drawing.Size(235, 23);
            // btnSetCategory
            btnSetCategory.Location = new System.Drawing.Point(10, 340);
            btnSetCategory.Size = new System.Drawing.Size(110, 25);
            btnSetCategory.Text = "Set category";
            btnSetCategory.Click += btnSetCategory_Click;
            // btnRemoveCategory
            btnRemoveCategory.Location = new System.Drawing.Point(135, 340);
            btnRemoveCategory.Size = new System.Drawing.Size(110, 25);
            btnRemoveCategory.Text = "Remove category";
            btnRemoveCategory.Click += btnRemoveCategory_Click;
            // btnDelete
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new System.Drawing.Point(10, 380);
            btnDelete.Size = new System.Drawing.Size(110, 25);
            btnDelete.Text = "Remove feed";
            btnDelete.Click += btnDelete_Click;
            // btnRename
            btnRename.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRename.Location = new System.Drawing.Point(135, 380);
            btnRename.Size = new System.Drawing.Size(110, 25);
            btnRename.Text = "Rename feed";
            btnRename.Click += btnRename_Click;
            grpMyPodcasts.Controls.Add(lblCategoryFilter);
            grpMyPodcasts.Controls.Add(cmbCategoryFilter);
            grpMyPodcasts.Controls.Add(lstPodcasts);
            grpMyPodcasts.Controls.Add(lblFeedCategory);
            grpMyPodcasts.Controls.Add(cmbFeedCategory);
            grpMyPodcasts.Controls.Add(btnSetCategory);
            grpMyPodcasts.Controls.Add(btnRemoveCategory);
            grpMyPodcasts.Controls.Add(btnDelete);
            grpMyPodcasts.Controls.Add(btnRename);
            // grpEpisodes
            grpEpisodes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpEpisodes.Location = new System.Drawing.Point(280, 80);
            grpEpisodes.Size = new System.Drawing.Size(530, 470);
            grpEpisodes.Text = "Episode list";
            // dgvEpisodes
            dgvEpisodes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvEpisodes.Location = new System.Drawing.Point(10, 22);
            dgvEpisodes.ReadOnly = true;
            dgvEpisodes.AllowUserToAddRows = false;
            dgvEpisodes.AllowUserToDeleteRows = false;
            dgvEpisodes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEpisodes.MultiSelect = false;
            dgvEpisodes.RowHeadersVisible = false;
            dgvEpisodes.Size = new System.Drawing.Size(510, 220);
            dgvEpisodes.SelectionChanged += dgvEpisodes_SelectionChanged;
            colTitle.HeaderText = "Title";
            colTitle.Name = "colTitle";
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colPublishDate.HeaderText = "Publish date";
            colPublishDate.Name = "colPublishDate";
            colPublishDate.Width = 110;
            colDuration.HeaderText = "Duration";
            colDuration.Name = "colDuration";
            colDuration.Width = 80;
            dgvEpisodes.Columns.AddRange(new DataGridViewColumn[] { colTitle, colPublishDate, colDuration });
            // lblEpisodeTitle
            lblEpisodeTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblEpisodeTitle.Location = new System.Drawing.Point(10, 250);
            lblEpisodeTitle.Size = new System.Drawing.Size(510, 20);
            lblEpisodeTitle.Text = "No episodes.";
            lblEpisodeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            // txtDescription
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new System.Drawing.Point(10, 273);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.ReadOnly = true;
            txtDescription.Size = new System.Drawing.Size(510, 150);
            // btnOpenExternalLink
            btnOpenExternalLink.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOpenExternalLink.Location = new System.Drawing.Point(380, 430);
            btnOpenExternalLink.Size = new System.Drawing.Size(140, 25);
            btnOpenExternalLink.Text = "Open link";
            btnOpenExternalLink.Click += btnOpenExternalLink_Click;
            grpEpisodes.Controls.Add(dgvEpisodes);
            grpEpisodes.Controls.Add(lblEpisodeTitle);
            grpEpisodes.Controls.Add(txtDescription);
            grpEpisodes.Controls.Add(btnOpenExternalLink);
            // grpCategories
            grpCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpCategories.Location = new System.Drawing.Point(820, 80);
            grpCategories.Size = new System.Drawing.Size(360, 470);
            grpCategories.Text = "Categories";
            // lstCategoriesRight
            lstCategoriesRight.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstCategoriesRight.Location = new System.Drawing.Point(10, 22);
            lstCategoriesRight.Size = new System.Drawing.Size(340, 124);
            // lblNewCategory
            lblNewCategory.AutoSize = true;
            lblNewCategory.Location = new System.Drawing.Point(10, 155);
            lblNewCategory.Text = "New category (create):";
            // txtNewCategoryName
            txtNewCategoryName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNewCategoryName.Location = new System.Drawing.Point(10, 173);
            txtNewCategoryName.Size = new System.Drawing.Size(245, 23);
            // btnCreateCategory
            btnCreateCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreateCategory.Location = new System.Drawing.Point(270, 172);
            btnCreateCategory.Size = new System.Drawing.Size(80, 25);
            btnCreateCategory.Text = "Create";
            btnCreateCategory.Click += btnCreateCategory_Click;
            // lblCategoryEdit
            lblCategoryEdit.AutoSize = true;
            lblCategoryEdit.Location = new System.Drawing.Point(10, 210);
            lblCategoryEdit.Text = "Category to edit:";
            // cmbCategoryEdit
            cmbCategoryEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbCategoryEdit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryEdit.Location = new System.Drawing.Point(10, 228);
            cmbCategoryEdit.Size = new System.Drawing.Size(340, 23);
            // lblNewCategoryName
            lblNewCategoryName.AutoSize = true;
            lblNewCategoryName.Location = new System.Drawing.Point(10, 260);
            lblNewCategoryName.Text = "New name:";
            // txtEditCategoryName
            txtEditCategoryName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtEditCategoryName.Location = new System.Drawing.Point(10, 278);
            txtEditCategoryName.Size = new System.Drawing.Size(245, 23);
            // btnRenameCategory
            btnRenameCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRenameCategory.Location = new System.Drawing.Point(270, 277);
            btnRenameCategory.Size = new System.Drawing.Size(80, 25);
            btnRenameCategory.Text = "Rename";
            btnRenameCategory.Click += btnRenameCategory_Click;
            // btnDeleteCategory
            btnDeleteCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnDeleteCategory.Location = new System.Drawing.Point(10, 315);
            btnDeleteCategory.Size = new System.Drawing.Size(120, 25);
            btnDeleteCategory.Text = "Delete category";
            btnDeleteCategory.Click += btnDeleteCategory_Click;
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
            // txtLog
            txtLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.Location = new System.Drawing.Point(12, 560);
            txtLog.Multiline = true;
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new System.Drawing.Size(1168, 80);
            // Add to form
            Controls.Add(lblRssUrl);
            Controls.Add(txtRssUrl);
            Controls.Add(btnFetch);
            Controls.Add(lblCustomName);
            Controls.Add(txtCustomName);
            Controls.Add(btnSaveFeed);
            Controls.Add(grpMyPodcasts);
            Controls.Add(grpEpisodes);
            Controls.Add(grpCategories);
            Controls.Add(txtLog);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
