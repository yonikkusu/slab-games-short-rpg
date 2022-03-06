using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// アイテムパネル
/// </summary>
public class ItemPanel : SingletonMonoBehaviour<ItemPanel>
{
    [SerializeField] private ItemPanelNode itemPanelNodeBase = default;
    [SerializeField] private Transform nodeParent = default;
    [SerializeField] private Image selectFrame = default;
    [SerializeField] private ItemSynthesisPresenter synthesisPresenter = default;
    [SerializeField] private Button synthesisButton = default;
    [SerializeField] private ItemPanelInput itemPanelInput = default;

    private List<ItemPanelNode> nodeList;

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Start()
    {
        itemPanelNodeBase.gameObject.SetActive(false);
        synthesisPresenter.Initialize();
        synthesisButton.OnClickAsObservable().Subscribe(_ => synthesisPresenter.Show()).AddTo(this);
        itemPanelInput.OnWheelValueChanged.Subscribe(moveCursor).AddTo(this);
    }

    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        createNodes();
        moveSelectFrame(nodeIndex: 0);
    }

    /// <summary>
    /// Viewを更新する
    /// </summary>
    public void RenderView()
    {
        nodeList?.ForEach(node => node.Initialize(ItemID.None));
        var possessionItemList = PlayerData.Instance.ItemManager?.PossessionItemList;
        if(possessionItemList == null) return;

        for(var i = 0; i < nodeList.Count; i++) {
            if((possessionItemList.Count - 1) < i) {
                nodeList[i].Initialize(ItemID.None);
                continue;
            }
            var itemId = possessionItemList[i].ID;
            nodeList[i].Initialize(itemId);
        }
    }

    /// <summary>
    /// ノード生成
    /// </summary>
    private void createNodes()
    {
        nodeList?.ForEach(node => Destroy(node.gameObject));
        nodeList = new List<ItemPanelNode>();
        for(var i = 0; i < ItemManager.MaxItemNum; i++) {
            var node = Instantiate(itemPanelNodeBase, parent: nodeParent);
            node.Initialize(ItemID.None);
            node.gameObject.SetActive(true);
            nodeList.Add(node);
        }
    }

    /// <summary>
    /// 選択中カーソルを移動する
    /// </summary>
    /// <param name="wheelValue">マウスホイールの移動量</param>
    private void moveCursor(float wheelValue)
    {
        if(wheelValue == 0) return;
        var selectedItemIndex = PlayerData.Instance.ItemManager?.SelectedItemIndex;
        if(!selectedItemIndex.HasValue) return;
        var index = wheelValue > 0f ? selectedItemIndex.Value - 1 : selectedItemIndex.Value + 1;
        PlayerData.Instance.ItemManager.UpdateSelectedItem(index);
        var newIndex = PlayerData.Instance.ItemManager.SelectedItemIndex;
        moveSelectFrame(newIndex);
    }

    /// <summary>
    /// 選択フレームを移動する
    /// </summary>
    /// <param name="nodeIndex">ノードIndex</param>
    private void moveSelectFrame(int nodeIndex)
    {
        var hasTargetNode = (nodeList.Count - 1) < nodeIndex;
        selectFrame.gameObject.SetActive(hasTargetNode);
        if(!hasTargetNode) return;
        selectFrame.transform.SetParent(nodeList[nodeIndex].transform);
        selectFrame.transform.localPosition = new Vector3();
    }
}
