using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// テスト画面マネージャー
/// </summary>
public class TestManager : MonoBehaviour
{
    /// <summary>
    /// テスト用セーブデータ
    /// NOTE: SerializeFieldを付ける or publicにしないとJsonに変換できない
    ///       (=SetClassでセーブ出来ない)ので注意
    /// </summary>
    public class TestSaveData
    {
        /// <summary>テスト用bool値</summary>
        public bool BoolData;
        /// <summary>テスト用int値</summary>
        public int IntData;
        /// <summary>テスト用float値</summary>
        public float FloatData;
        /// <summary>テスト用string値</summary>
        public string StringData;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="boolData">bool値</param>
        /// <param name="intData">int値</param>
        /// <param name="floatData">float値</param>
        /// <param name="stringData">string値</param>
        public TestSaveData(bool boolData = false, int intData = 0, float floatData = 0f, string stringData = "")
        {
            BoolData = boolData;
            IntData = intData;
            FloatData = floatData;
            StringData = stringData;
        }
    }

    [SerializeField] private Button saveButton = default;
    [SerializeField] private Button loadButton = default;
    [SerializeField] private Button deleteButton = default;
    [SerializeField] private Text playerDataText = default;
    [SerializeField] private TextAsset playerData = default;

    /// <summary>
    /// 起動時処理
    /// </summary>
    void Start()
    {
        // セーブボタン
        saveButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            // テキストファイルから文字列データをカンマ区切りで取り出す
            // NOTE: 先頭から順にbool,int,float,string型の値をテキストファイルに入れている
            var splitData = playerData.text.Split(',');
            // 取り出したデータをPlayerPrefsにセットする
            SaveManager.Instance.SetBool(SaveKey.TestData, bool.Parse(splitData[0]), "bool");
            SaveManager.Instance.SetInt(SaveKey.TestData, int.Parse(splitData[1]), "int");
            SaveManager.Instance.SetFloat(SaveKey.TestData, float.Parse(splitData[2]), "float");
            SaveManager.Instance.SetString(SaveKey.TestData, splitData[3], "string");
            // NOTE: TestSaveDataはクラス保存テスト用のクラス
            //       上記4つの値をセットしている
            var testSaveData = new TestSaveData(
                bool.Parse(splitData[0]), 
                int.Parse(splitData[1]), 
                float.Parse(splitData[2]), 
                splitData[3]
            );
            SaveManager.Instance.SetClass<TestSaveData>(SaveKey.TestData, testSaveData, "class");
            // ここで実際にディスクにセットしたデータがセーブされる
            SaveManager.Instance.Save();
        });
        // ロードボタン
        loadButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            // セーブしたデータを取り出す
            var boolValue = SaveManager.Instance.GetBool(SaveKey.TestData, subKey: "bool");
            var intValue = SaveManager.Instance.GetInt(SaveKey.TestData, subKey: "int");
            var floatValue = SaveManager.Instance.GetFloat(SaveKey.TestData, subKey: "float");
            var stringValue = SaveManager.Instance.GetString(SaveKey.TestData, subKey: "string");
            var classValue = SaveManager.Instance.GetClass<TestSaveData>(SaveKey.TestData, new TestSaveData(), subKey: "class");
            // 取り出したデータを表示する
            playerDataText.text = $"{boolValue},{intValue},{floatValue},{stringValue},"
                + $"{classValue.BoolData},{classValue.IntData},{classValue.FloatData},{classValue.StringData}";
        });
        // 削除ボタン
        deleteButton.onClick.AddListener(() => {
            SoundManager.Instance.PlaySe(Se.Tap);
            SaveManager.Instance.DeleteAll();
        });
    }
}
