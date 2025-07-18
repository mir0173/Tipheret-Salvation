using UnityEngine;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextAsset csvFile;

    public class DialogueLine
    {
        public int id;
        public string character;
        public string detail;
        public string image1;
        public string image2;
    }

    public static List<DialogueLine> lines = new List<DialogueLine>();

    void Start()
    {
        LoadCSV();
    }

    void LoadCSV()
    {
        string[] rows = csvFile.text.Split('\n');
        for(int i=1; i<rows.Length; i++) 
        {
            if(string.IsNullOrWhiteSpace(rows[i])) continue;
            string[] cols = rows[i].Split(',');
            DialogueLine dialogue = new DialogueLine();
            dialogue.id = int.Parse(cols[0]);
            dialogue.character = cols[1];
            dialogue.detail = cols[2].Trim(); 
            dialogue.image1 = cols[3].Trim(); 
            dialogue.image2 = cols[4].Trim(); 
            lines.Add(dialogue);
        }
    }
}
