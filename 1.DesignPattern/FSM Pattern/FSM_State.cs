using System;

namespace MyLibrary.DesignPattern
{
    public abstract class FSM_State<T>
    {
        public Action<T> OnChanged;
        public FSM_State()
        {
            
        }

        public virtual void Enter(T t)
        {
            
        }

        public virtual void Update(T t)
        {

        }

        public virtual void Exit(T t)
        {

        }
    }
}

