using UnityEngine;

namespace MyLibrary.DesignPattern
{
    public partial class Base : MonoBehaviour
    {
        public FSM_State<Base> ChangeFSM
        {
            set { nextFSMState = value; }
        }

        private FSM_State<Base> currentFSMState;
        private FSM_State<Base> nextFSMState;

        protected void Awake()
        {
            currentFSMState = new IdleState();
            nextFSMState = currentFSMState;
            currentFSMState.Enter(this);
        }

        protected void Update()
        {
            currentFSMState.Update(this);

            if ((currentFSMState.GetType() == nextFSMState.GetType()) == false) 
            {
                currentFSMState.Exit(this);
                currentFSMState = nextFSMState;
                currentFSMState.Enter(this);
            }
        }
    }
}