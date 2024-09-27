using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class CheckDis : MonoBehaviour
{
 
    [SerializeField] LayerMask mask;
    [SerializeField] public Collider[] colliders;

  

    public void NearDis(GameObject obj)
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
            CheckMinBySqr(obj);


            return;
        }

        obj.transform.position = minMob.transform.position;

        colliders = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }


    void CheckMinBySqr(GameObject obj)
    {
      
        List<GameObject> targets = MobPool.Instance.activeMob;
        GameObject closestTarget = null;


        closestTarget = targets.MinBy(target => (target.transform.position - transform.position).sqrMagnitude);
        if (closestTarget == null)
        {
            return;
        }

        obj.transform.position = closestTarget.transform.position;
    }
}
