using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// セーブロードパネル
/// </summary>
//--------------------------------------------------------------------------/
public class SaveLoadPanel : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private Text information;
    [SerializeField] private SaveLoadButton[] saveLoadButtons;

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
        for(var i = 0; i < saveLoadButtons.Length; i++) {
            saveLoadButtons[i].SetData(i);
        }
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// パネルを表示する
    /// </summary>
    /// <param name="type">表示タイプ</param>
    //--------------------------------------------------------------------------/
    public void Show(SaveLoadPanelType type)
    {
        if(type == SaveLoadPanelType.Save) {
            foreach(var saveLoadButton in saveLoadButtons) {
                saveLoadButton.SetButtonType(SaveLoadPanelType.Save);
            }
            information.text = "どのファイルにセーブしますか？";
        } else if(type == SaveLoadPanelType.Load) {
            foreach(var saveLoadButton in saveLoadButtons) {
                saveLoadButton.SetButtonType(SaveLoadPanelType.Load);
            }
            information.text = "どのファイルをロードしますか？";
        }
        this.gameObject.SetActive(true);
    }
}

//--------------------------------------------------------------------------/
/// <summary>
/// セーブロードパネルの表示タイプ
/// </summary>
//--------------------------------------------------------------------------/
public enum SaveLoadPanelType
{
    Save,
    Load,
}
