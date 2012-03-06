namespace Sample
{
    partial class Sample
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this._mSplitter = new System.Windows.Forms.SplitContainer();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this._mPictureBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._mSplitter)).BeginInit();
            this._mSplitter.Panel1.SuspendLayout();
            this._mSplitter.Panel2.SuspendLayout();
            this._mSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._mPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(616, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileOpen});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuFileOpen.Text = "&Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.mnuFileOpen_Click);
            // 
            // _mSplitter
            // 
            this._mSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mSplitter.Location = new System.Drawing.Point(0, 24);
            this._mSplitter.Name = "_mSplitter";
            // 
            // _mSplitter.Panel1
            // 
            this._mSplitter.Panel1.Controls.Add(this.txtOutput);
            // 
            // _mSplitter.Panel2
            // 
            this._mSplitter.Panel2.Controls.Add(this._mPictureBox);
            this._mSplitter.Size = new System.Drawing.Size(616, 396);
            this._mSplitter.SplitterDistance = 205;
            this._mSplitter.TabIndex = 1;
            // 
            // txtOutput
            // 
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Location = new System.Drawing.Point(0, 0);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(205, 396);
            this.txtOutput.TabIndex = 0;
            // 
            // _mPictureBox
            // 
            this._mPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mPictureBox.Location = new System.Drawing.Point(0, 0);
            this._mPictureBox.Name = "_mPictureBox";
            this._mPictureBox.Size = new System.Drawing.Size(407, 396);
            this._mPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this._mPictureBox.TabIndex = 0;
            this._mPictureBox.TabStop = false;
            // 
            // Sample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 420);
            this.Controls.Add(this._mSplitter);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Sample";
            this.Text = "USGS DEM Sample";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this._mSplitter.Panel1.ResumeLayout(false);
            this._mSplitter.Panel1.PerformLayout();
            this._mSplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._mSplitter)).EndInit();
            this._mSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._mPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileOpen;
        private System.Windows.Forms.SplitContainer _mSplitter;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.PictureBox _mPictureBox;
    }
}

