using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーン切り替え処理の制御を行うクラス
/// </summary>
public class SceneTransitionController : MonoBehaviour
{
    [Header("Model,Viewを登録")]
    [SerializeField] private SceneTransitionView view;
    [SerializeField] private SceneTransitionModel model;

    public static SceneTransitionController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        view.FadeStarted += OnFadeBegun;
        view.FadeOutCompleted += OnFadeOutEnd;
        view.FadeInCompleted += OnFadeCompleted;
    }

    private void OnDisable()
    {
        view.FadeStarted -= OnFadeBegun;
        view.FadeOutCompleted -= OnFadeOutEnd;
        view.FadeInCompleted -= OnFadeCompleted;
    }

    /// <summary> シーン切り替えを制御するメソッド </summary>
    /// <param name="_nextSceneName"> 切り替えるシーン名 </param>
    /// <param name="_loadMode"> シーンを切り替えるか追加するか </param>
    /// <param name="_isFadeEnabled"> フェード処理を挟むかのフラグ </param>
    public async void LoadScene(string _nextSceneName, LoadSceneMode _loadMode = LoadSceneMode.Single, bool _isFadeEnabled = false)
    {
        if (model.isFading) return;
        model.SetNextSceneName(_nextSceneName);

        if (_loadMode == LoadSceneMode.Additive)
        {
            await model.ExecuteSceneLoad(_loadMode);
            return;
        }


        if (!_isFadeEnabled)
        {
            await model.ExecuteSceneLoad();
            return;
        }

        view.FadeOut();
    }

    /// <summary> フェードアウト終了時にシーンを切り替えるメソッド </summary>
    private async UniTask WaitSceneChange()
    {
        await model.ExecuteSceneLoad();
        if (model.isFading)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(model.fadeWaitTime), ignoreTimeScale: true, cancellationToken: destroyCancellationToken);
            view.FadeIn();
        }
    }

    /// <summary> シーンを破棄するメソッド </summary>
    /// <param name="_unLoadSceneName"> 破棄するシーン名 </param>
    public void UnloadScene(string _unLoadSceneName)=> SceneManager.UnloadSceneAsync(_unLoadSceneName);

    /// <summary> Viewで受け取ったアニメーションイベントをModelに渡すメソッド </summary>
    private void OnFadeBegun() => model.SetIsFading(true);

    /// <summary> Viewで受け取ったアニメーションイベントをModelに渡すメソッド </summary>
    private void OnFadeCompleted() => model.SetIsFading(false);

    /// <summary> Viewで受け取ったアニメーションイベントカラ非同期処理を開始するメソッド </summary>
    private void OnFadeOutEnd() => WaitSceneChange().Forget();

    

}
