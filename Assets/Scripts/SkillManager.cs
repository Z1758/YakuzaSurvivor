using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    const int maxLevel = 4;

    [SerializeField] GameObject selectUI;

    [SerializeField] float[] levelProbability;
    [SerializeField] float[] typeProbability;

    [SerializeField] SkillInfo[] passiveSkills;
    [SerializeField] SkillInfo[] activeSkills;
    [SerializeField] SkillInfo[] spSkills;



    [SerializeField] TextMeshProUGUI[] text;
    [SerializeField] Image[] image;

    [SerializeField] List<int> resultID;
    [SerializeField] List<int> resultLevel;
    [SerializeField] List<Skill> resultSkills;

    [SerializeField] List<SkillInfo> skillInfos;

    [SerializeField] List<Skill> skills;

    [SerializeField] int activeSkillCount;

    private void Awake()
    {
        resultID = new List<int>();
        resultLevel = new List<int>();
        resultSkills = new List<Skill>();
        skillInfos = new List<SkillInfo>();
   
        foreach (SkillInfo info in passiveSkills)
        {
            skillInfos.Add(info);
        }
        foreach (SkillInfo info in activeSkills)
        {
            skillInfos.Add(info);
        }
        foreach (SkillInfo info in spSkills)
        {
            skillInfos.Add(info);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           StartCoroutine(LevelUP());
        }
    }

    public IEnumerator LevelUP()
    {
      
        while (true)
        {
            
            yield return new WaitForSeconds(0.02f);
            int grade = GetProbability(levelProbability);
            int type = GetProbability(typeProbability);

            int id = 0;

            if (type ==2 && spSkills.Length <= 0)
            {
                continue;
            }

            switch (type)
            {
                case 0:

                    id = passiveSkills[Random.Range(0, passiveSkills.Length)].id;
                    break;
                case 1:
                    id = activeSkills[Random.Range(0, activeSkills.Length)].id;
                    break;
                case 2:
                    
                    id = spSkills[Random.Range(0, spSkills.Length)].id;
                    break;
            }
         
            //중복 제거
            bool overlap = false;
            foreach (int i in resultID)
            {
                if (id == i)
                {
                    overlap = true;
                }
            }
            if (overlap)
            {
                continue;
            }

           
            foreach (Skill s in skills)
            {
              


                if (s.id == id && s.level < maxLevel)
                {

                    // 타입 == 1 액티브 스킬
                    // 레벨 == 0 새로 습득해야하는 스킬
                    // count == 3 습득할 수 있는 액티브 스킬의 최대치
                    if (type == 1 && s.level == 0 && activeSkillCount == 3)
                    {
                        break;
                    }

                    

                    resultID.Add(id);
                    resultLevel.Add(s.level);
                    resultSkills.Add(s);
                    break;
                }
            }

            if (resultID.Count >= 3)
            {
                break;
            }
            Debug.Log("연산중");
        }




        for (int i = 0; i < image.Length; i++)
        {
            foreach (SkillInfo s in skillInfos)
            {
                if (s.id.Equals(resultID[i]))
                {
                  
                    image[i].sprite = s.image;
                    if (100 < s.id && s.id < 200)
                    {
                        text[i].text = s.explain[resultLevel[i]];
                    }
                    else
                    {
                        text[i].text = s.explain[0];
                    }
                }

            }

        }
        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
        selectUI.SetActive(true);
    }

    public int GetProbability(float[] probabilit)
    {
        float ran = Random.Range(0, 101);
        int result = 0;
        float temp = 0;
        for (int i = 0; i < probabilit.Length; i++)
        {
            if (i == 0)
            {
                if (0 <= ran && ran < probabilit[0])
                {
                    result = 0;
                    break;
                }
            }
            temp += probabilit[i];
            if (temp <= ran && ran < temp + probabilit[i])
            {
                result = i;
            }
        }
        return result;
    }

    public void SelectSkill(int select)
    {

        selectUI.SetActive(false);
        foreach (Skill s in resultSkills) {
            if (s.id.Equals(resultID[select]))
            {
                if(s.level == 0 && 100 < s.id && s.id < 200)
                {
                    activeSkillCount++;
                }
                s.level++;

                if(200 < s.id && s.id < 300)
                {
                    foreach (SkillInfo si in skillInfos)
                    {
                        if (si.id == s.id)
                        {
                            skillInfos.Remove(si);
                            break;
                        }
                    }
                }


                if(s.level >= maxLevel)
                {
                    foreach(SkillInfo si in skillInfos)
                    {
                       if(si.id == s.id)
                        {
                            skillInfos.Remove(si);
                            break;
                        }
                    }
                   
                }
            }
         }
        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
        resultID.Clear();
        resultLevel.Clear();
        resultSkills.Clear();
    }
}
