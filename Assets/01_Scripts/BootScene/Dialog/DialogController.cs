using System;
using UnityEngine;
using UniRx;
using static LastHit.DialogDatas;


public class DialogController : MonoBehaviour
{
    [Header("Viewを登録")]
    [SerializeField] private DialogView view;

    private Action confirmButtonClick;  // 確定ボタンが押されたときのコールバック
    private Action cancelButtonClick;   // キャンセルボタンが押されたときのコールバック

    public static DialogController Instance { get; private set; }
    private void Awake()
    {
        // シングルトンの設定
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // 最初に非表示にしておく
        view.SetActiveSingleButtonDialog(false);
        view.SetActiveTwoButtonDialog(false);
    }

    private void Start()
    {
        // ボタンのイベントを登録する
        view.ConfirmButtonClick
            .Subscribe(_ => OnConfirmButtonClicked())
            .AddTo(this);

        view.CancelButtonClick
            .Subscribe(_ => OnCancelButtonClicked())
            .AddTo(this);
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    /// <summary> ダイアログの表示、描画切り替えを行うメソッド </summary>
    /// <param name="_showTexts"> 表示するテキストデータ </param>
    /// <param name="_confirmButtonClick"> 確定ボタンが押されたときのコールバック </param>
    /// <param name="_cancelButtonClick"> キャンセルボタンが押されたときのコールバック </param>
    public void ShowDialog(DialogData _showTexts, Action _confirmButtonClick, Action _cancelButtonClick = null)
    {

        confirmButtonClick = _confirmButtonClick;
        cancelButtonClick = _cancelButtonClick;

        view.SetDialogTexts(_showTexts);

        int buttonCount = _showTexts.buttonTexts?.Length ?? 0;
        view.SetActiveSingleButtonDialog(buttonCount == 1);
        view.SetActiveTwoButtonDialog(buttonCount == 2);
    }

    /// <summary> 確定ボタンが押された時の処理を行うメソッド </summary>
    private void OnConfirmButtonClicked()
    {
        CloseDialog(confirmButtonClick);
    }

    /// <summary> キャンセルボタンが押された時の処理を行うメソッド </summary>
    private void OnCancelButtonClicked()
    {
        CloseDialog(cancelButtonClick);
    }

    /// <summary>ダイアログを閉じて、コールバックを実行するメソッド </summary>
    /// <param name="callback"> 実行するコールバック </param>
    private void CloseDialog(Action callback)
    {
        confirmButtonClick = null;
        cancelButtonClick = null;
        view.SetActiveSingleButtonDialog(false);
        view.SetActiveTwoButtonDialog(false);

        callback?.Invoke();
    }
}
