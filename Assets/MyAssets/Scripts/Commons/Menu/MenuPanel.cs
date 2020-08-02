using UnityEngine;
using UnityEngine.UI;
using UniRx.Async;

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
            SceneManagerExtension.LoadSceneAsync(SceneName.Title).Forget();
        });
        saveLoadPanel.gameObject.SetActive(false);
    }
}
