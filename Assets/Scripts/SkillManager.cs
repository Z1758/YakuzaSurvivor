using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    private static SkillManager instance;
    const int maxLevel = 4;

    [SerializeField] GameObject selectUI;

    [SerializeField] TextMeshProUGUI[] text;
    [SerializeField] Image[] image;


    [SerializeField] float[] typeProbability;

    [SerializeField] List<Skill> passiveSkills;
    [SerializeField] List<Skill> activeSkills;
    [SerializeField] List<Skill> spSkills;



    [SerializeField] List<Skill> resultSkills;

 

    [SerializeField] int activeSkillCount;

    [SerializeField] int skillPoint;


    public UnityAction StopSkills;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.Clear();

           // LevelUP();
        }
    }
    public int GetSkillPoint()
    {
        return skillPoint;
    }


    private void Awake()
    {
        if (null == instance)
        {

            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }

        resultSkills = new List<Skill>();
        
    }
    public static SkillManager Instance
    {

        get
        {

            return instance;
        }

    }
   

    public void LevelUP()
    {
        skillPoint++;
        if (Time.timeScale > 0.8f)
            SetSelect();
    }


    public void SetSelect()
    {
       
      
        while (true)
        {
            if(passiveSkills.Count <= 0 && activeSkills.Count <= 0 && spSkills.Count <= 0)
            {
                Debug.Log("만렙");
                skillPoint = 0;
                return;
            }

                int type = -1;
            type = GetProbability(typeProbability);
 

            Skill skill = null;

            switch (type)
            {
                case (int)SkillType.Passive:

                    if (passiveSkills.Count <= 0)
                    {
                        type = (int)SkillType.Active;
                        continue;
                    }

                    skill = passiveSkills[Random.Range(0, passiveSkills.Count)];
                    break;
                case (int)SkillType.Active:
                    if (activeSkills.Count <= 0)
                    {
                        type = (int)SkillType.Passive;
                        continue;
                    }

                    skill = activeSkills[Random.Range(0, activeSkills.Count)];
                    break;
                case (int)SkillType.Sp:
                    if (spSkills.Count <= 0)
                    {
                        type = (int)SkillType.Passive;
                        continue;
                    }
                    skill = spSkills[Random.Range(0, spSkills.Count)];
                    break;
            }


            //중복 제거
            if (passiveSkills.Count + activeSkills.Count+ spSkills.Count > 2)
            {
               
                bool overlap = false;
                foreach (Skill s in resultSkills)
                {
                    if (skill.info.id == s.info.id)
                    {
                        overlap = true;
                    }
                }
                if (overlap)
                {
                    continue;
                }
            }
           
          

            if(skill!=null)
            resultSkills.Add(skill);

            if (resultSkills.Count >= 3)
            {
                break;
            }
         
        }


        SetSkillUI();

        Cursor.lockState = CursorLockMode.None;

        Cursor.visible = true;
        selectUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void SetSkillUI()
    {
        for(int i = 0; i < resultSkills.Count; i++)
        {
            if (resultSkills[i].level + 1 == resultSkills[i].info.maxLevel && resultSkills[i].info.type == SkillType.Active)
            {
                image[i].sprite = resultSkills[i].info.ultImage;
            }
            else
            {
                image[i].sprite = resultSkills[i].info.image;
            }



            if (resultSkills[i].info.type == SkillType.Active)
            {
                text[i].text = resultSkills[i].info.explain[resultSkills[i].level];
            }
            else
            {
                text[i].text = resultSkills[i].info.explain[0];
            }
        }
    }
  
    public void SelectSkill(int select)
    {

        selectUI.SetActive(false);

        resultSkills[select].SkillLevelUp();

        // 최대 액티브 스킬 개수 달성시 나머지 제거
        if (resultSkills[select].info.type == SkillType.Active && resultSkills[select].level == 1)
        {
            activeSkillCount++;

            StopSkills += resultSkills[select].StopSkill;

            if(activeSkillCount >= 3)
            {
                List<Skill> tempList = new List<Skill>();
                foreach (Skill s in activeSkills)
                {
                    if (s.level == 0)
                    {
                        tempList.Add(s);
                    }
                }

                foreach (Skill s in tempList)
                {
                    activeSkills.Remove(s);
                }

                tempList.Clear();
            }



        }

        // 최대 레벨 달성시 리스트에서 제거
        if (resultSkills[select].level >= resultSkills[select].info.maxLevel)
        {
            switch (resultSkills[select].info.type)
            {
                case SkillType.Passive:
                    passiveSkills.Remove(resultSkills[select]);
                   
                    break;
                case SkillType.Active:
                    activeSkills.Remove(resultSkills[select]);
                    break;
                case SkillType.Sp:
                    spSkills.Remove(resultSkills[select]);
                    break;
            }
        }

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;
 
        resultSkills.Clear();

        skillPoint--;
       

        Time.timeScale = 1.0f;

        if(skillPoint > 0)
        {
            SetSelect();
        }

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
            else
            {
                temp += probabilit[i-1];
                if (temp <= ran && ran < temp + probabilit[i])
                {
                    result = i;
                }
            }
        }
    
        return result;
    }

}
