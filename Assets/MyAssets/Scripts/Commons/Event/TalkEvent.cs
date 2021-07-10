using UnityEngine;
using UniRx.Async;
using Fungus;

/// <summary>
/// 会話イベント
/// InspectorのmessageにFlowchartで発動させたい会話のメッセージIDを設定する
/// </summary>
public class TalkEvent : MapEvent
{
    [SerializeField] private string message = default;

    /// <summary>
    /// 調べられた時の処理
    /// </summary>
    protected override async UniTask onInspectedAsync()
    {
        await FlowchartHelper.Instance.PlayAdvAsync(message);
    }
}
