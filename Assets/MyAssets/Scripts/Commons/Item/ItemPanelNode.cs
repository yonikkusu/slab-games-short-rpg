using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテムパネル Node
/// </summary>
public class ItemPanelNode : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="itemId">アイテムID</param>
    public void Initialize(ItemID itemId)
    {
        var itemMaster = MasterGetter.GetItemData(itemId);
        itemImage.sprite = itemMaster?.Sprite;
    }
}
