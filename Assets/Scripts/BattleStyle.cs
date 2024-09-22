using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStyle : MonoBehaviour
{

    [SerializeField] public GameObject weapon;
    [SerializeField] public AnimationClip styleChangeAni;
    [SerializeField] public RuntimeAnimatorController anim;

    [SerializeField] public float changeTime;

    [SerializeField] public float[] atkEndTimeRatio;
    [SerializeField] public AnimationClip[] atkClips;
    [SerializeField] public float[] atkClipsEnd;
    [SerializeField] public AnimationClip[] fClips;
    [SerializeField] public float[] fClipsEnd;

   

    [SerializeField] public float[]loopAtkTime;
    [SerializeField] public float[] loopAtkInputTime;
    [SerializeField] public float[] loopAtkEndTime;

    [SerializeField] public int[] loopCheck;
    [SerializeField] public bool[] loopMoveCheck;

    public WaitForSeconds[] atkEndTime;
    public WaitForSeconds[] atkInputTime;
    public WaitForSeconds[] fEndTime;

    public WaitForSeconds[] lEndTime;
    public WaitForSeconds[] lAtkInputTime;
    public WaitForSeconds[] lAtkEndTime;



    public WaitForSeconds changeTimeWFS;


    private void Awake()
    {
        InitWFS();
    }
    public void InitWFS()
    {
        atkEndTime = new WaitForSeconds[atkClips.Length];
        atkInputTime = new WaitForSeconds[atkClips.Length];
        fEndTime = new WaitForSeconds[fClips.Length];
        lEndTime = new WaitForSeconds[fClips.Length];
        lAtkInputTime = new WaitForSeconds[fClips.Length];
        lAtkEndTime = new WaitForSeconds[fClips.Length];
        

        for (int i = 0; i < atkClips.Length; i++)
        {

            atkEndTime[i] = new WaitForSeconds(atkClips[i].length * atkEndTimeRatio[i]);
            

            atkInputTime[i] = new WaitForSeconds(atkClipsEnd[i]);

            
        }
        for (int i = 0; i < fClips.Length; i++)
        {
            if (fClipsEnd[i] == 0)
            {
                fEndTime[i] = new WaitForSeconds(fClips[i].length * 0.95f);
            }
            else
            {
                fEndTime[i] = new WaitForSeconds(fClipsEnd[i]);
            }

            lEndTime[i] = new WaitForSeconds(loopAtkTime[i]);

            lAtkInputTime[i] = new WaitForSeconds(loopAtkInputTime[i]);

            lAtkEndTime[i] = new WaitForSeconds(loopAtkEndTime[i]);
        }
        changeTimeWFS = new WaitForSeconds(changeTime);
    }


}
