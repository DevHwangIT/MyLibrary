namespace MyLibrary.Utility.Sample
{
    public static class PlayerEvents
    {
        public class PlayerHPChanged : GameEvent
        {
            public readonly int previousValue;
            public readonly int currentValue;

            public PlayerHPChanged(int prev, int curr)
            {
                previousValue = prev;
                currentValue = curr;
            }
        }
        
        public class PlayerMPChanged : GameEvent
        {
            public readonly int previousValue;
            public readonly int currentValue;
            
            public PlayerMPChanged(int prev, int curr)
            {
                previousValue = prev;
                currentValue = curr;
            }
        }

        public class PlayerDead : GameEvent
        {
            
        }
    }
}