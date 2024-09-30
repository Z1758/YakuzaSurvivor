
using UnityEngine;

public class PlayerState
{
    private State playerState;

    public PlayerState(State defaultState)
    {
        playerState = defaultState;
    }

    public void SetState(State state)
    {
        playerState.ExitState();

        playerState = state;

        playerState.EnterState();
    }
    public State GetState()
    {
        return playerState;
    }
    public void Enter()
    {
        playerState.EnterState();
    }

    public void Update()
    {
        playerState.UpdateState();
    }


    public void Exit()
    {
        playerState.ExitState();
    }


}


public class IdleState : State
{

    private static IdleState instance;


    public IdleState(PlayController controller) : base(controller)
    {
        if (instance == null)
        {
            instance = this;

        }

    }

    public static IdleState Instance
    {
        get
        {
            return instance;
        }
    }

    public override void Dispose()
    {
        instance = null;
    }

    public override void EnterState()
    {
        controller.anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.Idle));


    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }
}

public class MoveState : State
{
    private static MoveState instance;
    public MoveState(PlayController controller) : base(controller)
    {
        if (instance == null)
        {
            instance = this;

        }

    }

    public static MoveState Instance
    {
        get
        {
            return instance;
        }
    }

    public override void EnterState()
    {
        controller.anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.Run));
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        controller.Move();
        controller.Rotate();
    }


    public override void Dispose()
    {
        instance = null;
    }

}

public class AttackState : State
{
    private static AttackState instance;
    public AttackState(PlayController controller) : base(controller)
    {
        if (instance == null)
        {
            instance = this;

        }

    }

    public static AttackState Instance
    {
        get
        {
            return instance;
        }
    }


    public override void EnterState()
    {


        controller.anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.Atk));
        controller.Atk();

    }

    public override void ExitState()
    {

    }


    public override void UpdateState()
    {

    }


    public override void Dispose()
    {
        instance = null;
    }

}

public class FinishAttackState : State
{
    private static FinishAttackState instance;
    public FinishAttackState(PlayController controller) : base(controller)
    {
        if (instance == null)
        {
            instance = this;

        }

    }

    public static FinishAttackState Instance
    {
        get
        {
            return instance;
        }
    }


    public override void EnterState()
    {
        controller.anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.FAtk));
        controller.FAtk();
    }

    public override void ExitState()
    {

    }


    public override void UpdateState()
    {

    }


    public override void Dispose()
    {
        instance = null;
    }

}
public class LoopAttackState : State
{
    private static LoopAttackState instance;

    public int loopCount = 0;

    public bool isLoopEnd;
    public bool isLoop;
    public bool isMove;

    public LoopAttackState(PlayController controller) : base(controller)
    {
        if (instance == null)
        {
            instance = this;

        }

    }

    public static LoopAttackState Instance
    {
        get
        {
            return instance;
        }
    }


    public override void EnterState()
    {

        controller.isAtk = true;
        if (loopCount == 0 && isLoop == false)
        {

            isLoop = true;
            isMove = controller.GetLoopMoveCheck();
            loopCount = controller.GetLoopCount();
            controller.anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.LoopAtk));
            controller.LoopAtkEnter();
            controller.DecreaseLoopCount();

            controller.SetSpeed(0.6f);
        }
        else if (loopCount - 1 <= 0 && isLoopEnd == false)
        {
            EndLoop();
            return;
        }
        else if (loopCount > 1)
        {
            controller.anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.LoopAtk));
            controller.LoopAtk();

        }

    }

    public override void ExitState()
    {

    }


    public override void UpdateState()
    {

        if (isMove)
        {
            controller.Move();
        }
    }

    public void EndLoop()
    {
        controller.isAtk = true;
        loopCount = 0;
        controller.anim.SetInteger("AniInt", EnumConvert<int>.Cast(AniState.LoopEnd));
        controller.LoopAtkEnd();
        controller.changedSpeed = false;
        controller.ResetSpeed();

        isLoopEnd = true;
        isMove = false;
    }


    public override void Dispose()
    {
        instance = null;
    }


}
public class InputWaitState : State
{
    private static InputWaitState instance;

    private State tempState;
    public bool isInput;

    public InputWaitState(PlayController controller) : base(controller)
    {
        if (instance == null)
        {
            instance = this;

        }

    }

    public static InputWaitState Instance
    {
        get
        {
            return instance;
        }
    }


    public override void EnterState()
    {

        isInput = true;
    }

    public override void ExitState()
    {
        tempState = null;
        isInput = false;
    }


    public override void UpdateState()
    {

        tempState = controller.InputWaitKey();

    }

    public State GetTempState()
    {
        return tempState;
    }


    public override void Dispose()
    {
        instance = null;
    }

}
public class ChangeState : State
{

    private static ChangeState instance;

 

    public ChangeState(PlayController controller) : base(controller)
    {
        if (instance == null)
        {
            instance = this;

        }

    }

    public static ChangeState Instance
    {
        get
        {
            return instance;
        }
    }

    public override void EnterState()
    {


        controller.SetChangeStyle();
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }

    public override void Dispose()
    {
        instance = null;
    }

}


public class DeadState : State
{

    private static DeadState instance;



    public DeadState(PlayController controller) : base(controller)
    {
        if (instance == null)
        {
            instance = this;

        }

    }

    public static DeadState Instance
    {
        get
        {
            return instance;
        }
    }

    public override void EnterState()
    {
        controller.anim.SetTrigger("Dead");
        controller.ColOff();
        GameManager.Instance.GameOver();

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }


    public override void Dispose()
    {
        instance = null;
    }

}

