using System.Collections;
using UnityEngine;

public enum AniState
{
    Idle = 0, Run, Atk, FAtk, LoopAtk, LoopEnd, InputWait
}

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] PlayController player;

    [SerializeField] BattleStyle[] styles;


    [SerializeField] BattleStyle curStyle;



    WaitForSeconds comboEndTime = new WaitForSeconds(0.1f);
    WaitForSeconds loopDTime = new WaitForSeconds(1f);


    Coroutine atkAniEnd;
    Coroutine loopCountCoroutine;

    private void Awake()
    {
        SetAnim(0);


    }

    public void SetAnim(int index)
    {
        curStyle = styles[index];
        anim.runtimeAnimatorController = curStyle.anim;

    }
    public void SetWFS()
    {
        /*
          atkEndTime = new WaitForSeconds[curStyle.atkClips.Length];
          atkInputTime = new WaitForSeconds[curStyle.atkClips.Length];
          fEndTime = new WaitForSeconds[curStyle.fClips.Length];
          lEndTime = new WaitForSeconds[curStyle.fClips.Length];
          lAtkInputTime = new WaitForSeconds[curStyle.fClips.Length];
          lAtkEndTime = new WaitForSeconds[curStyle.fClips.Length];


          for (int i = 0; i < curStyle.atkClips.Length; i++)
          {

              atkEndTime[i] = new WaitForSeconds(curStyle.atkClips[i].length * curStyle.atkEndTimeRatio[i]);


              atkInputTime[i] = new WaitForSeconds(curStyle.atkClipsEnd[i]);


              fEndTime[i] = new WaitForSeconds(curStyle.fClips[i].length * 0.95f);

              lEndTime[i] = new WaitForSeconds(curStyle.loopAtkTime[i]);

              lAtkInputTime[i] = new WaitForSeconds(curStyle.loopAtkInputTime[i]);

              lAtkEndTime[i] = new WaitForSeconds(curStyle.loopAtkEndTime[i]);
          }

          changeTime = new WaitForSeconds(curStyle.changeTime);
          */

    }


    public bool GetAniName(string name)
    {


        if (anim.GetCurrentAnimatorStateInfo(0).IsName(name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }




    public void AtkAniCoroutine(int cnt, bool type)
    {
        CoroutineStopCheck(atkAniEnd);

        if (type)
        {
            if (ChangeState.Instance.index == 3)
            {
                player.ColOff();
            }

            PlayAtk(cnt);
            atkAniEnd = StartCoroutine(AtkAniEnd(curStyle.atkEndTime[cnt], curStyle.atkInputTime[cnt]));
        }
        else
        {
            PlayFAtk(cnt );
            atkAniEnd = StartCoroutine(FAtkAniEnd(curStyle.fEndTime[cnt ]));
        }



    }


    public IEnumerator AtkAniEnd(WaitForSeconds time, WaitForSeconds inputTime)
    {


        yield return time;

        player.AtkStateChange();
        if (ChangeState.Instance.index == 3)
        {
            player.ColOn();
        }

        yield return inputTime;

        player.AtkInputTimeEnd();

        yield return comboEndTime;
        player.ComboReset();

    }
    public IEnumerator FAtkAniEnd(WaitForSeconds time)
    {


        yield return time;
        player.FAtkEnd();


    }
 

    public void LoopAtkAniCoroutine(int cnt)
    {
        CoroutineStopCheck(atkAniEnd);



        atkAniEnd = StartCoroutine(AtkAniEnd(curStyle.lEndTime[cnt ], curStyle.lAtkInputTime[cnt ]));

    }
    public void LoopEndAniCoroutine(int cnt)
    {
        CoroutineStopCheck(atkAniEnd);


        atkAniEnd = StartCoroutine(LoopAniEnd(curStyle.lAtkEndTime[cnt]));


    }

    public void DecreaseLoopCountStart()
    {
        CoroutineStopCheck(loopCountCoroutine);

        loopCountCoroutine = StartCoroutine(DecreaseLoopCountCoroutine());

    }

    public IEnumerator DecreaseLoopCountCoroutine()
    {
        while (LoopAttackState.Instance.loopCount > 1)
        {
            yield return loopDTime;
            LoopAttackState.Instance.loopCount--;
            if (LoopAttackState.Instance.loopCount < 0)
            {
                LoopAttackState.Instance.loopCount = 0;
                break;
            }
        }

    }

    public IEnumerator LoopAniEnd(WaitForSeconds time)
    {


        yield return time;
        player.LoopAniEnd();

    }


    public void ChangeAniCoroutine(int index)
    {
        CoroutineStopCheck(atkAniEnd);



        anim.Play(curStyle.styleChangeAni.name);
        atkAniEnd = StartCoroutine(FAtkAniEnd(curStyle.changeTimeWFS));

    }

    public void PlayAtk(int cnt)
    {

        anim.Play(curStyle.atkClips[cnt].name);
    }

    public void PlayFAtk(int cnt)
    {
        anim.Play(curStyle.fClips[cnt].name);

    }

    public int GetAtkClipCount()
    {

        return curStyle.atkClips.Length;
    }

    public int LoopAtkCheck(int cnt)
    {

        return curStyle.loopCheck[cnt];

    }
    public bool LoopMoveCheck(int cnt)
    {

        return curStyle.loopMoveCheck[cnt];

    }
    void CoroutineStopCheck(Coroutine coroutine)
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }
}
