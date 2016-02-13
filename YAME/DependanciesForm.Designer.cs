namespace YAME
{
    partial class DependanciesForm
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
        	this.pictureBox1 = new System.Windows.Forms.PictureBox();
        	this.TextBox = new System.Windows.Forms.RichTextBox();
        	this.label1 = new System.Windows.Forms.Label();
        	this.label2 = new System.Windows.Forms.Label();
        	this.buttonYes = new System.Windows.Forms.Button();
        	this.buttonNo = new System.Windows.Forms.Button();
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// pictureBox1
        	// 
        	this.pictureBox1.Image = global::YAME.Properties.Resources.warning;
        	this.pictureBox1.Location = new System.Drawing.Point(6, 4);
        	this.pictureBox1.Name = "pictureBox1";
        	this.pictureBox1.Size = new System.Drawing.Size(45, 40);
        	this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
        	this.pictureBox1.TabIndex = 0;
        	this.pictureBox1.TabStop = false;
        	// 
        	// TextBox
        	// 
        	this.TextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
        	this.TextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.TextBox.Location = new System.Drawing.Point(57, 5);
        	this.TextBox.Name = "TextBox";
        	this.TextBox.ReadOnly = true;
        	this.TextBox.Size = new System.Drawing.Size(460, 169);
        	this.TextBox.TabIndex = 1;
        	this.TextBox.Text = "";
        	this.TextBox.WordWrap = false;
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(137, 179);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(291, 13);
        	this.label1.TabIndex = 2;
        	this.label1.Text = "Enabling this mod may have adverese effects on your game.";
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.Location = new System.Drawing.Point(174, 196);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(205, 13);
        	this.label2.TabIndex = 3;
        	this.label2.Text = "Are you sure you wish to enable this mod?";
        	// 
        	// buttonYes
        	// 
        	this.buttonYes.Location = new System.Drawing.Point(178, 216);
        	this.buttonYes.Name = "buttonYes";
        	this.buttonYes.Size = new System.Drawing.Size(77, 25);
        	this.buttonYes.TabIndex = 4;
        	this.buttonYes.Text = "Yes";
        	this.buttonYes.UseVisualStyleBackColor = true;
        	this.buttonYes.Click += new System.EventHandler(this.buttonYes_Click);
        	// 
        	// buttonNo
        	// 
        	this.buttonNo.Location = new System.Drawing.Point(302, 216);
        	this.buttonNo.Name = "buttonNo";
        	this.buttonNo.Size = new System.Drawing.Size(77, 25);
        	this.buttonNo.TabIndex = 5;
        	this.buttonNo.Text = "No";
        	this.buttonNo.UseVisualStyleBackColor = true;
        	this.buttonNo.Click += new System.EventHandler(this.buttonNo_Click);
        	// 
        	// DependanciesForm
        	// 
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
        	this.ClientSize = new System.Drawing.Size(522, 243);
        	this.ControlBox = false;
        	this.Controls.Add(this.buttonNo);
        	this.Controls.Add(this.buttonYes);
        	this.Controls.Add(this.label2);
        	this.Controls.Add(this.label1);
        	this.Controls.Add(this.TextBox);
        	this.Controls.Add(this.pictureBox1);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        	this.Name = "DependanciesForm";
        	this.ShowIcon = false;
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        	this.Text = "Warning enabling";
        	((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
        	this.ResumeLayout(false);
        	this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RichTextBox TextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonYes;
        private System.Windows.Forms.Button buttonNo;
    }
}