using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SocialPlatforms;

public class DisMob : MonoBehaviour
{
    [SerializeField] NavMeshObstacle obstacle;
    [SerializeField] NavMeshAgent agent;
    NavMeshPath navMeshPath;

    [SerializeField] Transform player;
    [SerializeField] Animator anim;

    [SerializeField] int hp;
    [SerializeField] int maxhp;
    [SerializeField] float range;

    [SerializeField] public Action<GameObject> dEvent;


    Coroutine damage;
    protected Coroutine state;
    Coroutine cooldown;


    [SerializeField] float delay;
    [SerializeField] float atkHitTime;
    [SerializeField] float atkEndTime;
    [SerializeField] float defaultAtkCooldown;
    [SerializeField] float atkCooldown;

    [SerializeField] bool isDown;

    WaitForSeconds wfs;

    WaitForSeconds atkHitWFS;
    WaitForSeconds atkEndWFS;
  
    private void Awake()
    {
        wfs = new WaitForSeconds(delay);
        atkHitWFS = new WaitForSeconds(atkHitTime);
        atkEndWFS = new WaitForSeconds(atkEndTime);
     

        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshPath = new NavMeshPath();
    }
 

    private void Update()
    {
       
    }

    private void OnEnable()
    {
        StopAllCoroutines(); 
        hp = maxhp;
        atkCooldown = 0;
        Trace();
    }

    void Die()
    {
        StopAllCoroutines();
      
        dEvent?.Invoke(gameObject);

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

        while ((player.position - transform.position).sqrMagnitude > range)
        {

             agent.CalculatePath(player.position, navMeshPath);
            
             
            

            anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.Run));
           // agent.SetDestination(player.position);
          
            

            agent.SetPath(navMeshPath);
            yield return wfs;


        }


        AgentReset();
        agent.enabled = false;
        obstacle.enabled = true;
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
      
       
       
        anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.Idle));
        while (atkCooldown > 0)
        {
            yield return wfs;
            if(((player.position - transform.position).sqrMagnitude > range))
            {
               
                Trace();
                yield break;
            }
        }
      
        if (((player.position - transform.position).sqrMagnitude < range))
        {

            Attack();
        }
        else
        {
            Trace();
        }
      
    }

    IEnumerator AttackCorutine()
    {
        transform.LookAt(player.position);
        anim.Play("Atk");
        yield return atkHitWFS;
        atkCooldown = defaultAtkCooldown;
        if (cooldown != null)
        {
            StopCoroutine(cooldown);
        }
        cooldown = StartCoroutine(AttackCoolDownCorutine());

        if (((player.position - transform.position).sqrMagnitude < range))
        {
            
            Debug.Log("크아악");
        }


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

    IEnumerator  TakeTamage()
    {
        while (hp>0)
        {
            yield return new WaitForSeconds(0.2f);
            hp--;
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    protected void StopCoroutineNullCheck()
    {
      
        if (state == null)
        {
          
            return;
           
        }
        StopCoroutine(state);
    }
}
