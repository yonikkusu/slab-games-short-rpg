using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// タイトルマネージャー
/// </summary>
//--------------------------------------------------------------------------/
public class TitleManager : MonoBehaviour
{
    [SerializeField] private Button startButton;

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
            // メインメニュー画面へ遷移
            SceneManager.Instance.LoadScene(Scene.MainMenu);
        });
        // BGMを鳴らす
        SoundManager.Instance.PlayBgm(Bgm.Title);
    }
}
