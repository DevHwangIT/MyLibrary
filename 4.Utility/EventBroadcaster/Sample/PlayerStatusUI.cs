using UnityEngine;
using UnityEngine.UI;

namespace MyLibrary.Utility.Sample
{
    public class PlayerStatusUI : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverText;
        [SerializeField] private Text hpText;
        [SerializeField] private Text mpText;
    
        void Awake()
        {
            gameOverText.SetActive(false);
        
            EventBroadcaster.Subscribe<PlayerEvents.PlayerHPChanged>(OnPlayerHPChanged);   
            EventBroadcaster.Subscribe<PlayerEvents.PlayerMPChanged>(OnPlayerMPChanged);   
            EventBroadcaster.Subscribe<PlayerEvents.PlayerDead>(OnPlayerDead);
        }

        private void OnPlayerHPChanged(PlayerEvents.PlayerHPChanged ev)
        {
            hpText.text = ev.currentValue.ToString();
        }
    
        private void OnPlayerMPChanged(PlayerEvents.PlayerMPChanged ev)
        {
            mpText.text = ev.currentValue.ToString();
        }

        private void OnPlayerDead(PlayerEvents.PlayerDead ev)
        {
            gameOverText.SetActive(true);
            
            EventBroadcaster.Unsubscribe<PlayerEvents.PlayerHPChanged>(OnPlayerHPChanged);   
            EventBroadcaster.Unsubscribe<PlayerEvents.PlayerMPChanged>(OnPlayerMPChanged);
            EventBroadcaster.Unsubscribe<PlayerEvents.PlayerDead>(OnPlayerDead);
        }
    }
}