using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace MyLibrary.DesignPattern.Sample
{
    public partial class FSMExampleCharacter : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        public FSM_State<FSMExampleCharacter> ChangeFSM
        {
            set { nextFSMState = value; }
        }

        private FSM_State<FSMExampleCharacter> currentFSMState;
        private FSM_State<FSMExampleCharacter> nextFSMState;

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