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
    [SerializeField] private GameObject savePanel;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        saveButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            savePanel.gameObject.SetActive(true);
        });
        titleButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
        });
        savePanel.SetActive(false);
    }
}
