using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace Mp3Sort.Services
{
    /// <summary>
    /// ログ出力用のファイル属性格納クラス。
    /// </summary>
    public class LogFileAttributes
    {
        /// <summary>ファイルの拡張子を取得します。</summary>
        public string FileExtension
        {
            get
            {
                // 空文字化を判定します。
                if (string.IsNullOrWhiteSpace(this.OriginalFilePath))
                    // 空文字を返します。
                    return string.Empty;
                else
                    // ファイルの拡張子を返します。
                    return Path.GetExtension(this.OriginalFilePath);
            }
        }

        /// <summary>ドットを除いたファイルの拡張子を取得します。</summary>
        public string FileExtensionWithoutDot
        {
            get
            {
                // 空文字化を判定します。
                if (string.IsNullOrEmpty(this.FileExtension))
                    // 空文字を返します。
                    return string.Empty;
                else
                    // "."を抜いたファイルの拡張子を返します。
                    return this.FileExtension.Replace(".", string.Empty);
            }
        }

        /// <summary>ファイルの元パスを設定・取得します。</summary>
        public string OriginalFilePath { get; set; }

        /// <summary>アーティスト名不明値。</summary>
        public static readonly string UNKNOWN_ARTIST_VALUE = "アーティスト不明";

        /// <summary>アルバム名名不明値。</summary>
        public static readonly string UNKNOWN_ALBUM_VALUE = "アルバム不明";

        /// <summary>アーティスト名格納用変数。</summary>
        private string _artistName;

        /// <summary>アーティスト名を設定・取得します。</summary>
        public string ArtistName
        {
            get
            {
                // アーティスト名を判定します。
                if (string.IsNullOrWhiteSpace(this._artistName))
                    return UNKNOWN_ARTIST_VALUE;
                else
                    return this._artistName;
            }
            set
            {
                this._artistName = value;
            }
        }

        /// <summary>アルバム名格納用変数。</summary>
        private string _albumName;

        /// <summary>アルバム名を設定・取得します。</summary>
        public string AlbumName
        {
            get
            {
                // アルバム名を判定します。
                if (string.IsNullOrWhiteSpace(this._albumName))
                    return UNKNOWN_ALBUM_VALUE;
                else
                    return this._albumName;
            }
            set
            {
                this._albumName = value;
            }
        }

        /// <summary>転送先のファイルパスを設定・取得します。</summary>
        public string ToFilePath { get; set; } = string.Empty;

        /// <summary>元ファイルのザイズを取得します。</summary>
        public double OriginalFileSize
        {
            get
            {
                return new FileInfo(this.OriginalFilePath).Length;
            }
        }
    }

    /// <summary>
    /// ログ出力サービス。
    /// </summary>
    public class LogService : IDisposable
    {
        // ログファイル名のテンプレート。
        private static readonly string LOG_FILENAME_TMP = "Mp3Sort_{0}.log";

        // ログ出力用のタイムスタンプ。
        private static readonly DateTime TIMESTAMP = DateTime.Now;

        // ログファイル名用のタイムスタンプのフォーマット
        private static readonly string LOG_FILENAME_TIMESTAMP_FORMAT = "yyyyMMddHHmmssfff";

        // ログ出力用のタイムスタンプのフォーマット
        private static readonly string LOG_TIMESTAMP_FORMAT = "yyyy年M月d日 HH:mm:ss.fff";

        // ログ用インデント。
        private static readonly string INDENT = "　";

        // ログ用ファイル属性出力のテンプレート。
        private static readonly string FILE_ATTRIBUTES_LOG_TMP_01 = "{0}元ファイル" +
                                                                    Environment.NewLine + INDENT + INDENT +
                                                                    "サイズ：{1}MB" +
                                                                    Environment.NewLine + INDENT + INDENT +
                                                                    "アーティスト名：{2}" +
                                                                    Environment.NewLine + INDENT + INDENT +
                                                                    "アルバム名：{3}" +
                                                                    Environment.NewLine + INDENT + INDENT +
                                                                    "パス：{4}";
        // ログ用ファイル属性出力のテンプレート。
        private static readonly string FILE_ATTRIBUTES_LOG_TMP_02 = "{0}先ファイル" +
                                                                    Environment.NewLine + INDENT + INDENT +
                                                                    "パス：{1}";

        /// <summary>ログファイル名を取得します。</summary>
        public string LogFileName { get; private set; }

        //ログ出力用のビルダー変数。
        private StringBuilder _logBuilder;

        /// <summary>
        /// コンストラクタ定義。
        /// </summary>
        public LogService()
        {
            // ログファイル名を設定します。
            this.LogFileName = string.Format(LOG_FILENAME_TMP, TIMESTAMP.ToString(LOG_FILENAME_TIMESTAMP_FORMAT));

            // ログビルダーをインスタンス化します。
            this._logBuilder = new StringBuilder();

            // ログにヘッダを設定します。
            this.WriteHeader();
        }

        /// <summary>
        /// ファイル属性をログに設定します。
        /// </summary>
        /// <param name="fileAttributes">設定するファイル属性。</param>
        /// <param name="behavior">転送方法。</param>
        /// <param name="overWrite">上書きの有無。</param>
        public void WriteLogFileAttributes(LogFileAttributes fileAttributes, Behavior behavior)
        {
            // 転送方法用の文字列。
            string behaviorStr;

            // 転送方法を判定します。
            if (behavior == Behavior.Move)
                behaviorStr = "移動";
            else if (behavior == Behavior.Copy)
                behaviorStr = "コピー";
            else
                return;

            // ログを設定します。
            this._logBuilder.Append(INDENT);
            this._logBuilder.AppendFormat(FILE_ATTRIBUTES_LOG_TMP_01,
                                          behaviorStr,
                                          (fileAttributes.OriginalFileSize / Math.Pow(10, 6)).ToString("0.00"),
                                          fileAttributes.ArtistName,
                                          fileAttributes.AlbumName,
                                          fileAttributes.OriginalFilePath);
            this._logBuilder.AppendLine();
            this._logBuilder.Append(INDENT);
            this._logBuilder.AppendFormat(FILE_ATTRIBUTES_LOG_TMP_02,
                                          behaviorStr,
                                          fileAttributes.ToFilePath);
            this._logBuilder.AppendLine();
            this._logBuilder.AppendLine();
        }

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="outputDirectory">出力先のディレクトリ。</param>
        /// <param name="openLog">ログファイルを開くかの判定値。</param>
        public void Output(string outputDirectory, bool openLog = false)
        {
            // ログを出力します。
            this.Output(outputDirectory, Encoding.UTF8, openLog);
        }

        /// <summary>
        /// ログを出力します。
        /// </summary>
        /// <param name="outputDirectory">出力先のディレクトリ。</param>
        /// <param name="encoding">ログのエンコード。</param>
        /// <param name="openLog">ログファイルを開くかの判定値。</param>
        public void Output(string outputDirectory, Encoding encoding, bool openLog = false)
        {
            // フッタを設定します。
            this.WriteFooter();

            // ログファイルのパスを取得します。
            var logFilePath = Path.Combine(outputDirectory, this.LogFileName);

            // ログ出力用のストリームをインスタンス化します。
            using (var sw = new StreamWriter(logFilePath, false, encoding))
            {
                // ログを出力します。
                sw.WriteLine(this._logBuilder.ToString());

                // ストリームを閉じます。
                sw.Close();
            }

            // ログファイルを開くか判定します。
            if (openLog)
                Process.Start(logFilePath);
        }

        /// <summary>
        /// オブジェクトの破棄をします。
        /// </summary>
        public void Dispose()
        {
            // ビルダーを破棄します。
            if (this._logBuilder != null)
                this._logBuilder = null;
        }

        // ヘッダ用のテンプレート。
        private static readonly string HEADER_TMP = "[{0}] Start Mp3Sort";

        /// <summary>
        /// ログにヘッダを設定します。
        /// </summary>
        private void WriteHeader()
        {
            // ヘッダを出力します。
            this._logBuilder.AppendFormat(HEADER_TMP, TIMESTAMP.ToString(LOG_TIMESTAMP_FORMAT));
            this._logBuilder.AppendLine();
        }

        // フッタ用のテンプレート。
        private static readonly string FOOTER_TMP = "[{0}] End Mp3Sort";

        /// <summary>
        /// ログにフッタを設定します。
        /// </summary>
        private void WriteFooter()
        {
            // フッタを出力します。
            this._logBuilder.AppendFormat(FOOTER_TMP, DateTime.Now.ToString(LOG_TIMESTAMP_FORMAT));
        }
    }
}
