using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollider : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] float multiplier;
    [SerializeField] HitAniType hitAniType;
    private void OnTriggerEnter(Collider other)
    {
     
        if (other.TryGetComponent<Enemy>(out Enemy component))
            component.TakeDamage(PlayerStat.Instance.ATK * multiplier, hitAniType);

        if(clip != null)
        HitSoundManager.Instance.SkillSound(transform.position, clip);
    }

}
