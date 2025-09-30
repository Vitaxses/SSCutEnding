using System;
using HutongGames.PlayMaker;

namespace CutEnding;

public class CallActionFsm : FsmStateAction
{
    public Action? action;

    public CallActionFsm(Action action) { this.action = action; }

    public override void Reset()
    {
        action = null;
        base.Reset();
    }

    public override void OnEnter()
    {
        action?.Invoke();
        Finish();
    }
}