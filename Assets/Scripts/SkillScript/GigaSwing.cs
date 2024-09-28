using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GigaSwing : MonoBehaviour
{
    [SerializeField] GameObject col;
    [SerializeField] AudioClip clip;
    [SerializeField] float cooltime;
  
    WaitForSeconds delay;
    WaitForSeconds duration;

    Coroutine coroutine;

    Transform player;

    [SerializeField] int count;

    private void Awake()
    {
        delay = new WaitForSeconds(0.5f);
        duration = new WaitForSeconds(0.3f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        coroutine = StartCoroutine(Giga());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public IEnumerator Giga()
    {
        transform.position = player.position;

        for (int i = 0; i < count; i++)
        {
            yield return delay;
            HitSoundManager.Instance.SkillSound(transform.position, clip);
            col.SetActive(true);
            yield return duration;
            col.SetActive(false);
        }
        
    }

}
