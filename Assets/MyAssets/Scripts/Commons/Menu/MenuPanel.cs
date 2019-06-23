using System;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// メニューパネル
/// </summary>
//--------------------------------------------------------------------------/
public class MenuPanel : MonoBehaviour
{
    [SerializeField] private Button saveButton;
    [SerializeField] private Button titleButton;
    [SerializeField] private SaveLoadPanel saveLoadPanel;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        saveButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            saveLoadPanel.Show(SaveLoadPanelType.Save);
        });
        titleButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            SceneManager.Instance.LoadScene(Scene.Title);
        });
        saveLoadPanel.gameObject.SetActive(false);
    }
}
