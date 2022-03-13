using UnityEngine;
using UniRx.Async;

/// <summary>
/// アイテム使用イベント
/// </summary>
public abstract class UseItemEvent : MapEvent
{
    [SerializeField] private GameObject[] deactiveObjects = default;
    [SerializeField] private GameObject[] activeObjects = default;
    [SerializeField] private OpenEventId openEventId = OpenEventId.None;
    [SerializeField] private ItemID keyItemId = ItemID.None;

    /// <summary>
    /// 初期化
    /// </summary>
    public override void Initialize()
    {
        base.Initialize();
        var isOpen = PlayerData.Instance.FlagManager.HasOpenEventSwitch(openEventId);
        if(!isOpen) return;
        foreach(var deactiveObject in deactiveObjects) {
            deactiveObject.gameObject.SetActive(!isOpen);
        }
        foreach(var activeObject in activeObjects) {
            activeObject.gameObject.SetActive(isOpen);
        }
    }

    /// <summary>
    /// 調べられた時の処理
    /// </summary>
    /// <returns>UniTask</returns>
    protected override async UniTask onInspectedAsync()
    {
        if(PlayerData.Instance.FlagManager.HasOpenEventSwitch(openEventId)) return;
        await showInspectedMessagePopupAsync();
    }

    /// <summary>
    /// アイテム使用時の処理
    /// </summary>
    /// <param name="usedItemId">使用するアイテムのID</param>
    /// <returns>UniTask</returns>
    protected override async UniTask onUsedItemAsync(ItemID usedItemId)
    {
        if(usedItemId == ItemID.None) return;
        if(PlayerData.Instance.FlagManager.HasOpenEventSwitch(openEventId)) return;
        if(usedItemId != keyItemId) {
            var popup = PopupCreator.Instance.CreatePopup();
            await popup.ShowAsync(MessageCreator.Create(MessageId.DontUseItem));
            return;
        }
        setActiveObjects();
        updateItemModelAndView(usedItemId);
        PlayerData.Instance.FlagManager.SetOpenEventSwitchOn(openEventId);
        await usedItemAsync(usedItemId);
    }

    /// <summary>
    /// 調べられた時のメッセージを表示する
    /// </summary>
    /// <returns>UniTask</returns>
    protected abstract UniTask showInspectedMessagePopupAsync();

    /// <summary>
    /// アイテム使用後処理
    /// </summary>
    /// <param name="usedItemId">使用するアイテムのID</param>
    /// <returns>UniTask</returns>
    protected abstract UniTask usedItemAsync(ItemID usedItemId);

    /// <summary>
    /// アイテム使用後の指定オブジェクトの表示/非表示を行う
    /// </summary>
    private void setActiveObjects()
    {
        foreach(var deactiveObject in deactiveObjects) {
            deactiveObject.gameObject.SetActive(false);
        }
        foreach(var activeObject in activeObjects) {
            activeObject.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 所持アイテムのModelとViewを更新する
    /// </summary>
    /// <param name="usedItemId">使用するアイテムのID</param>
    private void updateItemModelAndView(ItemID usedItemId)
    {
        PlayerData.Instance.ItemManager.UseItem(usedItemId);
        ItemPanel.Instance.RenderView();
    }
}
