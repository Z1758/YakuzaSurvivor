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

    [SerializeField] Transform player;
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
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshPath = new NavMeshPath();
    }
 
    private void OnEnable()
    {
      
        StopAllCoroutines(); 
        es.SetMaxHp();
        atkCooldown = 0;
        Trace();
    }

    void Die()
    {
        StopAllCoroutines();
      
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
        isDown = true;
       
        StopAllCoroutines();
        transform.LookAt(player.position);
        cooldown = StartCoroutine(AttackCoolDownCorutine());
        state = StartCoroutine(DownCorutine());
    }

    public void Hit()
    {
        // 데미지는 들어오도록

        if (isDown)
        {
            return;
        }
        AgentReset();
        StopAllCoroutines();
        transform.LookAt(player.position);
        cooldown = StartCoroutine(AttackCoolDownCorutine());
        state = StartCoroutine(HitCorutine());
    }
    IEnumerator TraceCorutine()
    {
       
        obstacle.enabled = false;
        yield return wfs;
        agent.enabled = true;
        
        agent.isStopped = false;

        while ((player.position - transform.position).sqrMagnitude > es.stats.range)
        {

             agent.CalculatePath(player.position, navMeshPath);
            
             
            

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
            if(((player.position - transform.position).sqrMagnitude > es.stats.range))
            {
               
                Trace();
                yield break;
            }
        }
      
        if (((player.position - transform.position).sqrMagnitude < es.stats.range))
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
        transform.LookAt(player.position);
        anim.Play("Atk");
        yield return atkHitWFS;
        atkCooldown = es.stats.defaultAtkCooldown;
        if (cooldown != null)
        {
            StopCoroutine(cooldown);
        }
        cooldown = StartCoroutine(AttackCoolDownCorutine());

        if (((player.position - transform.position).sqrMagnitude < es.stats.range))
        {
            
            Debug.Log("크아악");
        }


        yield return atkEndWFS;

        Idle();
    }

    public IEnumerator RangeAttackCorutine(GameObject bullet)
    {
        transform.LookAt(player.position);
        anim.Play("Atk");
        yield return atkHitWFS;
        atkCooldown = es.stats.defaultAtkCooldown;
        if (cooldown != null)
        {
            StopCoroutine(cooldown);
        }
        cooldown = StartCoroutine(AttackCoolDownCorutine());

        Instantiate(bullet, transform.position, transform.rotation);
        


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
