using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// アイテムパネル
/// </summary>
public class ItemPanel : SingletonMonoBehaviour<ItemPanel>
{
    // 所持可能アイテム上限
    private const int MaxItemNum = 8;

    [SerializeField] private Image[] itemImageList = default;
    [SerializeField] private Image selectFrame = default;
    [SerializeField] private ItemSynthesisPresenter synthesisPresenter = default;
    [SerializeField] private Button synthesisButton = default;
    [SerializeField] private ItemPanelInput itemPanelInput = default;

    /// <summary>選択中アイテムのID</summary>
    public ItemData SelectedItem => itemList[selectedItemIndex];
    private ItemData[] itemList;
    private int selectedItemIndex;
    private bool isFirstInitialized;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        itemList = new ItemData[MaxItemNum];
        UpdateItemList();
        if(!isFirstInitialized) {
            synthesisPresenter.Initialize();
            synthesisButton.OnClickAsObservable().Subscribe(_ => synthesisPresenter.Show()).AddTo(this);
            itemPanelInput.OnWheelValueChanged.Subscribe(moveCursor).AddTo(this);
            isFirstInitialized = true;
        }
    }

    /// <summary>
    /// アイテムを所持品に追加する
    /// </summary>
    /// <param name="itemId">追加するアイテムID</param>
    public void AddItem(int itemId)
    {
        if(!isFirstInitialized) return;

        PlayerData.Instance.ItemManager.AddItem((ItemID)itemId);
        UpdateItemList();
    }

    /// <summary>
    /// アイテムリストを更新する
    /// </summary>
    public void UpdateItemList()
    {
        if(!isFirstInitialized) return;

        var possessionItemList = PlayerData.Instance.ItemManager?.PossessionItemList;

        if(possessionItemList == null) return;

        // アイテム画像表示を一旦リセット
        foreach(var itemImage in itemImageList) {
            itemImage.sprite = null;
        }
        for(var i = 0; i < itemList.Length; i++) {
            itemList[i] = null;
        }

        // 所持アイテムのデータと画像をセット
        for(var i = 0; i < possessionItemList.Count; i++) {
            itemList[i] = possessionItemList[i];
            itemImageList[i].sprite = possessionItemList[i].Sprite;
        }
    }

    /// <summary>
    /// 選択中カーソルを移動する
    /// </summary>
    /// <param name="wheelValue">マウスホイールの移動量</param>
    void moveCursor(float wheelValue)
    {
        if(!isFirstInitialized) return;
        if(wheelValue == 0) return;

        var index = wheelValue > 0f ? selectedItemIndex - 1 : selectedItemIndex + 1;
        selectedItemIndex = index < 0 ? MaxItemNum - 1 : 
                            index >= MaxItemNum ? 0 :
                            index;
        selectFrame.transform.SetParent(itemImageList[selectedItemIndex].transform);
        selectFrame.transform.localPosition = new Vector3();
    }
}
