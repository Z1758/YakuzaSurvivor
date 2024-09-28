using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityIncrease : Skill
{
    enum AbiltyType
    {
        Atk, Speed ,End
    }
    [SerializeField] AbiltyType type;


    public override void UseSkill()
    {
        switch (type)
        {
            case AbiltyType.Atk:
                PlayerStat.Instance.ATK += 2;
         
                break;
            case AbiltyType.Speed:
                PlayerStat.Instance.DEFAULTSPEED += 0.4f;

                break;
        }
    }
    public override void SkillLevelUp()
    {
     
        level++;
        UseSkill();
    }

}
