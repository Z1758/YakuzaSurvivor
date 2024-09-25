using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStat : MonoBehaviour
{

  
    [SerializeField] private float hp;
    [SerializeField] public EnemyStats stats;

    public float HP { get { return hp; } set { hp = value;} }

    private void Awake()
    {
        SetMaxHp();
    }

    public void SetMaxHp()
    {
        hp = stats.maxHp;
    }

}
