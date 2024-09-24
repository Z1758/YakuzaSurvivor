using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.LightingExplorerTableColumn;

public class HitboxManager : MonoBehaviour
{
    [SerializeField] PlayController player;

    [SerializeField] BattleStyleHitbox[] styles;


    [SerializeField] BattleStyleHitbox curStyle;

    Coroutine hitboxCoroutine;

    WaitForSeconds hitboxOffTime = new WaitForSeconds(0.15f);

    GameObject curBox;

    [SerializeField] bool atkType;

    private void Awake()
    {
        SetBox(0);


    }

    public void SetBox(int index)
    {
        curStyle = styles[index];

    }



    private void OnTriggerEnter(Collider other)
    {
        if (atkType || PlayerStat.Instance.StyleIndex  == 3)
        {   
            //다운
            if (other.TryGetComponent<DisMob>(out DisMob component))
                component.Down();
         
        }
        else
        {
            //경직
            if (other.TryGetComponent<DisMob>(out DisMob component))
                component.Hit();
            
        }
        

    }

    public void AtkHitboxCoroutine(int cnt)
    {
       CoroutineStopCheck(hitboxCoroutine);
      
       ResetBox();
        atkType = false;

        hitboxCoroutine = StartCoroutine(OnHitbox(curStyle.atkColWFS[cnt], curStyle.AtkCol[cnt]));
    }

    public void FAtkHitboxCoroutine(int cnt)
    {
        CoroutineStopCheck(hitboxCoroutine);
        ResetBox();
        atkType = true;

        hitboxCoroutine = StartCoroutine(OnHitbox(curStyle.FAtkColWFS[cnt], curStyle.FAtkCol[cnt]));
    }


    public void StartLoopHitboxCoroutine(int cnt)
    {
        CoroutineStopCheck(hitboxCoroutine);

        ResetBox();
        atkType = false;

        hitboxCoroutine = StartCoroutine(OnHitboxEnter(curStyle.startLoopAtkWFS[cnt], curStyle.startLoopAtkCol[cnt],cnt));
    }


  

    public void EndLoopHitboxCoroutine(int cnt)
    {
        
        CoroutineStopCheck(hitboxCoroutine);

        ResetBox();
        atkType = true;

        hitboxCoroutine = StartCoroutine(OnHitbox(curStyle.endLoopAtkWFS[cnt], curStyle.endLoopAtkCol[cnt]));
    }



    IEnumerator OnHitbox(WaitForSeconds wfs, GameObject box)
    {
        yield return wfs;
        box.SetActive(true);
        curBox = box;
        
        yield return hitboxOffTime;
        box.SetActive(false);
    }

    IEnumerator OnHitboxEnter(WaitForSeconds wfs, GameObject box, int cnt)
    {
        yield return wfs;
        box.SetActive(true);
        curBox = box;

        yield return hitboxOffTime;
        box.SetActive(false);
        hitboxCoroutine = StartCoroutine(OnHitboxLoop(curStyle.workLoopAtkWFS[cnt], curStyle.workLoopAtkCol[cnt]));
    }

    IEnumerator OnHitboxLoop(WaitForSeconds wfs, GameObject box)
    {
        while (true)
        {
            yield return wfs;
            box.SetActive(true);
            curBox = box;

            yield return hitboxOffTime;
            box.SetActive(false);
        }
    }


    void CoroutineStopCheck(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }


    void ResetBox()
    {
        if(curBox != null)
        curBox.SetActive(false);
      
    }
}
