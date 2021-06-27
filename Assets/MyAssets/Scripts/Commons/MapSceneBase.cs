using UnityEngine;
using UniRx;
using UniRx.Async;

/// <summary>
/// マップシーン基底クラス
/// </summary>
public class MapSceneBase : MonoBehaviour
{
    /// <summary>画面外座標</summary>
    public static readonly Vector3 OffScreenPos = new Vector3(0f, 5000f, 0f);

    [SerializeField] private Player player = default;
    [SerializeField] private Bgm bgm = Bgm.None;

    private MapEvent[] mapEvents;
    private Vector3 defaultTransformPos;

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Awake()
    {
        // プレイヤーデータがないならデバッグ用に作成する
        if(PlayerData.Instance.CurrentData == null) {
            PlayerData.Instance.Create("デバッグプレイヤー");
        }

        // インゲームで使う表示物を表示する
        DisplayManager.Instance.ShowInGameDisplayObjects();

        // 初期化完了まで一旦画面外に退避させる
        defaultTransformPos = transform.position;
        transform.position = OffScreenPos;

        initializeMapEvents();
        initializePlayer();

        // 画面位置をもとに戻す
        transform.position = defaultTransformPos;

        // BGM再生
        SoundManager.Instance.PlayBgm(bgm);

        // 画面フェードイン
        DisplayManager.Instance.FadeInDisplayAsync().Forget();

        // 自動イベントチェック
        checkAutoEventsAsync().Forget();
    }

    /// <summary>
    /// 各マップイベントの初期化
    /// </summary>
    private void initializeMapEvents()
    {
        // シーン上に配置されてるマップイベントを全て取得する
        mapEvents = FindObjectsOfType<MapEvent>();

        // マップイベントの初期化
        foreach(var mapEvent in mapEvents) {
            mapEvent.Initialize();
        }
    }

    /// <summary>
    /// プレイヤー初期化
    /// </summary>
    private void initializePlayer()
    {
        // 初期位置設定
        var parameter = SceneManagerExtension.SceneParameter;
        if(parameter != null) {
            player.Initialize(parameter.StartPosition, parameter.StartDirection);
        } else {
            player.Initialize(new Vector2(0.5f, 0.5f), Player.DIRECTION.DOWN);
        }
        // 購読処理
        player.OnInspect.Subscribe(checkInspectEvents).AddTo(this);
        player.OnUseItem.Subscribe(values => checkUseItemEvents(values.checkedPosition, values.usedItemId)).AddTo(this);
        player.OnMoved.Subscribe(checkFloorEvents).AddTo(this);
    }

    /// <summary>
    /// 調べるイベントを発動させるかチェックする
    /// </summary>
    /// <param name="checkedPosition">調べた位置</param>
    private void checkInspectEvents(Vector2 checkedPosition)
    {
#if DEBUG_LOG
        Debug.Log($"調べた位置({checkedPosition.x}, {checkedPosition.y})");
#endif
        foreach(var mapEvent in mapEvents) {
            mapEvent.CheckInspectEvent(checkedPosition);
        }
    }

    /// <summary>
    /// 床イベントを発動させるかチェックする
    /// </summary>
    /// <param name="playerModel">プレイヤー情報</param>
    private void checkFloorEvents(IReadOnlyPlayerModel playerModel)
    {
#if DEBUG_LOG
        Debug.Log($"踏んだ位置({playerModel.CurrentPosition.x}, {playerModel.CurrentPosition.y})");
#endif
        foreach(var mapEvent in mapEvents) {
            mapEvent.CheckFloorEvent(playerModel);
        }
    }

    /// <summary>
    /// アイテム使用イベントを発動させるかチェックする
    /// </summary>
    /// <param name="checkedPosition">使用する位置</param>
    /// <param name="usedItemId">使用するアイテムのID</param>
    private void checkUseItemEvents(Vector2 checkedPosition, ItemID usedItemId)
    {
#if DEBUG_LOG
        Debug.Log($"使用する位置({checkedPosition.x}, {checkedPosition.y})");
#endif
        foreach(var mapEvent in mapEvents) {
            mapEvent.CheckUseItemEvent(checkedPosition, usedItemId);
        }
    }

    /// <summary>
    /// 自動イベントを発動させるかチェックする
    /// </summary>
    private async UniTaskVoid checkAutoEventsAsync()
    {
        foreach(var mapEvent in mapEvents) {
            var autoEvent = mapEvent as AutoEvent;
            if(autoEvent == null) continue;
            await autoEvent.CheckAutoEventAsync();
        }
    }
}

/// <summary>
/// マップシーン読み込み時に使うパラメータ
/// </summary>
public class MapSceneParameter
{
    /// <summary>プレイヤーの初期位置</summary>
    public Vector2 StartPosition { get; private set; }
    /// <summary>プレイヤーの初期方向</summary>
    public Player.DIRECTION StartDirection { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="position">プレイヤーの初期位置</param>
    /// <param name="direction">プレイヤーの初期方向</param>
    public MapSceneParameter(Vector2 position, Player.DIRECTION direction)
    {
        StartPosition = position;
        StartDirection = direction;
    }
}
