using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
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
    private static readonly byte[] ENCRYPTION_KEY = Convert.FromBase64String("wnPRxZm1gdX3oWmZKU8svDx5yYoH4VDX6YW8CsRgt7E=");
    private static readonly byte[] AUTHENTICATION_KEY = Convert.FromBase64String("aMkHjn86zCpxcIJtYmrQoBK3FaNyEhMtZXjOwJv5Vn4=");

    /// <summary> アプリケーション終了時に設定データを保存するメソッド </summary>
    private void OnApplicationQuit()
    {
        SaveConfigData();
    }

    /// <summary> 設定データをjsonから読み込むメソッド </summary>
    public void LoadConfigData()
    {
        if (File.Exists(FILE_PATH))
        {
            try
            {
                string json = Decrypt(File.ReadAllText(FILE_PATH));
                configData = JsonConvert.DeserializeObject<Config>(json);
                return;
            }
            // 読み込み失敗時はファイルを削除
            catch (Exception exception) when (exception is JsonException || exception is IOException || exception is CryptographicException || exception is FormatException)
            {
                File.Delete(FILE_PATH);
            }
        }

        // 読み込み失敗、ファイルが存在しない場合は新規作成
        configData = defaultConfigData.defaultConfig;
        SaveConfigData();
    }

    /// <summary> 設定データをjsonに書き込むメソッド </summary>
    public void SaveConfigData()
    {
        // フォルダを隠しフォルダとして作成
        Directory.CreateDirectory(FOLDER_PATH);
        File.SetAttributes(FOLDER_PATH, File.GetAttributes(FOLDER_PATH) | FileAttributes.Hidden);

        File.WriteAllText(FILE_PATH, Encrypt(JsonConvert.SerializeObject(configData, Formatting.Indented)));
    }

    /// <summary> JSONを暗号化し、改ざん検出用の署名を付ける </summary>
    private static string Encrypt(string json)
    {
        using var aes = Aes.Create();
        aes.Key = ENCRYPTION_KEY;
        aes.GenerateIV();
        using var encryptor = aes.CreateEncryptor();
        byte[] plain = Encoding.UTF8.GetBytes(json);
        byte[] cipher = encryptor.TransformFinalBlock(plain, 0, plain.Length);
        byte[] data = new byte[aes.IV.Length + cipher.Length + 32];
        Buffer.BlockCopy(aes.IV, 0, data, 0, aes.IV.Length);
        Buffer.BlockCopy(cipher, 0, data, aes.IV.Length, cipher.Length);
        using var hmac = new HMACSHA256(AUTHENTICATION_KEY);
        byte[] signature = hmac.ComputeHash(data, 0, data.Length - 32);
        Buffer.BlockCopy(signature, 0, data, data.Length - 32, 32);
        return Convert.ToBase64String(data);
    }

    /// <summary> 改ざんを確認して暗号化データをJSONへ復号する </summary>
    private static string Decrypt(string encrypted)
    {
        byte[] data = Convert.FromBase64String(encrypted);
        if (data.Length < 64 || (data.Length - 48) % 16 != 0)
            throw new CryptographicException("設定ファイルが破損しています。");

        using var hmac = new HMACSHA256(AUTHENTICATION_KEY);
        byte[] signature = hmac.ComputeHash(data, 0, data.Length - 32);
        int difference = 0;
        for (int i = 0; i < 32; i++)
            difference |= signature[i] ^ data[data.Length - 32 + i];
        if (difference != 0)
            throw new CryptographicException("設定ファイルが改ざんされています。");

        using var aes = Aes.Create();
        aes.Key = ENCRYPTION_KEY;
        byte[] iv = new byte[16];
        Buffer.BlockCopy(data, 0, iv, 0, iv.Length);
        aes.IV = iv;
        using var decryptor = aes.CreateDecryptor();
        return Encoding.UTF8.GetString(decryptor.TransformFinalBlock(data, 16, data.Length - 48));
    }

    public Config GetConfigData() => configData;

    public void SetConfigData(Config _configData) => configData = _configData;

}
