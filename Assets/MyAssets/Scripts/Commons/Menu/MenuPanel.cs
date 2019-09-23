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
    [SerializeField] private Button saveButton = default;
    [SerializeField] private Button titleButton = default;
    [SerializeField] private SaveLoadPanel saveLoadPanel = default;

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
            SceneManagerExtension.LoadScene(SceneName.Title);
        });
        saveLoadPanel.gameObject.SetActive(false);
    }
}
