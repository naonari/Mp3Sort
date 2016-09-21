using System;
using System.IO;
using System.Windows.Forms;
using NexFx.Controls;
using Mp3Sort.Services;
using Mp3Sort.Resources;

namespace Mp3Sort.Presentations
{
    /// <summary>
    /// Mp3SortのProgressBarの動作インターフェイス。
    /// </summary>
    public interface IMp3SortProgressBehavior
    {
        /// <summary>
        /// ProgressBarエリアを表示します。
        /// </summary>
        void DisplayArea();

        /// <summary>
        /// ProgressBarエリアを非表示にします。
        /// </summary>
        void HideArea();

        /// <summary>
        /// ProgressBarを初期化します。
        /// </summary>
        void InitializeProgress();

        /// <summary>
        /// 処理の総件数を設定します。
        /// </summary>
        /// <param name="processCount">処理の総件数。</param>
        void SetTotalProcess(int processCount);

        /// <summary>
        /// ファイル名を設定します。
        /// </summary>
        /// <param name="fileName">設定するファイル名。</param>
        void SetFileName(string fileName);

        /// <summary>
        /// 進行済みの処理件数を設定します。
        /// </summary>
        /// <param name="processCount">進行済みの処理件数。</param>
        void SetProgressedProcess(int processCount);

        /// <summary>
        /// 進行済みの処理件数を加算します。
        /// </summary>
        /// <param name="incrementValue">加算する件数。(省略可)</param>
        void IncrementProgressedProcess(int incrementValue = 1);

        /// <summary>
        /// ProgressBarを完了させます。
        /// </summary>
        void SetCompleteProgressedProcess();
    }

    /// <summary>
    /// Mp3Sortの設定画面クラス。
    /// </summary>
    public partial class Mp3Sort : ExForm, IMp3SortProgressBehavior
    {
        /// <summary>
        /// コンストラクタ定義。
        /// </summary>
        public Mp3Sort()
        {
            // 初期設定を行います。
            InitializeComponent();

            //// ファイルタイプチェックボックスのイベントを紐づけます。
            //this.cbMp3.CheckedChanged += new EventHandler(this.FileType_CheckedChanged);
            //this.cbAac.CheckedChanged += new EventHandler(this.FileType_CheckedChanged);
            //this.cbFlac.CheckedChanged += new EventHandler(this.FileType_CheckedChanged);
        }

        /// <summary>
        /// 画面読込時の処理を行います。
        /// </summary>
        private void Mp3Sort_Load(object sender, EventArgs e)
        {
            // 画面の初期化を行います。
            this.Mp3Sort_Initialize(true);
        }

        /// <summary>
        /// 画面の初期化を行います。
        /// </summary>
        /// <param name="forceInitialize">強制画面初期化値。</param>
        private void Mp3Sort_Initialize(bool forceInitialize)
        {
            // 設定ファイルサービスを取得します。
            var css = ConfigSingletonService.GetInstance();

            // 各コントロールに初期化します。
            if (forceInitialize || css.GetCompleteInitialize(this.rbMove.Key, true))
            {
                this.rbMove.Checked = css.GetDefaultValue(this.rbMove.Key, true);
                if (css.GetSetFocus(this.rbMove.Key, true)) this.rbMove.Select();
            }

            if (forceInitialize || css.GetCompleteInitialize(this.rbCopy.Key, true))
            {
                this.rbCopy.Checked = css.GetDefaultValue(this.rbCopy.Key, false);
                if (css.GetSetFocus(this.rbCopy.Key, false)) this.rbCopy.Select();
            }

            if (forceInitialize || css.GetCompleteInitialize(this.cbMp3.Key, true))
            {
                this.cbMp3.Checked = css.GetDefaultValue(this.cbMp3.Key, true);
                if (css.GetSetFocus(this.cbMp3.Key, false)) this.cbMp3.Select();
            }

            if (forceInitialize || css.GetCompleteInitialize(this.cbAac.Key, true))
            {
                this.cbAac.Checked = css.GetDefaultValue(this.cbAac.Key, true);
                if (css.GetSetFocus(this.cbAac.Key, false)) this.cbAac.Select();
            }

            if (forceInitialize || css.GetCompleteInitialize(this.cbFlac.Key, true))
            {
                this.cbFlac.Checked = css.GetDefaultValue(this.cbFlac.Key, true);
                if (css.GetSetFocus(this.cbFlac.Key, false)) this.cbFlac.Select();
            }

            if (forceInitialize || css.GetCompleteInitialize(this.cbCreateExtensionDirectory.Key, true))
            {
                this.cbCreateExtensionDirectory.Checked = css.GetDefaultValue(this.cbCreateExtensionDirectory.Key, true);
                if (css.GetSetFocus(this.cbCreateExtensionDirectory.Key, false)) this.cbCreateExtensionDirectory.Select();
            }

            if (forceInitialize || css.GetCompleteInitialize(this.cbConfirmOverWrite.Key, true))
            {
                this.cbConfirmOverWrite.Checked = css.GetDefaultValue(this.cbConfirmOverWrite.Key, true);
                if (css.GetSetFocus(this.cbConfirmOverWrite.Key, false)) this.cbConfirmOverWrite.Select();
            }

            if (forceInitialize || css.GetCompleteInitialize(this.txtDirectory.Key, true))
            {
                this.txtDirectory.Text = css.GetDefaultValue(this.txtDirectory.Key);
                if (css.GetSetFocus(this.txtDirectory.Key, false))
                {
                    this.txtDirectory.SelectAll();
                    this.txtDirectory.Select();
                }
            }

            if (forceInitialize || css.GetCompleteInitialize(this.cbLog.Key, true))
            {
                this.cbLog.Checked = css.GetDefaultValue(this.cbLog.Key, true);
                if (css.GetSetFocus(this.cbLog.Key, false)) this.cbLog.Select();
            }

            // ProgressBarエリアの初期化を行います。
            this.InitializeProgress();

            // ProgressBarエリアの表示を切り替えます。
            if (forceInitialize || css.GetCompleteInitialize(this.pnlProgress.Name, true))
            {
                if (css.GetDefaultValue(this.pnlProgress.Name, true))
                    this.DisplayArea();
                else
                    this.HideArea();
            }
        }

        ///// <summary>
        ///// ファイルタイプチェックボックスの値変更時の処理を行います。
        ///// </summary>
        //private void FileType_CheckedChanged(object sender, EventArgs e)
        //{
        //    var checkedCount = 0;

        //    if (this.cbMp3.Checked) checkedCount++;
        //    if (this.cbAac.Checked) checkedCount++;
        //    if (this.cbFlac.Checked) checkedCount++;

        //    static readonly int MULTIPLE = 2;

        //    this.cbCreateExtensionDirectory.Enabled = (checkedCount >= MULTIPLE);
        //    if (!this.cbCreateExtensionDirectory.Enabled) this.cbCreateExtensionDirectory.Checked = false;
        //}

        /// <summary>
        /// ディレクトリボタン押下時の処理を行います。
        /// </summary>
        private void btnDirectory_Click(object sender, EventArgs e)
        {
            // ディレクトリ選択ダイアログをインスタン化します。
            using (var ofd = new FolderBrowserDialog())
            {
                // ダイアログの結果を判定し、テキストボックスにパスを設定します。
                if (ofd.ShowDialog() == DialogResult.OK)
                    this.txtDirectory.Text = ofd.SelectedPath;
            }
        }

        /// <summary>
        /// 実行ボタン押下時の処理を行います。
        /// </summary>
        private void btnExecute_Click(object sender, EventArgs e)
        {
            // サービスに渡すコンテナを生成します。
            var container = new Mp3SortServiceContainer();

            // コンテナに値を設定します。
            container.BehaviorValue = this.rbMove.Checked ? Behavior.Move : Behavior.Copy;

            container.Mp3 = this.cbMp3.Checked;
            container.Aac = this.cbAac.Checked;
            container.Flac = this.cbFlac.Checked;
            container.CreateExtensionDirectory = this.cbCreateExtensionDirectory.Checked;

            container.Path = this.txtDirectory.Text;
            if (this.cbConfirmOverWrite.Checked) container.ConfirmOverWrite = this.ConfirmOverWriteFile;

            container.ProgressBehavior = this;
            container.OutputLog = this.cbLog.Checked;

            // サービスをインスタンス化します。
            using (var service = new Mp3SortService(container))
            {
                // 値の検証を行います。
                var msr = service.Validate();

                //処理結果を判定します。
                if (!msr.Result)
                {
                    // エラーメッセージを表示します。
                    MessageBox.Show(msr.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // ディレクトリをロックします。
                msr = service.LockMp3SortDirectory();

                //処理結果を判定します。
                if (!msr.Result)
                {
                    // エラーメッセージを表示します。
                    MessageBox.Show(msr.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // ファイルを仕分けます。
                    msr = service.Sort();

                    //処理結果を判定します。
                    if (!msr.Result)
                    {
                        // エラーメッセージを表示します。
                        MessageBox.Show(msr.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                finally
                {
                    // ディレクトリをロックを解除します。
                    msr = service.UnLockMp3SortDirectory();
                }

                //処理結果を判定します。
                if (!msr.Result)
                {
                    // エラーメッセージを表示します。
                    MessageBox.Show(msr.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 完了処理を行います。
                msr = service.Complete();
                MessageBox.Show(msr.Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // 初期化処理を行います。
            this.Mp3Sort_Initialize(false);
        }

        /// <summary>
        /// ProgressBarエリア表示切り替えボタン押下時の処理を行います。
        /// </summary>
        private void btnToggleProgress_Click(object sender, EventArgs e)
        {
            // ProgressBarエリアの表示を切り返します。
            if (this.pnlProgress.Visible)
                this.HideArea();
            else
                this.DisplayArea();
        }

        /// <summary>
        /// 上書き確認の処理を行います。
        /// </summary>
        /// <param name="fromFilePath">転送元のファイルパス。</param>
        /// <param name="toFilePath">転送先のファイルパス。</param>
        /// <returns>上書きをするかの判定値を返します。</returns>
        public bool ConfirmOverWriteFile(string fromFilePath,string toFilePath)
        {
            // MB表記用の桁補正値。
            var shrinkToM = Math.Pow(10,6);

            // ファイル名を取得します。
            var fileName = Path.GetFileName(fromFilePath);

　           // 転送元のファイルのサイズを取得します。
            var fromFileSize = ((double)new FileInfo(fromFilePath).Length) / shrinkToM;

            // 転送元のファイル用のメッセージを作成します。
            var fromMsg = string.Format(Messages.N0003, "転送元ファイル", fromFileSize.ToString("0.00"), File.GetLastAccessTime(fromFilePath).ToString("yyyy/M/d hh:mm:ss"));

            // 転送先のファイルのサイズを取得します。
            var toFileSize = ((double)new FileInfo(toFilePath).Length) / shrinkToM;

            // 転送先のファイル用のメッセージを作成します。
            var toMsg = string.Format(Messages.N0003, "転送先ファイル", toFileSize.ToString("0.00"), File.GetLastAccessTime(toFilePath).ToString("yyyy/M/d hh:mm:ss"));

            // 確認のメッセージボックスを表示し、結果を取得します。
            var messageResult = MessageBox.Show(string.Format(Messages.Q0001,Environment.NewLine, fileName, fromMsg, toMsg),
                                                "Confirm",
                                                MessageBoxButtons.YesNo,
                                                MessageBoxIcon.Question,
                                                MessageBoxDefaultButton.Button2);

            return DialogResult.Yes.Equals(messageResult);
        }

        /// <summary>
        /// ProgressBarエリアを表示します。
        /// </summary>
        public void DisplayArea()
        {
            // ProgressBarエリアの表示を判定します。
            if (!this.pnlProgress.Visible)
            {
                // ProgressBarエリアを表示します。
                this.pnlProgress.Visible = true;
                this.Height += this.pnlProgress.Height;
            }
        }

        /// <summary>
        /// ProgressBarエリアを非表示にします。
        /// </summary>
        public void HideArea()
        {
            // ProgressBarエリアの表示を判定します。
            if (this.pnlProgress.Visible)
            {
                // ProgressBarエリアを非表示にします。
                this.pnlProgress.Visible = false;
                this.Height -= this.pnlProgress.Height;
            }
        }

        /// <summary>
        /// ProgressBarを初期化します。
        /// </summary>
        public void InitializeProgress()
        {
            // ProgressBarを初期化します。
            this.pgProcess.Minimum = 0;
            this.pgProcess.Maximum = 0;
            this.pgProcess.Value = 0;

            // 表記を初期化します。
            this.lblFileName.Text = string.Empty;
            this.lblTotalProcess.Text = string.Empty;
            this.lblProgressedProcess.Text = string.Empty;
        }

        /// <summary>
        /// 処理の総件数を設定します。
        /// </summary>
        /// <param name="processCout">処理の総件数。</param>
        public void SetTotalProcess(int processCount)
        {
            // 処理の総件数を設定します。
            this.pgProcess.Maximum = processCount;
            this.lblTotalProcess.Text = processCount.ToString();
            this.pgProcess.Update();
            this.lblTotalProcess.Update();
            this.lblDivide.Update();
        }

        /// <summary>
        /// ファイル名を設定します。
        /// </summary>
        /// <param name="fileName">設定するファイル名。</param>
        public void SetFileName(string fileName)
        {
            // ファイル名称を設定します。
            this.lblFileName.Text = fileName;
            this.lblFileName.Update();
        }

        /// <summary>
        /// 進行済みの処理件数を設定します。
        /// </summary>
        /// <param name="processCout">進行済みの処理件数。</param>
        public void SetProgressedProcess(int processCount)
        {
            // 進行済みの処理件数を設定します。
            this.pgProcess.Value = processCount;
            this.lblProgressedProcess.Text = processCount.ToString();
            this.pgProcess.Update();
            this.lblProgressedProcess.Update();
        }

        /// <summary>
        /// 進行済みの処理件数を加算します。
        /// </summary>
        /// <param name="incrementValue">加算する件数。(省略可)</param>
        public void IncrementProgressedProcess(int incrementValue = 1)
        {
            // 進行済みの処理件数を加算します。
            this.pgProcess.Value += incrementValue;
            this.lblProgressedProcess.Text = this.pgProcess.Value.ToString();
            this.pgProcess.Update();
            this.lblProgressedProcess.Update();
        }

        /// <summary>
        /// ProgressBarを完了させます。
        /// </summary>
        public void SetCompleteProgressedProcess()
        {
            // 進行済みの処理件数を加算します。
            this.pgProcess.Value = this.pgProcess.Maximum;
            this.lblProgressedProcess.Text = this.pgProcess.Value.ToString();
            this.pgProcess.Update();
            this.lblProgressedProcess.Update();
        }
    }
}
