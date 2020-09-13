using System;
using System.Collections.Generic;
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

    /// <summary>アイテム一覧</summary>
    public List<ItemData> ItemList => itemList;
}

//--------------------------------------------------------------------------/
/// <summary>
/// アイテムデータ
/// </summary>
//--------------------------------------------------------------------------/
[Serializable]
public class ItemData
{
    [SerializeField] private string name = default;
    [SerializeField] private Sprite sprite = default;

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
    Key,
    Book,
    Tabasco,
    WaterGun,
}
