using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpSkill : Skill
{

  
    [SerializeField] float cooltime;
    WaitForSeconds cool;
    bool coolTimeFlag;
    private void Awake()
    {
        cool = new WaitForSeconds(cooltime);
    }

    public bool CoolDownCheck(Vector3 vec)
    {
        if (coolTimeFlag == false)
        {
            Vector3 newVec = new Vector3((transform.position.x + vec.x) / 2, 0f, (transform.position.z + vec.z) / 2);

            ult.transform.position = newVec;
            ult.transform.LookAt(vec);
            ult.gameObject.SetActive(true);
          
            coolTimeFlag = true;

            StartCoroutine(CoolDown());

            return true;
        }
        else
        {

            return false;
        }
    }

    IEnumerator CoolDown()
    {
       yield return cool;
        coolTimeFlag = false;
    }

    public override void UseSkill()
    {
        UISoundManager.Instance.PlayerUISound(ultClip);
        BGM_Manager.Instance.ChangeBgmOnce(ultBgmClip);
        PlayController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayController>();
        player.hitEvent = new PlayController.HitEvent(CoolDownCheck);

        gameObject.SetActive(true);
    }

    public override void SkillLevelUp()
    {
        level++;
        if (level == 1)
        {
            UseSkill();
        }
    }
}
