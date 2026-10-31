using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン遷移の情報を定義するクラス
/// </summary>
public class SceneTransitionModel : MonoBehaviour
{
    [field: Header("フェード暗転後の待機時間を設定")]
    [field: SerializeField, Range(0.01f, 2f)] public float fadeWaitTime { get; private set; } = 0.5f;
    public bool isFading { get; private set; }          // フェード中かのフラグ
    public string nextSceneName { get; private set; }   // 切り替えるシーン名

    /// <summary> シーンの追加、切り替えを行うメソッド </summary>
    /// <param name="_loadMode"> シーンを追加、切り替えにするか指定 </param>
    public async UniTask ExecuteSceneLoad(LoadSceneMode loadMode = LoadSceneMode.Single)
    {
        await SceneManager.LoadSceneAsync(nextSceneName, loadMode);
        nextSceneName = null;
    }

    public void SetIsFading(bool _isFading) => isFading = _isFading;

    public void SetNextSceneName(string _nextSceneName) => nextSceneName = _nextSceneName;
}
