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
        LastScene = (int)Scene.Field;
        PlayerPositionX = 0f;
        PlayerPositionY = 0f;
    }
}
