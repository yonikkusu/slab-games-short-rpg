using UnityEngine;

/// <summary>
/// マスタを取得する
/// </summary>
public static class MasterGetter
{
    /// <summary>
    /// アイテムデータを取得する
    /// </summary>
    /// <param name="itemId">アイテムID</param>
    /// <returns>アイテムデータ</returns>
    public static ItemData GetItemData(ItemID itemId)
    {
        var itemDataList = Resources.Load<ItemDataList>("ScriptableObjects/ItemDataList");
        return itemDataList.Get(itemId);
    }
}
