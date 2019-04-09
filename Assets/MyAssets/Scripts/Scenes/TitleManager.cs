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
        // スタートボタンタップでメインメニュー画面へ遷移
        startButton.onClick.AddListener(() => SceneManager.Instance.LoadScene(Scene.MainMenu));
    }
}
