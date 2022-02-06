using UnityEngine;
using UniRx.Async;

/// <summary>
/// ドアを開くイベント
/// </summary>
public class OpenDoorEvent : UseItemEvent
{
    /// <summary>
    /// 調べられた時のメッセージを表示する
    /// </summary>
    /// <returns>UniTask</returns>
    protected override async UniTask showInspectedMessagePopupAsync()
    {
        var popup = PopupCreator.Instance.CreatePopup();
        await popup.ShowAsync(MessageCreator.Create(MessageId.CloseDoor));
    }

    /// <summary>
    /// アイテム使用後処理
    /// </summary>
    /// <param name="usedItemId">使用するアイテムのID</param>
    /// <returns>UniTask</returns>
    protected override async UniTask usedItemAsync(ItemID usedItemId)
    {
        SoundManager.Instance.PlaySe(Se.OpenDoor);
        var popup = PopupCreator.Instance.CreatePopup();
        var usedItemName = MasterGetter.GetItemData(usedItemId).Name;
        await popup.ShowAsync(MessageCreator.Create(MessageId.OpenDoor, usedItemName));
    }
}
