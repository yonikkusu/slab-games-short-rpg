using UnityEngine;

/// <summary>
/// アイテム合成マスタ
/// </summary>
public static class ItemSynthesisMaster
{
    /// <summary>
    /// 合成結果アイテムを取得する
    /// </summary>
    /// <param name="materialItemID1">素材アイテム1のアイテムID</param>
    /// <param name="materialItemID2">素材アイテム2のアイテムID</param>
    /// <returns>合成結果アイテム</returns>
    public static ItemData GetByPrimaryKey(ItemID materialItemID1, ItemID materialItemID2)
    {
        var resultItemID = getResultItemID(materialItemID1, materialItemID2);
        if(resultItemID == ItemID.None) return null;
        var itemDataList = Resources.Load<ItemDataList>("ScriptableObjects/ItemDataList");
        return itemDataList.Get(resultItemID);
    }

    /// <summary>
    /// 合成結果アイテムIDを取得する
    /// </summary>
    /// <param name="materialItemID1">素材アイテム1のアイテムID</param>
    /// <param name="materialItemID2">素材アイテム2のアイテムID</param>
    /// <returns>合成結果アイテムID</returns>
    private static ItemID getResultItemID(ItemID materialItemID1, ItemID materialItemID2)
    {
        if((materialItemID1 == ItemID.Tabasco && materialItemID2 == ItemID.WaterGun) ||
            (materialItemID2 == ItemID.Tabasco && materialItemID1 == ItemID.WaterGun)) {
            return ItemID.Book;
        }
        return ItemID.None;
    }
}
