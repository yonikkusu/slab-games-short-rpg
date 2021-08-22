using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;

/// <summary>
/// 合成画面 所持アイテムパネルView
/// </summary>
public class SynthesisItemPanel : MonoBehaviour
{
    [SerializeField] private SynthesisItemPanelNode[] nodes;

    /// <summary>アイテムボタンタップ通知(リストのIndexを通知)</summary>
    public IObservable<int?> OnTapItemButton => Observable.Merge(nodes.Select(node => node.OnTapButton));

    /// <summary>
    /// 描画
    /// </summary>
    /// <param name="itemDataList">表示するアイテムリスト</param>
    public void Render(List<ItemData> itemDataList)
    {
        foreach(var node in nodes) {
            node.Clear();
        }
        for(var i = 0; i < itemDataList.Count; i++) {
            nodes[i].Render(i, itemDataList[i]);
        }
    }
}
