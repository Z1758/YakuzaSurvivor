using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxManager : MonoBehaviour
{
    [SerializeField] PlayController player;

    [SerializeField] BattleStyleHitbox[] styles;


    [SerializeField] BattleStyleHitbox curStyle;

    [SerializeField] GameObject effect;

    Coroutine hitboxCoroutine;

    WaitForSeconds hitboxOffTime = new WaitForSeconds(0.15f);

    GameObject curBox;

    [SerializeField] bool atkType;

    bool audioOnce;

    int curCnt;

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
        int damage;
        int atk;
        float mul;
        if (atkType || PlayerStat.Instance.StyleIndex  == 3)
        {   
            //다운
            if (other.TryGetComponent<Enemy>(out Enemy component))
                component.Down();
             atk = PlayerStat.Instance.ATK ;
             mul = PlayerStat.Instance.Multiplier[PlayerStat.Instance.StyleIndex].fAtkMultiplier[curCnt];

            if(mul == 0)
            {
                mul = PlayerStat.Instance.Multiplier[PlayerStat.Instance.StyleIndex].loopAtkEndMultiplier[curCnt];
            }

            damage = (int)(atk * mul) ;

        
            component.TakeDamage(damage);
         
        }
        else
        {
            //경직
            if (other.TryGetComponent<Enemy>(out Enemy component))
                component.Hit();
             atk = PlayerStat.Instance.ATK;
             mul = PlayerStat.Instance.Multiplier[PlayerStat.Instance.StyleIndex].atkMultiplier[curCnt];

            if (mul == 0)
            {
                mul = PlayerStat.Instance.Multiplier[PlayerStat.Instance.StyleIndex].loopAtkMultiplier[curCnt];
            }


            damage = (int)(atk * mul);

            

            component.TakeDamage(damage);
        }

        Debug.Log(damage);

        Vector3 vec = other.ClosestPoint(transform.position);

        
       
        if (audioOnce == false)
        {
            HitSoundManager.Instance.PlayerHitSound(vec, curCnt, atkType);
            audioOnce = true;
        }
        
        Instantiate(effect, vec, Quaternion.identity);
        

    }

    public void AtkHitboxCoroutine(int cnt)
    {
       CoroutineStopCheck(hitboxCoroutine);
      
       ResetBox();
        curCnt = cnt;
        atkType = false;

        hitboxCoroutine = StartCoroutine(OnHitbox(curStyle.atkColWFS[cnt], curStyle.AtkCol[cnt]));
    }

    public void FAtkHitboxCoroutine(int cnt)
    {
        CoroutineStopCheck(hitboxCoroutine);
        ResetBox();
        curCnt = cnt;
        atkType = true;

        hitboxCoroutine = StartCoroutine(OnHitbox(curStyle.FAtkColWFS[cnt], curStyle.FAtkCol[cnt]));
    }


    public void StartLoopHitboxCoroutine(int cnt)
    {
        CoroutineStopCheck(hitboxCoroutine);

        ResetBox();
        curCnt = cnt;
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
        ResetAudioCall();
        box.SetActive(true);
        curBox = box;
        
        yield return hitboxOffTime;
        box.SetActive(false);
    }

    IEnumerator OnHitboxEnter(WaitForSeconds wfs, GameObject box, int cnt)
    {
        yield return wfs;
        ResetAudioCall();
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
            ResetAudioCall();
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
        if (curBox != null)
        {
            curBox.SetActive(false);
            ResetAudioCall();
        }
      
    }

    void ResetAudioCall()
    {
        audioOnce = false;
    }
}
