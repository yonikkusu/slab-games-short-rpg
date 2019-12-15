using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// アイテム マネージャー
/// </summary>
//--------------------------------------------------------------------------/
public class ItemManager
{
    /// <summary>所持アイテムリスト</summary>
    public List<ItemData> PossessionItemList { get; private set; }

    // アイテムデータ一覧
    private ItemDataList itemDataList;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="itemIds">所持アイテムIDリスト</param>
    //--------------------------------------------------------------------------/
    public ItemManager(int[] itemIds = null)
    {
        itemDataList = Resources.Load<ItemDataList>("ScriptableObjects/ItemDataList");
        PossessionItemList = itemIds?.Select(itemId => itemDataList.ItemList[itemId]).ToList() ?? new List<ItemData>();
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アイテムを使用する
    /// </summary>
    /// <param name="index">使用するアイテムのIndex</param>
    //--------------------------------------------------------------------------/
    public void UseItem(int index)
    {
        var item = PossessionItemList[index];
        Debug.Log($"{item.Name}を使用した");
        PossessionItemList.Remove(item);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アイテムリストを取得する
    /// </summary>
    /// <returns></returns>
    //--------------------------------------------------------------------------/
    public int[] GetItemIdList()
    {
        return itemDataList.ItemList
            .Select((item, index) => new { Item = item, Index = index })
            .Where(item => PossessionItemList.Any(possessionItem => possessionItem == item.Item))
            .Select(item => item.Index).ToArray(); 
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アイテムを所持アイテムリストに追加する
    /// </summary>
    /// <param name="itemId">追加するアイテムのID</param>
    //--------------------------------------------------------------------------/
    public void AddItem(ItemID itemId)
    {
        PossessionItemList.Add(itemDataList.ItemList[(int)itemId]);
    }
}
