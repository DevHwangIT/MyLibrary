using UnityEngine;

namespace MyLibrary.Utility.Sample
{
    public class Player : MonoBehaviour
    {
        private int _hp = 100;
        private int _mp = 100;

        void Start()
        {
            EventBroadcaster.Broadcast(new PlayerEvents.PlayerHPChanged(_hp, _hp));
            EventBroadcaster.Broadcast(new PlayerEvents.PlayerMPChanged(_mp, _mp));
        }
    
        public void HitByMonster()
        {
            UpdateHP(-10);
        }

        private void UpdateHP(int amount)
        {
            var prev = _hp;
            _hp += amount;
            EventBroadcaster.Broadcast(new PlayerEvents.PlayerHPChanged(prev, _hp));
            
            if (_hp <= 0)
            {
                EventBroadcaster.Broadcast(new PlayerEvents.PlayerDead());
            }
        }

        public void FireFireball()
        {
            UpdateMP(-10);
        }

        private void UpdateMP(int amount)
        {
            var prev = _mp;
            _mp += amount;
            EventBroadcaster.Broadcast(new PlayerEvents.PlayerMPChanged(prev, _mp));
        }

        public void UseHPPotion()
        {
            UpdateHP(10);
        }

        public void UseMPPotion()
        {
            UpdateMP(10);
        }
    }
   
}