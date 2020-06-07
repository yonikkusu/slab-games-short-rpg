using System;
using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// セーブデータ
/// </summary>
//--------------------------------------------------------------------------/
[Serializable]
public class SaveData
{
    // 直接参照可能なデータ
    /// <summary>プレイヤー名</summary>
    [SerializeField] public string Name;
    /// <summary>最初のプレイ日時</summary>
    [SerializeField] public long FirstPlayDate;
    /// <summary>最終プレイ日時</summary>
    [SerializeField] public long LastPlayDate;
    /// <summary>プレイ時間</summary>
    [SerializeField] public long PlayTime;
    /// <summary>最後にいたシーン</summary>
    [SerializeField] public int LastScene;
    /// <summary>プレイヤー位置X</summary>
    [SerializeField] public float PlayerPositionX;
    /// <summary>プレイヤー位置Y</summary>
    [SerializeField] public float PlayerPositionY;

    // 直接参照しないデータ
    /// <summary>スイッチフラグリスト</summary>
    [SerializeField] public SwitchFlag[] SwitchFlagList;
    /// <summary>カウントフラグリスト</summary>
    [SerializeField] public CountFlag[] CountFlagList;
    /// <summary>所持アイテムIDリスト</summary>
    [SerializeField] public int[] ItemIds;

    /// <summary>プレイヤーの座標</summary>
    public Vector2 PlayerPosition => new Vector2(PlayerPositionX, PlayerPositionY);

    //--------------------------------------------------------------------------/
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="name">プレイヤー名</param>
    //--------------------------------------------------------------------------/
    public SaveData(string name = "")
    {
        Name = name;
        FirstPlayDate = GameUtility.GetUnixTime();
        LastPlayDate = FirstPlayDate;
        PlayTime = 0;
        LastScene = (int)SceneName.GuestRoom1;
        PlayerPositionX = 0.5f;
        PlayerPositionY = 0.5f;
        ItemIds = new int[0];
        SwitchFlagList = new SwitchFlag[0];
        CountFlagList = new CountFlag[0];
    }
}

//--------------------------------------------------------------------------/
/// <summary>
/// スイッチフラグ
/// </summary>
//--------------------------------------------------------------------------/
[Serializable]
public struct SwitchFlag
{
    /// <summary>キー</summary>
    [SerializeField] public string Key;
    /// <summary>値</summary>
    [SerializeField] public bool Value;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">セットする値</param>
    //--------------------------------------------------------------------------/
    public SwitchFlag(string key, bool value)
    {
        Key = key;
        Value = value;
    }
}

//--------------------------------------------------------------------------/
/// <summary>
/// カウントフラグ
/// </summary>
//--------------------------------------------------------------------------/
[Serializable]
public struct CountFlag
{
    /// <summary>キー</summary>
    [SerializeField] public string Key;
    /// <summary>値</summary>
    [SerializeField] public int Value;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">セットする値</param>
    //--------------------------------------------------------------------------/
    public CountFlag(string key, int value)
    {
        Key = key;
        Value = value;
    }
}
