using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FSM;

public class StateMachine <T>
{
    public State<T> currentState { get ; private set; }
    public T Owner;

    public StateMachine(T o)
    {
        Owner = o;
        currentState = null;
    }
    
    public void ChangeState(State<T> newState){
        if (currentState != null) currentState.Exit(Owner);        
        currentState = newState;
        currentState.Enter(Owner);
    }

    public void Update(){
        if (currentState != null)
            currentState.Execute(Owner);
    }
}

namespace FSM{
    public interface State<T>
    {
        void Enter(T owner);
        void Execute(T owner);
        void Exit(T owner);
    }
}