using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// セーブマネージャー
/// </summary>
//--------------------------------------------------------------------------/
public class SaveManager : SingletonMonoBehaviour<SaveManager>
{
    #region setValue
    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応するbool値をPlayerPrefsにセットする
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">セットするbool値</param>
    /// <param name="subKey">サブキー</param>
    //--------------------------------------------------------------------------/
    public void SetBool(SaveKey key, bool value, string subKey = "")
    {
        // NOTE: false=0, true=1としてセットする
        var valueInt = value ? 1 : 0;
        PlayerPrefs.SetInt(key.ToString() + subKey, valueInt);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応するint値をPlayerPrefsにセットする
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">セットするint値</param>
    /// <param name="subKey">サブキー</param>
    //--------------------------------------------------------------------------/
    public void SetInt(SaveKey key, int value, string subKey = "")
    {
        PlayerPrefs.SetInt(key.ToString() + subKey, value);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応するfloat値をPlayerPrefsにセットする
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">セットするfloat値</param>
    /// <param name="subKey">サブキー</param>
    //--------------------------------------------------------------------------/
    public void SetFloat(SaveKey key, float value, string subKey = "")
    {
        PlayerPrefs.SetFloat(key.ToString() + subKey, value);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応するstring値をPlayerPrefsにセットする
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="value">セットするstring値</param>
    /// <param name="subKey">サブキー</param>
    //--------------------------------------------------------------------------/
    public void SetString(SaveKey key, string value, string subKey = "")
    {
        PlayerPrefs.SetString(key.ToString() + subKey, value);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応するクラスをPlayerPrefsにセットする
    /// </summary>
    /// <typeparam name="T">任意の型</typeparam>
    /// <param name="key">キー</param>
    /// <param name="obj">セットするクラス</param>
    /// <param name="subKey">サブキー</param>
    //--------------------------------------------------------------------------/
    public void SetClass<T>(SaveKey key, T obj, string subKey = "")
    {
        // T型をJson形式のstringに変換し、string型としてセットする
        var json = JsonUtility.ToJson(obj);
        Debug.Log(json);
        PlayerPrefs.SetString(key.ToString() + subKey, json);
    }
    #endregion

    #region getValue
    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応するbool値を取得する
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="defaultValue">初期値</param>
    /// <param name="subKey">サブキー</param>
    /// <returns></returns>
    //--------------------------------------------------------------------------/
    public bool GetBool(SaveKey key, bool defaultValue = false, string subKey = "")
    {
        // NOTE: false=0, true=1として取得する
        var defaultValueInt = defaultValue ? 1 : 0;
        var getValue = PlayerPrefs.GetInt(key.ToString() + subKey, defaultValueInt);
        return getValue == 1 ? true : false;
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応するint値を取得する
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="defaultValue">初期値</param>
    /// <param name="subKey">サブキー</param>
    /// <returns></returns>
    //--------------------------------------------------------------------------/
    public int GetInt(SaveKey key, int defaultValue = 0, string subKey = "")
    {
        return PlayerPrefs.GetInt(key.ToString() + subKey, defaultValue);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応するfloat値を取得する
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="defaultValue">初期値</param>
    /// <param name="subKey">サブキー</param>
    /// <returns></returns>
    //--------------------------------------------------------------------------/
    public float GetFloat(SaveKey key, float defaultValue = 0f, string subKey = "")
    {
        return PlayerPrefs.GetFloat(key.ToString() + subKey, defaultValue);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応するstring値を取得する
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="defaultValue">初期値</param>
    /// <param name="subKey">サブキー</param>
    /// <returns></returns>
    //--------------------------------------------------------------------------/
    public string GetString(SaveKey key, string defaultValue = "", string subKey = "")
    {
        return PlayerPrefs.GetString(key.ToString() + subKey, defaultValue);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応するクラスを取得する
    /// </summary>
    /// <typeparam name="T">任意の型</typeparam>
    /// <param name="key">キー</param>
    /// <param name="defaultValue">初期値</param>
    /// <param name="subKey">サブキー</param>
    /// <returns></returns>
    //--------------------------------------------------------------------------/
    public T GetClass<T>(SaveKey key, T defaultValue, string subKey = "")
    {
        // キーがないならデフォルト値を返す
        if(!HasKey(key, subKey)) {
            Debug.Log("defaultきちゃったー");
            return defaultValue;
        }

        // Json形式のstringを取り出し、T型に変換して返す
        string json = PlayerPrefs.GetString(key.ToString() + subKey);
        Debug.Log(json);
        return JsonUtility.FromJson<T>(json);
    }
    #endregion

    //--------------------------------------------------------------------------/
    /// <summary>
    /// Set〇〇でセットした値を実際にディスクに保存する
    /// NOTE: 処理に時間が掛かるので頻繁に呼ばない
    /// </summary>
    //--------------------------------------------------------------------------/
    public void Save()
    {
        PlayerPrefs.Save();
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応する値があるか
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="subKey">サブキー</param>
    /// <returns></returns>
    //--------------------------------------------------------------------------/
    public bool HasKey(SaveKey key, string subKey = "")
    {
        return PlayerPrefs.HasKey(key.ToString() + subKey);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// キーに対応する値を削除する
    /// </summary>
    /// <param name="key">キー</param>
    /// <param name="subKey">サブキー</param>
    //--------------------------------------------------------------------------/
    public void DeleteKey(SaveKey key, string subKey = "")
    {
        PlayerPrefs.DeleteKey(key.ToString() + subKey);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 保存されたすべての値を削除する
    /// </summary>
    //--------------------------------------------------------------------------/
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}

//--------------------------------------------------------------------------/
/// <summary>
/// セーブ用キー
/// </summary>
//--------------------------------------------------------------------------/
public enum SaveKey
{
    TestData,
}
