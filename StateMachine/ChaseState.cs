using UnityEngine;
using FSM;
using UnityEngine.AI;

namespace FSM
{
    public class ChaseState : State<EnemyNavigation>
    {
        private static ChaseState _instance;

        private NavMeshAgent nav;
        private GameObject player;

        private ChaseState()
        {
            if (_instance != null) return;

            _instance = this;
        }

        public static ChaseState Instance
        {
            get
            {
                if (_instance == null)
                {
                    new ChaseState();
                }
                return _instance;
            }
        }

        public void Enter(EnemyNavigation owner)
        {
            nav = owner.navMeshAgent;
            owner.SetNavDefaults();
            player = GameObject.FindGameObjectWithTag("Player");
        }
        public void Execute(EnemyNavigation owner)
        {
            nav.SetDestination(player.transform.position);
        }
        public void Exit(EnemyNavigation owner)
        {

        }
    }
}

