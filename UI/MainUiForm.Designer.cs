using System.Windows.Forms;

namespace UI
{
    partial class MainUiForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            // 
            // MainUiForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1200, 650);
            Name = "MainUiForm";
            Text = "Podcast Manager";
            Load += MainUiForm_Load;

            // ======= TOP CONTROLS (RSS URL + CUSTOM NAME + BUTTONS) =================

            // lblRssUrl
            lblRssUrl.AutoSize = true;
            lblRssUrl.Location = new System.Drawing.Point(12, 15);
            lblRssUrl.Name = "lblRssUrl";
            lblRssUrl.Size = new System.Drawing.Size(52, 15);
            lblRssUrl.Text = "RSS URL";

            // txtRssUrl
            txtRssUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRssUrl.Location = new System.Drawing.Point(70, 12);
            txtRssUrl.Name = "txtRssUrl";
            txtRssUrl.Size = new System.Drawing.Size(740, 23);

            // btnFetch
            btnFetch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnFetch.Location = new System.Drawing.Point(820, 11);
            btnFetch.Name = "btnFetch";
            btnFetch.Size = new System.Drawing.Size(90, 25);
            btnFetch.Text = "Fetch feed";
            btnFetch.UseVisualStyleBackColor = true;
            btnFetch.Click += btnFetch_Click;

            // lblCustomName
            lblCustomName.AutoSize = true;
            lblCustomName.Location = new System.Drawing.Point(12, 45);
            lblCustomName.Name = "lblCustomName";
            lblCustomName.Size = new System.Drawing.Size(84, 15);
            lblCustomName.Text = "Custom name";

            // txtCustomName
            txtCustomName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtCustomName.Location = new System.Drawing.Point(102, 42);
            txtCustomName.Name = "txtCustomName";
            txtCustomName.Size = new System.Drawing.Size(708, 23);

            // btnSaveFeed
            btnSaveFeed.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSaveFeed.Location = new System.Drawing.Point(820, 41);
            btnSaveFeed.Name = "btnSaveFeed";
            btnSaveFeed.Size = new System.Drawing.Size(90, 25);
            btnSaveFeed.Text = "Save feed";
            btnSaveFeed.UseVisualStyleBackColor = true;
            btnSaveFeed.Click += btnSaveFeed_Click;

            // ======= LEFT: MY PODCASTS ===============================================

            grpMyPodcasts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            grpMyPodcasts.Location = new System.Drawing.Point(12, 80);
            grpMyPodcasts.Name = "grpMyPodcasts";
            grpMyPodcasts.Size = new System.Drawing.Size(260, 470);
            grpMyPodcasts.Text = "My podcasts";

            // lblCategoryFilter
            lblCategoryFilter.AutoSize = true;
            lblCategoryFilter.Location = new System.Drawing.Point(10, 22);
            lblCategoryFilter.Name = "lblCategoryFilter";
            lblCategoryFilter.Size = new System.Drawing.Size(55, 15);
            lblCategoryFilter.Text = "Category";

            // cmbCategoryFilter
            cmbCategoryFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryFilter.Location = new System.Drawing.Point(10, 40);
            cmbCategoryFilter.Name = "cmbCategoryFilter";
            cmbCategoryFilter.Size = new System.Drawing.Size(235, 23);
            cmbCategoryFilter.SelectedIndexChanged += cmbCategoryFilter_SelectedIndexChanged;

            // lstPodcasts
            lstPodcasts.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstPodcasts.Location = new System.Drawing.Point(10, 70);
            lstPodcasts.Name = "lstPodcasts";
            lstPodcasts.Size = new System.Drawing.Size(235, 214);
            lstPodcasts.SelectedIndexChanged += lstPodcasts_SelectedIndexChanged;

            // lblFeedCategory
            lblFeedCategory.AutoSize = true;
            lblFeedCategory.Location = new System.Drawing.Point(10, 290);
            lblFeedCategory.Name = "lblFeedCategory";
            lblFeedCategory.Size = new System.Drawing.Size(80, 15);
            lblFeedCategory.Text = "Feed category";

            // cmbFeedCategory
            cmbFeedCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFeedCategory.Location = new System.Drawing.Point(10, 308);
            cmbFeedCategory.Name = "cmbFeedCategory";
            cmbFeedCategory.Size = new System.Drawing.Size(235, 23);

            // btnSetCategory
            btnSetCategory.Location = new System.Drawing.Point(10, 340);
            btnSetCategory.Name = "btnSetCategory";
            btnSetCategory.Size = new System.Drawing.Size(110, 25);
            btnSetCategory.Text = "Set category";
            btnSetCategory.UseVisualStyleBackColor = true;
            btnSetCategory.Click += btnSetCategory_Click;

            // btnRemoveCategory
            btnRemoveCategory.Location = new System.Drawing.Point(135, 340);
            btnRemoveCategory.Name = "btnRemoveCategory";
            btnRemoveCategory.Size = new System.Drawing.Size(110, 25);
            btnRemoveCategory.Text = "Remove category";
            btnRemoveCategory.UseVisualStyleBackColor = true;
            btnRemoveCategory.Click += btnRemoveCategory_Click;

            // btnDelete (Remove feed)
            btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDelete.Location = new System.Drawing.Point(10, 380);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new System.Drawing.Size(110, 25);
            btnDelete.Text = "Remove feed";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;

            // btnRename (Rename feed)
            btnRename.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRename.Location = new System.Drawing.Point(135, 380);
            btnRename.Name = "btnRename";
            btnRename.Size = new System.Drawing.Size(110, 25);
            btnRename.Text = "Rename feed";
            btnRename.UseVisualStyleBackColor = true;
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

            // ======= MIDDLE: EPISODES ================================================

            grpEpisodes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpEpisodes.Location = new System.Drawing.Point(280, 80);
            grpEpisodes.Name = "grpEpisodes";
            grpEpisodes.Size = new System.Drawing.Size(530, 470);
            grpEpisodes.Text = "Episode list";

            // dgvEpisodes
            dgvEpisodes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvEpisodes.Location = new System.Drawing.Point(10, 22);
            dgvEpisodes.Name = "dgvEpisodes";
            dgvEpisodes.ReadOnly = true;
            dgvEpisodes.AllowUserToAddRows = false;
            dgvEpisodes.AllowUserToDeleteRows = false;
            dgvEpisodes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEpisodes.MultiSelect = false;
            dgvEpisodes.RowHeadersVisible = false;
            dgvEpisodes.Size = new System.Drawing.Size(510, 220);
            dgvEpisodes.SelectionChanged += dgvEpisodes_SelectionChanged;

            // Columns
            colTitle.HeaderText = "Title";
            colTitle.Name = "colTitle";
            colTitle.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            colPublishDate.HeaderText = "Publish date";
            colPublishDate.Name = "colPublishDate";
            colPublishDate.Width = 110;

            colDuration.HeaderText = "Duration";
            colDuration.Name = "colDuration";
            colDuration.Width = 80;

            dgvEpisodes.Columns.AddRange(new DataGridViewColumn[]
            {
                colTitle, colPublishDate, colDuration
            });

            // lblEpisodeTitle
            lblEpisodeTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblEpisodeTitle.Location = new System.Drawing.Point(10, 250);
            lblEpisodeTitle.Name = "lblEpisodeTitle";
            lblEpisodeTitle.Size = new System.Drawing.Size(510, 20);
            lblEpisodeTitle.Text = "No episodes.";
            lblEpisodeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);

            // txtDescription
            txtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtDescription.Location = new System.Drawing.Point(10, 273);
            txtDescription.Multiline = true;
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.ReadOnly = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new System.Drawing.Size(510, 150);

            // btnOpenExternalLink
            btnOpenExternalLink.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOpenExternalLink.Location = new System.Drawing.Point(380, 430);
            btnOpenExternalLink.Name = "btnOpenExternalLink";
            btnOpenExternalLink.Size = new System.Drawing.Size(140, 25);
            btnOpenExternalLink.Text = "Open link";
            btnOpenExternalLink.UseVisualStyleBackColor = true;
            btnOpenExternalLink.Click += btnOpenExternalLink_Click;

            grpEpisodes.Controls.Add(dgvEpisodes);
            grpEpisodes.Controls.Add(lblEpisodeTitle);
            grpEpisodes.Controls.Add(txtDescription);
            grpEpisodes.Controls.Add(btnOpenExternalLink);

            // ======= RIGHT: CATEGORY MANAGEMENT =====================================

            grpCategories.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            grpCategories.Location = new System.Drawing.Point(820, 80);
            grpCategories.Name = "grpCategories";
            grpCategories.Size = new System.Drawing.Size(360, 470);
            grpCategories.Text = "Categories";

            // lstCategoriesRight
            lstCategoriesRight.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lstCategoriesRight.Location = new System.Drawing.Point(10, 22);
            lstCategoriesRight.Name = "lstCategoriesRight";
            lstCategoriesRight.Size = new System.Drawing.Size(340, 124);

            // lblNewCategory
            lblNewCategory.AutoSize = true;
            lblNewCategory.Location = new System.Drawing.Point(10, 155);
            lblNewCategory.Name = "lblNewCategory";
            lblNewCategory.Size = new System.Drawing.Size(121, 15);
            lblNewCategory.Text = "New category (create):";

            // txtNewCategoryName
            txtNewCategoryName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtNewCategoryName.Location = new System.Drawing.Point(10, 173);
            txtNewCategoryName.Name = "txtNewCategoryName";
            txtNewCategoryName.Size = new System.Drawing.Size(245, 23);

            // btnCreateCategory
            btnCreateCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreateCategory.Location = new System.Drawing.Point(270, 172);
            btnCreateCategory.Name = "btnCreateCategory";
            btnCreateCategory.Size = new System.Drawing.Size(80, 25);
            btnCreateCategory.Text = "Create";
            btnCreateCategory.UseVisualStyleBackColor = true;
            btnCreateCategory.Click += btnCreateCategory_Click;

            // lblCategoryEdit
            lblCategoryEdit.AutoSize = true;
            lblCategoryEdit.Location = new System.Drawing.Point(10, 210);
            lblCategoryEdit.Name = "lblCategoryEdit";
            lblCategoryEdit.Size = new System.Drawing.Size(92, 15);
            lblCategoryEdit.Text = "Category to edit:";

            // cmbCategoryEdit
            cmbCategoryEdit.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbCategoryEdit.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoryEdit.Location = new System.Drawing.Point(10, 228);
            cmbCategoryEdit.Name = "cmbCategoryEdit";
            cmbCategoryEdit.Size = new System.Drawing.Size(340, 23);

            // lblNewCategoryName
            lblNewCategoryName.AutoSize = true;
            lblNewCategoryName.Location = new System.Drawing.Point(10, 260);
            lblNewCategoryName.Name = "lblNewCategoryName";
            lblNewCategoryName.Size = new System.Drawing.Size(67, 15);
            lblNewCategoryName.Text = "New name:";

            // txtEditCategoryName
            txtEditCategoryName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtEditCategoryName.Location = new System.Drawing.Point(10, 278);
            txtEditCategoryName.Name = "txtEditCategoryName";
            txtEditCategoryName.Size = new System.Drawing.Size(245, 23);

            // btnRenameCategory
            btnRenameCategory.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRenameCategory.Location = new System.Drawing.Point(270, 277);
            btnRenameCategory.Name = "btnRenameCategory";
            btnRenameCategory.Size = new System.Drawing.Size(80, 25);
            btnRenameCategory.Text = "Rename";
            btnRenameCategory.UseVisualStyleBackColor = true;
            btnRenameCategory.Click += btnRenameCategory_Click;

            // btnDeleteCategory
            btnDeleteCategory.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnDeleteCategory.Location = new System.Drawing.Point(10, 315);
            btnDeleteCategory.Name = "btnDeleteCategory";
            btnDeleteCategory.Size = new System.Drawing.Size(120, 25);
            btnDeleteCategory.Text = "Delete category";
            btnDeleteCategory.UseVisualStyleBackColor = true;
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

            // ======= LOG ============================================================

            txtLog.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtLog.Location = new System.Drawing.Point(12, 560);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new System.Drawing.Size(1168, 80);

            // ======= ADD TO FORM ====================================================

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

        #endregion
    }
}
