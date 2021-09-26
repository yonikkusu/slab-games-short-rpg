/// <summary>
/// メッセージ生成クラス
/// </summary>
public static class MessageCreator
{
    /// <summary>
    /// メッセージを生成する
    /// </summary>
    /// <param name="id">メッセージID</param>
    /// <param name="embedMessages">埋め込むメッセージ</param>
    /// <returns>メッセージ</returns>
    public static string Create(MessageId id, params string[] embedMessages)
    {
        var rawMessage = getRawMessage(id);
        return string.Format(rawMessage, embedMessages);
    }

    /// <summary>
    /// 未加工のメッセージを取得する
    /// </summary>
    /// <param name="id">メッセージID</param>
    /// <returns>未加工のメッセージ</returns>
    private static string getRawMessage(MessageId id)
    {
        switch(id) {
            case MessageId.GetItem: return "「{0}」を手に入れました。";
            case MessageId.DontUseItem: return "ここでは使えなさそうだ。";
            case MessageId.OpenDoor: return "「{0}」を使って扉を開けた。";
            case MessageId.PutOutFire: return "「{0}」を使って火を消した。";
            default: return "";
        }
    }
}

/// <summary>
/// メッセージID
/// </summary>
public enum MessageId
{
    None,
    GetItem,
    DontUseItem,
    OpenDoor,
    PutOutFire,
}
