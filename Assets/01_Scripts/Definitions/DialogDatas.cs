using System;

namespace LastHit
{
    /// <summary>
    /// ダイアログに表示するテキストを定義するクラス
    /// </summary>
    public static class DialogDatas
    {
        // ダイアログ内に入れるテキストデータを定義
        [Serializable]
        public struct DialogData
        {
            public string headerText;       // ヘッダーテキスト
            public string messageText;      // メッセージテキスト
            public string[] buttonTexts;    // ボタン内テキスト
        }

        public static readonly DialogData quitWindow = new DialogData   // ゲーム終了時
        {
            headerText = "ゲームを終了",
            messageText = "ゲームを終了しますか？\n保存されていないデータは破棄されます。",
            buttonTexts = new string[] { "終了する", "続けてプレイ" }
        };

        public static readonly DialogData discardChangesWindow = new DialogData   // 設定変更破棄時
        {
            headerText = "設定変更の破棄",
            messageText = "設定の変更を破棄しますか？\n保存されていないデータは破棄されます。",
            buttonTexts = new string[] { "破棄する", "続けて設定" }
        };
    }
}
