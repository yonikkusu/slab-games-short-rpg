using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// 合成アイテムアイコン
/// </summary>
public class SynthesisItemIcon : MonoBehaviour
{
    [SerializeField] private Image itemImage = default;
    [SerializeField] private Button deleteButton = default;
    [SerializeField] private GameObject selectedFrame = default;

    /// <summary>削除ボタンタップ通知</summary>
    public IObservable<Unit> OnTapDeleteButton => deleteButton.OnClickAsObservable();

    /// <summary>
    /// 描画
    /// </summary>
    /// <param name="itemData">描画するアイテム(nullなら空画像をセット)</param>
    /// <param name="showsSelectedFrame">選択枠を表示するか</param>
    public void Render(ItemData itemData, bool showsSelectedFrame)
    {
        itemImage.sprite = itemData?.Sprite;
        var hasItem = itemData?.Sprite != null;
        itemImage.gameObject.SetActive(hasItem);
        selectedFrame.SetActive(showsSelectedFrame);
        deleteButton.gameObject.SetActive(hasItem);
    }
}
