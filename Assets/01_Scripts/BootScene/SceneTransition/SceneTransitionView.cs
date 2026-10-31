using System;
using UnityEngine;

/// <summary>
/// フェードの描画情報を定義するクラス
/// </summary>
public class SceneTransitionView : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;

    public event Action FadeStarted;
    public event Action FadeOutCompleted;
    public event Action FadeInCompleted;

    public void FadeOut() => fadeAnimator.SetTrigger("StartFade");
    public void FadeIn() => fadeAnimator.SetTrigger("FinishWait");

    // Animation Eventから呼び出す。
    public void OnFadeStarted() => FadeStarted?.Invoke();
    public void OnFadeOutCompleted() => FadeOutCompleted?.Invoke();
    public void OnFadeInCompleted() => FadeInCompleted?.Invoke();
}
