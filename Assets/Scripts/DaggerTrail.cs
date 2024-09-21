using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerTrail : MonoBehaviour
{

    [SerializeField] GameObject trailPrefab;
    [SerializeField] SkinnedMeshRenderer smr;
    List<BakeTrail> bts;

    [SerializeField] float delay;
    [SerializeField] int trailCnt;
    WaitForSeconds delayWFS;

    Coroutine BCorouine;

    Vector3 prevVector;
    Quaternion prevRotation;

    [SerializeField] int index;



    private void Awake()
    {
        bts = new List<BakeTrail>();
        for (int i = 0; i < trailCnt; i++)
        {
            bts.Add(Instantiate(trailPrefab).GetComponent<BakeTrail>());
            bts[i].SetSMR(smr);
            bts[i].SetMinus(trailCnt);
        }
    }

    void Start()
    {
        

        delayWFS = new WaitForSeconds(delay);

        prevRotation = Quaternion.identity;
        prevVector = transform.localPosition;

        BCorouine = StartCoroutine("BakeCoroutine");
    }


    IEnumerator BakeCoroutine()
    {

        while (true)
        {
           

            bts[index].Bake();
           
            bts[index].SetFade();
            for (int i = 0; i < bts.Count; i++)
            {
                bts[i].FadeOut();
            }

            bts[index].gameObject.transform.position = prevVector;
            bts[index].gameObject.transform.rotation = prevRotation;

            prevVector = transform.position;
            prevRotation = transform.rotation;

            index++;

            if(index >= bts.Count)
            {
                index = 0;
            }

            yield return delayWFS;
        }

    }
}
