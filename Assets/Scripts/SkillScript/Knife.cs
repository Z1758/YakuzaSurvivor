using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] GameObject hitEffectPrefabs;
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip swingClip;
    [SerializeField] Rigidbody rb;
    [SerializeField] public Action<Knife> returnEvent;
    [SerializeField] float speed;

    Coroutine time;

    public WaitForSeconds knifeDurationWFS;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
  
    public void Throwing()
    {
        StopAllCoroutines();
        time = StartCoroutine(DurationTime());
        rb.velocity = transform.forward * speed;
        HitSoundManager.Instance.SkillSound(transform.position, swingClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(hitEffectPrefabs, transform.position, Quaternion.identity);
        HitSoundManager.Instance.SkillSound(transform.position, hitClip);

        if (other.TryGetComponent<Enemy>(out Enemy component))
            component.TakeDamage(PlayerStat.Instance.ATK * 0.5f , HitAniType.None);

        StopAllCoroutines();
        returnEvent?.Invoke(this);
    }

    IEnumerator DurationTime()
    {
        yield return knifeDurationWFS;
        returnEvent?.Invoke(this);

    }
}
