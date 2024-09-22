using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStyleSound : MonoBehaviour
{
    [SerializeField] public AudioClip changeClip;
    [SerializeField] public AudioClip changeSwingClip;

    [SerializeField] public AudioClip[] atkVoiceClips;
    [SerializeField] public AudioClip[] atkSwingClips;

    [SerializeField] public AudioClip[] fAtkVoiceClips;
    [SerializeField] public AudioClip[] fAtkSwingClips;

    [SerializeField] public AudioClip[] loopStartVoiceClips;
    [SerializeField] public AudioClip[] loopVoiceClips;
    [SerializeField] public AudioClip[] loopEndVoiceClips;

    [SerializeField] public AudioClip[] loopStartSwingClips;
    [SerializeField] public AudioClip[] loopSwingClips;
    [SerializeField] public AudioClip[] loopEndSwingClips;
}
