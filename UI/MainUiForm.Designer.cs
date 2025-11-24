namespace UI
{
    partial class MainUiForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Top controls
        private System.Windows.Forms.Label lblRssUrl;
        private System.Windows.Forms.TextBox txtRssUrl;
        private System.Windows.Forms.Button btnFetch;
        private System.Windows.Forms.Label lblCustomName;
        private System.Windows.Forms.TextBox txtCustomName;
        private System.Windows.Forms.Button btnSaveFeed;

        // Left: podcasts + categories
        private System.Windows.Forms.GroupBox grpPodcasts;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategoryFilter;
        private System.Windows.Forms.ListBox lstPodcasts;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRename;

        // Category management
        private System.Windows.Forms.Label lblNewCategory;
        private System.Windows.Forms.TextBox txtNewCategoryName;
        private System.Windows.Forms.Button btnCreateCategory;
        private System.Windows.Forms.Label lblEditCategory;
        private System.Windows.Forms.TextBox txtEditCategoryName;
        private System.Windows.Forms.Button btnRenameCategory;
        private System.Windows.Forms.Button btnDeleteCategory;

        // Right: episodes
        private System.Windows.Forms.GroupBox grpEpisodes;
        private System.Windows.Forms.DataGridView dgvEpisodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPublishDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDuration;

        // Details
        private System.Windows.Forms.GroupBox grpDetails;
        private System.Windows.Forms.Label lblEpisodeTitle;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Button btnOpenExternalLink;

        // Log
        private System.Windows.Forms.GroupBox grpLog;
        private System.Windows.Forms.TextBox txtLog;

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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblRssUrl = new System.Windows.Forms.Label();
            this.txtRssUrl = new System.Windows.Forms.TextBox();
            this.btnFetch = new System.Windows.Forms.Button();
            this.lblCustomName = new System.Windows.Forms.Label();
            this.txtCustomName = new System.Windows.Forms.TextBox();
            this.btnSaveFeed = new System.Windows.Forms.Button();
            this.grpPodcasts = new System.Windows.Forms.GroupBox();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.btnRenameCategory = new System.Windows.Forms.Button();
            this.txtEditCategoryName = new System.Windows.Forms.TextBox();
            this.lblEditCategory = new System.Windows.Forms.Label();
            this.btnCreateCategory = new System.Windows.Forms.Button();
            this.txtNewCategoryName = new System.Windows.Forms.TextBox();
            this.lblNewCategory = new System.Windows.Forms.Label();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lstPodcasts = new System.Windows.Forms.ListBox();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.grpEpisodes = new System.Windows.Forms.GroupBox();
            this.dgvEpisodes = new System.Windows.Forms.DataGridView();
            this.colTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPublishDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpDetails = new System.Windows.Forms.GroupBox();
            this.btnOpenExternalLink = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblEpisodeTitle = new System.Windows.Forms.Label();
            this.grpLog = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.grpPodcasts.SuspendLayout();
            this.grpEpisodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainUiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 640);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainUiForm";
            this.Text = "Podcast Manager";
            this.Load += new System.EventHandler(this.MainUiForm_Load);
            // 
            // lblRssUrl
            // 
            this.lblRssUrl.AutoSize = true;
            this.lblRssUrl.Location = new System.Drawing.Point(12, 15);
            this.lblRssUrl.Name = "lblRssUrl";
            this.lblRssUrl.Size = new System.Drawing.Size(52, 15);
            this.lblRssUrl.TabIndex = 0;
            this.lblRssUrl.Text = "RSS URL";
            // 
            // txtRssUrl
            // 
            this.txtRssUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRssUrl.Location = new System.Drawing.Point(90, 12);
            this.txtRssUrl.Name = "txtRssUrl";
            this.txtRssUrl.Size = new System.Drawing.Size(820, 23);
            this.txtRssUrl.TabIndex = 1;
            // 
            // btnFetch
            // 
            this.btnFetch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFetch.Location = new System.Drawing.Point(930, 11);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(100, 25);
            this.btnFetch.TabIndex = 2;
            this.btnFetch.Text = "Fetch feed";
            this.btnFetch.UseVisualStyleBackColor = true;
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
            // 
            // lblCustomName
            // 
            this.lblCustomName.AutoSize = true;
            this.lblCustomName.Location = new System.Drawing.Point(12, 46);
            this.lblCustomName.Name = "lblCustomName";
            this.lblCustomName.Size = new System.Drawing.Size(83, 15);
            this.lblCustomName.TabIndex = 3;
            this.lblCustomName.Text = "Custom name";
            // 
            // txtCustomName
            // 
            this.txtCustomName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomName.Location = new System.Drawing.Point(101, 43);
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Size = new System.Drawing.Size(809, 23);
            this.txtCustomName.TabIndex = 4;
            // 
            // btnSaveFeed
            // 
            this.btnSaveFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveFeed.Enabled = false;
            this.btnSaveFeed.Location = new System.Drawing.Point(930, 42);
            this.btnSaveFeed.Name = "btnSaveFeed";
            this.btnSaveFeed.Size = new System.Drawing.Size(100, 25);
            this.btnSaveFeed.TabIndex = 5;
            this.btnSaveFeed.Text = "Save feed";
            this.btnSaveFeed.UseVisualStyleBackColor = true;
            this.btnSaveFeed.Click += new System.EventHandler(this.btnSaveFeed_Click);
            // 
            // grpPodcasts
            // 
            this.grpPodcasts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpPodcasts.Controls.Add(this.btnDeleteCategory);
            this.grpPodcasts.Controls.Add(this.btnRenameCategory);
            this.grpPodcasts.Controls.Add(this.txtEditCategoryName);
            this.grpPodcasts.Controls.Add(this.lblEditCategory);
            this.grpPodcasts.Controls.Add(this.btnCreateCategory);
            this.grpPodcasts.Controls.Add(this.txtNewCategoryName);
            this.grpPodcasts.Controls.Add(this.lblNewCategory);
            this.grpPodcasts.Controls.Add(this.btnRename);
            this.grpPodcasts.Controls.Add(this.btnDelete);
            this.grpPodcasts.Controls.Add(this.lstPodcasts);
            this.grpPodcasts.Controls.Add(this.cmbCategoryFilter);
            this.grpPodcasts.Controls.Add(this.lblCategory);
            this.grpPodcasts.Location = new System.Drawing.Point(12, 80);
            this.grpPodcasts.Name = "grpPodcasts";
            this.grpPodcasts.Size = new System.Drawing.Size(280, 470);
            this.grpPodcasts.TabIndex = 6;
            this.grpPodcasts.TabStop = false;
            this.grpPodcasts.Text = "My podcasts";
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDeleteCategory.Location = new System.Drawing.Point(10, 370);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(120, 25);
            this.btnDeleteCategory.TabIndex = 11;
            this.btnDeleteCategory.Text = "Delete category";
            this.btnDeleteCategory.UseVisualStyleBackColor = true;
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // btnRenameCategory
            // 
            this.btnRenameCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRenameCategory.Location = new System.Drawing.Point(210, 334);
            this.btnRenameCategory.Name = "btnRenameCategory";
            this.btnRenameCategory.Size = new System.Drawing.Size(60, 25);
            this.btnRenameCategory.TabIndex = 10;
            this.btnRenameCategory.Text = "Rename";
            this.btnRenameCategory.UseVisualStyleBackColor = true;
            this.btnRenameCategory.Click += new System.EventHandler(this.btnRenameCategory_Click);
            // 
            // txtEditCategoryName
            // 
            this.txtEditCategoryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEditCategoryName.Location = new System.Drawing.Point(10, 335);
            this.txtEditCategoryName.Name = "txtEditCategoryName";
            this.txtEditCategoryName.Size = new System.Drawing.Size(190, 23);
            this.txtEditCategoryName.TabIndex = 9;
            // 
            // lblEditCategory
            // 
            this.lblEditCategory.AutoSize = true;
            this.lblEditCategory.Location = new System.Drawing.Point(10, 315);
            this.lblEditCategory.Name = "lblEditCategory";
            this.lblEditCategory.Size = new System.Drawing.Size(149, 15);
            this.lblEditCategory.TabIndex = 8;
            this.lblEditCategory.Text = "Edit selected category name:";
            // 
            // btnCreateCategory
            // 
            this.btnCreateCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCreateCategory.Location = new System.Drawing.Point(210, 279);
            this.btnCreateCategory.Name = "btnCreateCategory";
            this.btnCreateCategory.Size = new System.Drawing.Size(60, 25);
            this.btnCreateCategory.TabIndex = 7;
            this.btnCreateCategory.Text = "Create";
            this.btnCreateCategory.UseVisualStyleBackColor = true;
            this.btnCreateCategory.Click += new System.EventHandler(this.btnCreateCategory_Click);
            // 
            // txtNewCategoryName
            // 
            this.txtNewCategoryName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewCategoryName.Location = new System.Drawing.Point(10, 280);
            this.txtNewCategoryName.Name = "txtNewCategoryName";
            this.txtNewCategoryName.Size = new System.Drawing.Size(190, 23);
            this.txtNewCategoryName.TabIndex = 6;
            // 
            // lblNewCategory
            // 
            this.lblNewCategory.AutoSize = true;
            this.lblNewCategory.Location = new System.Drawing.Point(10, 260);
            this.lblNewCategory.Name = "lblNewCategory";
            this.lblNewCategory.Size = new System.Drawing.Size(132, 15);
            this.lblNewCategory.TabIndex = 5;
            this.lblNewCategory.Text = "New category (create):";
            // 
            // btnRename
            // 
            this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRename.Location = new System.Drawing.Point(110, 220);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(90, 25);
            this.btnRename.TabIndex = 4;
            this.btnRename.Text = "Rename feed";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(10, 220);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(90, 25);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Remove feed";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lstPodcasts
            // 
            this.lstPodcasts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPodcasts.FormattingEnabled = true;
            this.lstPodcasts.ItemHeight = 15;
            this.lstPodcasts.Location = new System.Drawing.Point(10, 60);
            this.lstPodcasts.Name = "lstPodcasts";
            this.lstPodcasts.Size = new System.Drawing.Size(257, 154);
            this.lstPodcasts.TabIndex = 2;
            this.lstPodcasts.SelectedIndexChanged += new System.EventHandler(this.lstPodcasts_SelectedIndexChanged);
            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryFilter.FormattingEnabled = true;
            this.cmbCategoryFilter.Location = new System.Drawing.Point(80, 22);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(187, 23);
            this.cmbCategoryFilter.TabIndex = 1;
            this.cmbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryFilter_SelectedIndexChanged);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(10, 25);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(55, 15);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Category";
            // 
            // grpEpisodes
            // 
            this.grpEpisodes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpEpisodes.Controls.Add(this.dgvEpisodes);
            this.grpEpisodes.Location = new System.Drawing.Point(305, 80);
            this.grpEpisodes.Name = "grpEpisodes";
            this.grpEpisodes.Size = new System.Drawing.Size(863, 230);
            this.grpEpisodes.TabIndex = 7;
            this.grpEpisodes.TabStop = false;
            this.grpEpisodes.Text = "Episode list";
            // 
            // dgvEpisodes
            // 
            this.dgvEpisodes.AllowUserToAddRows = false;
            this.dgvEpisodes.AllowUserToDeleteRows = false;
            this.dgvEpisodes.AllowUserToResizeRows = false;
            this.dgvEpisodes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEpisodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEpisodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTitle,
            this.colPublishDate,
            this.colDuration});
            this.dgvEpisodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEpisodes.Location = new System.Drawing.Point(3, 19);
            this.dgvEpisodes.MultiSelect = false;
            this.dgvEpisodes.Name = "dgvEpisodes";
            this.dgvEpisodes.ReadOnly = true;
            this.dgvEpisodes.RowHeadersVisible = false;
            this.dgvEpisodes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvEpisodes.Size = new System.Drawing.Size(857, 208);
            this.dgvEpisodes.TabIndex = 0;
            this.dgvEpisodes.SelectionChanged += new System.EventHandler(this.dgvEpisodes_SelectionChanged);
            // 
            // colTitle
            // 
            this.colTitle.HeaderText = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            // 
            // colPublishDate
            // 
            this.colPublishDate.HeaderText = "Publish date";
            this.colPublishDate.Name = "colPublishDate";
            this.colPublishDate.ReadOnly = true;
            this.colPublishDate.Width = 140;
            // 
            // colDuration
            // 
            this.colDuration.HeaderText = "Duration";
            this.colDuration.Name = "colDuration";
            this.colDuration.ReadOnly = true;
            this.colDuration.Width = 100;
            // 
            // grpDetails
            // 
            this.grpDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDetails.Controls.Add(this.btnOpenExternalLink);
            this.grpDetails.Controls.Add(this.txtDescription);
            this.grpDetails.Controls.Add(this.lblEpisodeTitle);
            this.grpDetails.Location = new System.Drawing.Point(305, 316);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(863, 180);
            this.grpDetails.TabIndex = 8;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Details";
            // 
            // btnOpenExternalLink
            // 
            this.btnOpenExternalLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenExternalLink.Location = new System.Drawing.Point(773, 147);
            this.btnOpenExternalLink.Name = "btnOpenExternalLink";
            this.btnOpenExternalLink.Size = new System.Drawing.Size(84, 25);
            this.btnOpenExternalLink.TabIndex = 2;
            this.btnOpenExternalLink.Text = "Open link";
            this.btnOpenExternalLink.UseVisualStyleBackColor = true;
            this.btnOpenExternalLink.Click += new System.EventHandler(this.btnOpenExternalLink_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)
                        | System.Windows.Forms.AnchorStyles.Bottom)));
            this.txtDescription.Location = new System.Drawing.Point(10, 43);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(757, 129);
            this.txtDescription.TabIndex = 1;
            // 
            // lblEpisodeTitle
            // 
            this.lblEpisodeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEpisodeTitle.AutoSize = true;
            this.lblEpisodeTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEpisodeTitle.Location = new System.Drawing.Point(10, 22);
            this.lblEpisodeTitle.Name = "lblEpisodeTitle";
            this.lblEpisodeTitle.Size = new System.Drawing.Size(77, 15);
            this.lblEpisodeTitle.TabIndex = 0;
            this.lblEpisodeTitle.Text = "No episodes";
            // 
            // grpLog
            // 
            this.grpLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLog.Controls.Add(this.txtLog);
            this.grpLog.Location = new System.Drawing.Point(12, 556);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(1156, 80);
            this.grpLog.TabIndex = 9;
            this.grpLog.TabStop = false;
            this.grpLog.Text = "Log";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 19);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(1150, 58);
            this.txtLog.TabIndex = 0;
            // 
            // Add controls to form
            // 
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.grpDetails);
            this.Controls.Add(this.grpEpisodes);
            this.Controls.Add(this.grpPodcasts);
            this.Controls.Add(this.btnSaveFeed);
            this.Controls.Add(this.txtCustomName);
            this.Controls.Add(this.lblCustomName);
            this.Controls.Add(this.btnFetch);
            this.Controls.Add(this.txtRssUrl);
            this.Controls.Add(this.lblRssUrl);
            this.grpPodcasts.ResumeLayout(false);
            this.grpPodcasts.PerformLayout();
            this.grpEpisodes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.grpLog.ResumeLayout(false);
            this.grpLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
