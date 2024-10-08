using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SkillData", menuName = "Scriptable Object/SkillData", order = int.MaxValue)]



public class SkillInfo : ScriptableObject
{
   
    [SerializeField] public int id;
    [TextArea]
    [SerializeField] public string[] explain;
    [SerializeField] public Sprite image;
    [SerializeField] public Sprite ultImage;
    [SerializeField] public Sprite ultUIImage;
    [SerializeField] public int maxLevel;
    [SerializeField] public SkillType type;

   
}
