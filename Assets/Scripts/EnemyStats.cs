using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/EnemyData", order = int.MaxValue)]
public class EnemyStats : ScriptableObject
{

   [SerializeField] public float maxHp;
   [SerializeField] public int atk;
   [SerializeField] public float range;
   [SerializeField] public int exp;
}
