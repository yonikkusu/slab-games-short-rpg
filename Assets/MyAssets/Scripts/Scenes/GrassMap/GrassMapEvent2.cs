using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------/
/// <summary>
/// GrassMap イベント2
/// </summary>
//--------------------------------------------------------------------------/
public class GrassMapEvent2 : MapEvent
{
    //--------------------------------------------------------------------------/
    /// <summary>
    /// 踏まれた時の処理
    /// </summary>
    //--------------------------------------------------------------------------/
    protected override void onStepped()
    {
        SceneManagerExtension.LoadScene(SceneName.Field);
    }
}
