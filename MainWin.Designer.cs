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
            this.LoginButton2 = new System.Windows.Forms.Button();
            this.LoginButton1 = new System.Windows.Forms.Button();
            this.LoginkeyText = new System.Windows.Forms.TextBox();
            this.PasswordText = new System.Windows.Forms.TextBox();
            this.EmailText = new System.Windows.Forms.TextBox();
            this.LoginkeyLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.EmailLabel = new System.Windows.Forms.Label();
            this.DownloadGroup = new System.Windows.Forms.GroupBox();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.TxtButton = new System.Windows.Forms.RadioButton();
            this.EpubButton = new System.Windows.Forms.RadioButton();
            this.NovelNoText = new System.Windows.Forms.TextBox();
            this.ExtensionLabel = new System.Windows.Forms.Label();
            this.NovelNoLable = new System.Windows.Forms.Label();
            this.ConsoleBox = new System.Windows.Forms.TextBox();
            this.ThreadLabel = new System.Windows.Forms.Label();
            this.ThreadNum = new System.Windows.Forms.NumericUpDown();
            this.IntervalLabel = new System.Windows.Forms.Label();
            this.SecondLabel = new System.Windows.Forms.Label();
            this.IntervalNum = new System.Windows.Forms.NumericUpDown();
            this.FromCheck = new System.Windows.Forms.CheckBox();
            this.FromNum = new System.Windows.Forms.NumericUpDown();
            this.FromLabel = new System.Windows.Forms.Label();
            this.ToLabel = new System.Windows.Forms.Label();
            this.ToNum = new System.Windows.Forms.NumericUpDown();
            this.ToCheck = new System.Windows.Forms.CheckBox();
            this.LoginGroup.SuspendLayout();
            this.DownloadGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToNum)).BeginInit();
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
            // LoginkeyText
            // 
            this.LoginkeyText.Location = new System.Drawing.Point(103, 107);
            this.LoginkeyText.Name = "LoginkeyText";
            this.LoginkeyText.Size = new System.Drawing.Size(244, 31);
            this.LoginkeyText.TabIndex = 5;
            // 
            // PasswordText
            // 
            this.PasswordText.Location = new System.Drawing.Point(103, 67);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.Size = new System.Drawing.Size(244, 31);
            this.PasswordText.TabIndex = 4;
            // 
            // EmailText
            // 
            this.EmailText.Location = new System.Drawing.Point(103, 30);
            this.EmailText.Name = "EmailText";
            this.EmailText.Size = new System.Drawing.Size(244, 31);
            this.EmailText.TabIndex = 3;
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
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(12, 70);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(84, 25);
            this.PasswordLabel.TabIndex = 1;
            this.PasswordLabel.Text = "비밀번호";
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
            // DownloadGroup
            // 
            this.DownloadGroup.Controls.Add(this.ToLabel);
            this.DownloadGroup.Controls.Add(this.ToNum);
            this.DownloadGroup.Controls.Add(this.ToCheck);
            this.DownloadGroup.Controls.Add(this.FromLabel);
            this.DownloadGroup.Controls.Add(this.FromNum);
            this.DownloadGroup.Controls.Add(this.FromCheck);
            this.DownloadGroup.Controls.Add(this.DownloadButton);
            this.DownloadGroup.Controls.Add(this.TxtButton);
            this.DownloadGroup.Controls.Add(this.EpubButton);
            this.DownloadGroup.Controls.Add(this.NovelNoText);
            this.DownloadGroup.Controls.Add(this.ExtensionLabel);
            this.DownloadGroup.Controls.Add(this.NovelNoLable);
            this.DownloadGroup.Location = new System.Drawing.Point(13, 214);
            this.DownloadGroup.Name = "DownloadGroup";
            this.DownloadGroup.Size = new System.Drawing.Size(439, 152);
            this.DownloadGroup.TabIndex = 1;
            this.DownloadGroup.TabStop = false;
            this.DownloadGroup.Text = "다운로드";
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(352, 67);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(75, 66);
            this.DownloadButton.TabIndex = 10;
            this.DownloadButton.Text = "다운\r\n로드";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // TxtButton
            // 
            this.TxtButton.AutoSize = true;
            this.TxtButton.Location = new System.Drawing.Point(249, 104);
            this.TxtButton.Name = "TxtButton";
            this.TxtButton.Size = new System.Drawing.Size(68, 29);
            this.TxtButton.TabIndex = 8;
            this.TxtButton.Text = "TXT";
            this.TxtButton.UseVisualStyleBackColor = true;
            // 
            // EpubButton
            // 
            this.EpubButton.AutoSize = true;
            this.EpubButton.Checked = true;
            this.EpubButton.Location = new System.Drawing.Point(120, 104);
            this.EpubButton.Name = "EpubButton";
            this.EpubButton.Size = new System.Drawing.Size(80, 29);
            this.EpubButton.TabIndex = 7;
            this.EpubButton.TabStop = true;
            this.EpubButton.Text = "EPUB";
            this.EpubButton.UseVisualStyleBackColor = true;
            // 
            // NovelNoText
            // 
            this.NovelNoText.Location = new System.Drawing.Point(102, 67);
            this.NovelNoText.Name = "NovelNoText";
            this.NovelNoText.Size = new System.Drawing.Size(244, 31);
            this.NovelNoText.TabIndex = 6;
            // 
            // ExtensionLabel
            // 
            this.ExtensionLabel.AutoSize = true;
            this.ExtensionLabel.Location = new System.Drawing.Point(20, 106);
            this.ExtensionLabel.Name = "ExtensionLabel";
            this.ExtensionLabel.Size = new System.Drawing.Size(66, 25);
            this.ExtensionLabel.TabIndex = 1;
            this.ExtensionLabel.Text = "확장자";
            // 
            // NovelNoLable
            // 
            this.NovelNoLable.AutoSize = true;
            this.NovelNoLable.Location = new System.Drawing.Point(11, 70);
            this.NovelNoLable.Name = "NovelNoLable";
            this.NovelNoLable.Size = new System.Drawing.Size(84, 25);
            this.NovelNoLable.TabIndex = 0;
            this.NovelNoLable.Text = "소설번호";
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConsoleBox.Location = new System.Drawing.Point(458, 23);
            this.ConsoleBox.Multiline = true;
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.ReadOnly = true;
            this.ConsoleBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleBox.Size = new System.Drawing.Size(420, 343);
            this.ConsoleBox.TabIndex = 5;
            // 
            // ThreadLabel
            // 
            this.ThreadLabel.AutoSize = true;
            this.ThreadLabel.Location = new System.Drawing.Point(18, 186);
            this.ThreadLabel.Name = "ThreadLabel";
            this.ThreadLabel.Size = new System.Drawing.Size(108, 25);
            this.ThreadLabel.TabIndex = 11;
            this.ThreadLabel.Text = "스레드 개수";
            // 
            // ThreadNum
            // 
            this.ThreadNum.Location = new System.Drawing.Point(132, 183);
            this.ThreadNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ThreadNum.Name = "ThreadNum";
            this.ThreadNum.Size = new System.Drawing.Size(82, 31);
            this.ThreadNum.TabIndex = 12;
            this.ThreadNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // IntervalLabel
            // 
            this.IntervalLabel.AutoSize = true;
            this.IntervalLabel.Location = new System.Drawing.Point(268, 186);
            this.IntervalLabel.Name = "IntervalLabel";
            this.IntervalLabel.Size = new System.Drawing.Size(48, 25);
            this.IntervalLabel.TabIndex = 13;
            this.IntervalLabel.Text = "간격";
            // 
            // SecondLabel
            // 
            this.SecondLabel.AutoSize = true;
            this.SecondLabel.Location = new System.Drawing.Point(410, 186);
            this.SecondLabel.Name = "SecondLabel";
            this.SecondLabel.Size = new System.Drawing.Size(30, 25);
            this.SecondLabel.TabIndex = 14;
            this.SecondLabel.Text = "초";
            // 
            // IntervalNum
            // 
            this.IntervalNum.DecimalPlaces = 1;
            this.IntervalNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.IntervalNum.Location = new System.Drawing.Point(322, 183);
            this.IntervalNum.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.IntervalNum.Name = "IntervalNum";
            this.IntervalNum.Size = new System.Drawing.Size(82, 31);
            this.IntervalNum.TabIndex = 15;
            // 
            // FromCheck
            // 
            this.FromCheck.AutoSize = true;
            this.FromCheck.Location = new System.Drawing.Point(25, 35);
            this.FromCheck.Name = "FromCheck";
            this.FromCheck.Size = new System.Drawing.Size(22, 21);
            this.FromCheck.TabIndex = 11;
            this.FromCheck.UseVisualStyleBackColor = true;
            this.FromCheck.CheckedChanged += new System.EventHandler(this.FromCheck_CheckedChanged);
            // 
            // FromNum
            // 
            this.FromNum.Enabled = false;
            this.FromNum.Location = new System.Drawing.Point(53, 30);
            this.FromNum.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.FromNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FromNum.Name = "FromNum";
            this.FromNum.Size = new System.Drawing.Size(69, 31);
            this.FromNum.TabIndex = 12;
            this.FromNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FromLabel
            // 
            this.FromLabel.AutoSize = true;
            this.FromLabel.Enabled = false;
            this.FromLabel.Location = new System.Drawing.Point(128, 33);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(66, 25);
            this.FromLabel.TabIndex = 13;
            this.FromLabel.Text = "장부터";
            // 
            // ToLabel
            // 
            this.ToLabel.AutoSize = true;
            this.ToLabel.Enabled = false;
            this.ToLabel.Location = new System.Drawing.Point(352, 33);
            this.ToLabel.Name = "ToLabel";
            this.ToLabel.Size = new System.Drawing.Size(66, 25);
            this.ToLabel.TabIndex = 16;
            this.ToLabel.Text = "장까지";
            // 
            // ToNum
            // 
            this.ToNum.Enabled = false;
            this.ToNum.Location = new System.Drawing.Point(277, 30);
            this.ToNum.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.ToNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ToNum.Name = "ToNum";
            this.ToNum.Size = new System.Drawing.Size(69, 31);
            this.ToNum.TabIndex = 15;
            this.ToNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ToCheck
            // 
            this.ToCheck.AutoSize = true;
            this.ToCheck.Location = new System.Drawing.Point(249, 35);
            this.ToCheck.Name = "ToCheck";
            this.ToCheck.Size = new System.Drawing.Size(22, 21);
            this.ToCheck.TabIndex = 14;
            this.ToCheck.UseVisualStyleBackColor = true;
            this.ToCheck.CheckedChanged += new System.EventHandler(this.ToCheck_CheckedChanged);
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 387);
            this.Controls.Add(this.IntervalNum);
            this.Controls.Add(this.SecondLabel);
            this.Controls.Add(this.IntervalLabel);
            this.Controls.Add(this.ThreadNum);
            this.Controls.Add(this.ThreadLabel);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
            this.LoginGroup.ResumeLayout(false);
            this.LoginGroup.PerformLayout();
            this.DownloadGroup.ResumeLayout(false);
            this.DownloadGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToNum)).EndInit();
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
        private System.Windows.Forms.Label ThreadLabel;
        private System.Windows.Forms.NumericUpDown ThreadNum;
        private System.Windows.Forms.Label IntervalLabel;
        private System.Windows.Forms.Label SecondLabel;
        private System.Windows.Forms.NumericUpDown IntervalNum;
        private System.Windows.Forms.NumericUpDown FromNum;
        private System.Windows.Forms.CheckBox FromCheck;
        private System.Windows.Forms.Label FromLabel;
        private System.Windows.Forms.Label ToLabel;
        private System.Windows.Forms.NumericUpDown ToNum;
        private System.Windows.Forms.CheckBox ToCheck;
    }
}

