using UnityEngine;

//--------------------------------------------------------------------------/
/// <summary>
/// ゲーム中に常に存在させるオブジェクトの初期化クラス
/// </summary>
//--------------------------------------------------------------------------/
public class RuntimeInitializer
{
    //--------------------------------------------------------------------------/
    /// <summary>
    /// 各マネージャーをDontDestroyObjectとして生成する
    /// </summary>
    //--------------------------------------------------------------------------/
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeBeforeSceneLoad()
    {
        // シーンマネージャー
        var sceneManager = GameObject.Instantiate(Resources.Load("Prefabs/Commons/SceneManager"));
        GameObject.DontDestroyOnLoad(sceneManager);
        // サウンドマネージャー
        var soundManager = GameObject.Instantiate(Resources.Load("Prefabs/Commons/SoundManager"));
        GameObject.DontDestroyOnLoad(soundManager);
        // セーブマネージャー
        var saveManager = GameObject.Instantiate(Resources.Load("Prefabs/Commons/SaveManager"));
        GameObject.DontDestroyOnLoad(saveManager);
        // プレイヤーデータ
        var playerData = GameObject.Instantiate(Resources.Load("Prefabs/Commons/PlayerData"));
        GameObject.DontDestroyOnLoad(playerData);
    }
}
