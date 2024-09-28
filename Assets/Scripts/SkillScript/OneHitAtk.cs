using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneHitAtk : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] float multiplier;
    [SerializeField] float delay;
    [SerializeField] float endDelay;
    BoxCollider col;
    WaitForSeconds wfs;
    WaitForSeconds endwfs;
    Coroutine coroutine;

    void Awake()
    {
        wfs = new WaitForSeconds(delay);
        endwfs = new WaitForSeconds(endDelay);
        col = GetComponent<BoxCollider>();
    }

   
  
    private void OnEnable()
    {
        coroutine = StartCoroutine(AtkCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }



    IEnumerator AtkCoroutine()
    {
        yield return wfs;
        
        col.enabled = true;
       
        HitSoundManager.Instance.SkillSound(transform.position, clip);
        yield return endwfs;
        col.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Enemy>(out Enemy component))
            component.TakeDamage(PlayerStat.Instance.ATK * multiplier, HitAniType.Down);


        
    }
}
