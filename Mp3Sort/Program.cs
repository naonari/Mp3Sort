using System;
using System.Windows.Forms;
using Microsoft.Win32;
using Mp3Sort.Resources;

namespace Mp3Sort
{
    /// <summary>
    /// アプリケーションのメイン エントリ クラスです。
    /// </summary>
    static class Program
    {
        // OSのバージョン情報が格納されたレジストリーキー。
        private static readonly string CURRENT_VERSION_KEY = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";

        // OSのバージョン情報が格納されたレジストリ名称。
        private static readonly string CURRENT_VERSION_NAME = "CurrentMajorVersionNumber";

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // OSのバージョンを取得します。
            var osMajorVersionObj = Registry.GetValue(CURRENT_VERSION_KEY, CURRENT_VERSION_NAME, "0");

            // windows10以下のOSの場合は処理を終了します。
            if (!(osMajorVersionObj is int) || (int)osMajorVersionObj < 10)
            {
                // エラーメッセージを表示します。
                MessageBox.Show(string.Format(Messages.C0001, Environment.NewLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Presentations.Mp3Sort());
        }
    }
}
