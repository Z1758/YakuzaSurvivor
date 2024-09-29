using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class CheckDis : MonoBehaviour
{
 
    [SerializeField] LayerMask mask;
    [SerializeField] public Collider[] colliders;

  

    public GameObject NearDis()
    {
        colliders = Physics.OverlapSphere(transform.position, 10f, mask);
        GameObject minMob = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (i >= colliders.Length)
            {
                break;
            }
            if (minMob == null)
            {
                minMob = colliders[i].gameObject;
            }
            else if ((colliders[i].transform.position - transform.position).sqrMagnitude < (minMob.transform.position - transform.position).sqrMagnitude)
            {

                minMob = colliders[i].gameObject;
            }

        }

        if (minMob == null)
        {
            return CheckMinBySqr();


            
        }
        colliders = null;

        return minMob;

       
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }

    */
    GameObject CheckMinBySqr()
    {
      
        List<GameObject> targets = MobPool.Instance.activeMob;
        GameObject closestTarget = null;


        closestTarget = targets.MinBy(target => (target.transform.position - transform.position).sqrMagnitude);
        if (closestTarget == null)
        {
            return null;
        }

        return closestTarget;
        
    }
}
