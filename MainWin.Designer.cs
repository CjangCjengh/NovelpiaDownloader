namespace NovelpiaDownloader
{
    partial class MainWin
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
            this.StopOnErrorCheck = new System.Windows.Forms.CheckBox();
            this.IncludeNovelNoCheck = new System.Windows.Forms.CheckBox();
            this.IncludeChapterRangeCheck = new System.Windows.Forms.CheckBox();
            this.DownloadImageCheck = new System.Windows.Forms.CheckBox();
            this.CompressCheck = new System.Windows.Forms.CheckBox();
            this.RetryNum = new System.Windows.Forms.NumericUpDown();
            this.RetryLabel = new System.Windows.Forms.Label();
            this.KeepHtmlCheck = new System.Windows.Forms.CheckBox();
            this.RemoveBlankCheck = new System.Windows.Forms.CheckBox();
            this.OutputDirButton = new System.Windows.Forms.Button();
            this.OutputDirText = new System.Windows.Forms.TextBox();
            this.OutputDirLabel = new System.Windows.Forms.Label();
            this.NoticeCheck = new System.Windows.Forms.CheckBox();
            this.ToLabel = new System.Windows.Forms.Label();
            this.ToNum = new System.Windows.Forms.NumericUpDown();
            this.ToCheck = new System.Windows.Forms.CheckBox();
            this.FromLabel = new System.Windows.Forms.Label();
            this.FromNum = new System.Windows.Forms.NumericUpDown();
            this.FromCheck = new System.Windows.Forms.CheckBox();
            this.DownloadButton = new System.Windows.Forms.Button();
            this.AddToListButton = new System.Windows.Forms.Button();
            this.DownloadList = new System.Windows.Forms.ListBox();
            this.QueueDownloadButton = new System.Windows.Forms.Button();
            this.QueueDeleteAllButton = new System.Windows.Forms.Button();
            this.QueueDeleteSelectedButton = new System.Windows.Forms.Button();
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
            this.FontLabel = new System.Windows.Forms.Label();
            this.FontButton = new System.Windows.Forms.Button();
            this.FontBox = new System.Windows.Forms.TextBox();
            this.LanguageBox = new System.Windows.Forms.ComboBox();
            this.LoginGroup.SuspendLayout();
            this.DownloadGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RetryNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalNum)).BeginInit();
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
            this.LoginGroup.Location = new System.Drawing.Point(12, 51);
            this.LoginGroup.Name = "LoginGroup";
            this.LoginGroup.Size = new System.Drawing.Size(439, 159);
            this.LoginGroup.TabIndex = 1;
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
            this.LoginkeyText.Location = new System.Drawing.Point(110, 107);
            this.LoginkeyText.Name = "LoginkeyText";
            this.LoginkeyText.Size = new System.Drawing.Size(237, 31);
            this.LoginkeyText.TabIndex = 5;
            // 
            // PasswordText
            // 
            this.PasswordText.Location = new System.Drawing.Point(110, 67);
            this.PasswordText.Name = "PasswordText";
            this.PasswordText.Size = new System.Drawing.Size(237, 31);
            this.PasswordText.TabIndex = 4;
            // 
            // EmailText
            // 
            this.EmailText.Location = new System.Drawing.Point(110, 30);
            this.EmailText.Name = "EmailText";
            this.EmailText.Size = new System.Drawing.Size(237, 31);
            this.EmailText.TabIndex = 3;
            // 
            // LoginkeyLabel
            // 
            this.LoginkeyLabel.AutoSize = true;
            this.LoginkeyLabel.Location = new System.Drawing.Point(12, 110);
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
            this.EmailLabel.Location = new System.Drawing.Point(12, 33);
            this.EmailLabel.Name = "EmailLabel";
            this.EmailLabel.Size = new System.Drawing.Size(66, 25);
            this.EmailLabel.TabIndex = 0;
            this.EmailLabel.Text = "이메일";
            // 
            // DownloadGroup
            // 
            this.DownloadGroup.Controls.Add(this.StopOnErrorCheck);
            this.DownloadGroup.Controls.Add(this.IncludeNovelNoCheck);
            this.DownloadGroup.Controls.Add(this.IncludeChapterRangeCheck);
            this.DownloadGroup.Controls.Add(this.DownloadImageCheck);
            this.DownloadGroup.Controls.Add(this.CompressCheck);
            this.DownloadGroup.Controls.Add(this.KeepHtmlCheck);
            this.DownloadGroup.Controls.Add(this.RemoveBlankCheck);
            this.DownloadGroup.Controls.Add(this.OutputDirButton);
            this.DownloadGroup.Controls.Add(this.OutputDirText);
            this.DownloadGroup.Controls.Add(this.OutputDirLabel);
            this.DownloadGroup.Controls.Add(this.NoticeCheck);
            this.DownloadGroup.Controls.Add(this.ToLabel);
            this.DownloadGroup.Controls.Add(this.ToNum);
            this.DownloadGroup.Controls.Add(this.ToCheck);
            this.DownloadGroup.Controls.Add(this.FromLabel);
            this.DownloadGroup.Controls.Add(this.FromNum);
            this.DownloadGroup.Controls.Add(this.FromCheck);
            this.DownloadGroup.Controls.Add(this.AddToListButton);
            this.DownloadGroup.Controls.Add(this.DownloadButton);
            this.DownloadGroup.Controls.Add(this.TxtButton);
            this.DownloadGroup.Controls.Add(this.EpubButton);
            this.DownloadGroup.Controls.Add(this.NovelNoText);
            this.DownloadGroup.Controls.Add(this.ExtensionLabel);
            this.DownloadGroup.Controls.Add(this.NovelNoLable);
            this.DownloadGroup.Location = new System.Drawing.Point(12, 255);
            this.DownloadGroup.Name = "DownloadGroup";
            this.DownloadGroup.Size = new System.Drawing.Size(439, 318);
            this.DownloadGroup.TabIndex = 2;
            this.DownloadGroup.TabStop = false;
            this.DownloadGroup.Text = "다운로드";
            // 
            // StopOnErrorCheck
            // 
            this.StopOnErrorCheck.AutoSize = true;
            this.StopOnErrorCheck.Location = new System.Drawing.Point(220, 126);
            this.StopOnErrorCheck.Name = "StopOnErrorCheck";
            this.StopOnErrorCheck.Size = new System.Drawing.Size(140, 29);
            this.StopOnErrorCheck.TabIndex = 30;
            this.StopOnErrorCheck.Text = "오류 시 중단";
            this.StopOnErrorCheck.UseVisualStyleBackColor = true;
            // 
            // IncludeNovelNoCheck
            // 
            this.IncludeNovelNoCheck.AutoSize = true;
            this.IncludeNovelNoCheck.Location = new System.Drawing.Point(15, 155);
            this.IncludeNovelNoCheck.Name = "IncludeNovelNoCheck";
            this.IncludeNovelNoCheck.Size = new System.Drawing.Size(170, 29);
            this.IncludeNovelNoCheck.TabIndex = 31;
            this.IncludeNovelNoCheck.Text = "파일명에 소설 번호 추가";
            this.IncludeNovelNoCheck.UseVisualStyleBackColor = true;
            // 
            // IncludeChapterRangeCheck
            // 
            this.IncludeChapterRangeCheck.AutoSize = true;
            this.IncludeChapterRangeCheck.Location = new System.Drawing.Point(220, 155);
            this.IncludeChapterRangeCheck.Name = "IncludeChapterRangeCheck";
            this.IncludeChapterRangeCheck.Size = new System.Drawing.Size(170, 29);
            this.IncludeChapterRangeCheck.TabIndex = 32;
            this.IncludeChapterRangeCheck.Text = "파일명에 화수 범위 추가";
            this.IncludeChapterRangeCheck.UseVisualStyleBackColor = true;
            // 
            // DownloadImageCheck
            // 
            this.DownloadImageCheck.AutoSize = true;
            this.DownloadImageCheck.Checked = true;
            this.DownloadImageCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.DownloadImageCheck.Location = new System.Drawing.Point(15, 126);
            this.DownloadImageCheck.Name = "DownloadImageCheck";
            this.DownloadImageCheck.Size = new System.Drawing.Size(170, 29);
            this.DownloadImageCheck.TabIndex = 27;
            this.DownloadImageCheck.Text = "이미지 다운로드";
            this.DownloadImageCheck.UseVisualStyleBackColor = true;
            // 
            // CompressCheck
            // 
            this.CompressCheck.AutoSize = true;
            this.CompressCheck.Checked = true;
            this.CompressCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CompressCheck.Location = new System.Drawing.Point(220, 97);
            this.CompressCheck.Name = "CompressCheck";
            this.CompressCheck.Size = new System.Drawing.Size(123, 29);
            this.CompressCheck.TabIndex = 26;
            this.CompressCheck.Text = "EPUB 압축";
            this.CompressCheck.UseVisualStyleBackColor = true;
            // 
            // RetryNum
            // 
            this.RetryNum.Location = new System.Drawing.Point(95, 155);
            this.RetryNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.RetryNum.Name = "RetryNum";
            this.RetryNum.Size = new System.Drawing.Size(70, 31);
            this.RetryNum.TabIndex = 25;
            this.RetryNum.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // RetryLabel
            // 
            this.RetryLabel.AutoSize = true;
            this.RetryLabel.Location = new System.Drawing.Point(15, 158);
            this.RetryLabel.Name = "RetryLabel";
            this.RetryLabel.Size = new System.Drawing.Size(66, 25);
            this.RetryLabel.TabIndex = 24;
            this.RetryLabel.Text = "재시도";
            // 
            // KeepHtmlCheck
            // 
            this.KeepHtmlCheck.AutoSize = true;
            this.KeepHtmlCheck.Checked = true;
            this.KeepHtmlCheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.KeepHtmlCheck.Location = new System.Drawing.Point(15, 97);
            this.KeepHtmlCheck.Name = "KeepHtmlCheck";
            this.KeepHtmlCheck.Size = new System.Drawing.Size(129, 29);
            this.KeepHtmlCheck.TabIndex = 23;
            this.KeepHtmlCheck.Text = "HTML 유지";
            this.KeepHtmlCheck.UseVisualStyleBackColor = true;
            // 
            // RemoveBlankCheck
            // 
            this.RemoveBlankCheck.AutoSize = true;
            this.RemoveBlankCheck.Location = new System.Drawing.Point(220, 68);
            this.RemoveBlankCheck.Name = "RemoveBlankCheck";
            this.RemoveBlankCheck.Size = new System.Drawing.Size(122, 29);
            this.RemoveBlankCheck.TabIndex = 22;
            this.RemoveBlankCheck.Text = "빈 줄 제거";
            this.RemoveBlankCheck.UseVisualStyleBackColor = true;
            // 
            // OutputDirButton
            // 
            this.OutputDirButton.Location = new System.Drawing.Point(353, 188);
            this.OutputDirButton.Name = "OutputDirButton";
            this.OutputDirButton.Size = new System.Drawing.Size(75, 32);
            this.OutputDirButton.TabIndex = 21;
            this.OutputDirButton.Text = "찾아보기";
            this.OutputDirButton.UseVisualStyleBackColor = true;
            this.OutputDirButton.Click += new System.EventHandler(this.OutputDirButton_Click);
            // 
            // OutputDirText
            // 
            this.OutputDirText.Location = new System.Drawing.Point(110, 190);
            this.OutputDirText.Name = "OutputDirText";
            this.OutputDirText.Size = new System.Drawing.Size(237, 31);
            this.OutputDirText.TabIndex = 20;
            // 
            // OutputDirLabel
            // 
            this.OutputDirLabel.AutoSize = true;
            this.OutputDirLabel.Location = new System.Drawing.Point(15, 193);
            this.OutputDirLabel.Name = "OutputDirLabel";
            this.OutputDirLabel.Size = new System.Drawing.Size(90, 25);
            this.OutputDirLabel.TabIndex = 19;
            this.OutputDirLabel.Text = "저장 경로";
            // 
            // NoticeCheck
            // 
            this.NoticeCheck.AutoSize = true;
            this.NoticeCheck.Location = new System.Drawing.Point(15, 68);
            this.NoticeCheck.Name = "NoticeCheck";
            this.NoticeCheck.Size = new System.Drawing.Size(116, 29);
            this.NoticeCheck.TabIndex = 18;
            this.NoticeCheck.Text = "공지 포함";
            this.NoticeCheck.UseVisualStyleBackColor = true;
            // 
            // ToLabel
            // 
            this.ToLabel.AutoSize = true;
            this.ToLabel.Enabled = false;
            this.ToLabel.Location = new System.Drawing.Point(352, 33);
            this.ToLabel.Name = "ToLabel";
            this.ToLabel.Size = new System.Drawing.Size(66, 25);
            this.ToLabel.TabIndex = 16;
            this.ToLabel.Text = "화까지";
            this.ToLabel.Visible = false;
            // 
            // ToNum
            // 
            this.ToNum.Enabled = false;
            this.ToNum.Location = new System.Drawing.Point(355, 30);
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
            this.ToNum.Size = new System.Drawing.Size(70, 31);
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
            this.ToCheck.Location = new System.Drawing.Point(220, 33);
            this.ToCheck.Name = "ToCheck";
            this.ToCheck.Size = new System.Drawing.Size(92, 29);
            this.ToCheck.TabIndex = 14;
            this.ToCheck.Text = "화까지";
            this.ToCheck.UseVisualStyleBackColor = true;
            this.ToCheck.CheckedChanged += new System.EventHandler(this.ToCheck_CheckedChanged);
            // 
            // FromLabel
            // 
            this.FromLabel.AutoSize = true;
            this.FromLabel.Enabled = false;
            this.FromLabel.Location = new System.Drawing.Point(128, 33);
            this.FromLabel.Name = "FromLabel";
            this.FromLabel.Size = new System.Drawing.Size(66, 25);
            this.FromLabel.TabIndex = 13;
            this.FromLabel.Text = "화부터";
            this.FromLabel.Visible = false;
            // 
            // FromNum
            // 
            this.FromNum.Enabled = false;
            this.FromNum.Location = new System.Drawing.Point(150, 30);
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
            this.FromNum.Size = new System.Drawing.Size(70, 31);
            this.FromNum.TabIndex = 12;
            this.FromNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // FromCheck
            // 
            this.FromCheck.AutoSize = true;
            this.FromCheck.Location = new System.Drawing.Point(15, 33);
            this.FromCheck.Name = "FromCheck";
            this.FromCheck.Size = new System.Drawing.Size(92, 29);
            this.FromCheck.TabIndex = 11;
            this.FromCheck.Text = "화부터";
            this.FromCheck.UseVisualStyleBackColor = true;
            this.FromCheck.CheckedChanged += new System.EventHandler(this.FromCheck_CheckedChanged);
            // 
            // DownloadButton
            // 
            this.DownloadButton.Location = new System.Drawing.Point(347, 268);
            this.DownloadButton.Name = "DownloadButton";
            this.DownloadButton.Size = new System.Drawing.Size(80, 36);
            this.DownloadButton.TabIndex = 10;
            this.DownloadButton.Text = "다운로드";
            this.DownloadButton.UseVisualStyleBackColor = true;
            this.DownloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // TxtButton
            // 
            this.TxtButton.AutoSize = true;
            this.TxtButton.Location = new System.Drawing.Point(180, 270);
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
            this.EpubButton.Location = new System.Drawing.Point(85, 270);
            this.EpubButton.Name = "EpubButton";
            this.EpubButton.Size = new System.Drawing.Size(80, 29);
            this.EpubButton.TabIndex = 7;
            this.EpubButton.TabStop = true;
            this.EpubButton.Text = "EPUB";
            this.EpubButton.UseVisualStyleBackColor = true;
            // 
            // NovelNoText
            // 
            this.NovelNoText.Location = new System.Drawing.Point(110, 232);
            this.NovelNoText.Name = "NovelNoText";
            this.NovelNoText.Size = new System.Drawing.Size(317, 31);
            this.NovelNoText.TabIndex = 6;
            // 
            // ExtensionLabel
            // 
            this.ExtensionLabel.AutoSize = true;
            this.ExtensionLabel.Location = new System.Drawing.Point(15, 273);
            this.ExtensionLabel.Name = "ExtensionLabel";
            this.ExtensionLabel.Size = new System.Drawing.Size(48, 25);
            this.ExtensionLabel.TabIndex = 1;
            this.ExtensionLabel.Text = "형식";
            // 
            // NovelNoLable
            // 
            this.NovelNoLable.AutoSize = true;
            this.NovelNoLable.Location = new System.Drawing.Point(15, 235);
            this.NovelNoLable.Name = "NovelNoLable";
            this.NovelNoLable.Size = new System.Drawing.Size(90, 25);
            this.NovelNoLable.TabIndex = 0;
            this.NovelNoLable.Text = "소설 번호";
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsoleBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ConsoleBox.Location = new System.Drawing.Point(458, 12);
            this.ConsoleBox.Multiline = true;
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.ReadOnly = true;
            this.ConsoleBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ConsoleBox.Size = new System.Drawing.Size(420, 607);
            this.ConsoleBox.TabIndex = 5;
            // 
            // DownloadList
            // 
            this.DownloadList.FormattingEnabled = true;
            this.DownloadList.IntegralHeight = false;
            this.DownloadList.ItemHeight = 20;
            this.DownloadList.Location = new System.Drawing.Point(458, 12);
            this.DownloadList.Name = "DownloadList";
            this.DownloadList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.DownloadList.Size = new System.Drawing.Size(420, 280);
            this.DownloadList.TabIndex = 50;
            // 
            // QueueDeleteAllButton
            // 
            this.QueueDeleteAllButton.Location = new System.Drawing.Point(458, 12);
            this.QueueDeleteAllButton.Name = "QueueDeleteAllButton";
            this.QueueDeleteAllButton.Size = new System.Drawing.Size(100, 32);
            this.QueueDeleteAllButton.TabIndex = 51;
            this.QueueDeleteAllButton.Text = "전체 삭제";
            this.QueueDeleteAllButton.UseVisualStyleBackColor = true;
            this.QueueDeleteAllButton.Click += new System.EventHandler(this.QueueDeleteAllButton_Click);
            // 
            // QueueDeleteSelectedButton
            // 
            this.QueueDeleteSelectedButton.Location = new System.Drawing.Point(564, 12);
            this.QueueDeleteSelectedButton.Name = "QueueDeleteSelectedButton";
            this.QueueDeleteSelectedButton.Size = new System.Drawing.Size(100, 32);
            this.QueueDeleteSelectedButton.TabIndex = 52;
            this.QueueDeleteSelectedButton.Text = "선택 삭제";
            this.QueueDeleteSelectedButton.UseVisualStyleBackColor = true;
            this.QueueDeleteSelectedButton.Click += new System.EventHandler(this.QueueDeleteSelectedButton_Click);
            // 
            // QueueDownloadButton
            // 
            this.QueueDownloadButton.Location = new System.Drawing.Point(670, 12);
            this.QueueDownloadButton.Name = "QueueDownloadButton";
            this.QueueDownloadButton.Size = new System.Drawing.Size(100, 32);
            this.QueueDownloadButton.TabIndex = 53;
            this.QueueDownloadButton.Text = "다운로드";
            this.QueueDownloadButton.UseVisualStyleBackColor = true;
            this.QueueDownloadButton.Click += new System.EventHandler(this.QueueDownloadButton_Click);
            // 
            // AddToListButton
            // 
            this.AddToListButton.Location = new System.Drawing.Point(244, 270);
            this.AddToListButton.Name = "AddToListButton";
            this.AddToListButton.Size = new System.Drawing.Size(120, 36);
            this.AddToListButton.TabIndex = 54;
            this.AddToListButton.Text = "목록에 추가";
            this.AddToListButton.UseVisualStyleBackColor = true;
            this.AddToListButton.Click += new System.EventHandler(this.AddToListButton_Click);
            // 
            // ThreadLabel
            // 
            this.ThreadLabel.AutoSize = true;
            this.ThreadLabel.Location = new System.Drawing.Point(18, 219);
            this.ThreadLabel.Name = "ThreadLabel";
            this.ThreadLabel.Size = new System.Drawing.Size(66, 25);
            this.ThreadLabel.TabIndex = 11;
            this.ThreadLabel.Text = "스레드";
            // 
            // ThreadNum
            // 
            this.ThreadNum.Location = new System.Drawing.Point(132, 216);
            this.ThreadNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ThreadNum.Name = "ThreadNum";
            this.ThreadNum.Size = new System.Drawing.Size(70, 31);
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
            this.IntervalLabel.Location = new System.Drawing.Point(218, 219);
            this.IntervalLabel.Name = "IntervalLabel";
            this.IntervalLabel.Size = new System.Drawing.Size(48, 25);
            this.IntervalLabel.TabIndex = 13;
            this.IntervalLabel.Text = "간격 (초)";
            // 
            // SecondLabel
            // 
            this.SecondLabel.AutoSize = true;
            this.SecondLabel.Location = new System.Drawing.Point(360, 219);
            this.SecondLabel.Name = "SecondLabel";
            this.SecondLabel.Size = new System.Drawing.Size(30, 25);
            this.SecondLabel.TabIndex = 14;
            this.SecondLabel.Text = "";
            // 
            // IntervalNum
            // 
            this.IntervalNum.DecimalPlaces = 1;
            this.IntervalNum.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.IntervalNum.Location = new System.Drawing.Point(280, 216);
            this.IntervalNum.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.IntervalNum.Name = "IntervalNum";
            this.IntervalNum.Size = new System.Drawing.Size(70, 31);
            this.IntervalNum.TabIndex = 15;
            // 
            // FontLabel
            // 
            this.FontLabel.AutoSize = true;
            this.FontLabel.Location = new System.Drawing.Point(19, 586);
            this.FontLabel.Name = "FontLabel";
            this.FontLabel.Size = new System.Drawing.Size(90, 25);
            this.FontLabel.TabIndex = 16;
            this.FontLabel.Text = "글자 치환";
            // 
            // FontButton
            // 
            this.FontButton.Location = new System.Drawing.Point(376, 583);
            this.FontButton.Name = "FontButton";
            this.FontButton.Size = new System.Drawing.Size(75, 36);
            this.FontButton.TabIndex = 9;
            this.FontButton.Text = "열기";
            this.FontButton.UseVisualStyleBackColor = true;
            this.FontButton.Click += new System.EventHandler(this.FontButton_Click);
            // 
            // FontBox
            // 
            this.FontBox.Location = new System.Drawing.Point(115, 583);
            this.FontBox.Name = "FontBox";
            this.FontBox.Size = new System.Drawing.Size(255, 31);
            this.FontBox.TabIndex = 8;
            this.FontBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FontBox_KeyPress);
            // 
            // LanguageBox
            // 
            this.LanguageBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LanguageBox.FormattingEnabled = true;
            this.LanguageBox.Items.AddRange(new object[] {
            "한국어",
            "English",
            "简体中文",
            "繁體中文",
            "日本語",
            "Tiếng Việt",
            "ภาษาไทย",
            "Bahasa Indonesia"});
            this.LanguageBox.Location = new System.Drawing.Point(12, 12);
            this.LanguageBox.Name = "LanguageBox";
            this.LanguageBox.Size = new System.Drawing.Size(439, 33);
            this.LanguageBox.TabIndex = 0;
            this.LanguageBox.SelectedIndexChanged += new System.EventHandler(this.LanguageBox_SelectedIndexChanged);
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 631);
            this.Controls.Add(this.LanguageBox);
            this.Controls.Add(this.QueueDeleteAllButton);
            this.Controls.Add(this.QueueDeleteSelectedButton);
            this.Controls.Add(this.QueueDownloadButton);
            this.Controls.Add(this.DownloadList);
            this.Controls.Add(this.RetryNum);
            this.Controls.Add(this.RetryLabel);
            this.Controls.Add(this.FontButton);
            this.Controls.Add(this.FontLabel);
            this.Controls.Add(this.FontBox);
            this.Controls.Add(this.IntervalNum);
            this.Controls.Add(this.SecondLabel);
            this.Controls.Add(this.IntervalLabel);
            this.Controls.Add(this.ThreadNum);
            this.Controls.Add(this.ThreadLabel);
            this.Controls.Add(this.ConsoleBox);
            this.Controls.Add(this.DownloadGroup);
            this.Controls.Add(this.LoginGroup);
            this.Font = new System.Drawing.Font("Malgun Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(906, 681);
            this.Name = "MainWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NovelpiaDownloader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWin_FormClosed);
            this.LoginGroup.ResumeLayout(false);
            this.LoginGroup.PerformLayout();
            this.DownloadGroup.ResumeLayout(false);
            this.DownloadGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RetryNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ToNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FromNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThreadNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox LoginGroup;
        internal System.Windows.Forms.Button LoginButton2;
        internal System.Windows.Forms.Button LoginButton1;
        internal System.Windows.Forms.TextBox LoginkeyText;
        internal System.Windows.Forms.TextBox PasswordText;
        internal System.Windows.Forms.TextBox EmailText;
        internal System.Windows.Forms.Label LoginkeyLabel;
        internal System.Windows.Forms.Label PasswordLabel;
        internal System.Windows.Forms.Label EmailLabel;
        internal System.Windows.Forms.GroupBox DownloadGroup;
        internal System.Windows.Forms.RadioButton TxtButton;
        internal System.Windows.Forms.RadioButton EpubButton;
        internal System.Windows.Forms.TextBox NovelNoText;
        internal System.Windows.Forms.Label ExtensionLabel;
        internal System.Windows.Forms.Label NovelNoLable;
        internal System.Windows.Forms.Button DownloadButton;
        internal System.Windows.Forms.Button AddToListButton;
        internal System.Windows.Forms.ListBox DownloadList;
        internal System.Windows.Forms.Button QueueDownloadButton;
        internal System.Windows.Forms.Button QueueDeleteAllButton;
        internal System.Windows.Forms.Button QueueDeleteSelectedButton;
        internal System.Windows.Forms.TextBox ConsoleBox;
        internal System.Windows.Forms.Label ThreadLabel;
        internal System.Windows.Forms.NumericUpDown ThreadNum;
        internal System.Windows.Forms.Label IntervalLabel;
        internal System.Windows.Forms.Label SecondLabel;
        internal System.Windows.Forms.NumericUpDown IntervalNum;
        internal System.Windows.Forms.NumericUpDown FromNum;
        internal System.Windows.Forms.CheckBox FromCheck;
        internal System.Windows.Forms.Label FromLabel;
        internal System.Windows.Forms.Label ToLabel;
        internal System.Windows.Forms.NumericUpDown ToNum;
        internal System.Windows.Forms.CheckBox ToCheck;
        internal System.Windows.Forms.Label FontLabel;
        internal System.Windows.Forms.Button FontButton;
        internal System.Windows.Forms.TextBox FontBox;
        internal System.Windows.Forms.ComboBox LanguageBox;
        internal System.Windows.Forms.CheckBox NoticeCheck;
        internal System.Windows.Forms.Label OutputDirLabel;
        internal System.Windows.Forms.TextBox OutputDirText;
        internal System.Windows.Forms.Button OutputDirButton;
        internal System.Windows.Forms.CheckBox RemoveBlankCheck;
        internal System.Windows.Forms.CheckBox KeepHtmlCheck;
        internal System.Windows.Forms.Label RetryLabel;
        internal System.Windows.Forms.NumericUpDown RetryNum;
        internal System.Windows.Forms.CheckBox CompressCheck;
        internal System.Windows.Forms.CheckBox DownloadImageCheck;
        internal System.Windows.Forms.CheckBox StopOnErrorCheck;
        internal System.Windows.Forms.CheckBox IncludeNovelNoCheck;
        internal System.Windows.Forms.CheckBox IncludeChapterRangeCheck;
    }
}
