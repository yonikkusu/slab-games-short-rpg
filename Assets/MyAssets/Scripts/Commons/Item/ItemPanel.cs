using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// アイテムパネル
/// </summary>
//--------------------------------------------------------------------------/
public class ItemPanel : MonoBehaviour
{
    // 所持可能アイテム上限
    private const int MaxItemNum = 8;

    [SerializeField] private Image[] itemImageList = default;
    [SerializeField] private Image selectFrame = default;

    /// <summary>選択中アイテムインデックス</summary>
    public int CurrentItemIndex { get; private set; }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Start()
    {
        UpdateItemList();
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アップデート処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Update()
    {
        // マウスのホイールで選択中アイテムを切り替える
        var wheelValue = Input.GetAxis("Mouse ScrollWheel");
        if(wheelValue > 0f) {
            moveCursor(CurrentItemIndex - 1);
        } else if(wheelValue < 0f) {
            moveCursor(CurrentItemIndex + 1);
        }

        // 選択中カーソルを移動する
        void moveCursor(int index)
        {
           CurrentItemIndex = index < 0 ? MaxItemNum - 1 : 
                              index >= MaxItemNum ? 0 :
                              index;
           selectFrame.transform.SetParent(itemImageList[CurrentItemIndex].transform);
           selectFrame.transform.localPosition = new Vector3();
        }
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// アイテムリストを更新する
    /// </summary>
    //--------------------------------------------------------------------------/
    public void UpdateItemList()
    {
        var possessionItemList = PlayerData.Instance.ItemManager.PossessionItemList;

        if(possessionItemList == null) return;

        // アイテム画像表示を一旦リセット
        foreach(var itemImage in itemImageList) {
            itemImage.sprite = null;
        }

        // 所持アイテムの画像をセット
        for(var i = 0; i < possessionItemList.Count; i++) {
            itemImageList[i].sprite = possessionItemList[i].Sprite;
        }
    }
}
