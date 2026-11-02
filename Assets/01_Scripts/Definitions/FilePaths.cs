using UnityEngine;
using System.IO;

namespace LastHit
{
    /// <summary>
    /// ファイルパスの定義
    /// </summary>
    public static class FilePaths
    {
        public static readonly string FOLDER_PATH = Path.Combine(Directory.GetParent(Application.dataPath).FullName,"LastHit"); // 保存データのフォルダパス
        public const string CONFIG_FILE = "ConfigData.json";    // 設定の保存ファイル名
    }
}
