using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CachingWFS : MonoBehaviour
{
    private static CachingWFS instance;
    public WaitForSeconds enemyWFS;
    public WaitForSeconds enemyDownWFS;
    public WaitForSeconds enemyHitWFS;

    public EnemyStats[] enemyStats;
    public WaitForSeconds[] atkHitWFS;
    public WaitForSeconds[] atkEndWFS;

 
    private void Awake()
    {
        if (null == instance)
        {

            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        SetWFS();
    }
    public static CachingWFS Instance
    {

        get
        {

            return instance;
        }

    }

    void SetWFS()
    {
        enemyWFS = new WaitForSeconds(0.2f);
        enemyDownWFS = new WaitForSeconds(4.6f);
        enemyHitWFS = new WaitForSeconds(0.6f);

        atkHitWFS = new WaitForSeconds[enemyStats.Length];
        atkEndWFS = new WaitForSeconds[enemyStats.Length];
        for (int i = 0; i < enemyStats.Length; i++) {
            atkHitWFS[i] = new WaitForSeconds(enemyStats[i].atkHitTime);
            atkEndWFS[i] = new WaitForSeconds(enemyStats[i].atkEndTime);
        }


    }
}