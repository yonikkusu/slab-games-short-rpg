using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// アイテムパネル
/// </summary>
public class ItemPanel : SingletonMonoBehaviour<ItemPanel>
{
    [SerializeField] private Image[] itemImageList = default;
    [SerializeField] private Image selectFrame = default;
    [SerializeField] private ItemSynthesisPresenter synthesisPresenter = default;
    [SerializeField] private Button synthesisButton = default;
    [SerializeField] private ItemPanelInput itemPanelInput = default;

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Start()
    {
        synthesisPresenter.Initialize();
        synthesisButton.OnClickAsObservable().Subscribe(_ => synthesisPresenter.Show()).AddTo(this);
        itemPanelInput.OnWheelValueChanged.Subscribe(moveCursor).AddTo(this);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        RenderView();
    }

    /// <summary>
    /// Viewを更新する
    /// </summary>
    public void RenderView()
    {
        foreach(var itemImage in itemImageList) {
            itemImage.sprite = null;
        }
        var possessionItemList = PlayerData.Instance.ItemManager?.PossessionItemList;
        if(possessionItemList == null) return;

        for(var i = 0; i < possessionItemList.Count; i++) {
            itemImageList[i].sprite = possessionItemList[i].Sprite;
        }
    }

    /// <summary>
    /// 選択中カーソルを移動する
    /// </summary>
    /// <param name="wheelValue">マウスホイールの移動量</param>
    private void moveCursor(float wheelValue)
    {
        if(wheelValue == 0) return;
        var selectedItemIndex = PlayerData.Instance.ItemManager?.SelectedItemIndex;
        if(!selectedItemIndex.HasValue) return;
        var index = wheelValue > 0f ? selectedItemIndex.Value - 1 : selectedItemIndex.Value + 1;
        PlayerData.Instance.ItemManager.UpdateSelectedItem(index);
        var newIndex = PlayerData.Instance.ItemManager.SelectedItemIndex;
        selectFrame.transform.SetParent(itemImageList[newIndex].transform);
        selectFrame.transform.localPosition = new Vector3();
    }
}
