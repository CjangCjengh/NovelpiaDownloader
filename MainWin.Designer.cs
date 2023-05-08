namespace NovelpiaDownloader
{
    partial class MainWin
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
            this.LoginGroup = new System.Windows.Forms.GroupBox();
            this.EmailLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.LoginkeyLabel = new System.Windows.Forms.Label();
            this.EmailText = new System.Windows.Forms.TextBox();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.LoginkeyText = new System.Windows.Forms.TextBox();
            this.LoginButton1 = new System.Windows.Forms.Button();
            this.LoginButton2 = new System.Windows.Forms.Button();
            this.DownloadGroup = new System.Windows.Forms.GroupBox();
            this.NovelNoLable = new System.Windows.Forms.Label();
            this.ExtensionLabel = new System.Windows.Forms.Label();
            this.NovelNoText = new System.Windows.Forms.TextBox();
            this.EpubButton = new System.Windows.Forms.RadioButton();
            this.TxtButton = new System.Windows.Forms.RadioButton();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.ConsoleBox = new System.Windows.Forms.TextBox();
            this.LoginGroup.SuspendLayout();
            this.DownloadGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // LoginGroup
            // 
            this.LoginGroup.Controls.Add(this.LoginButton2);
            this.LoginGroup.Controls.Add(this.LoginButton1);
            this.LoginGroup.Controls.Add(this.LoginkeyText);
            this.LoginGroup.Controls.Add(this.PasswordText);
            this.LoginGroup.Controls.Add(this.EmailText);
            this.LoginGroup.Controls.Add(this.LoginkeyLabel);
            this.LoginGroup.Controls.Add(this.PasswordLabel);
            this.LoginGroup.Controls.Add(this.EmailLabel);
            this.LoginGroup.Location = new System.Drawing.Point(12, 12);
            this.LoginGroup.Name = "LoginGroup";
            this.LoginGroup.Size = new System.Drawing.Size(439, 159);
            this.LoginGroup.TabIndex = 0;
            this.LoginGroup.TabStop = false;
            this.LoginGroup.Text = "로그인";
            // 
            // EmailLabel
            // 
            this.EmailLabel.AutoSize = true;
            this.EmailLabel.Location = new System.Drawing.Point(21, 33);
            this.EmailLabel.Name = "EmailLabel";
            this.EmailLabel.Size = new System.Drawing.Size(66, 25);
            this.EmailLabel.TabIndex = 0;
            this.EmailLabel.Text = "이메일";
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(12, 70);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(84, 25);
            this.PasswordLabel.TabIndex = 1;
            this.PasswordLabel.Text = "비밀번호";
            // 
            // LoginkeyLabel
            // 
            this.LoginkeyLabel.AutoSize = true;
            this.LoginkeyLabel.Location = new System.Drawing.Point(6, 110);
            this.LoginkeyLabel.Name = "LoginkeyLabel";
            this.LoginkeyLabel.Size = new System.Drawing.Size(97, 25);
            this.LoginkeyLabel.TabIndex = 2;
            this.LoginkeyLabel.Text = "LOGINKEY";
            // 
            // EmailText
            // 
            this.EmailText.Location = new System.Drawing.Point(103, 30);
            this.EmailText.Name = "EmailText";
            this.EmailText.Size = new System.Drawing.Size(244, 31);
            this.EmailText.TabIndex = 3;
            // 
            // PasswordText
            // 
            this.PasswordText.Location = new System.Drawing.Point(103, 67);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.Size = new System.Drawing.Size(244, 31);
            this.PasswordText.TabIndex = 4;
            // 
            // LoginkeyText
            // 
            this.LoginkeyText.Location = new System.Drawing.Point(103, 107);
            this.LoginkeyText.Name = "LoginkeyText";
            this.LoginkeyText.Size = new System.Drawing.Size(244, 31);
            this.LoginkeyText.TabIndex = 5;
            // 
            // LoginButton1
            // 
            this.LoginButton1.Location = new System.Drawing.Point(353, 30);
            this.LoginButton1.Name = "LoginButton1";
            this.LoginButton1.Size = new System.Drawing.Size(75, 68);
            this.LoginButton1.TabIndex = 6;
            this.LoginButton1.Text = "로그인";
            this.LoginButton1.UseVisualStyleBackColor = true;
            this.LoginButton1.Click += new System.EventHandler(this.LoginButton1_Click);
            // 
            // LoginButton2
            // 
            this.LoginButton2.Location = new System.Drawing.Point(353, 104);
            this.LoginButton2.Name = "LoginButton2";
            this.LoginButton2.Size = new System.Drawing.Size(75, 36);
            this.LoginButton2.TabIndex = 7;
            this.LoginButton2.Text = "로그인";
            this.LoginButton2.UseVisualStyleBackColor = true;
            this.LoginButton2.Click += new System.EventHandler(this.LoginButton2_Click);
            // 
            // DownloadGroup
            // 
            this.DownloadGroup.Controls.Add(this.DownloadButton);
            this.DownloadGroup.Controls.Add(this.TxtButton);
            this.DownloadGroup.Controls.Add(this.EpubButton);
            this.DownloadGroup.Controls.Add(this.NovelNoText);
            this.DownloadGroup.Controls.Add(this.ExtensionLabel);
            this.DownloadGroup.Controls.Add(this.NovelNoLable);
            this.DownloadGroup.Location = new System.Drawing.Point(12, 177);
            this.DownloadGroup.Name = "DownloadGroup";
            this.DownloadGroup.Size = new System.Drawing.Size(439, 115);
            this.DownloadGroup.TabIndex = 1;
            this.DownloadGroup.TabStop = false;
            this.DownloadGroup.Text = "다운로드";
            // 
            // NovelNoLable
            // 
            this.NovelNoLable.AutoSize = true;
            this.NovelNoLable.Location = new System.Drawing.Point(12, 33);
            this.NovelNoLable.Name = "NovelNoLable";
            this.NovelNoLable.Size = new System.Drawing.Size(84, 25);
            this.NovelNoLable.TabIndex = 0;
            this.NovelNoLable.Text = "소설번호";
            // 
            // ExtensionLabel
            // 
            this.ExtensionLabel.AutoSize = true;
            this.ExtensionLabel.Location = new System.Drawing.Point(21, 69);
            this.ExtensionLabel.Name = "ExtensionLabel";
            this.ExtensionLabel.Size = new System.Drawing.Size(66, 25);
            this.ExtensionLabel.TabIndex = 1;
            this.ExtensionLabel.Text = "확장자";
            // 
            // NovelNoText
            // 
            this.NovelNoText.Location = new System.Drawing.Point(103, 30);
            this.NovelNoText.Name = "NovelNoText";
            this.NovelNoText.Size = new System.Drawing.Size(244, 31);
            this.NovelNoText.TabIndex = 6;
            // 
            // EpubButton
            // 
            this.EpubButton.AutoSize = true;
            this.EpubButton.Checked = true;
            this.EpubButton.Location = new System.Drawing.Point(121, 67);
            this.EpubButton.Name = "EpubButton";
            this.EpubButton.Size = new System.Drawing.Size(80, 29);
            this.EpubButton.TabIndex = 7;
            this.EpubButton.TabStop = true;
            this.EpubButton.Text = "EPUB";
            this.EpubButton.UseVisualStyleBackColor = true;
            // 
            // TxtButton
            // 
            this.TxtButton.AutoSize = true;
            this.TxtButton.Location = new System.Drawing.Point(250, 67);
            this.TxtButton.Name = "TxtButton";
            this.TxtButton.Size = new System.Drawing.Size(68, 29);
            this.TxtButton.TabIndex = 8;
            this.TxtButton.Text = "TXT";
            this.TxtButton.UseVisualStyleBackColor = true;
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(353, 30);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(75, 66);
            this.DownloadButton.TabIndex = 10;
            this.DownloadButton.Text = "다운\r\n로드";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConsoleBox.Location = new System.Drawing.Point(458, 23);
            this.ConsoleBox.Multiline = true;
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.ReadOnly = true;
            this.ConsoleBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleBox.Size = new System.Drawing.Size(420, 269);
            this.ConsoleBox.TabIndex = 5;
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 314);
            this.Controls.Add(this.ConsoleBox);
            this.Controls.Add(this.DownloadGroup);
            this.Controls.Add(this.LoginGroup);
            this.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "MainWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NovelpiaDownloader";
            this.LoginGroup.ResumeLayout(false);
            this.LoginGroup.PerformLayout();
            this.DownloadGroup.ResumeLayout(false);
            this.DownloadGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox LoginGroup;
        private System.Windows.Forms.Button LoginButton2;
        private System.Windows.Forms.Button LoginButton1;
        private System.Windows.Forms.TextBox LoginkeyText;
        private System.Windows.Forms.TextBox PasswordText;
        private System.Windows.Forms.TextBox EmailText;
        private System.Windows.Forms.Label LoginkeyLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.Label EmailLabel;
        private System.Windows.Forms.GroupBox DownloadGroup;
        private System.Windows.Forms.RadioButton TxtButton;
        private System.Windows.Forms.RadioButton EpubButton;
        private System.Windows.Forms.TextBox NovelNoText;
        private System.Windows.Forms.Label ExtensionLabel;
        private System.Windows.Forms.Label NovelNoLable;
        private System.Windows.Forms.Button DownloadButton;
        private System.Windows.Forms.TextBox ConsoleBox;
    }
}

