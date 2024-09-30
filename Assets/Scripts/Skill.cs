using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum SkillType
{
    Passive, Active, Sp, END
}
public class Skill : MonoBehaviour
{
    public SkillInfo info;
    public int level;
    [SerializeField] protected GameObject ult;
    [SerializeField] protected AudioClip ultClip;
    [SerializeField] protected AudioClip ultBgmClip;
    [SerializeField] protected Image uiUltAniImage;
    protected WaitForSeconds onDelay;
    protected WaitForSeconds offDelay;
    public virtual void UseSkill()
    {

    }

    public virtual void GetUlt()
    {
        StopAllCoroutines();
        UISoundManager.Instance.PlayerUISound(ultClip);
        BGM_Manager.Instance.ChangeBgmOnce(ultBgmClip);
        PlayUltUIAni();
    }


    public virtual void SkillLevelUp()
    {
        level++;
        if (level == 1)
        {
            gameObject.SetActive(true);
        }
    }

    public virtual void PlayUltUIAni()
    {
        uiUltAniImage.sprite = info.ultUIImage;
        uiUltAniImage.gameObject.SetActive(true);
    }

    public virtual void StopSkill()
    {
        StopAllCoroutines();
        if(ult!=null)
        ult.SetActive(false);
    }
}
