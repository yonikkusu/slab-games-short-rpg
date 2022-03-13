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
    [SerializeField] private ItemDetailView itemDetailView;
    [SerializeField] private ItemSynthesisPresenter synthesisPresenter = default;
    [SerializeField] private Button synthesisButton = default;
    [SerializeField] private ItemPanelInput itemPanelInput = default;

    private List<ItemPanelNode> nodeList;

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Start()
    {
        createNodes();
        moveSelectFrame(nodeIndex: PlayerData.Instance.ItemManager.SelectedItemIndex);
        itemPanelNodeBase.gameObject.SetActive(false);
        synthesisPresenter.Initialize();
        synthesisButton.OnClickAsObservable().Subscribe(_ => synthesisPresenter.Show()).AddTo(this);
        itemPanelInput.OnWheelValueChanged.Subscribe(moveCursor).AddTo(this);
    }

    /// <summary>
    /// Viewを更新する
    /// </summary>
    public void RenderView()
    {
        var possessionItemList = PlayerData.Instance.ItemManager?.PossessionItemList;

        for(var i = 0; i < nodeList.Count; i++) {
            var maxNodeIndex = (possessionItemList?.Count - 1) ?? -1;
            if(maxNodeIndex < i) {
                nodeList[i].Initialize(i, ItemID.None);
                continue;
            }
            var itemId = possessionItemList[i].ID;
            nodeList[i].Initialize(i, itemId);
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
            node.Initialize(i, ItemID.None);
            node.OnTap.Subscribe(index => showItemDetailView(index)).AddTo(node);
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
        var hasTargetNode = (nodeList.Count - 1) >= nodeIndex;
        selectFrame.gameObject.SetActive(hasTargetNode);
        if(!hasTargetNode) return;
        selectFrame.transform.SetParent(nodeList[nodeIndex].transform);
        selectFrame.transform.localPosition = new Vector3();
    }

    /// <summary>
    /// アイテム詳細Viewを表示する
    /// </summary>
    /// <param name="index">ノードIndex</param>
    private void showItemDetailView(int index)
    {
        var possessionItemList = PlayerData.Instance.ItemManager.PossessionItemList;
        var hasTargetItem = (possessionItemList.Count - 1) >= index;
        if(!hasTargetItem) return;
        itemDetailView.Initialize(possessionItemList[index]);
        itemDetailView.Show();
    }
}
