using System;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// スタートメニューパネル
/// </summary>
//--------------------------------------------------------------------------/
public class StartMenuPanel : MonoBehaviour
{
    [SerializeField] private Button newGameButton = default;
    [SerializeField] private Button continueButton = default;
    [SerializeField] private SaveLoadPanel saveLoadPanel = default;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        // ニューゲームボタン
        newGameButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            // プレイヤーデータ作成
            PlayerData.Instance.Create("マサタカ");
            // TODO: 物語の最初の画面へ遷移
            SceneManagerExtension.LoadScene(SceneName.GrassMap);
        });

        // コンテニューボタン
        continueButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            // ロードパネルを開く
            saveLoadPanel.Show(SaveLoadPanelType.Load);
        });
        saveLoadPanel.gameObject.SetActive(false);
    }
}
