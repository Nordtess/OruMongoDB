using System.Windows.Forms;

namespace UI
{
    partial class MainUiForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.grpFetch = new System.Windows.Forms.GroupBox();
            this.btnSaveFeed = new System.Windows.Forms.Button();
            this.txtCustomName = new System.Windows.Forms.TextBox();
            this.lblCustomName = new System.Windows.Forms.Label();
            this.btnFetch = new System.Windows.Forms.Button();
            this.txtRssUrl = new System.Windows.Forms.TextBox();
            this.lblRssUrl = new System.Windows.Forms.Label();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.grpPodcasts = new System.Windows.Forms.GroupBox();
            this.btnRename = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.lstPodcasts = new System.Windows.Forms.ListBox();
            this.cmbCategoryFilter = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.splitRight = new System.Windows.Forms.SplitContainer();
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
            this.grpFetch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.grpPodcasts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).BeginInit();
            this.splitRight.Panel1.SuspendLayout();
            this.splitRight.Panel2.SuspendLayout();
            this.splitRight.SuspendLayout();
            this.grpEpisodes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).BeginInit();
            this.grpDetails.SuspendLayout();
            this.grpLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpFetch
            // 
            this.grpFetch.Controls.Add(this.btnSaveFeed);
            this.grpFetch.Controls.Add(this.txtCustomName);
            this.grpFetch.Controls.Add(this.lblCustomName);
            this.grpFetch.Controls.Add(this.btnFetch);
            this.grpFetch.Controls.Add(this.txtRssUrl);
            this.grpFetch.Controls.Add(this.lblRssUrl);
            this.grpFetch.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFetch.Location = new System.Drawing.Point(0, 0);
            this.grpFetch.Name = "grpFetch";
            this.grpFetch.Size = new System.Drawing.Size(984, 90);
            this.grpFetch.TabIndex = 0;
            this.grpFetch.TabStop = false;
            this.grpFetch.Text = "Fetch podcast via RSS";
            // 
            // btnSaveFeed
            // 
            this.btnSaveFeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveFeed.Enabled = false;
            this.btnSaveFeed.Location = new System.Drawing.Point(881, 51);
            this.btnSaveFeed.Name = "btnSaveFeed";
            this.btnSaveFeed.Size = new System.Drawing.Size(90, 23);
            this.btnSaveFeed.TabIndex = 5;
            this.btnSaveFeed.Text = "Save feed";
            this.btnSaveFeed.UseVisualStyleBackColor = true;
            this.btnSaveFeed.Click += new System.EventHandler(this.btnSaveFeed_Click);
            // 
            // txtCustomName
            // 
            this.txtCustomName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCustomName.Location = new System.Drawing.Point(96, 52);
            this.txtCustomName.Name = "txtCustomName";
            this.txtCustomName.Size = new System.Drawing.Size(779, 23);
            this.txtCustomName.TabIndex = 4;
            // 
            // lblCustomName
            // 
            this.lblCustomName.AutoSize = true;
            this.lblCustomName.Location = new System.Drawing.Point(12, 55);
            this.lblCustomName.Name = "lblCustomName";
            this.lblCustomName.Size = new System.Drawing.Size(78, 15);
            this.lblCustomName.TabIndex = 3;
            this.lblCustomName.Text = "Custom name:";
            // 
            // btnFetch
            // 
            this.btnFetch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFetch.Location = new System.Drawing.Point(881, 22);
            this.btnFetch.Name = "btnFetch";
            this.btnFetch.Size = new System.Drawing.Size(90, 23);
            this.btnFetch.TabIndex = 2;
            this.btnFetch.Text = "Fetch feed";
            this.btnFetch.UseVisualStyleBackColor = true;
            this.btnFetch.Click += new System.EventHandler(this.btnFetch_Click);
            // 
            // txtRssUrl
            // 
            this.txtRssUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRssUrl.Location = new System.Drawing.Point(96, 22);
            this.txtRssUrl.Name = "txtRssUrl";
            this.txtRssUrl.Size = new System.Drawing.Size(779, 23);
            this.txtRssUrl.TabIndex = 1;
            // 
            // lblRssUrl
            // 
            this.lblRssUrl.AutoSize = true;
            this.lblRssUrl.Location = new System.Drawing.Point(12, 25);
            this.lblRssUrl.Name = "lblRssUrl";
            this.lblRssUrl.Size = new System.Drawing.Size(55, 15);
            this.lblRssUrl.TabIndex = 0;
            this.lblRssUrl.Text = "RSS URL:";
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 90);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.grpPodcasts);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitRight);
            this.splitMain.Size = new System.Drawing.Size(984, 471);
            this.splitMain.SplitterDistance = 250;
            this.splitMain.TabIndex = 1;
            // 
            // grpPodcasts
            // 
            this.grpPodcasts.Controls.Add(this.btnRename);
            this.grpPodcasts.Controls.Add(this.btnDelete);
            this.grpPodcasts.Controls.Add(this.lstPodcasts);
            this.grpPodcasts.Controls.Add(this.cmbCategoryFilter);
            this.grpPodcasts.Controls.Add(this.lblCategory);
            this.grpPodcasts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPodcasts.Location = new System.Drawing.Point(0, 0);
            this.grpPodcasts.Name = "grpPodcasts";
            this.grpPodcasts.Size = new System.Drawing.Size(250, 471);
            this.grpPodcasts.TabIndex = 0;
            this.grpPodcasts.TabStop = false;
            this.grpPodcasts.Text = "My podcasts";
            // 
            // btnRename
            // 
            this.btnRename.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRename.Location = new System.Drawing.Point(169, 438);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(75, 23);
            this.btnRename.TabIndex = 4;
            this.btnRename.Text = "Rename";
            this.btnRename.UseVisualStyleBackColor = true;
            this.btnRename.Click += new System.EventHandler(this.btnRename_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDelete.Location = new System.Drawing.Point(6, 438);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Remove";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // lstPodcasts
            // 
            this.lstPodcasts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPodcasts.FormattingEnabled = true;
            this.lstPodcasts.ItemHeight = 15;
            this.lstPodcasts.Location = new System.Drawing.Point(6, 63);
            this.lstPodcasts.Name = "lstPodcasts";
            this.lstPodcasts.Size = new System.Drawing.Size(238, 364);
            this.lstPodcasts.TabIndex = 2;
            this.lstPodcasts.SelectedIndexChanged += new System.EventHandler(this.lstPodcasts_SelectedIndexChanged);
            // 
            // cmbCategoryFilter
            // 
            this.cmbCategoryFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbCategoryFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategoryFilter.FormattingEnabled = true;
            this.cmbCategoryFilter.Location = new System.Drawing.Point(68, 22);
            this.cmbCategoryFilter.Name = "cmbCategoryFilter";
            this.cmbCategoryFilter.Size = new System.Drawing.Size(176, 23);
            this.cmbCategoryFilter.TabIndex = 1;
            this.cmbCategoryFilter.SelectedIndexChanged += new System.EventHandler(this.cmbCategoryFilter_SelectedIndexChanged);
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(6, 25);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(55, 15);
            this.lblCategory.TabIndex = 0;
            this.lblCategory.Text = "Category:";
            // 
            // splitRight
            // 
            this.splitRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitRight.Location = new System.Drawing.Point(0, 0);
            this.splitRight.Name = "splitRight";
            this.splitRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitRight.Panel1
            // 
            this.splitRight.Panel1.Controls.Add(this.grpEpisodes);
            // 
            // splitRight.Panel2
            // 
            this.splitRight.Panel2.Controls.Add(this.grpDetails);
            this.splitRight.Size = new System.Drawing.Size(730, 471);
            this.splitRight.SplitterDistance = 260;
            this.splitRight.TabIndex = 0;
            // 
            // grpEpisodes
            // 
            this.grpEpisodes.Controls.Add(this.dgvEpisodes);
            this.grpEpisodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpEpisodes.Location = new System.Drawing.Point(0, 0);
            this.grpEpisodes.Name = "grpEpisodes";
            this.grpEpisodes.Size = new System.Drawing.Size(730, 260);
            this.grpEpisodes.TabIndex = 0;
            this.grpEpisodes.TabStop = false;
            this.grpEpisodes.Text = "Episode list";
            // 
            // dgvEpisodes
            // 
            this.dgvEpisodes.AllowUserToAddRows = false;
            this.dgvEpisodes.AllowUserToDeleteRows = false;
            this.dgvEpisodes.AllowUserToResizeRows = false;
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
            this.dgvEpisodes.Size = new System.Drawing.Size(724, 238);
            this.dgvEpisodes.TabIndex = 0;
            this.dgvEpisodes.SelectionChanged += new System.EventHandler(this.dgvEpisodes_SelectionChanged);
            // 
            // colTitle
            // 
            this.colTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.colTitle.HeaderText = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.ReadOnly = true;
            // 
            // colPublishDate
            // 
            this.colPublishDate.HeaderText = "Publish date";
            this.colPublishDate.Name = "colPublishDate";
            this.colPublishDate.ReadOnly = true;
            this.colPublishDate.Width = 120;
            // 
            // colDuration
            // 
            this.colDuration.HeaderText = "Duration";
            this.colDuration.Name = "colDuration";
            this.colDuration.ReadOnly = true;
            this.colDuration.Width = 80;
            // 
            // grpDetails
            // 
            this.grpDetails.Controls.Add(this.btnOpenExternalLink);
            this.grpDetails.Controls.Add(this.txtDescription);
            this.grpDetails.Controls.Add(this.lblEpisodeTitle);
            this.grpDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDetails.Location = new System.Drawing.Point(0, 0);
            this.grpDetails.Name = "grpDetails";
            this.grpDetails.Size = new System.Drawing.Size(730, 207);
            this.grpDetails.TabIndex = 0;
            this.grpDetails.TabStop = false;
            this.grpDetails.Text = "Details";
            // 
            // btnOpenExternalLink
            // 
            this.btnOpenExternalLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenExternalLink.Location = new System.Drawing.Point(590, 22);
            this.btnOpenExternalLink.Name = "btnOpenExternalLink";
            this.btnOpenExternalLink.Size = new System.Drawing.Size(134, 23);
            this.btnOpenExternalLink.TabIndex = 2;
            this.btnOpenExternalLink.Text = "Open external link";
            this.btnOpenExternalLink.UseVisualStyleBackColor = true;
            this.btnOpenExternalLink.Click += new System.EventHandler(this.btnOpenExternalLink_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(6, 51);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDescription.Size = new System.Drawing.Size(718, 150);
            this.txtDescription.TabIndex = 1;
            // 
            // lblEpisodeTitle
            // 
            this.lblEpisodeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEpisodeTitle.Location = new System.Drawing.Point(6, 22);
            this.lblEpisodeTitle.Name = "lblEpisodeTitle";
            this.lblEpisodeTitle.Size = new System.Drawing.Size(578, 23);
            this.lblEpisodeTitle.TabIndex = 0;
            this.lblEpisodeTitle.Text = "Select an episode...";
            this.lblEpisodeTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpLog
            // 
            this.grpLog.Controls.Add(this.txtLog);
            this.grpLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpLog.Location = new System.Drawing.Point(0, 561);
            this.grpLog.Name = "grpLog";
            this.grpLog.Size = new System.Drawing.Size(984, 100);
            this.grpLog.TabIndex = 2;
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
            this.txtLog.Size = new System.Drawing.Size(978, 78);
            this.txtLog.TabIndex = 0;
            // 
            // MainUiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.grpLog);
            this.Controls.Add(this.grpFetch);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "MainUiForm";
            this.Text = "Podcast Manager";
            this.Load += new System.EventHandler(this.MainUiForm_Load);
            this.grpFetch.ResumeLayout(false);
            this.grpFetch.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.grpPodcasts.ResumeLayout(false);
            this.grpPodcasts.PerformLayout();
            this.splitRight.Panel1.ResumeLayout(false);
            this.splitRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitRight)).EndInit();
            this.splitRight.ResumeLayout(false);
            this.grpEpisodes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEpisodes)).EndInit();
            this.grpDetails.ResumeLayout(false);
            this.grpDetails.PerformLayout();
            this.grpLog.ResumeLayout(false);
            this.grpLog.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox grpFetch;
        private Button btnSaveFeed;
        private TextBox txtCustomName;
        private Label lblCustomName;
        private Button btnFetch;
        private TextBox txtRssUrl;
        private Label lblRssUrl;
        private SplitContainer splitMain;
        private GroupBox grpPodcasts;
        private Button btnRename;
        private Button btnDelete;
        private ListBox lstPodcasts;
        private ComboBox cmbCategoryFilter;
        private Label lblCategory;
        private SplitContainer splitRight;
        private GroupBox grpEpisodes;
        private DataGridView dgvEpisodes;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colPublishDate;
        private DataGridViewTextBoxColumn colDuration;
        private GroupBox grpDetails;
        private Button btnOpenExternalLink;
        private TextBox txtDescription;
        private Label lblEpisodeTitle;
        private GroupBox grpLog;
        private TextBox txtLog;
    }
}
