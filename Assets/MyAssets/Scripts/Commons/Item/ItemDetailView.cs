using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// アイテム詳細View
/// </summary>
public class ItemDetailView : MonoBehaviour
{
    [SerializeField] private Image itemDetailImage;
    [SerializeField] private Button closeButton;

    /// <summary>
    /// 起動時処理
    /// </summary>
    private void Start()
    {
        hide();
        closeButton.OnClickAsObservable()
            .Subscribe(_ =>hide())
            .AddTo(this);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="itemData">アイテムデータ</param>
    public void Initialize(ItemData itemData)
    {
        // TODO: 正しい画像に置き換える
        itemDetailImage.sprite = itemData.Sprite;
        itemDetailImage.SetNativeSize();
    }

    /// <summary>
    /// 表示する
    /// </summary>
    public void Show() => this.gameObject.SetActive(true);

    /// <summary>
    /// 非表示にする
    /// </summary>
    private void hide() => this.gameObject.SetActive(false);
}
