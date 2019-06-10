using System;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// セーブパネル
/// </summary>
//--------------------------------------------------------------------------/
public class SavePanel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private SaveButton[] saveButtons;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        closeButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            this.gameObject.SetActive(false);
        });
        for(var i = 0; i < saveButtons.Length; i++) {
            saveButtons[i].SetData(i);
        }
    }
}
