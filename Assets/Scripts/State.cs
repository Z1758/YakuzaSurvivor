
public abstract class State
{
    protected PlayController controller;

    public State(PlayController controller)
    {
        this.controller = controller;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}
