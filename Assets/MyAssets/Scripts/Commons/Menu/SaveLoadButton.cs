using System;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// セーブロードボタン
/// </summary>
//--------------------------------------------------------------------------/
public class SaveLoadButton : MonoBehaviour
{
    [SerializeField] private Button button = default;
    [SerializeField] private Text playerName = default;
    [SerializeField] private Text playDate = default;
    [SerializeField] private Text playTime = default;

    private int index;
    private SaveLoadPanelType type;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        button.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            // セーブならデータをセーブしてパネル表示を更新
            if(type == SaveLoadPanelType.Save) {
                PlayerData.Instance.Save(index);
                updateView(PlayerData.Instance.CurrentData);
            }
            // ロードならデータをロードして該当シーンに遷移
            else if(type == SaveLoadPanelType.Load) {
                PlayerData.Instance.Load(index);
                SceneManager.Instance.LoadScene((Scene)PlayerData.Instance.CurrentData.LastScene);
            }
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
        updateView(PlayerData.Instance.GetSaveData(index));
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// ボタンタイプを設定する
    /// </summary>
    /// <param name="type">ボタンタイプ</param>
    //--------------------------------------------------------------------------/
    public void SetButtonType(SaveLoadPanelType type) => this.type = type;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 表示更新
    /// </summary>
    /// <param name="playData">表示するセーブデータ</param>
    //--------------------------------------------------------------------------/
    private void updateView(SaveData saveData)
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
