
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UniRx;
using static LastHit.DialogDatas;

[Serializable]
struct ButtonData   // ボタンのデータ構造
{
    public Button button;           // ボタン
    public TextMeshProUGUI text;    // ボタン内のテキスト
}

[Serializable]
struct DialogWindow     // ダイアログウィンドウのデータ構造
{
    public GameObject window;       // ウィンドウ本体
    public TextMeshProUGUI header;  // ヘッダーテキスト
    public TextMeshProUGUI message; // メッセージテキスト
    public ButtonData[] buttons;    // ウィンドウ内にあるボタン
}

public class DialogView : MonoBehaviour
{
    [SerializeField] private DialogWindow singleButtonDialog;   // 1ボタンのダイアログ
    [SerializeField] private DialogWindow twoButtonDialog;      // 2ボタンのダイアログ

    // ウィンドウ内ボタンのクリック通知を送信
    public IObservable<Unit> ConfirmButtonClick => singleButtonDialog.buttons[0].button
        .OnClickAsObservable()
        .Merge(twoButtonDialog.buttons[0].button.OnClickAsObservable());

    public IObservable<Unit> CancelButtonClick => twoButtonDialog.buttons[1].button
        .OnClickAsObservable();

    /// <summary>
    /// ダイアログウィンドウに表示データを入れ込むメソッド
    /// </summary>
    /// <param name="_data"> 表示するデータ </param>
    public void SetDialogTexts(DialogData _data)
    {
        // ボタンの数に応じて入れ込むダイアログを切り替える
        var dialog = _data.buttonTexts.Length == 1 ? singleButtonDialog : twoButtonDialog;

        dialog.header.text = _data.headerText;
        dialog.message.text = _data.messageText;
        for (int i = 0; i < _data.buttonTexts.Length; i++)
            dialog.buttons[i].text.text = _data.buttonTexts[i];
    }

    /// <summary> 1ボタンダイアログの表示・非表示を切り替えるメソッド </summary>
    /// <param name="_isActive"> 表示フラグ </param>
    public void SetActiveSingleButtonDialog(bool _isActive) => singleButtonDialog.window.SetActive(_isActive);

    /// <summary> 2ボタンダイアログの表示・非表示を切り替えるメソッド </summary>
    /// <param name="_isActive"> 表示フラグ </param>
    public void SetActiveTwoButtonDialog(bool _isActive) => twoButtonDialog.window.SetActive(_isActive);
}

