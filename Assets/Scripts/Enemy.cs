using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyStat es;
    [SerializeField] NavMeshObstacle obstacle;
    [SerializeField] NavMeshAgent agent;
    NavMeshPath navMeshPath;

    [SerializeField] Transform playerPos;
    [SerializeField] PlayController player;
    [SerializeField] Animator anim;

   

    [SerializeField] public Action<GameObject, int> dieEvent;


    Coroutine damage;
    protected Coroutine state;
    Coroutine cooldown;



    [SerializeField] float atkCooldown;

    [SerializeField] bool isDown;

    WaitForSeconds wfs;

    WaitForSeconds atkHitWFS;
    WaitForSeconds atkEndWFS;
  
    private void Awake()
    {
        wfs = new WaitForSeconds(es.stats.delay);
        atkHitWFS = new WaitForSeconds(es.stats.atkHitTime);
        atkEndWFS = new WaitForSeconds(es.stats.atkEndTime);
     

        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayController>();
        navMeshPath = new NavMeshPath();
    }
 
    private void OnEnable()
    {
       

    }

    public void ActiveEnemy()
    {
        StopAllCoroutines();
        es.SetMaxHp();
        atkCooldown = 0;
        Trace();
        PlayVoice(es.stats.appearVoice);
        
    }

    void PlayVoice(AudioClip clip )
    {
        EnemyVoiceManager.Instance.PlayerVoice(clip, transform.position);
    }

    void Die()
    {
        StopAllCoroutines();
        PlayVoice(es.stats.dieVoice);
        dieEvent?.Invoke(gameObject, es.stats.type);

    }


    void Trace()
    {
       
        StopCoroutineNullCheck();
        state = StartCoroutine(TraceCorutine());
    }

    void Idle()
    {
        StopCoroutineNullCheck();
        state = StartCoroutine(IdleCorutine());
    }

    protected virtual void Attack()
    {
        StopCoroutineNullCheck();
        state = StartCoroutine(AttackCorutine());
    }

    public void Down()
    {


        AgentReset();
        PlayVoice(es.stats.downVoice);
        isDown = true;
       
        StopAllCoroutines();
        transform.LookAt(playerPos.position);
        cooldown = StartCoroutine(AttackCoolDownCorutine());
        state = StartCoroutine(DownCorutine());
    }

    public void Hit()
    {
      
        PlayVoice(es.stats.hitVoice);

        if (isDown)
        {
            return;
        }
        AgentReset();
        StopAllCoroutines();
        transform.LookAt(playerPos.position);
        cooldown = StartCoroutine(AttackCoolDownCorutine());
        state = StartCoroutine(HitCorutine());
    }
    IEnumerator TraceCorutine()
    {
       
        obstacle.enabled = false;
        yield return wfs;
        agent.enabled = true;
        
        agent.isStopped = false;

        while ((playerPos.position - transform.position).sqrMagnitude > es.stats.range)
        {

             agent.CalculatePath(playerPos.position, navMeshPath);
            
             
            

            anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.Run));
           // agent.SetDestination(player.position);
          
            

            agent.SetPath(navMeshPath);
            yield return wfs;


        }


      
        Idle();

    }

    void AgentReset()
    {
        if (agent.enabled == false)
            return;
        agent.isStopped = true;
        agent.ResetPath();
    }
    
    IEnumerator IdleCorutine()
    {

        AgentReset();
        agent.enabled = false;
        obstacle.enabled = true;

        anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.Idle));
        while (atkCooldown > 0)
        {
            yield return wfs;
            if(((playerPos.position - transform.position).sqrMagnitude > es.stats.range))
            {
               
                Trace();
                yield break;
            }
        }
      
        if (((playerPos.position - transform.position).sqrMagnitude < es.stats.range))
        {

            Attack();
        }
        else
        {
            Trace();
        }
      
    }

   public IEnumerator AttackCorutine()
    {
        transform.LookAt(playerPos.position);
        PlayVoice(es.stats.atkVoice);
        anim.Play("Atk");
        yield return atkHitWFS;
        atkCooldown = es.stats.defaultAtkCooldown;
        if (cooldown != null)
        {
            StopCoroutine(cooldown);
        }
        cooldown = StartCoroutine(AttackCoolDownCorutine());

        if (((playerPos.position - transform.position).sqrMagnitude < es.stats.range))
        {
            player.PlayerTakeDamage(es.stats.atk, transform.position);
            
         
        }


        yield return atkEndWFS;

        Idle();
    }

    public IEnumerator RangeAttackCorutine(GameObject bullet)
    {
        transform.LookAt(playerPos.position);
        anim.Play("Atk");
        yield return atkHitWFS;
        atkCooldown = es.stats.defaultAtkCooldown;
        if (cooldown != null)
        {
            StopCoroutine(cooldown);
        }
        cooldown = StartCoroutine(AttackCoolDownCorutine());

        GameObject b = Instantiate(bullet, transform.position, transform.rotation);
        EnemyBulletScript bs = b.GetComponent<EnemyBulletScript>();
        bs.dmg = es.stats.atk;
        bs.player = player;


        yield return atkEndWFS;

        Idle();
    }

    IEnumerator AttackCoolDownCorutine()
    {

        while (atkCooldown > 0)
        {
            yield return null;
            atkCooldown-= Time.deltaTime;
           
            if (atkCooldown <= 0)
            {
                yield break;
            }
           
        }
           
    }

    IEnumerator HitCorutine()
    {
       
        anim.SetTrigger("OnHit");
       
        yield return new WaitForSeconds(0.6f);

        Idle();
    }

    IEnumerator DownCorutine()
    {
       

        anim.SetTrigger("OnDown");
        yield return new WaitForSeconds(4.6f);
        isDown = false;
        Idle();

    }

  


    protected void StopCoroutineNullCheck()
    {
      
        if (state == null)
        {
          
            return;
           
        }
        StopCoroutine(state);
    }

    public void TakeDamage(float damage)
    {
        if (es.HP > 0)
        {
            es.HP -= damage;
            if (es.HP <= 0)
            {
                Die();
            }
        }
    }


}
