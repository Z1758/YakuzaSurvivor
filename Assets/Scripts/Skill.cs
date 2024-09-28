using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    protected WaitForSeconds onDelay;
    protected WaitForSeconds offDelay;
    public virtual void UseSkill()
    {

    }

    public virtual void GetUlt()
    {

    }


    public virtual void SkillLevelUp()
    {
        level++;
        if (level == 1)
        {
            gameObject.SetActive(true);
        }
    }
}
