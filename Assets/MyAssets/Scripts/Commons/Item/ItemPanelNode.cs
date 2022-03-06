using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// アイテムパネル Node
/// </summary>
public class ItemPanelNode : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private Button button;

    /// <summary>タップ通知(Value:ノードIndex)</summary>
    public IObservable<int> OnTap => button.OnClickAsObservable().Select(_ => index);

    private int index;

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="index">ノードIndex</param>
    /// <param name="itemId">アイテムID</param>
    public void Initialize(int index, ItemID itemId)
    {
        this.index = index;
        var itemMaster = MasterGetter.GetItemData(itemId);
        itemImage.sprite = itemMaster?.Sprite;
    }
}
