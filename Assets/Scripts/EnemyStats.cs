using UnityEngine;
[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Object/EnemyData", order = int.MaxValue)]
public class EnemyStats : ScriptableObject
{

    [SerializeField] public float maxHp;
    [SerializeField] public int atk;
    [SerializeField] public float range;

    [SerializeField] public int exp;
    [SerializeField] public int type;
    [SerializeField] public float delay;
    [SerializeField] public float atkHitTime;
    [SerializeField] public float atkEndTime;
    [SerializeField] public float defaultAtkCooldown;

    [SerializeField] public AudioClip appearVoice;
    [SerializeField] public AudioClip atkVoice;
    [SerializeField] public AudioClip dieVoice;
    [SerializeField] public AudioClip downVoice;
    [SerializeField] public AudioClip hitVoice;
}
