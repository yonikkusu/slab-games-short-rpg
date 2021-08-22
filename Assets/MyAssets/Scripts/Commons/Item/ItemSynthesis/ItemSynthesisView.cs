using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// アイテム合成 View
/// </summary>
public class ItemSynthesisView : MonoBehaviour
{
    [SerializeField] private SynthesisItemIcon materialItemIcon1 = default;
    [SerializeField] private SynthesisItemIcon materialItemIcon2 = default;
    [SerializeField] private Image resultItemImage = default;
    [SerializeField] private SynthesisItemPanel itemPanel = default;
    [SerializeField] private Button cancelButton = default;
    [SerializeField] private Button decideButton = default;
    [SerializeField] private Text descriptionText = default;

    /// <summary>素材アイテム1ボタンタップ通知</summary>
    public IObservable<Unit> OnTapMaterialItem1Button => materialItemIcon1.OnTapDeleteButton;
    /// <summary>素材アイテム2ボタンタップ通知</summary>
    public IObservable<Unit> OnTapMaterialItem2Button => materialItemIcon2.OnTapDeleteButton;
    /// <summary>所持アイテムボタンタップ通知(リストのIndexを通知)</summary>
    public IObservable<int?> OnTapPossessionItemButton => itemPanel.OnTapItemButton;
    /// <summary>キャンセルボタンタップ通知</summary>
    public IObservable<Unit> OnTapCancelButton => cancelButton.OnClickAsObservable();
    /// <summary>決定ボタンタップ通知</summary>
    public IObservable<Unit> OnTapDecideButton => decideButton.OnClickAsObservable();

    /// <summary>
    /// 描画
    /// </summary>
    /// <param name="model">Model</param>
    public void Render(ItemSynthesisModel model)
    {
        materialItemIcon1.Render(model.SelectedMaterialItem1, showsSelectedFrame: model.CurrentState == ItemSynthesisState.MaterialItem1PreSelect);
        materialItemIcon2.Render(model.SelectedMaterialItem2, showsSelectedFrame: model.CurrentState == ItemSynthesisState.MaterialItem2PreSelect);
        resultItemImage.sprite = model.ResultItem?.Sprite;
        var hasResultItem = model.ResultItem != null;
        resultItemImage.gameObject.SetActive(hasResultItem);
        itemPanel.Render(model.PossessionItemList);
        decideButton.interactable = model.CurrentState == ItemSynthesisState.SynthesisConfirm && hasResultItem;
        descriptionText.text = model.GetDescription();
    }
}
