using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// プレイヤーデータ
/// </summary>
//--------------------------------------------------------------------------/
public class PlayerData : SingletonMonoBehaviour<PlayerData>
{
    /// <summary>現在使っているプレイヤーデータ</summary>
    public SaveData CurrentData { get; private set; }
    /// <summary>フラグマネージャー</summary>
    public FlagManager FlagManager { get; private set; }
    /// <summary>アイテムマネージャー</summary>
    public ItemManager ItemManager { get; private set; }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// プレイヤーデータを生成する
    /// </summary>
    /// <param name="name">プレイヤー名</param>
    //--------------------------------------------------------------------------/
    public void Create(string name)
    {
        CurrentData = new SaveData(name);
        FlagManager = new FlagManager();
        ItemManager = new ItemManager();
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
        CurrentData.LastScene = (int)SceneManagerExtension.GetCurrentSceneName();
        // キャラクター位置情報
        var player = GameObject.FindGameObjectWithTag("Player");
        CurrentData.PlayerPositionX = player.transform.position.x;
        CurrentData.PlayerPositionY = player.transform.position.y;
        // スイッチフラグ、カウントフラグ
        CurrentData.SwitchFlagList = FlagManager.GetSwitchList();
        CurrentData.CountFlagList = FlagManager.GetCountList();
        // 所持アイテム
        CurrentData.ItemIds = ItemManager.GetItemIdList();

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

        // スイッチフラグリスト、カウントフラグリストをもとにフラグマネージャーを初期化
        FlagManager = new FlagManager(CurrentData.SwitchFlagList, CurrentData.CountFlagList);

        // 所持アイテムIDリストをもとにアイテムマネージャーを初期化
        ItemManager = new ItemManager(CurrentData.ItemIds);
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

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 指定されたファイル番号のセーブデータを取得する
    /// </summary>
    /// <param name="index">セーブファイル番号</param>
    //--------------------------------------------------------------------------/
    public void AddItem(ItemID itemId)
    {
        ItemManager.AddItem(itemId);
    }
}
