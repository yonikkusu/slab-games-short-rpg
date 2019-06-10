using System;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// セーブボタン
/// </summary>
//--------------------------------------------------------------------------/
public class SaveButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Text playerName;
    [SerializeField] private Text playDate;
    [SerializeField] private Text playTime;

    private int index;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        button.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            Player.Instance.Save(index);
            updateView(Player.Instance.CurrentData);
        });
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// データを設定する
    /// </summary>
    /// <param name="index">セーブファイル番号</param>
    //--------------------------------------------------------------------------/
    public void SetData(int index)
    {
        this.index = index;
        updateView(Player.Instance.GetSaveData(index));
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 表示更新
    /// </summary>
    /// <param name="playData">表示するセーブデータ</param>
    //--------------------------------------------------------------------------/
    private void updateView(Player.SaveData saveData)
    {
        // データがないなら表示をリセットする
        if(saveData == null) {
            playerName.text = "";
            playDate.text = "";
            playTime.text = "";
            return;
        }

        playerName.text = saveData.Name;
        var date = GameUtility.UnixTimeToDateTimeOffset(saveData.LastPlayDate);
        playDate.text = date.ToString("yyyy/MM/dd HH:mm:ss");
        playTime.text = new TimeSpan(0, 0, (int)saveData.PlayTime).ToString();
    }
}
