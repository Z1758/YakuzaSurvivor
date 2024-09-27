using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Zone : Skill
{
    [SerializeField] GameObject zone;

    [SerializeField] WaitForSeconds onDelay;
    [SerializeField] WaitForSeconds offDelay;



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
            coroutine = StartCoroutine(ActiveZone());
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
            scale = scale * 2;
            
        }
        zone.transform.localScale = new Vector3(zone.transform.localScale.x + scale, zone.transform.localScale.y, zone.transform.localScale.z + scale);
    }
  
    IEnumerator ActiveZone()
    {
        while (true)
        {

            zone.SetActive(true);
            yield return offDelay;
            zone.SetActive(false);
            yield return onDelay;


        }

    }

}
