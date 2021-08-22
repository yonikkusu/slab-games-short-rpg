using System.Collections.Generic;

/// <summary>
/// アイテム合成 Model
/// </summary>
public class ItemSynthesisModel
{
    /// <summary>所持アイテムリスト</summary>
    public List<ItemData> PossessionItemList { get; private set; }

    /// <summary>現在のステート</summary>
    public ItemSynthesisState CurrentState { get; private set; }
    /// <summary>選択中の素材アイテム1</summary>
    public ItemData SelectedMaterialItem1 { get; private set; }
    /// <summary>選択中の素材アイテム2</summary>
    public ItemData SelectedMaterialItem2 { get; private set; }
    /// <summary>合成後に生成されるアイテム</summary>
    public ItemData ResultItem { get; private set; }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="possessionItemList">所持アイテムリスト</param>
    public ItemSynthesisModel(List<ItemData> possessionItemList)
    {
        PossessionItemList = new List<ItemData>();
        if(possessionItemList != null) {
            PossessionItemList.AddRange(possessionItemList);
        }
        CurrentState = ItemSynthesisState.MaterialItem1PreSelect;
    }

    /// <summary>
    /// 素材アイテム1をセットする
    /// </summary>
    /// <param name="itemListIndex">所持アイテムリストのIndex</param>
    public void SetMaterialItem(int? itemListIndex)
    {
        if(!itemListIndex.HasValue) return;
        switch(CurrentState) {
            case ItemSynthesisState.MaterialItem1PreSelect:
                SelectedMaterialItem1 = PossessionItemList[itemListIndex.Value];
                PossessionItemList.Remove(SelectedMaterialItem1);
                CurrentState = ItemSynthesisState.MaterialItem2PreSelect;
                return;
            case ItemSynthesisState.MaterialItem2PreSelect:
                SelectedMaterialItem2 = PossessionItemList[itemListIndex.Value];
                PossessionItemList.Remove(SelectedMaterialItem2);
                ResultItem = ItemSynthesisMaster.GetByPrimaryKey(SelectedMaterialItem1.ID, SelectedMaterialItem2.ID);
                CurrentState = ItemSynthesisState.SynthesisConfirm;
                return;
            case ItemSynthesisState.SynthesisConfirm:
                return;
            default: return;
        }
    }

    /// <summary>
    /// 素材アイテム1を削除する
    /// </summary>
    public void DeleteMaterialItem1()
    {
        switch(CurrentState) {
            case ItemSynthesisState.MaterialItem2PreSelect:
                PossessionItemList.Add(SelectedMaterialItem1);
                SelectedMaterialItem1 = null;
                CurrentState = ItemSynthesisState.MaterialItem1PreSelect;
                return;
            case ItemSynthesisState.SynthesisConfirm:
                PossessionItemList.Add(SelectedMaterialItem1);
                SelectedMaterialItem1 = SelectedMaterialItem2;
                SelectedMaterialItem2 = null;
                ResultItem = null;
                CurrentState = ItemSynthesisState.MaterialItem2PreSelect;
                return;
            default: return;
        }
    }

    /// <summary>
    /// 素材アイテム2を削除する
    /// </summary>
    public void DeleteMaterialItem2()
    {
        switch(CurrentState) {
            case ItemSynthesisState.SynthesisConfirm:
                PossessionItemList.Add(SelectedMaterialItem2);
                SelectedMaterialItem2 = null;
                ResultItem = null;
                CurrentState = ItemSynthesisState.MaterialItem2PreSelect;
                return;
            default: return;
        }
    }

    /// <summary>
    /// 説明文言を取得する
    /// </summary>
    /// <returns>説明文言</returns>
    public string GetDescription()
    {
        switch(CurrentState) {
            case ItemSynthesisState.MaterialItem1PreSelect:
            case ItemSynthesisState.MaterialItem2PreSelect:
                return "合成に使うアイテムを2つを選択してください。";
            case ItemSynthesisState.SynthesisConfirm:
                return ResultItem != null ? $"{ResultItem.Name}を作成します。よろしいですか？" : "合成できない組み合わせです。";
            default:
                return "";
        }
    }
}

/// <summary>
/// アイテム合成ステート
/// </summary>
public enum ItemSynthesisState
{
    MaterialItem1PreSelect = 1,
    MaterialItem2PreSelect,
    SynthesisConfirm,
}