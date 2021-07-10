using UnityEngine;

/// <summary>
/// ゲーム中に常に存在させるオブジェクトの初期化クラス
/// </summary>
public class RuntimeInitializer
{
    /// <summary>
    /// 各マネージャーをDontDestroyObjectとして生成する
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad()
    {
        // 各マネージャーが付いてるPrefabをDontDestroyOnLoadに加える
        var dontDestroyObjects = GameObject.Instantiate(Resources.Load("Prefabs/Commons/DontDestroyObjects"));
        GameObject.DontDestroyOnLoad(dontDestroyObjects);
    }
}
