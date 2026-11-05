using UnityEngine;
using System.IO;
using static LastHit.ConfigData;
using static LastHit.FilePaths;


/// <summary>
/// 設定データのセーブ、ロードの情報を定義するクラス
/// </summary>
public class ConfigDataModel : MonoBehaviour
{
    [Header ("設定の初期値を定義")]
    [SerializeField] public ConfigSO defaultConfigData;     // 設定の初期値を定義するScriptableObject
    [HideInInspector] private Config configData;            // 設定データの実体
    private static readonly string FILE_PATH = Path.Combine(FOLDER_PATH, CONFIG_FILE);  // 設定データのファイルパス

    /// <summary> アプリケーション終了時に設定データを保存するメソッド </summary>
    private void OnApplicationQuit()
    {
        SaveConfigData();
    }

    /// <summary> 設定データをjsonから読み込むメソッド </summary>
    public void LoadConfigData()
    {
        // ファイルが存在していない場合、初期値を使用して作成
        if (!File.Exists(FILE_PATH))
        {
            configData = defaultConfigData.defaultConfig;
            SaveConfigData();
        }

        configData = JsonUtility.FromJson<Config>(File.ReadAllText(FILE_PATH));

    }

    /// <summary> 設定データをjsonに書き込むメソッド </summary>
    public void SaveConfigData()
    {
        // フォルダを隠しフォルダとして作成
        Directory.CreateDirectory(FOLDER_PATH);
        File.SetAttributes(FOLDER_PATH, File.GetAttributes(FOLDER_PATH) | FileAttributes.Hidden);

        File.WriteAllText(FILE_PATH, JsonUtility.ToJson(configData, true));
    }

    public Config GetConfigData() => configData;

    public void SetConfigData(Config _configData) => configData = _configData;

}
