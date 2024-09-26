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

   
    public virtual void UseSkill()
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
