using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoblinState 
{
    protected GoblinStateController gsc;

    public abstract void CheckTransitions();
    public abstract void Act();
    public virtual void OnStateEnter() {}
    public virtual void OnStateExit() {}

    public GoblinState(GoblinStateController gsc) {
        this.gsc = gsc;
    }
}
