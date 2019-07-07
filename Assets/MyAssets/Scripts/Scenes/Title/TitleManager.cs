using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// タイトルマネージャー
/// </summary>
//--------------------------------------------------------------------------/
public class TitleManager : MonoBehaviour
{
    [SerializeField] private Button startButton = default;
    [SerializeField] private StartMenuPanel startMenuPanel = default;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        // スタートボタンタップ時
        startButton.onClick.AddListener(() => {
            // タップ音を鳴らす
            SoundManager.Instance.PlaySe(Se.Tap);
            // スタートメニューを開く
            startMenuPanel.gameObject.SetActive(true);
        });

        // BGMを鳴らす
        SoundManager.Instance.PlayBgm(Bgm.Title);

        // 最初はスタートメニューを閉じておく
        startMenuPanel.gameObject.SetActive(false);
    }
}
