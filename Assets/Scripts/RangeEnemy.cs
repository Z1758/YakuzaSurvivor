using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] GameObject bullet;

    protected override void Attack()
    {
        StopCoroutineNullCheck();
        state = StartCoroutine(RangeAttackCorutine(bullet));
       
    }

    


}
