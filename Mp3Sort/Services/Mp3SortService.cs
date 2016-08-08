using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Mp3Sort.Presentations;
using Mp3Sort.Resources;
using Shell32;

namespace Mp3Sort.Services
{
    /// <summary>動作値。</summary>
    public enum Behavior
    {
        /// <summary>想定外値。</summary>
        None = -1,
        /// <summary>移動値。</summary>
        Move,
        /// <summary>コピー値。</summary>
        Copy
    }

    /// <summary>
    /// サービスの値格納クラス。
    /// </summary>
    public class Mp3SortServiceContainer
    {
        /// <summary>
        /// 上書き確認の処理を行います。
        /// </summary>
        /// <param name="fromFilePath">転送元のファイルパス。</param>
        /// <param name="toFilePath">転送先のファイルパス。</param>
        /// <returns>上書きをするかの判定値を返します。</returns>
        public delegate bool ConfirmDelegate(string fromFilePath, string toFilePath);

        /// <summary>
        /// MP3ファイルを対象とするかの判定値を設定・取得します。
        /// </summary>
        public bool Mp3 { get; set; }

        /// <summary>
        /// AACファイルを対象とするかの判定値を設定・取得します。
        /// </summary>
        public bool Aac { get; set; }

        /// <summary>
        /// FLACファイルを対象とするかの判定値を設定・取得します。
        /// </summary>
        public bool Flac { get; set; }

        /// <summary>
        /// 拡張子別のディレクトリを作成するかの判定値を設定・取得します。
        /// </summary>
        public bool CreateExtensionDirectory { get; set; }

        /// <summary>
        /// 動作の値を設定・取得します。
        /// </summary>
        public Behavior BehaviorValue { get; set; } = Behavior.None;

        /// <summary>
        /// 走査元のパスを設定・取得します。
        /// </summary>
        public string Path { get; set; } = string.Empty;

        /// <summary>
        /// ログ出力をするかの判定値を設定・取得します。
        /// </summary>
        public bool OutputLog { get; set; } = true;

        /// <summary>
        /// Mp3SortのProgressBarの動作インターフェイスを設定・取得します。
        /// </summary>
        public IMp3SortProgressBehavior ProgressBehavior { get; set; }

        /// <summary>
        /// 上書き確認のデリゲードを設定・取得します。
        /// </summary>
        public ConfirmDelegate ConfirmOverWrite { get; set; }
    }

    /// <summary>
    /// サービスの結果値格納クラス。
    /// </summary>
    public class Mp3SortResult
    {
        /// <summary>処理結果値を設定・取得します。</summary>
        public bool Result { get; set; } = true;

        /// <summary>処理結果のメッセージを設定・取得します。</summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>処理結果のObject派生型の値を設定・取得します。</summary>
        public object Tag { get; set; } = null;
    }

    /// <summary>
    /// ファイルの属性格納クラス。
    /// </summary>
    public class FileAttributes
    {
        /// <summary>
        /// ログ出力用のファイル属性格納クラスへの変換を定義します。
        /// </summary>
        /// <param name="value">変換元のファイルの属性格納クラス。</param>
        public static explicit operator LogFileAttributes(FileAttributes value)
        {
            return new LogFileAttributes() { AlbumName = value.AlbumName, ArtistName = value.ArtistName, OriginalFilePath = value.OriginalFilePath };
        }

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
    }

    /// <summary>
    /// Mp3Sortのサービスクラス。
    /// </summary>
    public class Mp3SortService : IDisposable
    {
        // ディレクトリのロック用ファイルの名称。
        private static readonly string LOCK_FILE_NAME_TMP = "lock{0}.lck";

        // 作業用のディレクトリ名称。
        private static readonly string WORK_DIRECTORY_NAME = "Mp3Sort";

        // MP3ファイルの小文字拡張子。
        private static readonly string MP3_LOWER_EXTENSION = ".mp3";

        // AACファイルの小文字拡張子。
        private static readonly string AAC_LOWER_EXTENSION = ".m4a";

        // FLACファイルの小文字拡張子。
        private static readonly string FLAC_LOWER_EXTENSION = ".flac";

        // コンテナ変数。
        private Mp3SortServiceContainer _container;

        // ログサービス変数。
        private LogService _logService;

        // dummyファイル用のファイルストリーム。
        private FileStream _dummyFs;

        // dummyファイル名。
        private string _lockFileName;

        // 作業ディレクトリのパス。
        private string _workDirectoryPath;

        // 転送件数。
        private int _transferCount = 0;

        // スキップ件数。
        private int _skipCount = 0;

        /// <summary>
        /// コンストラクタ定義。
        /// </summary>
        /// <param name="container">サービスのコンテナ。</param>
        public Mp3SortService(Mp3SortServiceContainer container)
        {
            // コンテナを設定します。
            this._container = container;

            // ロック用のファイル名を設定します。
            this.SetLockFileName();

            // 作業ディレクトリのパスを設定します。
            this._workDirectoryPath = Path.Combine(container.Path, WORK_DIRECTORY_NAME);
        }

        /// <summary>
        /// 値を検証します。
        /// </summary>
        /// <returns>処理の結果値を返します。</returns>
        public Mp3SortResult Validate()
        {
            // ファイル種類の値を検証します。
            if (!this._container.Mp3 && !this._container.Aac && !this._container.Flac)
                return new Mp3SortResult() { Result = false, Message = Messages.E0001 };

            // ファイル種類の値を検証します。
            if (this._container.BehaviorValue == Behavior.None)
                return new Mp3SortResult() { Result = false, Message = Messages.E0002 };

            // ディレクトリの存在確認を行います。
            if (!Directory.Exists(this._container.Path))
                return new Mp3SortResult() { Result = false, Message = Messages.E0003 };

            // ロック用のファイルの重複を検証します。
                    var lockFilePath = Path.Combine(this._container.Path, this._lockFileName);
            if (File.Exists(lockFilePath))
                return new Mp3SortResult() { Result = false, Message = Messages.E0004 };

            // 作業用ディレクトリの重複を検証します。
            var workDirectoryPath = Path.Combine(this._container.Path, WORK_DIRECTORY_NAME);
            if (Directory.Exists(workDirectoryPath))
                return new Mp3SortResult() { Result = false, Message = Messages.E0005 };

            return new Mp3SortResult() { Result = true, Message = string.Empty };
        }

        /// <summary>
        /// 対象ディレクトリのロックします。
        /// </summary>
        /// <returns>処理の結果値を返します。</returns>
        public Mp3SortResult LockMp3SortDirectory()
        {
            try
            {
                // ディレクトリのロック用ファイルを作成します。
                var lockFilePath = Path.Combine(this._container.Path, this._lockFileName);
                _dummyFs = File.Create(lockFilePath);
                var fi = new FileInfo(lockFilePath);
                fi.Attributes |= System.IO.FileAttributes.Hidden;
            }
            catch
            {
                return new Mp3SortResult() { Result = false, Message = Messages.E0006 };
            }

            return new Mp3SortResult() { Result = true, Message = string.Empty };
        }

        /// <summary>
        /// 対象ディレクトリのロックを解除します。
        /// </summary>
        /// <returns>処理の結果値を返します。</returns>
        public Mp3SortResult UnLockMp3SortDirectory()
        {
            try
            {
                // ディレクトリのロック用ファイルを削除します。
                var lockFilePath = Path.Combine(this._container.Path, this._lockFileName);
                this._dummyFs.Close();
                this.ForceDelete(lockFilePath);
            }
            catch
            {
                return new Mp3SortResult() { Result = false, Message = Messages.E0007 };
            }

            return new Mp3SortResult() { Result = true, Message = string.Empty };
        }

        /// <summary>
        /// ファイルの仕分けを行います。
        /// </summary>
        /// <returns>処理の結果値を返します。</returns>
        public Mp3SortResult Sort()
        {
            // ファイル属性用のリストをインスタンス化します。
            var fileAttributesList = new List<FileAttributes>();

            // 結果値格納用変数。
            var msr = new Mp3SortResult();

            // ファイル属性用のリストに情報を設定します。
            msr = this.SetFileAttributesList(this._container.Path, ref fileAttributesList);

            // リスト設定の結果値を判定します。
            if (!msr.Result)
                return msr;

            // 作業ディレクトリの作成を行います。
            msr = this.CreateDirectory(this._workDirectoryPath);

            // 作業ディレクトリの作成の結果値を判定します。
            if (!msr.Result)
                return msr;

            // 仕分けを行います。
            msr = this.SortCore(fileAttributesList);

            // 仕分けの結果値を判定します。
            if (!msr.Result)
                return msr;

            return new Mp3SortResult() { Result = true, Message = string.Empty };
        }

        /// <summary>
        /// 完了処理を行います。
        /// </summary>
        /// <returns>処理の結果値を返します。</returns>
        public Mp3SortResult Complete()
        {
            // ProgressBarを完了させます。
            this._container.ProgressBehavior.SetCompleteProgressedProcess();

            // ログ出力をするかを判定します。
            if (this._container.OutputLog)
                // ログを出力します。 
                this._logService.Output(this._workDirectoryPath, true);

            // 動作メッセージ用の変数。
            var behaviorMsg = string.Empty;

            // 動作の判定を行います。
            if (this._container.BehaviorValue == Behavior.Copy)
                behaviorMsg = "コピー";
            else if (this._container.BehaviorValue == Behavior.Move)
                behaviorMsg = "移動";

            // メッセージを作成します。
            string msg;

            // スキップ件数を判定します。
            if (this._skipCount == 0)
                msg = string.Format(Messages.N0001, this._transferCount, behaviorMsg);
            else
                msg = string.Format(Messages.N0002, this._transferCount, behaviorMsg, Environment.NewLine, this._skipCount);

            return new Mp3SortResult() { Result = true, Message = msg };
        }

        /// <summary>
        /// オブジェクトの破棄をします。
        /// </summary>
        public void Dispose()
        {
            // ストリームを破棄します。
            if (this._dummyFs != null)
            {
                this._dummyFs.Close();
                this._dummyFs.Dispose();
                this._dummyFs = null;
            }
        }

        /// <summary>
        /// ロック用のファイル名を設定します。
        /// </summary>
        private void SetLockFileName()
        {
            Guid guid = Guid.NewGuid();

            this._lockFileName = string.Format(LOCK_FILE_NAME_TMP, guid.ToString("N"));
        }

        // アーティスト情報のインデックス。
        private static readonly int ARTIST_INDEX = 13;

        // FALCファイルのアーティスト情報のインデックス。
        private static readonly int FLAC_ARTIST_INDEX = 230;

        // アルバム情報のインデックス。
        private static readonly int ALBUM_INDEX = 14;

        /// <summary>
        /// ファイル属性用のリストに情報を設定します。
        /// </summary>
        /// <param name="directoryPath">属性を格納するファイルが存在するディレクトリのパス。</param>
        /// <param name="list">属性を格納するリスト。</param>
        /// <returns>処理の結果値を返します。</returns>
        private Mp3SortResult SetFileAttributesList(string directoryPath, ref List<FileAttributes> list)
        {
            // COMオブジェクトを宣言します。
            ShellClass shell = null;
            Folder f = null;
            FolderItem fi = null;

            try
            {
                // COMオブジェクトをインスタンス化します。
                shell = new ShellClass();

                try
                {
                    // COMオブジェクトをインスタンス化します。
                    f = shell.NameSpace(directoryPath);

                    // ディレクトリ内のファイルを走査します。
                    foreach (var filePath in Directory.GetFiles(directoryPath))
                    {
                        // 対象ファイルの小文字拡張子を取得します。
                        var lowerExtension = this.GetTargetFileLowerExtension(filePath);

                        // 対象ファイルかを判定します。
                        if (string.IsNullOrEmpty(lowerExtension)) continue;

                        var fileName = Path.GetFileName(filePath);

                        try
                        {
                            // COMオブジェクトをインスタンス化します。
                            fi = f.ParseName(fileName);

                            // FLACファイルかを判定します。
                            if (FLAC_LOWER_EXTENSION.Equals(lowerExtension))
                            {
                                // 属性をリストに追加します。
                                list.Add(new FileAttributes() { OriginalFilePath = filePath, ArtistName = f.GetDetailsOf(fi, FLAC_ARTIST_INDEX), AlbumName = f.GetDetailsOf(fi, ALBUM_INDEX) });
                            }
                            else
                            {
                                // 属性をリストに追加します。
                                list.Add(new FileAttributes() { OriginalFilePath = filePath, ArtistName = f.GetDetailsOf(fi, ARTIST_INDEX), AlbumName = f.GetDetailsOf(fi, ALBUM_INDEX) });
                            }
                        }
                        catch (Exception ex)
                        {
                            return new Mp3SortResult() { Result = false, Message = ex.Message };
                        }
                        finally
                        {
                            // COMオブジェクトを破棄します。
                            if (fi != null) Marshal.ReleaseComObject(fi);
                        }
                    }
                }
                catch (Exception ex)
                {
                    return new Mp3SortResult() { Result = false, Message = ex.Message };
                }
                finally
                {
                    // COMオブジェクトを破棄します。
                    if (f != null) Marshal.ReleaseComObject(f);
                }
            }
            catch (Exception ex)
            {
                return new Mp3SortResult() { Result = false, Message = ex.Message };
            }
            finally
            {
                // COMオブジェクトを破棄します。
                if (shell != null) Marshal.ReleaseComObject(shell);
            }

            // ディレクトリ内のサブディレクトリを走査します。
            foreach (var subDirectoryPath in Directory.GetDirectories(directoryPath))
            {
                // 結果値格納用変数。
                var msr = new Mp3SortResult();

                // 再帰的に自身処理を行います。
                msr = this.SetFileAttributesList(subDirectoryPath, ref list);

                // リスト設定の結果値を判定します。
                if (!msr.Result)
                    return msr;
            }

            return new Mp3SortResult() { Result = true, Message = string.Empty };
        }

        /// <summary>
        /// ディレクトリを作成します。
        /// </summary>
        /// <param name="directoryPath">作成するディレクトリのパス。</param>
        /// <returns>処理の結果値を返します。</returns>
        private Mp3SortResult CreateDirectory(string directoryPath)
        {
            try
            {
                // 親ディレクトリのパスを取得します。
                var parentDirectoryPath = Directory.GetParent(directoryPath);

                // 親ディレクトリが存在するかを判定します。
                if (!Directory.Exists(parentDirectoryPath.FullName))
                    return new Mp3SortResult() { Result = false, Message = Messages.E0008 };

                // 作成するディレクトリが存在するかを判定し、存在しない場合はディレクトリを作成します。
                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);
            }
            catch (Exception ex)
            {
                return new Mp3SortResult() { Result = false, Message = ex.Message };
            }

            return new Mp3SortResult() { Result = true, Message = string.Empty };
        }

        // 置換用文字。
        private static readonly char REPLACE_CHAR = '_';

        /// <summary>
        /// ファイルの仕分けを行います。
        /// </summary>
        /// <param name="list">属性が格納されているリスト。</param>
        /// <returns>処理の結果値を返します。</returns>
        private Mp3SortResult SortCore(List<FileAttributes> list)
        {
            // ログ出力をするかを判定します。
            if (this._container.OutputLog)
                this._logService = new LogService();

            // 結果値格納用変数。
            var msr = new Mp3SortResult();

            // ソート結果格納用変数。
            List<FileAttributes> sortedList;

            // 拡張子別にディレクトリを作成するかを判定します。
            if (this._container.CreateExtensionDirectory)
            { 
                // 拡張子、アーティスト、アルバムにてソートします。
                var query = from p in list orderby p.FileExtensionWithoutDot ascending, p.ArtistName ascending, p.AlbumName ascending select p;

                // ソート結果をリストに格納します。
                sortedList = query.ToList();
            }
            else
            { 
                // アーティスト、アルバムにてソートします。
                var query = from p in list orderby p.ArtistName ascending, p.AlbumName ascending select p;

                // ソート結果をリストに格納します。
                sortedList = query.ToList();
            }
            // ProgressBarを設定します。
            this._container.ProgressBehavior.SetTotalProcess(sortedList.Count());

            // ソートしたリストにて走査します。
            foreach (var fileAttributes in sortedList)
            {
                // 作成元ディレクトリのパスを取得します。
                var workRootPath = Path.Combine(this._container.Path, WORK_DIRECTORY_NAME);

                // 拡張子別にディレクトリを作成するかを判定します。
                if (this._container.CreateExtensionDirectory)
                {
                    // アーティストディレクトリのパスを取得します。
                    workRootPath = Path.Combine(workRootPath, fileAttributes.FileExtensionWithoutDot);

                    // 拡張子ディレクトリを作成します。
                    msr = this.CreateDirectory(workRootPath);

                    // 拡張子ディレクトリ作成の結果値を判定します。
                    if (!msr.Result)
                        return msr;
                }

                // パスの禁則文字を置換します。
                var artistDirectoryName = this.ReplaceChar(fileAttributes.ArtistName, REPLACE_CHAR, Path.GetInvalidPathChars());

                // アーティストディレクトリのパスを取得します。
                var artistDirectoryPath = Path.Combine(workRootPath, artistDirectoryName);

                // アーティストディレクトリを作成します。
                msr = this.CreateDirectory(artistDirectoryPath);

                // アーティストディレクトリ作成の結果値を判定します。
                if (!msr.Result)
                    return msr;

                // パスの禁則文字を置換します。
                var albumDirectoryName = this.ReplaceChar(fileAttributes.AlbumName, REPLACE_CHAR, Path.GetInvalidPathChars());

                // アルバムディレクトリのパスを取得します。
                var albumDirectoryPath = Path.Combine(artistDirectoryPath, albumDirectoryName);

                // アルバムディレクトリを作成します。
                msr = this.CreateDirectory(albumDirectoryPath);

                // アルバムディレクトリ作成の結果値を判定します。
                if (!msr.Result)
                    return msr;

                // ファイル名を取得します。
                var fileName = Path.GetFileName(fileAttributes.OriginalFilePath);

                // Progressエリアにファイル名を設定します。
                this._container.ProgressBehavior.SetFileName(fileName);

                // 転送先のファイルパスを取得します。
                var filePath = Path.Combine(albumDirectoryPath, fileName);

                // ファイルを転送します。
                msr = this.Transfer(fileAttributes.OriginalFilePath, filePath);

                // ログ出力をするかを判定します。
                if (this._container.OutputLog)
                {
                    // ログ出力用のファイル属性格納クラスに変換します。
                    var lfa = (LogFileAttributes)fileAttributes;
                    lfa.ToFilePath = filePath;

                    // ログサービスにファイル属性格納クラスを設定します。
                    this._logService.WriteLogFileAttributes(lfa, this._container.BehaviorValue);
                }

                // ファイル転送の結果値を判定します。
                if (!msr.Result)
                    return msr;
            }

            return new Mp3SortResult() { Result = true, Message = string.Empty };
        }

        /// <summary>
        /// ファイルを転送します。
        /// </summary>
        /// <param name="fromPath">転送元のファイルパス。</param>
        /// <param name="toPath">転送先のファイルパス。</param>
        /// <returns>処理の結果値を返します。</returns>
        private Mp3SortResult Transfer(string fromPath,string toPath)
        {
            // 強制転送フラグ。
            var forceTransfer = true;

            // ファイルの重複確認を行います。
            if (File.Exists(toPath) && this._container.ConfirmOverWrite != null)
                forceTransfer = this._container.ConfirmOverWrite(fromPath, toPath);

            // ファイルの上書き判定を行います。
            if (forceTransfer)
                // ファイルを削除します。
                this.ForceDelete(toPath);
            else
            {
                // スキップ件数を加算します。
                this._skipCount++;

                // ProgressBarを進行させます。
                this._container.ProgressBehavior.IncrementProgressedProcess();

                // 転送せずに終了します。
                return new Mp3SortResult() { Result = true, Message = string.Empty };
            }

            try
            {
                // ファイルの転送方法を判定します。
                if (this._container.BehaviorValue == Behavior.Move)
                    // ファイルを移動します。
                    File.Move(fromPath, toPath);
                else if (this._container.BehaviorValue == Behavior.Copy)
                    // ファイルをコピーします。
                    File.Copy(fromPath, toPath);
                else
                    // 転送せずに終了します。
                    return new Mp3SortResult() { Result = true, Message = string.Empty };

                // 転送件数を加算します。
                this._transferCount++;

                // ProgressBarを進行させます。
                this._container.ProgressBehavior.IncrementProgressedProcess();
            }
            catch (Exception ex)
            {
                return new Mp3SortResult() { Result = false, Message = ex.Message };
            }

            return new Mp3SortResult() { Result = true, Message = string.Empty };
        }

        /// <summary>
        /// 対象ファイルの小文字拡張子を取得します。
        /// </summary>
        /// <param name="filePath">対象ファイルのパス。</param>
        /// <returns>対象ファイルの小文字拡張子を返します。</returns>
        private string GetTargetFileLowerExtension(string filePath)
        {
            // ファイルの小文字拡張子。
            var lowerExtension = Path.GetExtension(filePath).ToLower();

            // ファイルの拡張子を判定します。
            if (this._container.Mp3 && MP3_LOWER_EXTENSION.Equals(lowerExtension))
                return MP3_LOWER_EXTENSION;
            if (this._container.Aac && AAC_LOWER_EXTENSION.Equals(lowerExtension))
                return AAC_LOWER_EXTENSION;
            if (this._container.Flac && FLAC_LOWER_EXTENSION.Equals(lowerExtension))
                return FLAC_LOWER_EXTENSION;

            return null;
        }

        /// <summary>
        /// ファイルを削除します。
        /// </summary>
        /// <param name="filePath">削除するファイルのパス。</param>
        private void ForceDelete(string filePath)
        {
            // ファイル情報クラスをインスタンス化します。
            var fi = new FileInfo(filePath);

            // ファイルが存在するかを判定します。
            if (fi.Exists)
            {
                // 読み取り専用属性がある場合は、読み取り専用属性を解除します。
                if ((fi.Attributes & System.IO.FileAttributes.ReadOnly) == System.IO.FileAttributes.ReadOnly)
                {
                    fi.Attributes = System.IO.FileAttributes.Normal;
                }

                // ファイルを削除します。
                fi.Delete();
            }
        }

        /// <summary>
        /// 文字を置換します。
        /// </summary>
        /// <param name="str">置換対象の文字列。</param>
        /// <param name="replaceChar">置換する文字。</param>
        /// <param name="replacedChars">置換される文字。</param>
        /// <returns>置換した文字列を返します。</returns>
        private string ReplaceChar(string str, char replaceChar, params char[] replacedChars)
        {
            // 結果値用の変数。
            var result = str;

            // 禁則文字を置換します。
            Array.ForEach(replacedChars, c => { result = result.Replace(c, replaceChar); });

            return result;
        }
    }
}
