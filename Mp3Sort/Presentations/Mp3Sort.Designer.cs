namespace Mp3Sort.Presentations
{
    partial class Mp3Sort
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mp3Sort));
            this.pnlScreen = new NexFx.Controls.ExPanel();
            this.pnlBody = new NexFx.Controls.ExPanel();
            this.cbLog = new NexFx.Controls.ExCheckBox();
            this.cbCreateExtensionDirectory = new NexFx.Controls.ExCheckBox();
            this.cbConfirmOverWrite = new NexFx.Controls.ExCheckBox();
            this.btnToggleProgress = new NexFx.Controls.ExButton();
            this.gbType = new NexFx.Controls.ExGroupBox();
            this.cbFlac = new NexFx.Controls.ExCheckBox();
            this.cbAac = new NexFx.Controls.ExCheckBox();
            this.cbMp3 = new NexFx.Controls.ExCheckBox();
            this.btnExecute = new NexFx.Controls.ExButton();
            this.gbBehavior = new NexFx.Controls.ExGroupBox();
            this.rbCopy = new NexFx.Controls.ExRadioButton();
            this.rbMove = new NexFx.Controls.ExRadioButton();
            this.btnDirectory = new NexFx.Controls.ExButton();
            this.lblDirectory = new System.Windows.Forms.Label();
            this.txtDirectory = new NexFx.Controls.ExTextBox();
            this.pnlProgress = new NexFx.Controls.ExPanel();
            this.lblFileName = new System.Windows.Forms.Label();
            this.lblProgressCaption = new System.Windows.Forms.Label();
            this.lblTotalProcessCaption = new System.Windows.Forms.Label();
            this.lblProgressedProcessCaption = new System.Windows.Forms.Label();
            this.lblDivide = new System.Windows.Forms.Label();
            this.lblTotalProcess = new System.Windows.Forms.Label();
            this.lblProgressedProcess = new System.Windows.Forms.Label();
            this.pgProcess = new System.Windows.Forms.ProgressBar();
            this.pnlScreen.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.gbType.SuspendLayout();
            this.gbBehavior.SuspendLayout();
            this.pnlProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlScreen
            // 
            this.pnlScreen.BackColor = System.Drawing.Color.Transparent;
            this.pnlScreen.Controls.Add(this.pnlBody);
            this.pnlScreen.Controls.Add(this.pnlProgress);
            this.pnlScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlScreen.Location = new System.Drawing.Point(0, 0);
            this.pnlScreen.Name = "pnlScreen";
            this.pnlScreen.Size = new System.Drawing.Size(510, 207);
            this.pnlScreen.TabIndex = 0;
            // 
            // pnlBody
            // 
            this.pnlBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBody.BackColor = System.Drawing.Color.Transparent;
            this.pnlBody.Controls.Add(this.cbLog);
            this.pnlBody.Controls.Add(this.cbCreateExtensionDirectory);
            this.pnlBody.Controls.Add(this.cbConfirmOverWrite);
            this.pnlBody.Controls.Add(this.btnToggleProgress);
            this.pnlBody.Controls.Add(this.gbType);
            this.pnlBody.Controls.Add(this.btnExecute);
            this.pnlBody.Controls.Add(this.gbBehavior);
            this.pnlBody.Controls.Add(this.btnDirectory);
            this.pnlBody.Controls.Add(this.txtDirectory);
            this.pnlBody.Controls.Add(this.lblDirectory);
            this.pnlBody.Location = new System.Drawing.Point(0, 0);
            this.pnlBody.Margin = new System.Windows.Forms.Padding(0);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(510, 152);
            this.pnlBody.TabIndex = 0;
            // 
            // cbLog
            // 
            this.cbLog.CaptionControl = null;
            this.cbLog.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.cbLog.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.cbLog.Key = "Log";
            this.cbLog.Location = new System.Drawing.Point(347, 49);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(115, 24);
            this.cbLog.TabIndex = 4;
            this.cbLog.Text = "ログを出力する";
            this.cbLog.UseVisualStyleBackColor = true;
            // 
            // cbCreateExtensionDirectory
            // 
            this.cbCreateExtensionDirectory.CaptionControl = null;
            this.cbCreateExtensionDirectory.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.cbCreateExtensionDirectory.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.cbCreateExtensionDirectory.Key = "CreateExtensionDirectory";
            this.cbCreateExtensionDirectory.Location = new System.Drawing.Point(347, 5);
            this.cbCreateExtensionDirectory.Name = "cbCreateExtensionDirectory";
            this.cbCreateExtensionDirectory.Size = new System.Drawing.Size(151, 24);
            this.cbCreateExtensionDirectory.TabIndex = 2;
            this.cbCreateExtensionDirectory.Text = "拡張子別にフォルダを作成";
            this.cbCreateExtensionDirectory.UseVisualStyleBackColor = true;
            // 
            // cbConfirmOverWrite
            // 
            this.cbConfirmOverWrite.CaptionControl = null;
            this.cbConfirmOverWrite.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.cbConfirmOverWrite.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.cbConfirmOverWrite.Key = "ConfirmOverWrite";
            this.cbConfirmOverWrite.Location = new System.Drawing.Point(347, 27);
            this.cbConfirmOverWrite.Name = "cbConfirmOverWrite";
            this.cbConfirmOverWrite.Size = new System.Drawing.Size(115, 24);
            this.cbConfirmOverWrite.TabIndex = 3;
            this.cbConfirmOverWrite.Text = "上書き確認をする";
            this.cbConfirmOverWrite.UseVisualStyleBackColor = true;
            // 
            // btnToggleProgress
            // 
            this.btnToggleProgress.CaptionControl = null;
            this.btnToggleProgress.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.btnToggleProgress.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.btnToggleProgress.Location = new System.Drawing.Point(468, 114);
            this.btnToggleProgress.Name = "btnToggleProgress";
            this.btnToggleProgress.Size = new System.Drawing.Size(30, 30);
            this.btnToggleProgress.TabIndex = 9;
            this.btnToggleProgress.Text = "?";
            this.btnToggleProgress.UseVisualStyleBackColor = true;
            this.btnToggleProgress.Click += new System.EventHandler(this.btnToggleProgress_Click);
            // 
            // gbType
            // 
            this.gbType.Controls.Add(this.cbFlac);
            this.gbType.Controls.Add(this.cbAac);
            this.gbType.Controls.Add(this.cbMp3);
            this.gbType.Location = new System.Drawing.Point(150, 16);
            this.gbType.Name = "gbType";
            this.gbType.Size = new System.Drawing.Size(184, 52);
            this.gbType.TabIndex = 1;
            this.gbType.TabStop = false;
            this.gbType.Text = "種類";
            // 
            // cbFlac
            // 
            this.cbFlac.CaptionControl = null;
            this.cbFlac.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.cbFlac.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.cbFlac.Key = "Flac";
            this.cbFlac.Location = new System.Drawing.Point(118, 22);
            this.cbFlac.Name = "cbFlac";
            this.cbFlac.Size = new System.Drawing.Size(60, 24);
            this.cbFlac.TabIndex = 2;
            this.cbFlac.Text = "FLAC";
            this.cbFlac.UseVisualStyleBackColor = true;
            // 
            // cbAac
            // 
            this.cbAac.CaptionControl = null;
            this.cbAac.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.cbAac.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.cbAac.Key = "Aac";
            this.cbAac.Location = new System.Drawing.Point(62, 22);
            this.cbAac.Name = "cbAac";
            this.cbAac.Size = new System.Drawing.Size(50, 24);
            this.cbAac.TabIndex = 1;
            this.cbAac.Text = "AAC";
            this.cbAac.UseVisualStyleBackColor = true;
            // 
            // cbMp3
            // 
            this.cbMp3.CaptionControl = null;
            this.cbMp3.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.cbMp3.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.cbMp3.Key = "Mp3";
            this.cbMp3.Location = new System.Drawing.Point(6, 22);
            this.cbMp3.Name = "cbMp3";
            this.cbMp3.Size = new System.Drawing.Size(50, 24);
            this.cbMp3.TabIndex = 0;
            this.cbMp3.Text = "MP3";
            this.cbMp3.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            this.btnExecute.CaptionControl = null;
            this.btnExecute.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.btnExecute.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.btnExecute.Location = new System.Drawing.Point(12, 114);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(450, 30);
            this.btnExecute.TabIndex = 8;
            this.btnExecute.Text = "実行";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // gbBehavior
            // 
            this.gbBehavior.Controls.Add(this.rbCopy);
            this.gbBehavior.Controls.Add(this.rbMove);
            this.gbBehavior.Location = new System.Drawing.Point(12, 15);
            this.gbBehavior.Name = "gbBehavior";
            this.gbBehavior.Size = new System.Drawing.Size(132, 52);
            this.gbBehavior.TabIndex = 0;
            this.gbBehavior.TabStop = false;
            this.gbBehavior.Text = "動作";
            // 
            // rbCopy
            // 
            this.rbCopy.CaptionControl = null;
            this.rbCopy.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.rbCopy.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.rbCopy.Key = "Copy";
            this.rbCopy.Location = new System.Drawing.Point(68, 22);
            this.rbCopy.Name = "rbCopy";
            this.rbCopy.Size = new System.Drawing.Size(55, 24);
            this.rbCopy.TabIndex = 1;
            this.rbCopy.TabStop = true;
            this.rbCopy.Text = "コピー";
            this.rbCopy.UseVisualStyleBackColor = true;
            // 
            // rbMove
            // 
            this.rbMove.CaptionControl = null;
            this.rbMove.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.rbMove.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.rbMove.Key = "Move";
            this.rbMove.Location = new System.Drawing.Point(7, 22);
            this.rbMove.Name = "rbMove";
            this.rbMove.Size = new System.Drawing.Size(50, 24);
            this.rbMove.TabIndex = 0;
            this.rbMove.TabStop = true;
            this.rbMove.Text = "移動";
            this.rbMove.UseVisualStyleBackColor = true;
            // 
            // btnDirectory
            // 
            this.btnDirectory.CaptionControl = this.lblDirectory;
            this.btnDirectory.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.btnDirectory.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDirectory.Location = new System.Drawing.Point(64, 78);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.Size = new System.Drawing.Size(23, 23);
            this.btnDirectory.TabIndex = 6;
            this.btnDirectory.UseVisualStyleBackColor = true;
            this.btnDirectory.Click += new System.EventHandler(this.btnDirectory_Click);
            // 
            // lblDirectory
            // 
            this.lblDirectory.AutoSize = true;
            this.lblDirectory.Location = new System.Drawing.Point(16, 82);
            this.lblDirectory.Name = "lblDirectory";
            this.lblDirectory.Size = new System.Drawing.Size(42, 15);
            this.lblDirectory.TabIndex = 5;
            this.lblDirectory.Text = "フォルダ";
            // 
            // txtDirectory
            // 
            this.txtDirectory.BackColor = System.Drawing.SystemColors.Window;
            this.txtDirectory.CaptionControl = this.lblDirectory;
            this.txtDirectory.FocusedBackColor = System.Drawing.Color.LightYellow;
            this.txtDirectory.FocusedForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDirectory.Key = "Directory";
            this.txtDirectory.Location = new System.Drawing.Point(93, 79);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(405, 23);
            this.txtDirectory.TabIndex = 7;
            this.txtDirectory.TabStop = false;
            // 
            // pnlProgress
            // 
            this.pnlProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlProgress.Controls.Add(this.lblFileName);
            this.pnlProgress.Controls.Add(this.lblProgressCaption);
            this.pnlProgress.Controls.Add(this.lblTotalProcessCaption);
            this.pnlProgress.Controls.Add(this.lblProgressedProcessCaption);
            this.pnlProgress.Controls.Add(this.lblDivide);
            this.pnlProgress.Controls.Add(this.lblTotalProcess);
            this.pnlProgress.Controls.Add(this.lblProgressedProcess);
            this.pnlProgress.Controls.Add(this.pgProcess);
            this.pnlProgress.Key = "Progress";
            this.pnlProgress.Location = new System.Drawing.Point(0, 152);
            this.pnlProgress.Margin = new System.Windows.Forms.Padding(0);
            this.pnlProgress.Name = "pnlProgress";
            this.pnlProgress.Size = new System.Drawing.Size(510, 55);
            this.pnlProgress.TabIndex = 1;
            // 
            // lblFileName
            // 
            this.lblFileName.Location = new System.Drawing.Point(90, 4);
            this.lblFileName.Margin = new System.Windows.Forms.Padding(0);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(218, 22);
            this.lblFileName.TabIndex = 1;
            this.lblFileName.Text = "XXXXXXX";
            this.lblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblProgressCaption
            // 
            this.lblProgressCaption.AutoSize = true;
            this.lblProgressCaption.Location = new System.Drawing.Point(16, 8);
            this.lblProgressCaption.Name = "lblProgressCaption";
            this.lblProgressCaption.Size = new System.Drawing.Size(55, 15);
            this.lblProgressCaption.TabIndex = 0;
            this.lblProgressCaption.Text = "進行状況";
            // 
            // lblTotalProcessCaption
            // 
            this.lblTotalProcessCaption.Location = new System.Drawing.Point(483, 4);
            this.lblTotalProcessCaption.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalProcessCaption.Name = "lblTotalProcessCaption";
            this.lblTotalProcessCaption.Size = new System.Drawing.Size(15, 22);
            this.lblTotalProcessCaption.TabIndex = 6;
            this.lblTotalProcessCaption.Text = "件";
            this.lblTotalProcessCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProgressedProcessCaption
            // 
            this.lblProgressedProcessCaption.Location = new System.Drawing.Point(373, 4);
            this.lblProgressedProcessCaption.Margin = new System.Windows.Forms.Padding(0);
            this.lblProgressedProcessCaption.Name = "lblProgressedProcessCaption";
            this.lblProgressedProcessCaption.Size = new System.Drawing.Size(15, 22);
            this.lblProgressedProcessCaption.TabIndex = 3;
            this.lblProgressedProcessCaption.Text = "件";
            this.lblProgressedProcessCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDivide
            // 
            this.lblDivide.Location = new System.Drawing.Point(388, 4);
            this.lblDivide.Margin = new System.Windows.Forms.Padding(0);
            this.lblDivide.Name = "lblDivide";
            this.lblDivide.Size = new System.Drawing.Size(30, 22);
            this.lblDivide.TabIndex = 4;
            this.lblDivide.Text = "／";
            this.lblDivide.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTotalProcess
            // 
            this.lblTotalProcess.Location = new System.Drawing.Point(418, 4);
            this.lblTotalProcess.Margin = new System.Windows.Forms.Padding(0);
            this.lblTotalProcess.Name = "lblTotalProcess";
            this.lblTotalProcess.Size = new System.Drawing.Size(65, 22);
            this.lblTotalProcess.TabIndex = 5;
            this.lblTotalProcess.Text = "XXXXXXX";
            this.lblTotalProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProgressedProcess
            // 
            this.lblProgressedProcess.Location = new System.Drawing.Point(308, 4);
            this.lblProgressedProcess.Margin = new System.Windows.Forms.Padding(0);
            this.lblProgressedProcess.Name = "lblProgressedProcess";
            this.lblProgressedProcess.Size = new System.Drawing.Size(65, 22);
            this.lblProgressedProcess.TabIndex = 2;
            this.lblProgressedProcess.Text = "XXXXXXX";
            this.lblProgressedProcess.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pgProcess
            // 
            this.pgProcess.Location = new System.Drawing.Point(12, 26);
            this.pgProcess.Name = "pgProcess";
            this.pgProcess.Size = new System.Drawing.Size(486, 23);
            this.pgProcess.TabIndex = 7;
            // 
            // Mp3Sort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.ClientSize = new System.Drawing.Size(510, 207);
            this.Controls.Add(this.pnlScreen);
            this.DuplicateLastTimePosition = true;
            this.EnableEscClose = true;
            this.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.GradationColor1 = System.Drawing.Color.Azure;
            this.GradationColor2 = System.Drawing.Color.Lavender;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "Mp3Sort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mp3Sort";
            this.Load += new System.EventHandler(this.Mp3Sort_Load);
            this.pnlScreen.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.gbType.ResumeLayout(false);
            this.gbBehavior.ResumeLayout(false);
            this.pnlProgress.ResumeLayout(false);
            this.pnlProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private NexFx.Controls.ExPanel pnlScreen;
        private NexFx.Controls.ExPanel pnlBody;
        private NexFx.Controls.ExGroupBox gbType;
        private NexFx.Controls.ExCheckBox cbAac;
        private NexFx.Controls.ExCheckBox cbMp3;
        private NexFx.Controls.ExButton btnExecute;
        private NexFx.Controls.ExGroupBox gbBehavior;
        private NexFx.Controls.ExRadioButton rbCopy;
        private NexFx.Controls.ExRadioButton rbMove;
        private NexFx.Controls.ExButton btnDirectory;
        private System.Windows.Forms.Label lblDirectory;
        private NexFx.Controls.ExTextBox txtDirectory;
        private NexFx.Controls.ExPanel pnlProgress;
        private System.Windows.Forms.ProgressBar pgProcess;
        private NexFx.Controls.ExButton btnToggleProgress;
        private System.Windows.Forms.Label lblDivide;
        private System.Windows.Forms.Label lblTotalProcess;
        private System.Windows.Forms.Label lblProgressedProcess;
        private System.Windows.Forms.Label lblTotalProcessCaption;
        private System.Windows.Forms.Label lblProgressedProcessCaption;
        private System.Windows.Forms.Label lblProgressCaption;
        private NexFx.Controls.ExCheckBox cbFlac;
        private NexFx.Controls.ExCheckBox cbCreateExtensionDirectory;
        private System.Windows.Forms.Label lblFileName;
        private NexFx.Controls.ExCheckBox cbLog;
        private NexFx.Controls.ExCheckBox cbConfirmOverWrite;
    }
}
