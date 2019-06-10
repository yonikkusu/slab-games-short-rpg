using System;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// メニュー マネージャー
/// </summary>
//--------------------------------------------------------------------------/
public class Menu : MonoBehaviour
{
    [SerializeField] private Button menuButton;
    [SerializeField] private Button backgroundButton;
    [SerializeField] private GameObject menuPanel;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        menuButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            toggle();
        });
        menuButton.gameObject.SetActive(true);
        backgroundButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            toggle();
        });
        backgroundButton.gameObject.SetActive(false);
        menuPanel.SetActive(false);
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 表示を切り替える
    /// </summary>
    //--------------------------------------------------------------------------/
    private void toggle()
    {
        menuButton.gameObject.SetActive(!menuButton.gameObject.activeSelf);
        backgroundButton.gameObject.SetActive(!backgroundButton.gameObject.activeSelf);
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
