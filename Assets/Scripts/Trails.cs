using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trails : MonoBehaviour
{

    [SerializeField] GameObject trailPrefab;
    [SerializeField] SkinnedMeshRenderer smr;
    List<BakeTrail> bts;
    List<GameObject> trails;
    [SerializeField] float delay;
    [SerializeField] int trailCnt;
    WaitForSeconds delayWFS;

    Coroutine BCorouine;

    Vector3 prevVector;
    Quaternion prevRotation;

    [SerializeField] int index;



    private void Awake()
    {
        trails = new List<GameObject> ();
        bts = new List<BakeTrail>();
        for (int i = 0; i < trailCnt; i++)
        {
            GameObject trail = Instantiate(trailPrefab);
            trails.Add(trail);
            bts.Add(trail.GetComponent<BakeTrail>());
            bts[i].SetSMR(smr);
            bts[i].SetMinus(trailCnt);
            trail.SetActive(false);

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
    public void OnTrails(Color color)
    {
        for (int i = 0; i < trails.Count; i++)
        {
            bts[i].SetColor(color);
            bts[i].gameObject.transform.position = transform.position;
            bts[i].gameObject.transform.rotation = transform.rotation;
            trails[i].SetActive(true);
        }
    }

    public void OnTrail()
    {
        for (int i = 0; i < trails.Count; i++)
        {
         
            trails[i].SetActive(true);
        }
    }
    public void OffTrail()
    {
        for (int i = 0; i < trails.Count; i++)
        {
            trails[i].SetActive(false);
        }
    }
}
