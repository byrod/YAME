/*
 * Created by SharpDevelop.
 * User: gs0760
 * Date: 06/01/2016
 * Time: 09:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Windows.Forms;

namespace YAME
{
    partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{

            if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);

            

        }
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.listBoxModFound = new System.Windows.Forms.ListBox();
            this.listBoxModActivated = new System.Windows.Forms.ListBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelModFindAndActivated = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.labelModPath = new System.Windows.Forms.Label();
            this.labelModFound = new System.Windows.Forms.Label();
            this.labelModActivated = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuExplore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuRename = new System.Windows.Forms.ToolStripMenuItem();
            this.tbRenameMod = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonLoadSave = new System.Windows.Forms.Button();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonModAllMoins = new System.Windows.Forms.Button();
            this.buttonModMoins = new System.Windows.Forms.Button();
            this.buttonModPlus = new System.Windows.Forms.Button();
            this.tbCreateMod = new System.Windows.Forms.TextBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuGenerateSnapshot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuCompareSnapshot = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.picBackgroundGray = new System.Windows.Forms.PictureBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundGray)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxModFound
            // 
            this.listBoxModFound.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listBoxModFound.BackColor = System.Drawing.SystemColors.Window;
            this.listBoxModFound.Cursor = System.Windows.Forms.Cursors.Default;
            this.listBoxModFound.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxModFound.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxModFound.FormattingEnabled = true;
            this.listBoxModFound.HorizontalScrollbar = true;
            this.listBoxModFound.ItemHeight = 16;
            this.listBoxModFound.Location = new System.Drawing.Point(3, 91);
            this.listBoxModFound.Name = "listBoxModFound";
            this.listBoxModFound.Size = new System.Drawing.Size(326, 404);
            this.listBoxModFound.Sorted = true;
            this.listBoxModFound.TabIndex = 0;
            this.listBoxModFound.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItemHandler);
            this.listBoxModFound.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxModFound_onKeyDown);
            this.listBoxModFound.MouseMove += new System.Windows.Forms.MouseEventHandler(this.listBoxModFound_MouseHover);
            // 
            // listBoxModActivated
            // 
            this.listBoxModActivated.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.listBoxModActivated.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.listBoxModActivated.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxModActivated.FormattingEnabled = true;
            this.listBoxModActivated.HorizontalScrollbar = true;
            this.listBoxModActivated.ItemHeight = 16;
            this.listBoxModActivated.Location = new System.Drawing.Point(379, 91);
            this.listBoxModActivated.Name = "listBoxModActivated";
            this.listBoxModActivated.Size = new System.Drawing.Size(327, 404);
            this.listBoxModActivated.TabIndex = 1;
            this.listBoxModActivated.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.listBox_DrawItemHandler);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonClose.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClose.Location = new System.Drawing.Point(627, 14);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(70, 24);
            this.buttonClose.TabIndex = 5;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Visible = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1.ForeColor = System.Drawing.Color.LightGreen;
            this.progressBar1.Location = new System.Drawing.Point(6, 10);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(149, 17);
            this.progressBar1.TabIndex = 6;
            // 
            // labelModFindAndActivated
            // 
            this.labelModFindAndActivated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelModFindAndActivated.AutoSize = true;
            this.labelModFindAndActivated.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModFindAndActivated.Location = new System.Drawing.Point(2, 10);
            this.labelModFindAndActivated.Margin = new System.Windows.Forms.Padding(0);
            this.labelModFindAndActivated.Name = "labelModFindAndActivated";
            this.labelModFindAndActivated.Size = new System.Drawing.Size(157, 16);
            this.labelModFindAndActivated.TabIndex = 0;
            this.labelModFindAndActivated.Text = "0 mods found (0 activated)";
            this.labelModFindAndActivated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelVersion.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVersion.ForeColor = System.Drawing.Color.White;
            this.labelVersion.Location = new System.Drawing.Point(67, 11);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(316, 23);
            this.labelVersion.TabIndex = 9;
            this.labelVersion.Text = " Yet Another Mod Enabler - v1.2.0";
            // 
            // labelModPath
            // 
            this.labelModPath.AutoSize = true;
            this.labelModPath.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelModPath.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModPath.ForeColor = System.Drawing.Color.Gainsboro;
            this.labelModPath.Location = new System.Drawing.Point(73, 32);
            this.labelModPath.Name = "labelModPath";
            this.labelModPath.Size = new System.Drawing.Size(78, 16);
            this.labelModPath.TabIndex = 10;
            this.labelModPath.Text = "[ mod path ]";
            // 
            // labelModFound
            // 
            this.labelModFound.AutoSize = true;
            this.labelModFound.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModFound.Location = new System.Drawing.Point(3, 72);
            this.labelModFound.Name = "labelModFound";
            this.labelModFound.Size = new System.Drawing.Size(93, 16);
            this.labelModFound.TabIndex = 13;
            this.labelModFound.Text = "Available Mods";
            // 
            // labelModActivated
            // 
            this.labelModActivated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelModActivated.AutoSize = true;
            this.labelModActivated.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelModActivated.Location = new System.Drawing.Point(379, 74);
            this.labelModActivated.Name = "labelModActivated";
            this.labelModActivated.Size = new System.Drawing.Size(96, 16);
            this.labelModActivated.TabIndex = 14;
            this.labelModActivated.Text = "Activated Mods";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.labelModFindAndActivated);
            this.groupBox1.Location = new System.Drawing.Point(3, 492);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(544, 31);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Location = new System.Drawing.Point(547, 492);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 31);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.contextMenuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuExplore,
            this.toolStripSeparator1,
            this.toolStripMenuDelete,
            this.toolStripMenuRename});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 76);
            // 
            // toolStripMenuExplore
            // 
            this.toolStripMenuExplore.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuExplore.Name = "toolStripMenuExplore";
            this.toolStripMenuExplore.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuExplore.Text = "Explore";
            this.toolStripMenuExplore.Click += new System.EventHandler(this.toolStripMenuExplore_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(114, 6);
            // 
            // toolStripMenuDelete
            // 
            this.toolStripMenuDelete.Name = "toolStripMenuDelete";
            this.toolStripMenuDelete.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuDelete.Text = "Delete";
            this.toolStripMenuDelete.Click += new System.EventHandler(this.toolStripMenuDelete_Click);
            // 
            // toolStripMenuRename
            // 
            this.toolStripMenuRename.Name = "toolStripMenuRename";
            this.toolStripMenuRename.Size = new System.Drawing.Size(117, 22);
            this.toolStripMenuRename.Text = "Rename";
            this.toolStripMenuRename.Click += new System.EventHandler(this.toolStripMenuRename_Click);
            // 
            // tbRenameMod
            // 
            this.tbRenameMod.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.tbRenameMod.Location = new System.Drawing.Point(101, 68);
            this.tbRenameMod.Name = "tbRenameMod";
            this.tbRenameMod.Size = new System.Drawing.Size(70, 21);
            this.tbRenameMod.TabIndex = 18;
            this.tbRenameMod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbRenameMod_KeyDown);
            this.tbRenameMod.Leave += new System.EventHandler(this.tbRenameMod_Leave);
            // 
            // buttonLoadSave
            // 
            this.buttonLoadSave.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonLoadSave.BackgroundImage = global::YAME.Properties.Resources.SettingsGray;
            this.buttonLoadSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonLoadSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonLoadSave.FlatAppearance.BorderSize = 0;
            this.buttonLoadSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLoadSave.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonLoadSave.Location = new System.Drawing.Point(337, 269);
            this.buttonLoadSave.Name = "buttonLoadSave";
            this.buttonLoadSave.Size = new System.Drawing.Size(34, 34);
            this.buttonLoadSave.TabIndex = 20;
            this.toolTip1.SetToolTip(this.buttonLoadSave, "Load/Save mod profile");
            this.buttonLoadSave.UseVisualStyleBackColor = true;
            this.buttonLoadSave.Click += new System.EventHandler(this.buttonLoadSave_Click);
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonRefresh.BackgroundImage = global::YAME.Properties.Resources.ButtonRefresh;
            this.buttonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefresh.Location = new System.Drawing.Point(335, 459);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(38, 38);
            this.buttonRefresh.TabIndex = 15;
            this.toolTip1.SetToolTip(this.buttonRefresh, "Refresh available mods (F5)");
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonModAllMoins
            // 
            this.buttonModAllMoins.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonModAllMoins.BackgroundImage = global::YAME.Properties.Resources.ButtonCroix_a;
            this.buttonModAllMoins.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonModAllMoins.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonModAllMoins.FlatAppearance.BorderSize = 0;
            this.buttonModAllMoins.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonModAllMoins.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonModAllMoins.Location = new System.Drawing.Point(335, 183);
            this.buttonModAllMoins.Name = "buttonModAllMoins";
            this.buttonModAllMoins.Size = new System.Drawing.Size(38, 38);
            this.buttonModAllMoins.TabIndex = 4;
            this.toolTip1.SetToolTip(this.buttonModAllMoins, "Disable all mods");
            this.buttonModAllMoins.UseVisualStyleBackColor = true;
            this.buttonModAllMoins.Click += new System.EventHandler(this.buttonModAllMoins_Click);
            // 
            // buttonModMoins
            // 
            this.buttonModMoins.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonModMoins.BackgroundImage = global::YAME.Properties.Resources.ButtonMoins_a;
            this.buttonModMoins.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonModMoins.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonModMoins.FlatAppearance.BorderSize = 0;
            this.buttonModMoins.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonModMoins.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonModMoins.Location = new System.Drawing.Point(335, 139);
            this.buttonModMoins.Name = "buttonModMoins";
            this.buttonModMoins.Size = new System.Drawing.Size(38, 38);
            this.buttonModMoins.TabIndex = 3;
            this.toolTip1.SetToolTip(this.buttonModMoins, "Disable selected mod");
            this.buttonModMoins.UseVisualStyleBackColor = true;
            this.buttonModMoins.Click += new System.EventHandler(this.buttonModMoins_Click);
            // 
            // buttonModPlus
            // 
            this.buttonModPlus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.buttonModPlus.BackColor = System.Drawing.Color.Transparent;
            this.buttonModPlus.BackgroundImage = global::YAME.Properties.Resources.ButtonPlus_a;
            this.buttonModPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonModPlus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonModPlus.FlatAppearance.BorderSize = 0;
            this.buttonModPlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonModPlus.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonModPlus.Location = new System.Drawing.Point(335, 95);
            this.buttonModPlus.Name = "buttonModPlus";
            this.buttonModPlus.Size = new System.Drawing.Size(38, 38);
            this.buttonModPlus.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonModPlus, "Enable selected mod");
            this.buttonModPlus.UseVisualStyleBackColor = false;
            this.buttonModPlus.Click += new System.EventHandler(this.buttonModPlus_Click);
            // 
            // tbCreateMod
            // 
            this.tbCreateMod.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.tbCreateMod.Location = new System.Drawing.Point(177, 68);
            this.tbCreateMod.Name = "tbCreateMod";
            this.tbCreateMod.Size = new System.Drawing.Size(70, 21);
            this.tbCreateMod.TabIndex = 19;
            this.tbCreateMod.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbCreateMod_KeyDown);
            this.tbCreateMod.Leave += new System.EventHandler(this.tbCreateMod_Leave);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuLoad,
            this.toolStripMenuSave,
            this.toolStripSeparator2,
            this.toolStripMenuGenerateSnapshot,
            this.toolStripMenuCompareSnapshot,
            this.toolStripSeparator3,
            this.toolStripMenuHelp,
            this.toolStripMenuUpdate,
            this.toolStripSeparator4,
            this.toolStripMenuAbout});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(246, 198);
            // 
            // toolStripMenuLoad
            // 
            this.toolStripMenuLoad.Name = "toolStripMenuLoad";
            this.toolStripMenuLoad.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuLoad.Text = "Load mod profile...";
            this.toolStripMenuLoad.Click += new System.EventHandler(this.toolStripMenuLoad_Click);
            // 
            // toolStripMenuSave
            // 
            this.toolStripMenuSave.Name = "toolStripMenuSave";
            this.toolStripMenuSave.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuSave.Text = "Save mod profile...";
            this.toolStripMenuSave.Click += new System.EventHandler(this.toolStripMenuSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(242, 6);
            // 
            // toolStripMenuGenerateSnapshot
            // 
            this.toolStripMenuGenerateSnapshot.Name = "toolStripMenuGenerateSnapshot";
            this.toolStripMenuGenerateSnapshot.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuGenerateSnapshot.Text = "Generate snapshot of game files";
            this.toolStripMenuGenerateSnapshot.Click += new System.EventHandler(this.toolStripMenuGenerateSnapshot_Click);
            // 
            // toolStripMenuCompareSnapshot
            // 
            this.toolStripMenuCompareSnapshot.Name = "toolStripMenuCompareSnapshot";
            this.toolStripMenuCompareSnapshot.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuCompareSnapshot.Text = "Compare game files to snapshot";
            this.toolStripMenuCompareSnapshot.Click += new System.EventHandler(this.toolStripMenuCompareSnapshot_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(242, 6);
            // 
            // toolStripMenuHelp
            // 
            this.toolStripMenuHelp.Name = "toolStripMenuHelp";
            this.toolStripMenuHelp.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuHelp.Text = "Visit YAME Website...";
            this.toolStripMenuHelp.Click += new System.EventHandler(this.toolStripMenuHelp_Click);
            // 
            // toolStripMenuAbout
            // 
            this.toolStripMenuAbout.Name = "toolStripMenuAbout";
            this.toolStripMenuAbout.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuAbout.Text = "About...";
            this.toolStripMenuAbout.Click += new System.EventHandler(this.toolStripMenuAbout_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.SupportMultiDottedExtensions = true;
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(17, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(45, 45);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // picBackgroundGray
            // 
            this.picBackgroundGray.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picBackgroundGray.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.picBackgroundGray.Location = new System.Drawing.Point(0, 1);
            this.picBackgroundGray.Name = "picBackgroundGray";
            this.picBackgroundGray.Size = new System.Drawing.Size(717, 65);
            this.picBackgroundGray.TabIndex = 12;
            this.picBackgroundGray.TabStop = false;
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(242, 6);
            // 
            // toolStripMenuUpdate
            // 
            this.toolStripMenuUpdate.Name = "toolStripMenuUpdate";
            this.toolStripMenuUpdate.Size = new System.Drawing.Size(245, 22);
            this.toolStripMenuUpdate.Text = "Verify update...";
            this.toolStripMenuUpdate.Click += new System.EventHandler(this.toolStripMenuUpdate_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.ClientSize = new System.Drawing.Size(709, 524);
            this.Controls.Add(this.buttonLoadSave);
            this.Controls.Add(this.labelModPath);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.listBoxModFound);
            this.Controls.Add(this.listBoxModActivated);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.tbCreateMod);
            this.Controls.Add(this.tbRenameMod);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelModActivated);
            this.Controls.Add(this.labelModFound);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.buttonModAllMoins);
            this.Controls.Add(this.buttonModMoins);
            this.Controls.Add(this.buttonModPlus);
            this.Controls.Add(this.picBackgroundGray);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(709, 553);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " YAME";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_OnKeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBackgroundGray)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.ListBox listBoxModFound;
        private System.Windows.Forms.ListBox listBoxModActivated;
        private System.Windows.Forms.Button buttonModPlus;
        private System.Windows.Forms.Button buttonModMoins;
        private System.Windows.Forms.Button buttonModAllMoins;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelModFindAndActivated;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelModPath;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox picBackgroundGray;
        private System.Windows.Forms.Label labelModFound;
        private System.Windows.Forms.Label labelModActivated;
        private System.Windows.Forms.Button buttonRefresh;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem toolStripMenuExplore;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem toolStripMenuDelete;
        private ToolStripMenuItem toolStripMenuRename;
        private TextBox tbRenameMod;
        private System.Windows.Forms.ToolTip toolTip1;
        private TextBox tbCreateMod;
        private Button buttonLoadSave;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem toolStripMenuLoad;
        private ToolStripMenuItem toolStripMenuSave;
        private SaveFileDialog saveFileDialog1;
        private OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuCompareSnapshot;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuAbout;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuGenerateSnapshot;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuHelp;
        private ToolStripMenuItem toolStripMenuUpdate;
        private ToolStripSeparator toolStripSeparator4;
    }
}
