using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zone : Skill
{
    [SerializeField] GameObject zone;
  




    Coroutine coroutine;

    [SerializeField] float scale;

  
    private void Awake()
    {

        onDelay = new WaitForSeconds(0.7f);
        offDelay = new WaitForSeconds(0.3f);

    }

    public override void UseSkill()
    {
        if (level == 1)
        {
            gameObject.SetActive(true);
            coroutine = StartCoroutine(ActiveZone(zone));
        }

    }


    public override void SkillLevelUp()
    {
        level++;
        if (level == 1)
        {
            UseSkill();
        }

        if (level == info.maxLevel)
        {
            GetUlt();
          

            zone.SetActive(false);


            onDelay = new WaitForSeconds(1.0f);
            offDelay = new WaitForSeconds(2.7f);

            coroutine = StartCoroutine(ActiveZone(ult));
            return;
            
        }
        zone.transform.localScale = new Vector3(zone.transform.localScale.x + scale, zone.transform.localScale.y, zone.transform.localScale.z + scale);
    }

  
    IEnumerator ActiveZone(GameObject obj)
    {
        while (true)
        {

            obj.SetActive(true);
            yield return offDelay;
            obj.SetActive(false);
            yield return onDelay;


        }

    }


}
