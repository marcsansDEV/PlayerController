public class PlWalkState : PlState
{
    public override PlayerMoveFSM.TStates Type => PlayerMoveFSM.TStates.Walk;
    public override bool NormalizedInput => false;

    protected override bool CanEnterState()
    {
        return true;
    }

    protected override bool CanExitState()
    {
        return true;
    }
}