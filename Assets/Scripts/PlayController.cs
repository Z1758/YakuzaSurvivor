
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class PlayController : MonoBehaviour
{
 
    PlayerState ps;


    [SerializeField] public Animator anim;
    [SerializeField] private CharacterController cc;
   
    [SerializeField] private Transform cameraPos;
    [SerializeField] private PlayerStat stat;
    [SerializeField] private PlayerStatController psc;
    [SerializeField] PlayerAnimationManager playerAnimationManager;
    [SerializeField] PlayerSoundManager playerSoundManager;


    [SerializeField] HitboxManager hitboxManager;
 

    [SerializeField] private float rotateSpeed;

    public int atkComboCnt;

    public bool isAtk;
    public bool changedSpeed;


    Vector3 dir;

    public Vector2 moveInputVec;

    Vector3 vertical;
    Vector3 horizontal;

    public delegate bool HitEvent(Vector3 vec);
    public HitEvent hitEvent;


    List<State> states;

    private void Start()
    {
        psc = GetComponent<PlayerStatController>();
        stat.OnDefaultSpeedChanged += ResetSpeed;
        ResetSpeed();
        states = new List<State>();

        State idle = new IdleState(this);
        State move = new MoveState(this);
        State atk = new AttackState(this);
        State fatk = new FinishAttackState(this);
        State loop = new LoopAttackState(this);
        State inputWait = new InputWaitState(this);
        State change = new ChangeState(this);
        State dead = new DeadState(this);
        ps = new PlayerState(IdleState.Instance);

        states.Add(idle);
        states.Add(move);
        states.Add(atk);
        states.Add(fatk);
        states.Add(loop);
        states.Add(inputWait);
        states.Add(change);
        states.Add(dead);
    }

    public void MoveInput(InputAction.CallbackContext value)
    {
        moveInputVec = value.ReadValue<Vector2>();
        InputDir();

        if (isAtk || InputWaitState.Instance.isInput)
        {

            return;

        }

        ps.SetState(MoveState.Instance);


        if (value.canceled)
        {

            ps.SetState(IdleState.Instance);
        }

    }

    public void ActionInput(InputAction.CallbackContext value)
    {
        if (value.started)
        {

            int index = value.action.GetBindingIndexForControl(value.control);

            if (isAtk  || InputWaitState.Instance.isInput )
            {
                return;
            }
            if (atkComboCnt < playerAnimationManager.GetAtkClipCount() && index == 0 )
            {


                ps.SetState(AttackState.Instance);
            }

            if (stat.StyleIndex != index - 1 && index != 0)
            {

                if(index - 1 == (int)BattleStyleType.Legend && stat.HitGauge < 10 )
                {
                    return;
                }

                
                ChangeStyle(index-1);


            }



        }

    }
    public void InputDir()
    {


        vertical.x = cameraPos.forward.x;
        vertical.z = cameraPos.forward.z;

        horizontal.x = cameraPos.right.x;
        horizontal.z = cameraPos.right.z;


        vertical = vertical.normalized;
        horizontal = horizontal.normalized;


        dir = vertical * moveInputVec.y + horizontal * moveInputVec.x;

        dir.Normalize();
    }




    void Update()
    {

    

        ps.Update();



    }

    public State InputWaitKey()
    {

        State temp;

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (stat.StyleIndex == (int)BattleStyleType.Legend)
            {
                return null;
            }

            if (GetLoopCount() > 0)
            {

                ps.SetState(LoopAttackState.Instance);


            }
            else if (atkComboCnt > 0)
            {
                ps.SetState(FinishAttackState.Instance);

            }
        }
        else if (Mouse.current.leftButton.wasPressedThisFrame && atkComboCnt < playerAnimationManager.GetAtkClipCount() && LoopAttackState.Instance.isLoop == false)
        {
            ps.SetState(AttackState.Instance);

        }

        if (moveInputVec == Vector2.zero)
        {
            temp = IdleState.Instance;
        }
        else
        {
            temp = MoveState.Instance;
        }

        return temp;

    }

    public void Move()
    {
          cc.Move(dir * stat.SPEED * Time.deltaTime);

    }
    public void Rotate()
    {

        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRot, rotateSpeed * Time.deltaTime);


    }


    public void Atk()
    {


        isAtk = true;

        LookDir();

        playerAnimationManager.AtkAniCoroutine(atkComboCnt, true);

        hitboxManager.AtkHitboxCoroutine(atkComboCnt);

        atkComboCnt++;


    }

  

    public void FAtk()
    {

        isAtk = true;

        LookDir();
        playerAnimationManager.AtkAniCoroutine(atkComboCnt-1, false);

        playerAnimationManager.PlayFAtk(atkComboCnt - 1 );
        hitboxManager.FAtkHitboxCoroutine(atkComboCnt -1);
        atkComboCnt = 0;



    }
 
    public void LoopAtkEnter()
    {
        LookDir();
        playerAnimationManager.LoopAtkAniCoroutine(atkComboCnt - 1);
        playerAnimationManager.PlayFAtk(atkComboCnt - 1);
        hitboxManager.StartLoopHitboxCoroutine(atkComboCnt -1);
    }

    public void LoopAtk()
    {

        playerAnimationManager.LoopAtkAniCoroutine(atkComboCnt - 1);

    }

    public void LoopAtkEnd()
    {

        playerAnimationManager.LoopEndAniCoroutine(atkComboCnt - 1);
        hitboxManager.EndLoopHitboxCoroutine(atkComboCnt - 1);
    }


    public void LoopAniEnd()
    {


        isAtk = false;
        LoopAttackState.Instance.isLoop = false;
        ComboReset();
        ps.SetState(InputWaitState.Instance.GetTempState());
        LoopAttackState.Instance.isLoopEnd = false;

        IdleMoveCheck();
    }

    public void AtkStateChange()
    {
        isAtk = false;
        ps.SetState(InputWaitState.Instance);

    }

    public void AtkInputTimeEnd()
    {
        if (LoopAttackState.Instance.loopCount > 0)
        {
            LoopAttackState.Instance.EndLoop();
            return;
        }

        ps.SetState(InputWaitState.Instance.GetTempState());


    }

    public void IdleMoveCheck()
    {
        if (moveInputVec == Vector2.zero)
        {

            ps.SetState(IdleState.Instance);
        }
        else
        {
            ps.SetState(MoveState.Instance);
        }
    }


    public void LookDir()
    {
        if (dir.sqrMagnitude >= 0.1)
        {
            Quaternion lookRot = Quaternion.LookRotation(dir);
            transform.rotation = lookRot;
        }
    }

    public void ComboReset()
    {
        atkComboCnt = 0;

    }

    public void FAtkEnd()
    {


        isAtk = false;



        IdleMoveCheck();
    }


    public bool GetLoopMoveCheck()
    {
        return playerAnimationManager.LoopMoveCheck(atkComboCnt-1);
    }

    public int GetLoopCount()
    {
        return playerAnimationManager.LoopAtkCheck(atkComboCnt-1);
    }

    public void SetSpeed(float s)
    {
        changedSpeed = true;
        stat.SPEED = stat.DEFAULTSPEED * s;
    }
    public void ResetSpeed()
    {
        
        if (stat.StyleIndex == (int)BattleStyleType.Legend)
        {
            stat.SPEED = stat.DEFAULTSPEED;
            SetSpeed(1.3f);
            return;
        }
        else if (changedSpeed)
        {
            return;
        }
       

            stat.SPEED = stat.DEFAULTSPEED;
        
      
    }
    public void DecreaseLoopCount()
    {
        playerAnimationManager.DecreaseLoopCountStart();
    }
    public void ChangeStyle(int index)
    {
        stat.StyleIndex = index;
        ps.SetState(ChangeState.Instance);
    }
    public void SetChangeStyle()
    {
        hitboxManager.SetBox(stat.StyleIndex);
        playerAnimationManager.SetAnim(stat.StyleIndex);
        playerAnimationManager.ChangeAniCoroutine(stat.StyleIndex);
        playerSoundManager.SetSound(stat.StyleIndex);
        if(stat.StyleIndex == (int)BattleStyleType.Legend)
        {
            SetSpeed(1.3f);
        }
        else
        {
            changedSpeed=false;
            ResetSpeed();

        }

        ComboReset();
        isAtk = true;
        ColOn();
    }

    public void ColOff()
    {
        this.gameObject.layer = LayerNumber.playerPeneLayer;
     
    }

    public void ColOn()
    {
        this.gameObject.layer = LayerNumber.playerLayer;
     
    }

    public void PlayerTakeDamage(int damage, Vector3 vec)
    {
        
        if(hitEvent != null && hitEvent(vec))
        {


            return;
        }
        
        stat.HP-=damage;
        hitboxManager.PlayerHit(vec);

        if(stat.HP <= 0)
        {
            playerAnimationManager.SetAnim(0);
            ps.SetState(DeadState.Instance);
            
       

            
        }
    }

    public void End()
    {
        psc.StopAllCoroutines();
        cc.enabled = false;
        Time.timeScale = 1.0f;
        
        
    }

    private void OnDestroy()
    {
        foreach (State state in states) {
            state.Dispose();
                }
    }
}
