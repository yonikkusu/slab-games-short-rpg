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
    [SerializeField] private ItemSynthesisPresenter synthesisPresenter;
    [SerializeField] private Button synthesisButton;

    /// <summary>選択中アイテムのID</summary>
    public ItemData SelectedItem => itemList[selectedItemIndex];
    private ItemData[] itemList;
    private int selectedItemIndex;
    private bool initialized;

    /// <summary>
    /// アップデート処理
    /// </summary>
    void Update()
    {
        if(!initialized) return;

        // マウスのホイールで選択中アイテムを切り替える
        var wheelValue = Input.GetAxis("Mouse ScrollWheel");
        if(wheelValue > 0f) {
            moveCursor(selectedItemIndex - 1);
        } else if(wheelValue < 0f) {
            moveCursor(selectedItemIndex + 1);
        }

        // 選択中カーソルを移動する
        void moveCursor(int index)
        {
           selectedItemIndex = index < 0 ? MaxItemNum - 1 : 
                              index >= MaxItemNum ? 0 :
                              index;
           selectFrame.transform.SetParent(itemImageList[selectedItemIndex].transform);
           selectFrame.transform.localPosition = new Vector3();
        }
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        itemList = new ItemData[MaxItemNum];
        UpdateItemList();
        initialized = true;
        synthesisPresenter.Initialize();
        synthesisButton.OnClickAsObservable().Subscribe(_ => synthesisPresenter.Show()).AddTo(this);
    }

    /// <summary>
    /// アイテムを所持品に追加する
    /// </summary>
    /// <param name="itemId">追加するアイテムID</param>
    public void AddItem(int itemId)
    {
        if(!initialized) return;

        PlayerData.Instance.ItemManager.AddItem((ItemID)itemId);
        UpdateItemList();
    }

    /// <summary>
    /// アイテムリストを更新する
    /// </summary>
    public void UpdateItemList()
    {
        if(!initialized) return;

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
}
