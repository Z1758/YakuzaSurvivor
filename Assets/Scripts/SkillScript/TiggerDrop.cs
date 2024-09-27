using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiggerDrop : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    [SerializeField] float multiplier;

    BoxCollider col;
    WaitForSeconds wfs;

    Coroutine coroutine;

    void Awake()
    {
        wfs = new WaitForSeconds(0.13f);
        col = GetComponent<BoxCollider>();
    }

   
    public void AttackTiggerDrop()
    {
        StopAllCoroutines();
       coroutine =  StartCoroutine(TiggerDropCoroutine());
    }

    IEnumerator TiggerDropCoroutine()
    {
        yield return wfs;
        
        col.enabled = true;
        HitSoundManager.Instance.SkillSound(transform.position, clip);
        yield return wfs;
        col.enabled = false;
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent<Enemy>(out Enemy component))
            component.TakeDamage(PlayerStat.Instance.ATK * multiplier, HitAniType.Down);


        
    }
}
