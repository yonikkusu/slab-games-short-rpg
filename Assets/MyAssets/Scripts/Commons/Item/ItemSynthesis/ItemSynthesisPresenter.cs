using UnityEngine;
using UniRx;

/// <summary>
/// アイテム合成 Presenter
/// </summary>
public class ItemSynthesisPresenter : MonoBehaviour
{
    [SerializeField] private ItemSynthesisView view = default;

    private ItemSynthesisModel model;

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        view.OnTapMaterialItem1Button.Subscribe(_ => {
            model.DeleteMaterialItem1();
            view.Render(model);
        }).AddTo(this);
        view.OnTapMaterialItem2Button.Subscribe(_ => {
            model.DeleteMaterialItem2();
            view.Render(model);
        }).AddTo(this);
        view.OnTapPossessionItemButton.Subscribe(index => {
            model.SetMaterialItem(index);
            view.Render(model);
        }).AddTo(this);
        view.OnTapCancelButton.Subscribe(_ => hide()).AddTo(this);
        view.OnTapDecideButton.Subscribe(_ => {
            synthesize(model);
            hide();
            ItemPanel.Instance.UpdateItemList();
        }).AddTo(this);
        hide();
    }

    /// <summary>
    /// 表示
    /// </summary>
    public void Show()
    {
        model = new ItemSynthesisModel(PlayerData.Instance.ItemManager?.PossessionItemList);
        view.Render(model);
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 非表示
    /// </summary>
    private void hide() => this.gameObject.SetActive(false);

    /// <summary>
    /// 合成する
    /// </summary>
    /// <param name="model">Model</param>
    private void synthesize(ItemSynthesisModel model)
    {
        // 消費アイテムを減らす
        PlayerData.Instance.ItemManager.UseItem(model.SelectedMaterialItem1.ID);
        PlayerData.Instance.ItemManager.UseItem(model.SelectedMaterialItem2.ID);
        // 合成後アイテムを追加する
        PlayerData.Instance.ItemManager.AddItem(model.ResultItem.ID);
    }
}
