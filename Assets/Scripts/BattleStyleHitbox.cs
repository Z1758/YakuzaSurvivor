using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStyleHitbox : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] public GameObject[] AtkCol;
    [SerializeField] float[] AtkColTime;

    [Header("FinishAttack")]
    [SerializeField] public GameObject[] FAtkCol;
    [SerializeField] float[] FAtkColTime;


    [Header("LoopAttack")]
    [SerializeField] public GameObject[] startLoopAtkCol;
    [SerializeField] float[] startLoopAtkColTime;

    [SerializeField] public GameObject[] workLoopAtkCol;
    [SerializeField] float[] workLoopAtkColTime;

    [SerializeField] public GameObject[] endLoopAtkCol;
    [SerializeField] float[] endLoopAtkColTime;


    public WaitForSeconds[] atkColWFS;
    public WaitForSeconds[] FAtkColWFS;

    public WaitForSeconds[] startLoopAtkWFS;
    public WaitForSeconds[] workLoopAtkWFS;
    public WaitForSeconds[] endLoopAtkWFS;

    private void Awake()
    {
        InitWFS();
    }
    public void InitWFS()
    {
        atkColWFS = new WaitForSeconds[AtkColTime.Length];
        FAtkColWFS = new WaitForSeconds[FAtkColTime.Length];

        startLoopAtkWFS = new WaitForSeconds[startLoopAtkColTime.Length];
        workLoopAtkWFS = new WaitForSeconds[workLoopAtkColTime.Length];
        endLoopAtkWFS = new WaitForSeconds[endLoopAtkColTime.Length];


        for (int i = 0; i < AtkColTime.Length; i++)
        {

            atkColWFS[i] = new WaitForSeconds(AtkColTime[i]);

        }
        for (int i = 0; i < FAtkColTime.Length; i++)
        {
            FAtkColWFS[i] = new WaitForSeconds(FAtkColTime[i]);

        }
        for (int i = 0; i < startLoopAtkColTime.Length; i++)
        {


            startLoopAtkWFS[i] = new WaitForSeconds(startLoopAtkColTime[i]);

            workLoopAtkWFS[i] = new WaitForSeconds(workLoopAtkColTime[i]);

            endLoopAtkWFS[i] = new WaitForSeconds(endLoopAtkColTime[i]);
        }

    }

}
