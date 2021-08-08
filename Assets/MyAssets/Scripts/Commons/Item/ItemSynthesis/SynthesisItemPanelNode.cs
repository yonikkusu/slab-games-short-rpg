using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// 合成画面 所持アイテムパネルノード
/// </summary>
public class SynthesisItemPanelNode : MonoBehaviour
{
    [SerializeField] private Image itemImage = default;
    [SerializeField] private Button button = default;

    /// <summary>ボタンタップ通知(Indexを通知)</summary>
    public IObservable<int?> OnTapButton => onTapButton;
    private Subject<int?> onTapButton = new Subject<int?>();

    private int? index;

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Start()
    {
        button.OnClickAsObservable().Subscribe(_ => onTapButton.OnNext(index)).AddTo(this);
    }

    /// <summary>
    /// 描画
    /// </summary>
    /// <param name="index">Index</param>
    /// <param name="itemData">描画するアイテム</param>
    public void Render(int index, ItemData itemData)
    {
        this.index = index;
        itemImage.sprite = itemData.Sprite;
        itemImage.gameObject.SetActive(itemData?.Sprite != null);
    }

    /// <summary>
    /// クリア
    /// </summary>
    public void Clear()
    {
        index = null;
        itemImage.gameObject.SetActive(false);
    }
}
