namespace TextureUnpacker
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_Plist = new System.Windows.Forms.RadioButton();
            this.RB_Atlas = new System.Windows.Forms.RadioButton();
            this.CB_ShowFrame = new System.Windows.Forms.CheckBox();
            this.texture_box = new System.Windows.Forms.PictureBox();
            this.TB_ConfigPath = new System.Windows.Forms.TextBox();
            this.BT_OpenConfig = new System.Windows.Forms.Button();
            this.TB_TexturePath = new System.Windows.Forms.TextBox();
            this.BT_OpenTexture = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BT_Open = new System.Windows.Forms.Button();
            this.BT_Export = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.CB_ClipSpeite = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texture_box)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_Plist);
            this.groupBox1.Controls.Add(this.RB_Atlas);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(98, 109);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "FileType";
            // 
            // RB_Plist
            // 
            this.RB_Plist.AutoSize = true;
            this.RB_Plist.Checked = true;
            this.RB_Plist.Location = new System.Drawing.Point(6, 20);
            this.RB_Plist.Name = "RB_Plist";
            this.RB_Plist.Size = new System.Drawing.Size(53, 16);
            this.RB_Plist.TabIndex = 1;
            this.RB_Plist.TabStop = true;
            this.RB_Plist.Text = "Plist";
            this.RB_Plist.UseVisualStyleBackColor = true;
            // 
            // RB_Atlas
            // 
            this.RB_Atlas.AutoSize = true;
            this.RB_Atlas.Location = new System.Drawing.Point(6, 42);
            this.RB_Atlas.Name = "RB_Atlas";
            this.RB_Atlas.Size = new System.Drawing.Size(53, 16);
            this.RB_Atlas.TabIndex = 0;
            this.RB_Atlas.Text = "Atlas";
            this.RB_Atlas.UseVisualStyleBackColor = true;
            // 
            // CB_ShowFrame
            // 
            this.CB_ShowFrame.AutoSize = true;
            this.CB_ShowFrame.Checked = true;
            this.CB_ShowFrame.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_ShowFrame.Location = new System.Drawing.Point(197, 105);
            this.CB_ShowFrame.Name = "CB_ShowFrame";
            this.CB_ShowFrame.Size = new System.Drawing.Size(84, 16);
            this.CB_ShowFrame.TabIndex = 11;
            this.CB_ShowFrame.Text = "Show frame";
            this.CB_ShowFrame.UseVisualStyleBackColor = true;
            this.CB_ShowFrame.CheckedChanged += new System.EventHandler(this.CB_ShowFrame_CheckedChanged);
            // 
            // texture_box
            // 
            this.texture_box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.texture_box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.texture_box.Location = new System.Drawing.Point(13, 127);
            this.texture_box.Name = "texture_box";
            this.texture_box.Size = new System.Drawing.Size(616, 415);
            this.texture_box.TabIndex = 1;
            this.texture_box.TabStop = false;
            // 
            // TB_ConfigPath
            // 
            this.TB_ConfigPath.AllowDrop = true;
            this.TB_ConfigPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ConfigPath.Location = new System.Drawing.Point(116, 32);
            this.TB_ConfigPath.Name = "TB_ConfigPath";
            this.TB_ConfigPath.Size = new System.Drawing.Size(466, 21);
            this.TB_ConfigPath.TabIndex = 2;
            this.TB_ConfigPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.TB_ConfigPath_DragDrop);
            this.TB_ConfigPath.DragEnter += new System.Windows.Forms.DragEventHandler(this.TB_ConfigPath_DragEnter);
            // 
            // BT_OpenConfig
            // 
            this.BT_OpenConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_OpenConfig.Location = new System.Drawing.Point(588, 32);
            this.BT_OpenConfig.Name = "BT_OpenConfig";
            this.BT_OpenConfig.Size = new System.Drawing.Size(41, 23);
            this.BT_OpenConfig.TabIndex = 3;
            this.BT_OpenConfig.Text = "...";
            this.BT_OpenConfig.UseVisualStyleBackColor = true;
            this.BT_OpenConfig.Click += new System.EventHandler(this.BT_OpenConfig_Click);
            // 
            // TB_TexturePath
            // 
            this.TB_TexturePath.AllowDrop = true;
            this.TB_TexturePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_TexturePath.Location = new System.Drawing.Point(116, 73);
            this.TB_TexturePath.Name = "TB_TexturePath";
            this.TB_TexturePath.Size = new System.Drawing.Size(464, 21);
            this.TB_TexturePath.TabIndex = 4;
            this.TB_TexturePath.DragDrop += new System.Windows.Forms.DragEventHandler(this.TB_TexturePath_DragDrop);
            this.TB_TexturePath.DragEnter += new System.Windows.Forms.DragEventHandler(this.TB_TexturePath_DragEnter);
            // 
            // BT_OpenTexture
            // 
            this.BT_OpenTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_OpenTexture.Location = new System.Drawing.Point(588, 71);
            this.BT_OpenTexture.Name = "BT_OpenTexture";
            this.BT_OpenTexture.Size = new System.Drawing.Size(40, 23);
            this.BT_OpenTexture.TabIndex = 5;
            this.BT_OpenTexture.Text = "...";
            this.BT_OpenTexture.UseVisualStyleBackColor = true;
            this.BT_OpenTexture.Click += new System.EventHandler(this.BT_OpenTexture_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(116, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "plist or atlas";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(114, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "unpack texture";
            // 
            // BT_Open
            // 
            this.BT_Open.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_Open.Location = new System.Drawing.Point(116, 100);
            this.BT_Open.Name = "BT_Open";
            this.BT_Open.Size = new System.Drawing.Size(75, 23);
            this.BT_Open.TabIndex = 8;
            this.BT_Open.Text = "open";
            this.BT_Open.UseVisualStyleBackColor = true;
            this.BT_Open.Click += new System.EventHandler(this.BT_Open_Click);
            // 
            // BT_Export
            // 
            this.BT_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BT_Export.Location = new System.Drawing.Point(553, 100);
            this.BT_Export.Name = "BT_Export";
            this.BT_Export.Size = new System.Drawing.Size(75, 23);
            this.BT_Export.TabIndex = 9;
            this.BT_Export.Text = "export";
            this.BT_Export.UseVisualStyleBackColor = true;
            this.BT_Export.Click += new System.EventHandler(this.BT_Export_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "PackerData|*.atlas;*.plist|All|*.*";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Filter = "PNG|*.png";
            // 
            // CB_ClipSpeite
            // 
            this.CB_ClipSpeite.AutoSize = true;
            this.CB_ClipSpeite.Location = new System.Drawing.Point(457, 104);
            this.CB_ClipSpeite.Name = "CB_ClipSpeite";
            this.CB_ClipSpeite.Size = new System.Drawing.Size(90, 16);
            this.CB_ClipSpeite.TabIndex = 12;
            this.CB_ClipSpeite.Text = "clip sprite";
            this.CB_ClipSpeite.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 554);
            this.Controls.Add(this.CB_ClipSpeite);
            this.Controls.Add(this.CB_ShowFrame);
            this.Controls.Add(this.BT_Export);
            this.Controls.Add(this.BT_Open);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BT_OpenTexture);
            this.Controls.Add(this.TB_TexturePath);
            this.Controls.Add(this.BT_OpenConfig);
            this.Controls.Add(this.TB_ConfigPath);
            this.Controls.Add(this.texture_box);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "TextureUnpacker";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texture_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_Plist;
        private System.Windows.Forms.RadioButton RB_Atlas;
        private System.Windows.Forms.PictureBox texture_box;
        private System.Windows.Forms.TextBox TB_ConfigPath;
        private System.Windows.Forms.Button BT_OpenConfig;
        private System.Windows.Forms.TextBox TB_TexturePath;
        private System.Windows.Forms.Button BT_OpenTexture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BT_Open;
        private System.Windows.Forms.Button BT_Export;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.CheckBox CB_ShowFrame;
        private System.Windows.Forms.CheckBox CB_ClipSpeite;
    }
}

