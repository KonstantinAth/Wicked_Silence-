using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour {
    [System.Serializable]
    public struct InstructionData {
        public string instuctionName;
        public string instructionContent;
    }
    public InstructionData[] instructionDataToFeed;
    [SerializeField]
    public Dictionary<string, InstructionData> instructionData = new Dictionary<string, InstructionData>();
    public GameObject MainCanvas;
    public TextMeshProUGUI MainTextBox;
    // Start is called before the first frame update
    void Start() {
        MainCanvas.SetActive(false);
        InitDictionary();
    }
    public void InitDictionary() {
        for (int i = 0; i < instructionDataToFeed.Length; i++) {
            instructionData.Add(instructionDataToFeed[i].instuctionName, instructionDataToFeed[i]);
        }
    }
    public void SetCanvasState(bool open) {
        MainCanvas.SetActive(open);
    }
    public void SetTextBox(string name) {
        foreach(KeyValuePair<string, InstructionData> data in instructionData) {
            if(data.Key.Equals(name)) {
                MainTextBox.text = data.Value.instructionContent;
            }
        }
    }
}
