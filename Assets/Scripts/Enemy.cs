using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.AI;


public enum HitAniType
{
    None, Hit, Down, End
}


public class Enemy : MonoBehaviour
{
  

    [SerializeField] EnemyStat es;
    [SerializeField] NavMeshObstacle obstacle;
    [SerializeField] NavMeshAgent agent;
    NavMeshPath navMeshPath;

    [SerializeField] Transform playerPos;
    [SerializeField] PlayController player;
  //  [SerializeField] Animator anim;
   
   

    [SerializeField] public Action<GameObject, int> dieEvent;


    Coroutine damage;
    protected Coroutine state;
    Coroutine cooldown;



    [SerializeField] float atkCooldown;

    [SerializeField] bool isDown;

    [SerializeField] GameObject dieParticle;

    //[SerializeField] bool aniIns;
    [SerializeField] AnimationInstancing.AnimationInstancing ai;

    private void Awake()
    {
        

        dieParticle = Instantiate(es.stats.yenParticle);
        ai = GetComponent<AnimationInstancing.AnimationInstancing>();
        agent = GetComponent<NavMeshAgent>();
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayController>();
        navMeshPath = new NavMeshPath();
    }
 



    public void ActiveEnemy()
    {
        StopAllCoroutines();
        es.SetMaxHp();
        gameObject.layer = LayerNumber.alive;
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
        dieParticle.transform.position = transform.position;
        dieParticle.SetActive(true);
        PlayVoice(es.stats.dieVoice);
        gameObject.layer = LayerNumber.die;
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
        yield return CachingWFS.Instance.enemyWFS;
        agent.enabled = true;
        isDown = false;
        agent.isStopped = false;

        while ((playerPos.position - transform.position).sqrMagnitude > es.stats.range)
        {

             agent.CalculatePath(playerPos.position, navMeshPath);

            ai.PlayAnimation("Run");
            /*
            if (aniIns)
            {
                
                ai.PlayAnimation("Run");
            }
            else
            {
                anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.Run));
            }*/

           // agent.SetDestination(player.position);
          
            

            agent.SetPath(navMeshPath);
            yield return CachingWFS.Instance.enemyWFS;


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
        isDown = false;
        agent.enabled = false;
        obstacle.enabled = true;

        ai.PlayAnimation("Idle");
        /*
        // 수정
        if (aniIns)
        {
            ai.PlayAnimation("Idle");
        }
        else
        {
            anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.Idle));
        }
        */
        while (atkCooldown > 0)
        {
            yield return CachingWFS.Instance.enemyWFS;
            if (((playerPos.position - transform.position).sqrMagnitude > es.stats.range))
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

        ai.PlayAnimation("Atk");
        /*
        // 수정
        if (aniIns)
        {
            ai.PlayAnimation("Atk");
        }
        else
        {
            anim.Play("Atk");
        }*/
        yield return CachingWFS.Instance.atkHitWFS[es.stats.type];
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


        yield return CachingWFS.Instance.atkEndWFS[es.stats.type];

        Idle();
    }

    public IEnumerator RangeAttackCorutine(GameObject bullet)
    {
        transform.LookAt(playerPos.position);
        ai.PlayAnimation("Atk");
        /*
        // 수정
        if (aniIns)
        {
            ai.PlayAnimation("Atk");
        }
        else
        {
            anim.Play("Atk");
        }*/
        yield return CachingWFS.Instance.atkHitWFS[es.stats.type];
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


        yield return CachingWFS.Instance.atkEndWFS[es.stats.type];

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
        ai.Pause();
        ai.PlayAnimation("Hit");
        /*
        // 수정
        if (aniIns)
        {
            ai.PlayAnimation("Hit");
        }
        else
        {
            anim.SetTrigger("OnHit");
        }
        */
        yield return CachingWFS.Instance.enemyHitWFS;

        Idle();
    }

    IEnumerator DownCorutine()
    {
        ai.Pause();
        ai.PlayAnimation("Down2");
        yield return CachingWFS.Instance.enemyDownInsWFS;
        ai.PlayAnimation("StandUp");
        yield return CachingWFS.Instance.enemyStandUpInsWFS;
        isDown = false;
        Idle();
        /*
        // 수정
        if (aniIns)
        {
            ai.PlayAnimation("Down2");
            yield return CachingWFS.Instance.enemyDownInsWFS;
            ai.PlayAnimation("StandUp");
            yield return CachingWFS.Instance.enemyStandUpInsWFS;
            isDown = false;
            Idle();

        }
        else { 
          anim.SetTrigger("OnDown");
            yield return CachingWFS.Instance.enemyDownWFS;
            isDown = false;
            Idle();

        }
        */
    }

  


    protected void StopCoroutineNullCheck()
    {
      
        if (state == null)
        {
          
            return;
           
        }
        StopCoroutine(state);
    }


    public void TakeDamage(float damage, HitAniType type)
    {
        if (es.EnemyHP > 0)
        {
            es.EnemyHP -= damage;
            if (es.EnemyHP <= 0)
            {
                Down();
                Die();

            }
            else
            {
                switch (type)
                {
                    case HitAniType.None:
                        break;
                    case HitAniType.Down:
                        Down();
                        break;
                    case HitAniType.Hit:
                        Hit();
                        break;


                }
            }
        }
    }

    public int GetEXP()
    {
        return es.stats.exp;
    }

}
