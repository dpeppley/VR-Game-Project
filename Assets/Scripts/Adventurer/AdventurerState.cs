using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AdventurerState 
{
    protected AdventurerStateController asc;

    public abstract void CheckTransitions();
    public abstract void Act();
    public virtual void OnStateEnter() {}
    public virtual void OnStateExit() {}

    public AdventurerState(AdventurerStateController asc) {
        this.asc = asc;
    }
}
