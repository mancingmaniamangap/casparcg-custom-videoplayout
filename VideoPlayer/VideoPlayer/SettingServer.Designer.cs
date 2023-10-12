namespace VideoPlayer
{
    partial class SettingServer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingServer));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnMinimze = new System.Windows.Forms.PictureBox();
            this.btnMinimize = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.serverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conditionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblCon = new System.Windows.Forms.Label();
            this.txtStats = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gridServer = new System.Windows.Forms.DataGridView();
            this.klikKananKonek = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.connectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.folderPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFolderPath = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimze)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridServer)).BeginInit();
            this.klikKananKonek.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.SeaGreen;
            this.panel1.Controls.Add(this.btnMinimze);
            this.panel1.Controls.Add(this.btnMinimize);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1152, 40);
            this.panel1.TabIndex = 72;
            // 
            // btnMinimze
            // 
            this.btnMinimze.BackgroundImage = global::VideoPlayer.Properties.Resources.Back1;
            this.btnMinimze.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimze.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnMinimze.Location = new System.Drawing.Point(1110, 2);
            this.btnMinimze.Name = "btnMinimze";
            this.btnMinimze.Size = new System.Drawing.Size(35, 35);
            this.btnMinimze.TabIndex = 55;
            this.btnMinimze.TabStop = false;
            this.btnMinimze.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.DimGray;
            this.btnMinimize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMinimize.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.btnMinimize.Location = new System.Drawing.Point(1110, 2);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(35, 35);
            this.btnMinimize.TabIndex = 54;
            this.btnMinimize.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serverToolStripMenuItem,
            this.conditionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(6, 6);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(146, 25);
            this.menuStrip1.TabIndex = 51;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // serverToolStripMenuItem
            // 
            this.serverToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveServerToolStripMenuItem});
            this.serverToolStripMenuItem.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.serverToolStripMenuItem.Name = "serverToolStripMenuItem";
            this.serverToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.serverToolStripMenuItem.Text = "Server";
            // 
            // saveServerToolStripMenuItem
            // 
            this.saveServerToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.saveServerToolStripMenuItem.Name = "saveServerToolStripMenuItem";
            this.saveServerToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveServerToolStripMenuItem.Text = "Save Server";
            this.saveServerToolStripMenuItem.Click += new System.EventHandler(this.saveServerToolStripMenuItem_Click);
            // 
            // conditionToolStripMenuItem
            // 
            this.conditionToolStripMenuItem.BackColor = System.Drawing.Color.SeaGreen;
            this.conditionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.conditionToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.conditionToolStripMenuItem.Name = "conditionToolStripMenuItem";
            this.conditionToolStripMenuItem.Size = new System.Drawing.Size(79, 21);
            this.conditionToolStripMenuItem.Text = "Condition";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // lblCon
            // 
            this.lblCon.AutoSize = true;
            this.lblCon.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F);
            this.lblCon.ForeColor = System.Drawing.Color.Red;
            this.lblCon.Location = new System.Drawing.Point(734, 67);
            this.lblCon.Name = "lblCon";
            this.lblCon.Size = new System.Drawing.Size(384, 46);
            this.lblCon.TabIndex = 69;
            this.lblCon.Text = "DISCONNECTED !!!";
            // 
            // txtStats
            // 
            this.txtStats.Location = new System.Drawing.Point(916, 125);
            this.txtStats.Name = "txtStats";
            this.txtStats.Size = new System.Drawing.Size(100, 20);
            this.txtStats.TabIndex = 66;
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(803, 259);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(315, 20);
            this.txtDesc.TabIndex = 5;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(1012, 212);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(106, 20);
            this.txtPort.TabIndex = 4;
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(803, 212);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(135, 20);
            this.txtHost.TabIndex = 3;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(937, 167);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(181, 20);
            this.txtName.TabIndex = 2;
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(803, 167);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(62, 20);
            this.txtId.TabIndex = 77;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(861, 128);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 60;
            this.label6.Text = "Status";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(728, 262);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 59;
            this.label5.Text = "Description";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(962, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 58;
            this.label4.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(728, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 57;
            this.label3.Text = "Host";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(888, 170);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 56;
            this.label2.Text = "Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(728, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 13);
            this.label1.TabIndex = 55;
            this.label1.Text = "ID";
            // 
            // gridServer
            // 
            this.gridServer.AllowUserToAddRows = false;
            this.gridServer.AllowUserToDeleteRows = false;
            this.gridServer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridServer.ContextMenuStrip = this.klikKananKonek;
            this.gridServer.Location = new System.Drawing.Point(16, 74);
            this.gridServer.Name = "gridServer";
            this.gridServer.ReadOnly = true;
            this.gridServer.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridServer.Size = new System.Drawing.Size(696, 205);
            this.gridServer.TabIndex = 54;
            this.gridServer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridServer_CellClick);
            // 
            // klikKananKonek
            // 
            this.klikKananKonek.BackColor = System.Drawing.SystemColors.Control;
            this.klikKananKonek.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem1,
            this.disconnectToolStripMenuItem1});
            this.klikKananKonek.Name = "klikKananKonek";
            this.klikKananKonek.Size = new System.Drawing.Size(134, 48);
            // 
            // connectToolStripMenuItem1
            // 
            this.connectToolStripMenuItem1.BackColor = System.Drawing.Color.White;
            this.connectToolStripMenuItem1.Name = "connectToolStripMenuItem1";
            this.connectToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.connectToolStripMenuItem1.Text = "Connect";
            this.connectToolStripMenuItem1.Click += new System.EventHandler(this.connectToolStripMenuItem1_Click);
            // 
            // disconnectToolStripMenuItem1
            // 
            this.disconnectToolStripMenuItem1.BackColor = System.Drawing.Color.White;
            this.disconnectToolStripMenuItem1.Name = "disconnectToolStripMenuItem1";
            this.disconnectToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem1.Text = "Disconnect";
            this.disconnectToolStripMenuItem1.Click += new System.EventHandler(this.disconnectToolStripMenuItem1_Click);
            // 
            // folderPath
            // 
            this.folderPath.Location = new System.Drawing.Point(33, 299);
            this.folderPath.Name = "folderPath";
            this.folderPath.ReadOnly = true;
            this.folderPath.Size = new System.Drawing.Size(315, 20);
            this.folderPath.TabIndex = 6;
            // 
            // btnBrowseFolderPath
            // 
            this.btnBrowseFolderPath.BackgroundImage = global::VideoPlayer.Properties.Resources.browse;
            this.btnBrowseFolderPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnBrowseFolderPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBrowseFolderPath.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBrowseFolderPath.Location = new System.Drawing.Point(354, 296);
            this.btnBrowseFolderPath.Name = "btnBrowseFolderPath";
            this.btnBrowseFolderPath.Size = new System.Drawing.Size(40, 25);
            this.btnBrowseFolderPath.TabIndex = 7;
            this.btnBrowseFolderPath.UseVisualStyleBackColor = true;
            this.btnBrowseFolderPath.Click += new System.EventHandler(this.btnBrowseFolderPath_Click);
            // 
            // btnReset
            // 
            this.btnReset.BackgroundImage = global::VideoPlayer.Properties.Resources.reset;
            this.btnReset.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnReset.Location = new System.Drawing.Point(1078, 299);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(40, 25);
            this.btnReset.TabIndex = 11;
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = global::VideoPlayer.Properties.Resources.delete;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Location = new System.Drawing.Point(976, 299);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(40, 25);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.BackgroundImage = global::VideoPlayer.Properties.Resources.Edit;
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.Location = new System.Drawing.Point(858, 299);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(40, 25);
            this.btnEdit.TabIndex = 9;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::VideoPlayer.Properties.Resources.Add;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Location = new System.Drawing.Point(748, 299);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 25);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // SettingServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1152, 362);
            this.Controls.Add(this.btnBrowseFolderPath);
            this.Controls.Add(this.folderPath);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblCon);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtStats);
            this.Controls.Add(this.txtDesc);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gridServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "SettingServer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.SettingServer_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.menuStrip1_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimze)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMinimize)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridServer)).EndInit();
            this.klikKananKonek.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox btnMinimize;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem serverToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conditionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.Button btnReset;
        public System.Windows.Forms.Button btnDelete;
        public System.Windows.Forms.Label lblCon;
        public System.Windows.Forms.Button btnEdit;
        public System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.TextBox txtStats;
        public System.Windows.Forms.TextBox txtDesc;
        public System.Windows.Forms.TextBox txtPort;
        public System.Windows.Forms.TextBox txtHost;
        public System.Windows.Forms.TextBox txtName;
        public System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView gridServer;
        private System.Windows.Forms.PictureBox btnMinimze;
        private System.Windows.Forms.ContextMenuStrip klikKananKonek;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem1;
        public System.Windows.Forms.TextBox folderPath;
        private System.Windows.Forms.Button btnBrowseFolderPath;
    }
}