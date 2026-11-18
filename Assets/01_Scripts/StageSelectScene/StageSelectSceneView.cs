using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージ選択画面のUI表示とクリック通知を管理するクラス
/// </summary>
public class StageSelectSceneView : MonoBehaviour
{
    [SerializeField] private float CENTER_CARD_SCALE = 1.12f; // 中央のカードの最大拡大率。1.12なら元のサイズの112%。
    [SerializeField] private float SLIDE_SPEED = 0.16f; // 中央位置へ近づく速さの目安（秒）。小さいほど素早く揃う。

    [Header("画面上のボタンの登録")]
    [SerializeField] private Button backButton;             // 戻るボタン
    [SerializeField] private Button gameStartButton;        // ゲーム開始ボタン
    [SerializeField] private RectTransform stageCarousel;   // スライド枠

    private RectTransform[] stageCards; // Prefabに配置されたカードを、子オブジェクトの並び順で保持する。
    private float cardSpacingRatio; // Prefabに並べた先頭2枚の中心間隔。親の横幅に対する割合で保持する。
    private float scrollPosition; // 現在、中央にある位置。2は添字2のカード、2.5は添字2と3の中間を表す。
    private float targetPosition; // 中央に揃える移動先。カード1枚分を1として数える。
    private float slideVelocity; // SmoothDampが次のフレームへ引き継ぐ移動速度。単位はカード枚数/秒。

    // 各ボタンのクリック通知を送信
    public IObservable<Unit> GameStartButtonClick => gameStartButton.OnClickAsObservable();
    public IObservable<Unit> BackButtonClick => backButton.OnClickAsObservable();

    private void Awake()
    {
        // カードを取得し、クリック時の移動先を設定する
        stageCards = new RectTransform[stageCarousel.childCount];
        for (int i = 0; i < stageCards.Length; i++)
        {
            int index = i;
            var card = (RectTransform)stageCarousel.GetChild(i);
            stageCards[i] = card;
            card.GetComponent<Button>().onClick.AddListener(() => targetPosition = index);
        }

        if (stageCards.Length > 1)
            cardSpacingRatio = Mathf.Abs(stageCards[1].anchorMin.x - stageCards[0].anchorMin.x);

        PositionCards();
    }

    private void Update()
    {
        if (scrollPosition == targetPosition)
            return;

        // 中央に移動していく
        scrollPosition = Mathf.SmoothDamp(scrollPosition, targetPosition, ref slideVelocity,
            SLIDE_SPEED, Mathf.Infinity, Time.unscaledDeltaTime);

        if (Mathf.Abs(scrollPosition - targetPosition) < 0.001f)
        {
            scrollPosition = targetPosition;
            slideVelocity = 0f;
        }

        PositionCards();
    }

    /// <summary> カードの移動拡縮処理 </summary>
    private void PositionCards()
    {
        for (int i = 0; i < stageCards.Length; i++)
        {
            var card = stageCards[i];
            float offset = i - scrollPosition;
            float center = 0.5f + offset * cardSpacingRatio;
            // 中央から1枚分以内のカードを、中央に近いほど拡大する
            float emphasis = 1f - Mathf.Clamp01(Mathf.Abs(offset));
            card.localScale = Vector3.one * Mathf.Lerp(1f, CENTER_CARD_SCALE, emphasis);
            // 左右のアンカーを一緒に移動し、Prefabの幅を保つ
            card.anchorMin = new Vector2(center, card.anchorMin.y);
            card.anchorMax = new Vector2(center, card.anchorMax.y);
        }
    }
}
