using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAtk : MonoBehaviour
{
    [SerializeField] GameObject hitEffectPrefabs;
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip swingClip;
 
    [SerializeField] float speed;
    [SerializeField] float multiplier;
    Coroutine coroutine;

     WaitForSeconds delay;


    [SerializeField] Collider col;
    [SerializeField] int count;

    bool overlapSound;

    private void Awake()
    {
        delay = new WaitForSeconds(0.3f);
    }

    private void OnEnable()
    {
      
       
        coroutine = StartCoroutine(OnOffCol());
    }

    private void OnDisable()
    {
        col.enabled = false;
        StopAllCoroutines();
    }

   

    private void OnTriggerEnter(Collider other)
    {
        Vector3 vec = other.ClosestPoint(transform.position);
        vec.y += 1.2f;
        Instantiate(hitEffectPrefabs, vec, Quaternion.identity);

        if (overlapSound == false)
        {
            HitSoundManager.Instance.SkillSound(transform.position, hitClip);
            overlapSound = true;
        }

        if (other.TryGetComponent<Enemy>(out Enemy component))
            component.TakeDamage(PlayerStat.Instance.ATK * multiplier, HitAniType.None);


    }
    public IEnumerator OnOffCol()
    {
        yield return delay;
       
        for (int i = 0; i < count; i++)
        {
            overlapSound = false;
            HitSoundManager.Instance.SkillSound(transform.position, swingClip);
            col.enabled = true;
            yield return delay;
            col.enabled = false;
        }

    }
    
  
}
