using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HitSoundData", menuName = "Scriptable Object/HitSoundData", order = int.MaxValue)]

public class HitSounds : ScriptableObject
{
    [SerializeField] public AudioClip[] atkClips;

    [SerializeField] public AudioClip[] fAtkClips;
}
