using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
public class CheckDis : MonoBehaviour
{
    [SerializeField] GameObject b;
    [SerializeField] LayerMask mask;
    [SerializeField] public Collider[] colliders;

    Stopwatch watch0 = new Stopwatch();
    Stopwatch watch00 = new Stopwatch();
    Stopwatch watch1 = new Stopwatch();
    Stopwatch watch2 = new Stopwatch();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
      {
        //NearDis();
        /*
        watch0.Reset();
        watch00.Reset();

        
        watch1.Reset();
        watch2.Reset();

        watch1.Start();
        NearDis();
        watch1.Stop();
       
        UnityEngine.Debug.Log("NearDis : " + watch1.Elapsed.TotalMilliseconds + " ms");

        watch2.Start();
        CheckMinDis();
        watch2.Stop();
        
        UnityEngine.Debug.Log("CheckMinDis : " + watch2.Elapsed.TotalMilliseconds + " ms");
        



        watch0.Start();
        CheckMinByDis();
        watch0.Stop();
    
        UnityEngine.Debug.Log("CheckMinByDis : " + watch0.Elapsed.TotalMilliseconds + " ms");
       
        watch00.Start();
        CheckMinBySqr();
        watch00.Stop();

        UnityEngine.Debug.Log("CheckMinBySqr : " + watch00.Elapsed.TotalMilliseconds + " ms");
         */
    }

    void CheckMinDis()
    {
        List<GameObject> mob = MobPool.Instance.activeMob;

        GameObject minMob = null;

        for (int i = 0; i < mob.Count; i++)
        {
            if (i >= mob.Count)
            {
                break;
            }
            if (minMob == null)
            {
                minMob = mob[i];
            }
            else if ((mob[i].transform.position - transform.position).sqrMagnitude < (minMob.transform.position - transform.position).sqrMagnitude)
            {

                minMob = mob[i];
            }

        }

        if (minMob == null)
        {
            return;
        }

        b.transform.position = minMob.transform.position;


    }

    void NearDis()
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
            CheckMinBySqr();


            return;
        }

        b.transform.position = minMob.transform.position;

        colliders = null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }


    void CheckMinByDis()
    {
        List<GameObject> targets = MobPool.Instance.activeMob;
        GameObject closestTarget = null;

        closestTarget = targets.MinBy(target => Vector3.Distance(target.transform.position, transform.position));

        if (closestTarget == null)
        {
            return;
        }

        b.transform.position = closestTarget.transform.position;
    }

    void CheckMinBySqr()
    {
        List<GameObject> targets = MobPool.Instance.activeMob;
        GameObject closestTarget = null;


        closestTarget = targets.MinBy(target => (target.transform.position - transform.position).sqrMagnitude);
        if (closestTarget == null)
        {
            return;
        }

        b.transform.position = closestTarget.transform.position;
    }
}
