using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public struct StageData
{
    public int count;
    public float delay;
    public int type;

}

public class CSV_Parser : MonoBehaviour
{

    public Queue<StageData> stageDatas= new Queue<StageData>();



    private void Awake()
    {
        TextAsset csv = Resources.Load<TextAsset>("CSV/Stage") ;
     
       
 
        string file = csv.text;
        string[] lines = file.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            StageData data = new StageData();
            string[] values = lines[i].Split(',', '\t');

         
            data.type = int.Parse(values[0])-1;
            data.count = int.Parse(values[1]);
            data.delay = float.Parse(values[2]);
    
            stageDatas.Enqueue(data);
           
            
        }



    }


}
