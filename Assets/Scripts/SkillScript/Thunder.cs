using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : Skill
{
    [SerializeField] GameObject thunderPrefabs;
    [SerializeField] CheckDis cd;
    
    [SerializeField] WaitForSeconds delay;
 
    int count;

    Coroutine coroutine;

    GameObject thunder;

    private void Awake()
    {
        count =  info.maxLevel + 1;
        onDelay = new WaitForSeconds(2.0f);
        

        delay = new WaitForSeconds(0.4f);



        thunder = Instantiate(thunderPrefabs);

        thunder.SetActive(false);
           

    }

 
    public override void UseSkill()
    {
        gameObject.SetActive(true);
        coroutine = StartCoroutine(ActiveThunder());
    }

  
    public override void SkillLevelUp()
    {
        level++;
        if (level == 1)
        {
            UseSkill();
        }
        count--;

        if (level == info.maxLevel)
        {
            GetUlt();

            coroutine = StartCoroutine(ActiveUlt());
        }

    }

  

    public void ActiveSkill(GameObject obj)
    {
        if (MobPool.Instance.activeMob.Count > 0)
        {
            
            
            if(level == info.maxLevel)
            {
                GameObject vec = cd.NearDis();


                obj.transform.position = vec.transform.position + (vec.transform.forward * 2f);
                obj.transform.LookAt(vec.transform);
               
            }
            else
            {
                obj.transform.position = cd.NearDis().transform.position;
            }

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
            

                ActiveSkill(thunder);
            

           
        }
     
    }

    IEnumerator ActiveUlt()
    {
        while (true)
        {
            ActiveSkill(ult);
            yield return onDelay;

        }

    }
}
