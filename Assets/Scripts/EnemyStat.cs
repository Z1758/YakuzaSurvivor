using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStat : MonoBehaviour
{

  
    [SerializeField] private float enemyHP;
    [SerializeField] public EnemyStats stats;

    public float EnemyHP { get { return enemyHP; } set { enemyHP = value;} }

    private void Awake()
    {
        SetMaxHp();
    }

    public void SetMaxHp()
    {
        enemyHP = stats.maxHp;
    }

}
