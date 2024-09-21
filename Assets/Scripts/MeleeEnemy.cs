using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : DisMob
{
    protected override void Attack()
    {
        base.Attack();
        Debug.Log("M Atk");
    }

    


}
