using UnityEngine;

namespace MyLibrary.DesignPattern.Sample
{
//상태 정의
    public partial class FSMExampleCharacter : MonoBehaviour
    {
        public class IdleState : FSM_State<FSMExampleCharacter>
        {
            public override void Enter(FSMExampleCharacter body)
            {
                base.Enter(body);
            }

            public override void Update(FSMExampleCharacter body)
            {
                base.Update(body);
            }

            public override void Exit(FSMExampleCharacter body)
            {
                base.Exit(body);
            }
        }

        public class RunState : FSM_State<FSMExampleCharacter>
        {
            public override void Enter(FSMExampleCharacter body)
            {
                body._animator.SetBool("IsRun", true);
            }

            public override void Update(FSMExampleCharacter body)
            {
                base.Update(body);
            }

            public override void Exit(FSMExampleCharacter body)
            {
                body._animator.SetBool("IsRun", false);
            }
        }

        public class WalkState : FSM_State<FSMExampleCharacter>
        {
            public override void Enter(FSMExampleCharacter body)
            {
                body._animator.SetBool("IsWalk", true);
            }

            public override void Update(FSMExampleCharacter body)
            {
                base.Update(body);
            }

            public override void Exit(FSMExampleCharacter body)
            {
                body._animator.SetBool("IsWalk", false);
            }
        }
        
        public class JumpState : FSM_State<FSMExampleCharacter>
        {
            public override void Enter(FSMExampleCharacter body)
            {
                base.Enter(body);
            }

            public override void Update(FSMExampleCharacter body)
            {
                body._animator.SetTrigger("IsJump");
            }

            public override void Exit(FSMExampleCharacter body)
            {
                base.Exit(body);
            }
        }
    }
}
