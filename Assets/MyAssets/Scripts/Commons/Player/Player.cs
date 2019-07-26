using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// プレイヤー マネージャー
/// </summary>
//--------------------------------------------------------------------------/
public class Player : SingletonMonoBehaviour<Player>
{
    /// <summary>現在使っているプレイヤーデータ</summary>
    public SaveData CurrentData { get; private set; }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// プレイヤーデータを生成する
    /// </summary>
    /// <param name="name">プレイヤー名</param>
    //--------------------------------------------------------------------------/
    public void Create(string name)
    {
        CurrentData = new SaveData(name);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// プレイヤーデータをセーブする
    /// </summary>
    /// <param name="saveFileIndex">セーブするファイル番号</param>
    //--------------------------------------------------------------------------/
    public void Save(int saveFileIndex)
    {
        // プレイヤーデータがないなら何もしない
        if(CurrentData == null) return;

        // プレイ時間、日時更新
        var currentDate = GameUtility.GetUnixTime();
        CurrentData.PlayTime = currentDate - CurrentData.FirstPlayDate;
        CurrentData.LastPlayDate = currentDate;
        // 最後にいたシーン
        CurrentData.LastScene = (int)SceneManager.Instance.CurrentScene;
        // キャラクター位置情報
        var player = GameObject.FindGameObjectWithTag("Player");
        CurrentData.PlayerPositionX = player.transform.position.x;
        CurrentData.PlayerPositionY = player.transform.position.y;

        SaveManager.Instance.SetClass<SaveData>(SaveKey.PlayerData, CurrentData, saveFileIndex.ToString());
        SaveManager.Instance.Save();
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// プレイヤーデータをロードする
    /// </summary>
    /// <param name="loadFileIndex">ロードするファイル番号</param>
    //--------------------------------------------------------------------------/
    public void Load(int loadFileIndex)
    {
        // 該当データがないなら何もしない
        var loadData = GetSaveData(loadFileIndex);
        if(loadData == null) return;

        CurrentData = loadData;
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 指定されたファイル番号のセーブデータを取得する
    /// </summary>
    /// <param name="index">セーブファイル番号</param>
    //--------------------------------------------------------------------------/
    public SaveData GetSaveData(int index)
    {
        // 該当データがないならnullを返す
        if(!SaveManager.Instance.HasKey(SaveKey.PlayerData, index.ToString())) return null;

        return SaveManager.Instance.GetClass<SaveData>(SaveKey.PlayerData, new SaveData(), index.ToString());
    }
}
