using UnityEngine;
using UniRx.Async;

/// <summary>
/// 火を消すイベント
/// </summary>
public class ExtinguishFireEvent : UseItemEvent
{
    /// <summary>
    /// 調べられた時のメッセージを表示する
    /// </summary>
    /// <returns>UniTask</returns>
    protected override async UniTask showInspectedMessagePopupAsync()
    {
        var popup = PopupCreator.Instance.CreatePopup();
        await popup.ShowAsync(MessageCreator.Create(MessageId.Fire));
    }

    /// <summary>
    /// アイテム使用後処理
    /// </summary>
    /// <param name="usedItemId">使用するアイテムのID</param>
    /// <returns>UniTask</returns>
    protected override async UniTask usedItemAsync(ItemID usedItemId)
    {
        var popup = PopupCreator.Instance.CreatePopup();
        var usedItemName = Resources.Load<ItemDataList>("ScriptableObjects/ItemDataList").Get(usedItemId).Name;
        await popup.ShowAsync(MessageCreator.Create(MessageId.PutOutFire, usedItemName));
    }
}
