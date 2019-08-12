using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// GrassMap イベント1
/// </summary>
//--------------------------------------------------------------------------/
public class GrassMapEvent1 : MapEvent
{
    [SerializeField] private Text mainText = default;
    [SerializeField] private GameObject mainTextPanel = default;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 調べられた時の処理
    /// </summary>
    //--------------------------------------------------------------------------/
    protected override void onInspected()
    {
        // テキストの表示切り替え
        if(mainTextPanel.activeSelf) {
            mainText.text = "";
            mainTextPanel.SetActive(false);
        } else {
            mainText.text = "test";
            mainTextPanel.SetActive(true);
        }
    }
}
