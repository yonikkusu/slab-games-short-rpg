using UnityEngine;
using UniRx.Async;

/// <summary>
/// アイテム獲得イベント
/// </summary>
public class ItemGetEvent : MapEvent
{
    [SerializeField] private ItemID gettingItemId = default;
    [SerializeField] private OpenEventId triggerOpenEventId = default;
    [SerializeField] private GameObject[] deactiveObjects = default;

    /// <summary>
    /// 調べられた時の処理
    /// </summary>
    /// <returns>UniTask</returns>
    protected override async UniTask onInspectedAsync()
    {
        if(checkError(gettingItemId)) return;
        if(!PlayerData.Instance.FlagManager.HasOpenEventSwitch(triggerOpenEventId)) return;
        if(PlayerData.Instance.FlagManager.HasItemEventSwitch(gettingItemId)) return;
        PlayerData.Instance.FlagManager.SetItemEventSwitchOn(gettingItemId);
        foreach(var deactiveObject in deactiveObjects) {
            deactiveObject.gameObject.SetActive(false);
        }
        var popup = PopupCreator.Instance.CreatePopup();
        await popup.ShowAsync(getMessage(gettingItemId));
        PlayerData.Instance.ItemManager.AddItem(gettingItemId);
        ItemPanel.Instance.UpdateItemList();
    }

    /// <summary>
    /// エラーチェック
    /// </summary>
    /// <param name="itemId">取得するアイテムID</param>
    /// <returns>エラーならtrue</returns>
    private bool checkError(ItemID itemId)
    {
        var isError = itemId == ItemID.None;
        if(isError) {
            DebugLogger.LogError($"アイテムIDがNoneなのでアイテムを取得できませんでした。取得したいアイテムIDを{this.name}に設定してください。");
        }
        return isError;
    }

    /// <summary>
    /// 表示メッセージを取得する
    /// </summary>
    /// <param name="itemId">取得するアイテムID</param>
    /// <returns>表示メッセージ</returns>
    private string getMessage(ItemID itemId)
    {
        var itemDataList = Resources.Load<ItemDataList>("ScriptableObjects/ItemDataList");
        var itemData = itemDataList.Get(gettingItemId);
        return MessageCreator.Create(MessageId.GetItem, itemData.Name);
    }
    /// <summary>
    /// 初期化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        var isFinished = PlayerData.Instance.FlagManager.HasOpenEventSwitch(triggerOpenEventId)
            && PlayerData.Instance.FlagManager.HasItemEventSwitch(gettingItemId);
        if(!isFinished) return;
        foreach(var deactiveObject in deactiveObjects) {
            deactiveObject.gameObject.SetActive(false);
        }
    }
}
