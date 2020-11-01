using UnityEngine;
using FSM;

namespace FSM
{
    public class StateTemplate : State<EnemyNavigation>
    {
        static StateTemplate _instance;

        private StateTemplate()
        {
            if (_instance != null) return;

            _instance = this;
        }

        public static StateTemplate Instance
        {
            get
            {
                if (_instance == null)
                {
                    new StateTemplate();
                }
                return _instance;
            }
        }

        public void Enter(EnemyNavigation owner)
        {

        }
        public void Execute(EnemyNavigation owner)
        {

        }
        public void Exit(EnemyNavigation owner)
        {

        }
    }
}

