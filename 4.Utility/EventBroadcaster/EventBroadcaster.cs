using System;
using System.Collections.Generic;

namespace MyLibrary.Utility
{
    public abstract class GameEvent {}

    public static class EventBroadcaster
    {
        private static Dictionary<Type, object> _handlerGroups = new Dictionary<Type, object>();
        
        public static void Subscribe<T>(Action<T> handler) where T:GameEvent
        {
            var type = typeof(T);

            if (_handlerGroups.ContainsKey(type) == false)
            {
                _handlerGroups.Add(type, new EventHandlerGroup<T>());
            }
            ((EventHandlerGroup<T>)_handlerGroups[type]).Add(handler);
        }

        public static void Unsubscribe<T>(Action<T> handler) where T:GameEvent
        {
            if (_handlerGroups.TryGetValue(typeof(T), out var handlerGroup))
            {
                ((EventHandlerGroup<T>)handlerGroup).Remove(handler);
            }
        }

        public static void Broadcast<T>(T e) where T:GameEvent
        {
            if (_handlerGroups.TryGetValue(typeof(T), out var handlerGroup))
            {
                ((EventHandlerGroup<T>)handlerGroup).Invoke(e);
            }
        }

        private class EventHandlerGroup<T> where T:GameEvent
        {
            private event Action<T> CoreHandler;
            private HashSet<Action<T>> _members = new HashSet<Action<T>>();
            
            public void Invoke(T e)
            {
                CoreHandler?.Invoke(e);
            }

            public void Add(Action<T> e)
            {
                if (_members.Contains(e)) return;
                CoreHandler += e;
                _members.Add(e);
            }

            public void Remove(Action<T> e)
            {
                if (_members.Contains(e))
                {
                    CoreHandler -= e;
                    _members.Remove(e);
                }
            }
        }
    }
}