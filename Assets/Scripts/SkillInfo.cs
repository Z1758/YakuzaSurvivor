using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object/SkillData", order = int.MaxValue)]



public class SkillInfo : ScriptableObject
{
   
    [SerializeField] public int id;
    [SerializeField] public string[] explain;
    [SerializeField] public Sprite image;
    [SerializeField] public int maxLevel;
    [SerializeField] public SkillType type;

   
}
