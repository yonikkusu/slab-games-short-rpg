using System.Collections.Generic;
using System.Linq;

/// <summary>
/// アイテム マネージャー
/// </summary>
public class ItemManager
{
    // 所持可能アイテム上限
    public const int MaxItemNum = 8;

    /// <summary>所持アイテムリスト</summary>
    public List<ItemData> PossessionItemList { get; private set; }
    /// <summary>選択中のアイテムIndex</summary>
    public int SelectedItemIndex { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="itemIds">所持アイテムIDリスト</param>
    public ItemManager(int[] itemIds = null)
    {
        PossessionItemList = itemIds?.Select(itemId => MasterGetter.GetItemData((ItemID)itemId)).ToList() ?? new List<ItemData>();
        SelectedItemIndex = 0;
    }

    /// <summary>
    /// アイテムを使用する
    /// </summary>
    /// <param name="index">使用するアイテムのID</param>
    public void UseItem(ItemID itemId)
    {
        var item = PossessionItemList.FirstOrDefault(i => i.ID == itemId);
        if(item == null) {
            DebugLogger.LogError($"所持アイテムに{itemId}がないのでアイテムを使用できませんでした。");
            return;
        }
        DebugLogger.Log($"{item.Name}を使用した");
        PossessionItemList.Remove(item);
    }

    /// <summary>
    /// 所持アイテムのIDリストを取得する
    /// </summary>
    /// <returns></returns>
    public int[] GetItemIdList()
    {
        return PossessionItemList.Select(possessionItem => (int)possessionItem.ID).ToArray();
    }

    /// <summary>
    /// アイテムを所持アイテムリストに追加する
    /// </summary>
    /// <param name="itemId">追加するアイテムのID</param>
    public void AddItem(ItemID itemId)
    {
        var item = MasterGetter.GetItemData(itemId);
        if(item == null) {
            DebugLogger.LogError($"{itemId}に対応するアイテム情報がないので、所持アイテムを追加できませんでした。");
            return;
        }
        PossessionItemList.Add(item);
    }

    /// <summary>
    /// 選択中のアイテムを更新する
    /// </summary>
    /// <param name="index">新しく選択するアイテムIndex</param>
    public void UpdateSelectedItem(int index)
    {
        SelectedItemIndex =
            index < 0 ? MaxItemNum - 1 : 
            index >= MaxItemNum ? 0 :
            index;
    }

    /// <summary>
    /// 選択中のアイテムIDを取得する
    /// </summary>
    /// <returns>選択中のアイテムID</returns>
    public ItemID GetSelectedItemId()
    {
        if(PossessionItemList.Count <= SelectedItemIndex) return ItemID.None;
        return PossessionItemList[SelectedItemIndex].ID;
    }
}
