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
#if UNITY_EDITOR
        // 프로젝트 경로 => 게임 제작 중 사용
        string path = $"{Application.dataPath}/CSV";
#else
        // 개인 로컬 저장소 경로 => 게임 제작 완료 후 주로 세이브 파일 저장
        string persPath = Application.persistentDataPath;
        
#endif


        if (Directory.Exists(path) == false)
        {
            Debug.LogError("경로가 없습니다");
            return;
        }

        if (File.Exists($"{path}/Stage.csv") == false)
        {
            Debug.LogError("파일이 없습니다");
            return;
        }
 
        string file = File.ReadAllText($"{path}/Stage.csv");
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
