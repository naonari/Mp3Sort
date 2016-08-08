using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace Mp3Sort.Services
{
    /// <summary>
    /// 設定ファイルサービス。
    /// </summary>
    public class ConfigSingletonService
    {
        /// <summary>
        /// インスタンス化禁止。
        /// </summary>
        private ConfigSingletonService()
        {
            // 設定ファイルを読み込みします。
            this.LoadConfig();

            // アセンブリを取得します。
            this._assembly = this.GetType().Assembly;
        }

        // 単一のインスタンス。
        private static ConfigSingletonService _singleton = new ConfigSingletonService();

        /// <summary>
        /// 単一のインスタンスを取得します。
        /// </summary>
        /// <returns>単一のインスタンスを返します。</returns>
        public static ConfigSingletonService GetInstance()
        {
            // 単一のインスタンスを返します。
            return _singleton;
        }

        // 設定ファイルの名称。
        private const string CONFIG_FILE_NAME = "Mp3Sort.config";

        // 要素の定数。
        private static readonly string TOP_ELEMENT = "configuration";

        // XMLドキュメント。
        private XDocument _xDoc = null;

        // アセンブリ。
        private Assembly _assembly = null;

        /// <summary>
        /// 設定ファイルを再読み込みします。
        /// </summary>
        public void ReloadConfig()
        {
            // 設定ファイルを読み込みします。
            this.LoadConfig();
        }

        // 値の属性値
        private static readonly string VALUE_ATTRIBUTE = "value";

        /// <summary>
        /// 設定ファイルから初期値を取得します。
        /// </summary>
        /// <param name="key">取得する値のキー。</param>
        /// <returns>取得した初期値を返します。</returns>
        public string GetDefaultValue(string key)
        {
            // 設定ファイルのドキュメントのインスタンスを判定します。
            if (this._xDoc == null)
                return null;

            try
            {
                // 設定ファイルから値を取得します。
                return this._xDoc.Element(TOP_ELEMENT).Element(this._assembly.GetName().Name).Element(key).Attribute(VALUE_ATTRIBUTE).Value;
            }
            catch
            {
                // 取得に失敗した場合はnullを返します。
                return null;
            }
        }

        /// <summary>
        /// 設定ファイルから初期値を取得します。
        /// </summary>
        /// <typeparam name="T">結果値の型。</typeparam>
        /// <param name="key">取得する値のキー。</param>
        /// <param name="defaultValue">取得出来なかった場合は返り値。</param>
        /// <returns>取得した初期値を返します。</returns>
        public T GetDefaultValue<T>(string key, T defaultValue) where T : struct
        {
            // 設定ファイルから値を取得します。
            var strResult = this.GetDefaultValue(key);

            // 値が取得出来ない場合は規定値を返します。
            if (strResult == null)
                return defaultValue;

            try
            {
                // 値を変換します。
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(strResult);
            }
            catch
            {
                // 変換に失敗した場合は規定値を返します。
                return defaultValue;
            }
        }

        // 初期化値の属性。
        private static readonly string COMPLETE_INITIALIZE_ATTRIBUTE = "CompleteInitialize";

        /// <summary>
        /// 処理完了後に値を初期化するかの判定値を取得します。
        /// </summary>
        /// <param name="key">取得する値のキー。</param>
        /// <param name="defaultValue">取得出来なかった場合は返り値。</param>
        /// <returns>取得した判定値を返します。</returns>
        public bool GetCompleteInitialize(string key, bool defaultValue)
        {
            // 設定ファイルのドキュメントのインスタンスを判定します。
            if (this._xDoc == null)
                return defaultValue;

            try
            {
                // 結果値の文字列格納用変数。
                string strResult;

                // 設定ファイルから値を取得します。
                strResult = this._xDoc.Element(TOP_ELEMENT).Element(this._assembly.GetName().Name).Element(key).Attribute(COMPLETE_INITIALIZE_ATTRIBUTE).Value;

                // 結果値の文字列を返還します。
                return bool.Parse(strResult);
            }
            catch
            {
                // 失敗した場合は規定値を返します。
                return defaultValue;
            }
        }

        // フォーカスの属性。
        private static readonly string SET_FOCUS_ATTRIBUTE = "SetFocus";

        /// <summary>
        /// 処理完了後にフォーカスを設定するかの判定値を取得します。
        /// </summary>
        /// <param name="key">取得する値のキー。</param>
        /// <param name="defaultValue">取得出来なかった場合は返り値。</param>
        /// <returns>取得した判定値を返します。</returns>
        public bool GetSetFocus(string key, bool defaultValue)
        {
            // 設定ファイルのドキュメントのインスタンスを判定します。
            if (this._xDoc == null)
                return defaultValue;

            try
            {
                // 結果値の文字列格納用変数。
                string strResult;

                // 設定ファイルから値を取得します。
                strResult = this._xDoc.Element(TOP_ELEMENT).Element(this._assembly.GetName().Name).Element(key).Attribute(SET_FOCUS_ATTRIBUTE).Value;

                // 結果値の文字列を返還します。
                return bool.Parse(strResult);
            }
            catch
            {
                // 失敗した場合は規定値を返します。
                return defaultValue;
            }
        }

        /// <summary>
        /// 設定ファイルを読み込みします。
        /// </summary>
        private void LoadConfig()
        {
            // 設定ファイルのパスを取得します。
            var configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), CONFIG_FILE_NAME);

            // 設定ファイルの存在確認を行います。
            if (File.Exists(configPath))
            {
                // 設定ファイルを読み込みます。
                this._xDoc = XDocument.Load(configPath);
            }
        }
    }
}
