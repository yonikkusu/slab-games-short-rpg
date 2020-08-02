using UnityEngine;
using UniRx.Async;

//--------------------------------------------------------------------------/
/// <summary>
/// マップシーン基底クラス
/// </summary>
//--------------------------------------------------------------------------/
public class MapSceneBase : MonoBehaviour
{
    /// <summary>画面外座標</summary>
    public static readonly Vector3 OffScreenPos = new Vector3(0f, 5000f, 0f);

    [SerializeField] private Player player = default;
    [SerializeField] private Bgm bgm = Bgm.Title;

    private MapEvent[] mapEvents;
    private Vector3 defaultTransformPos;

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 起動時処理
    /// </summary>
    //--------------------------------------------------------------------------/
    void Awake()
    {
        // メニューボタンを表示する
        DisplayManager.Instance.SetActiveMenu(true);

        // シーン上に配置されてるマップイベントを全て取得する
        mapEvents = FindObjectsOfType<MapEvent>();

        // 初期化完了まで一旦画面外に退避させる
        defaultTransformPos = transform.position;
        transform.position = OffScreenPos;
        
        // プレイヤーの初期位置設定
        var parameter = SceneManagerExtension.SceneParameter;
        if(parameter != null) {
            player.Initialize(parameter.StartPosition, parameter.StartDirection);
        } else {
            player.Initialize(new Vector2(0.5f, 0.5f), Player.DIRECTION.DOWN);
        }

        // 画面位置をもとに戻す
        transform.position = defaultTransformPos;

        // BGM再生
        SoundManager.Instance.PlayBgm(bgm);

        // 画面フェードイン
        DisplayManager.Instance.FadeInDisplayAsync().Forget();

#if DEBUG_LOG
        Debug.Log($"位置({checkedPosition.x}, {checkedPosition.y})");
#endif
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 調べるイベントを発動させるかチェックする
    /// </summary>
    /// <param name="checkedPosition">調べた位置</param>
    //--------------------------------------------------------------------------/
    public void CheckInspectEvents(Vector2 checkedPosition)
    {
#if DEBUG_LOG
        Debug.Log($"調べた位置({checkedPosition.x}, {checkedPosition.y})");
#endif
        foreach(var mapEvent in mapEvents) {
            mapEvent.CheckInspectEvent(checkedPosition);
        }
    }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// 床イベントを発動させるかチェックする
    /// </summary>
    /// <param name="playerModel">プレイヤー情報</param>
    //--------------------------------------------------------------------------/
    public void CheckFloorEvents(IReadOnlyPlayerModel playerModel)
    {
#if DEBUG_LOG
        Debug.Log($"踏んだ位置({playerModel.CurrentPosition.x}, {playerModel.CurrentPosition.y})");
#endif
        foreach(var mapEvent in mapEvents) {
            mapEvent.CheckFloorEvent(playerModel);
        }
    }
}

//--------------------------------------------------------------------------/
/// <summary>
/// マップシーン読み込み時に使うパラメータ
/// </summary>
//--------------------------------------------------------------------------/
public class MapSceneParameter
{
    /// <summary>プレイヤーの初期位置</summary>
    public Vector2 StartPosition { get; private set; }
    /// <summary>プレイヤーの初期方向</summary>
    public Player.DIRECTION StartDirection { get; private set; }

    //--------------------------------------------------------------------------/
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="position">プレイヤーの初期位置</param>
    /// <param name="direction">プレイヤーの初期方向</param>
    //--------------------------------------------------------------------------/
    public MapSceneParameter(Vector2 position, Player.DIRECTION direction)
    {
        StartPosition = position;
        StartDirection = direction;
    }
}
