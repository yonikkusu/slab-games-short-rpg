using UnityEngine;
using UnityEngine.Tilemaps;
using UniRx.Async;

/// <summary>
/// 扉を開ける イベント
/// </summary>
public class OpenEvent : MapEvent
{
    [SerializeField] private GameObject[] deactiveObjects = default;
    [SerializeField] private GameObject[] activeObjects = default;
    [SerializeField] private OpenEventId openEventId = OpenEventId.None;
    [SerializeField] private ItemID keyItemId = ItemID.None;

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
            await popup.ShowAsync($"{usedItemId}では開かないようだ。");
            return;
        }
        await openAsync(usedItemId);
    }

    /// <summary>
    /// 開く
    /// </summary>
    /// <param name="usedItemId">使用するアイテムのID</param>
    /// <returns>UniTask</returns>
    private async UniTask openAsync(ItemID usedItemId)
    {
        // TODO: そのうちSetTileでタイル削除できるようにする
        // var deleteTilePosition = getCurrentCenterPosition();
        // objectMap.SetTile(deleteTilePosition, null);
        foreach(var deactiveObject in deactiveObjects) {
            deactiveObject.gameObject.SetActive(false);
        }
        foreach(var activeObject in activeObjects) {
            activeObject.gameObject.SetActive(true);
        }
        PlayerData.Instance.FlagManager.SetOpenEventSwitchOn(openEventId);
        PlayerData.Instance.ItemManager.UseItem(usedItemId);
        ItemPanel.Instance.UpdateItemList();
        var popup = PopupCreator.Instance.CreatePopup();
        await popup.ShowAsync($"{usedItemId}を使って扉を開けた。");
    }

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
}

/// <summary>
/// OpenEvent用ID
/// </summary>
public enum OpenEventId
{
    None,
    ExtinguishFire,
    OpenKitchen,
}
