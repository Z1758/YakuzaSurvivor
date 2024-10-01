using System.Collections;
using UnityEngine;

public enum AniState
{
    Idle = 0, Run, Atk, FAtk, LoopAtk, LoopEnd, InputWait, Die
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

    [SerializeField] GameObject darkUI;

    private void Awake()
    {
        SetAnim(0);
    }

    public void SetAnim(int index)
    {

        if (curStyle.weapon != null)
        {
            CharacterTrail.Instance.OffDaggerTrail();
            curStyle.weapon.SetActive(false);
        }

        curStyle = styles[index];
        anim.runtimeAnimatorController = curStyle.anim;

        if (curStyle.weapon != null)
        {
            curStyle.weapon.SetActive(true);
            if(index == (int)BattleStyleType.Legend )
            {
                CharacterTrail.Instance.OnDaggerTrail();
            }

        }
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
            if (PlayerStat.Instance.StyleIndex == (int)BattleStyleType.Legend)
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
        if (PlayerStat.Instance.StyleIndex == (int)BattleStyleType.Legend)
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
    public IEnumerator ChangeAniEnd(WaitForSecondsRealtime time)
    {

        Time.timeScale = 0.2f;
        darkUI.SetActive(true);
        yield return time;
       
        Time.timeScale = 1.0f;
        anim.updateMode = AnimatorUpdateMode.Normal;
        darkUI.SetActive(false);
        player.FAtkEnd();

        if(SkillManager.Instance.GetSkillPoint() > 0)
        {
            SkillManager.Instance.SetSelect();
        }



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


        anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        anim.Play(curStyle.styleChangeAni.name);
        atkAniEnd = StartCoroutine(ChangeAniEnd(curStyle.changeTimeWFS));

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

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
