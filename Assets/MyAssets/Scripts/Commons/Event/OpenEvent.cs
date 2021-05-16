using UnityEngine;
using UnityEngine.Tilemaps;

//--------------------------------------------------------------------------/
/// <summary>
/// 扉を開ける イベント
/// </summary>
//--------------------------------------------------------------------------/
public class OpenEvent : MapEvent
{
    [SerializeField] private Tilemap objectMap = default;
    [SerializeField] private SwitchFlagKey objectKey = SwitchFlagKey.None;
    [SerializeField] private ItemID keyItemId = ItemID.None;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アイテム使用時の処理
    /// </summary>
    /// <param name="usedItemId">使用するアイテムのID</param>
    //--------------------------------------------------------------------------/
    protected override void onUsedItem(ItemID usedItemId)
    {
        if(usedItemId == keyItemId) {
            // TODO: そのうちSetTileでタイル削除できるようにする
            // var deleteTilePosition = getCurrentCenterPosition();
            // objectMap.SetTile(deleteTilePosition, null);
            objectMap.gameObject.SetActive(false);
            PlayerData.Instance.FlagManager.SetSwitch(objectKey, true);
            PlayerData.Instance.ItemManager.UseItem(usedItemId);
            ItemPanel.Instance.UpdateItemList();
            DebugLogger.Log($"{usedItemId}を使って扉を開けた。");
        } else {
            DebugLogger.Log($"{usedItemId}では開かないようだ。");
        }
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 扉オブジェクトの初期化
    /// </summary>
    //--------------------------------------------------------------------------/
    public override void Initialize()
    {
        base.Initialize();
        var isOpenDoor = PlayerData.Instance.FlagManager.GetSwitch(objectKey);
        objectMap.gameObject.SetActive(!isOpenDoor);
    }
}
