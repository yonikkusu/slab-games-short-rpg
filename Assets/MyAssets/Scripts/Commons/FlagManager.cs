using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// フラグマネージャー
/// </summary>
public class FlagManager
{
    /// <summary>スイッチフラグのDictionary</summary>
    private Dictionary<string, bool> switchFlagDictionay = new Dictionary<string, bool>();
    /// <summary>カウントフラグのDictionary</summary>
    private Dictionary<string, int> countFlagDictionay = new Dictionary<string, int>();

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="switchFlagList">初期化に使うスイッチフラグリスト</param>
    /// <param name="countFlagList">初期化に使うカウントフラグリスト</param>
    public FlagManager(SwitchFlag[] switchFlagList = null, CountFlag[] countFlagList = null)
    {
        SetSwitchList(switchFlagList);
        SetCountList(countFlagList);
    }

    #region SwitchFlag
    /// <summary>
    /// スイッチの値をセットする
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">セットするbool値</param>
    public void SetSwitch(SwitchFlagKey key, bool value)
    {
        switchFlagDictionay[key.ToString()] = value;
    }

    /// <summary>
    /// スイッチの値を取得する
    /// 指定したキーに対応する値がない場合はfalseを返す
    /// </summary>
    /// <param name="key">キー</param>
    /// <returns></returns>
    public bool GetSwitch(SwitchFlagKey key)
    {
        if(switchFlagDictionay.ContainsKey(key.ToString())) {
            return switchFlagDictionay[key.ToString()];
        }
        Debug.Log($"key({key})に対応する値がなかったので、デフォルト値(false)を返しました。");
        return false;
    }

    /// <summary>
    /// スイッチリストを取得する
    /// </summary>
    /// <returns></returns>
    public SwitchFlag[] GetSwitchList()
    {
        return switchFlagDictionay
            .ToList()
            .Select(s => new SwitchFlag(s.Key, s.Value))
            .ToArray();
    }

    /// <summary>
    /// スイッチリストをセットする
    /// </summary>
    /// <param name="countFlagList">スイッチフラグリスト</param>
    public void SetSwitchList(SwitchFlag[] switchFlagList)
    {
        if(switchFlagList == null) { return; }

        switchFlagDictionay.Clear();
        foreach(var switchFlag in switchFlagList) {
            switchFlagDictionay[switchFlag.Key] = switchFlag.Value;
        }
    }
    #endregion

    #region CountFlag
    /// <summary>
    /// カウントの値をセットする
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">セットするint値</param>
    public void SetCount(CountFlagKey key, int value)
    {
        countFlagDictionay[key.ToString()] = value;
    }

    /// <summary>
    /// カウントの値を取得する
    /// 指定したキーに対応する値がない場合は0を返す
    /// </summary>
    /// <param name="key">キー</param>
    /// <returns></returns>
    public int GetCount(CountFlagKey key)
    {
        if(countFlagDictionay.ContainsKey(key.ToString())) {
            return countFlagDictionay[key.ToString()];
        }
        Debug.Log($"key({key})に対応する値がなかったので、デフォルト値(0)を返しました。");
        return 0;
    }

    /// <summary>
    /// カウントリストを取得する
    /// </summary>
    /// <returns></returns>
    public CountFlag[] GetCountList()
    {
        return countFlagDictionay
            .ToList()
            .Select(s => new CountFlag(s.Key, s.Value))
            .ToArray();
    }

    /// <summary>
    /// カウントリストをセットする
    /// </summary>
    /// <param name="countFlagList">カウントフラグリスト</param>
    public void SetCountList(CountFlag[] countFlagList)
    {
        if(countFlagList == null) { return; }

        countFlagDictionay.Clear();
        foreach(var countFlag in countFlagList) {
            countFlagDictionay[countFlag.Key] = countFlag.Value;
        }
    }
    #endregion

    #region AutoEventSwitchFlag
    /// <summary>
    /// 自動イベント用スイッチの値をONにする
    /// </summary>
    /// <param name="id">自動イベントID</param>
    public void SetAutoEventSwitchOn(AutoEventId id)
    {
        var key = $"{SwitchFlagKey.SwitchAutoEvent}_{id}";
        switchFlagDictionay[key] = true;
    }

    /// <summary>
    /// 自動イベント用スイッチの値があるか調べる
    /// </summary>
    /// <param name="id">自動イベントID</param>
    /// <returns>キーがあればtrue</returns>
    public bool HasAutoEventSwitch(AutoEventId id)
    {
        var key = $"{SwitchFlagKey.SwitchAutoEvent}_{id}";
        return switchFlagDictionay.ContainsKey(key);
    }
    #endregion
}

/// <summary>
/// スイッチフラグのキー
/// </summary>
public enum SwitchFlagKey
{
    None,
    SwitchDoorCorridor1FKitchen,
    SwitchAutoEvent,
}

/// <summary>
/// カウントフラグのキー
/// </summary>
public enum CountFlagKey
{
    Count1,
}
