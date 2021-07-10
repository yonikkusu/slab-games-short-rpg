using UnityEngine;
using UniRx.Async;
using Fungus;

/// <summary>
/// Flowchartを使いやすくするクラス
/// Flowchartを使うときはこれを通して使う
/// </summary>
public class FlowchartHelper : SingletonMonoBehaviour<FlowchartHelper>
{
    [SerializeField] private Flowchart flowchart;
    private UniTaskCompletionSource source;

    /// <summary>
    /// ADVを再生する
    /// </summary>
    /// <param name="sendMessage">識別メッセージ</param>
    /// <returns>UniTask</returns>
    public async UniTask PlayAdvAsync(string sendMessage)
    {
        flowchart.SendFungusMessage(sendMessage);
        source = new UniTaskCompletionSource();
        await source.Task;
    }

    /// <summary>
    /// ADVを終了させる
    /// </summary>
    public void FinishAdv()
    {
        source.TrySetResult();
    }
}
