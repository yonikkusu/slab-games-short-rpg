using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// アイテムデータリスト
/// </summary>
//--------------------------------------------------------------------------/
[CreateAssetMenu]
public class ItemDataList : ScriptableObject
{
    [SerializeField] private List<ItemData> itemList = new List<ItemData>();

    //--------------------------------------------------------------------------/
    /// <summary>
    /// データを取得する
    /// </summary>
    /// <param name="id">取得したいアイテムのアイテムID</param>
    /// <returns>アイテム情報</returns>
    //--------------------------------------------------------------------------/
    public ItemData Get(ItemID id)
    {
        return itemList.FirstOrDefault(item => item.ID == id);
    }
}

//--------------------------------------------------------------------------/
/// <summary>
/// アイテムデータ
/// </summary>
//--------------------------------------------------------------------------/
[Serializable]
public class ItemData
{
    [SerializeField] private ItemID id = ItemID.None;
    [SerializeField] private string name = default;
    [SerializeField] private Sprite sprite = default;

    /// <summary>アイテムID</summary>
    public ItemID ID => id;
    /// <summary>アイテム名</summary>
    public string Name => name;
    /// <summary>アイテム画像</summary>
    public Sprite Sprite => sprite;
}

//--------------------------------------------------------------------------/
/// <summary>
/// アイテムID
/// </summary>
//--------------------------------------------------------------------------/
public enum ItemID
{
    None,
    Key,
    Book,
    Tabasco,
    WaterGun,
}
