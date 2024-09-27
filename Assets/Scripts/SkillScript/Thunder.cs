using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Skill
{
    [SerializeField] GameObject thunderPrefabs;
    [SerializeField] CheckDis cd;
    
    [SerializeField] WaitForSeconds delay;
    [SerializeField] WaitForSeconds delay2;
    int count;

    Coroutine coroutine;

    List<GameObject> thunders;

    private void Awake()
    {
        count =  info.maxLevel + 1;
        delay = new WaitForSeconds(0.4f);

        thunders = new List<GameObject>();

      
            GameObject obj = Instantiate(thunderPrefabs);

            obj.gameObject.SetActive(false);

            thunders.Add(obj);
 

    }

 

  
    public override void SkillLevelUp()
    {
        level++;
        if (level == 1)
        {
            gameObject.SetActive(true);
            coroutine = StartCoroutine(ActiveThunder());
        }
        count--;

        if (level == info.maxLevel)
        {
            delay2 = new WaitForSeconds(0.1f);
            GameObject obj = Instantiate(thunderPrefabs);

            obj.gameObject.SetActive(false);

            thunders.Add(obj);
        }

    }
    public void ActiveSkill(GameObject obj)
    {
        if (MobPool.Instance.activeMob.Count > 0)
        {
            cd.NearDis(obj);
            obj.SetActive(true);
        }
    }

    IEnumerator ActiveThunder()
    {
        while (true)
        {
            for (int i = 0; i < count; i++)
            {
               
                yield return delay;
              
            }
            for(int i=0; i< thunders.Count; i++)
            {
                if (i == 1)
                {
                    yield return delay2;
                }


                ActiveSkill(thunders[i]);
            }

           
        }
     
    }
}
