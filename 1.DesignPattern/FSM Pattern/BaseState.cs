using UnityEngine;

namespace MyLibrary.DesignPattern
{
//상태 정의
    public partial class Base : MonoBehaviour
    {
        public class IdleState : FSM_State<Base>
        {
            public override void Enter(Base body)
            {
                base.Enter(body);
            }

            public override void Update(Base body)
            {
                base.Update(body);
            }

            public override void Exit(Base body)
            {
                base.Exit(body);
            }
        }

        public class RunState : FSM_State<Base>
        {
            public override void Enter(Base body)
            {
                base.Enter(body);
            }

            public override void Update(Base body)
            {
                base.Update(body);
            }

            public override void Exit(Base body)
            {
                base.Exit(body);
            }
        }

        public class DeadState : FSM_State<Base>
        {
            public override void Enter(Base body)
            {
                base.Enter(body);
            }

            public override void Update(Base body)
            {
                base.Update(body);
            }

            public override void Exit(Base body)
            {
                base.Exit(body);
            }
        }
    }
}
