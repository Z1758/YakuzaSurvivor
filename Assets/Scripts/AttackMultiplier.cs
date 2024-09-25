using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "AttackMultiplierData", menuName = "Scriptable Object/AttackMultiplierData", order = int.MaxValue)]
public class AttackMultiplier : ScriptableObject
{
    [SerializeField] public float[] atkMultiplier;
    [SerializeField] public float[] fAtkMultiplier;
    [SerializeField] public float[] loopAtkMultiplier;
    [SerializeField] public float[] loopAtkEndMultiplier;

}
