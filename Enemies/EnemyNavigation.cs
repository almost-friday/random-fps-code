using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using FSM;

// This script controls enemy navigation for now.
// Member functions:
//
//SetDestination(Vector3 destination) - Sets the NavMeshAgent destination to the destination param
[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof(EnemyHealth))]
public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private float startingSpeed;
    [SerializeField] private float startingAccel;
    [field: SerializeField] public EnemyHealth enemyHealth { get; private set; }
    [field: SerializeField] public NavMeshAgent navMeshAgent
    {
        get;
        private set;
    }

    private static StateMachine<EnemyNavigation> stateMachine;

    private void Awake()
    {
        if (stateMachine == null) stateMachine = new StateMachine<EnemyNavigation>(this);
        stateMachine.ChangeState(ChaseState.Instance);

        navMeshAgent.speed = startingSpeed;
        navMeshAgent.acceleration = startingAccel;
    }
        private void Update()
    {
        stateMachine.Update();
    }

    public void SetNavDefaults()
    {
        navMeshAgent.speed = startingSpeed;
        navMeshAgent.acceleration = startingAccel;
    }
    public void StopNav()
    {
        navMeshAgent.speed = 0;
        navMeshAgent.acceleration = 40;
    }

    public void SetDestination(Vector3 destination)
    {
        navMeshAgent.destination = destination;
    }

}
