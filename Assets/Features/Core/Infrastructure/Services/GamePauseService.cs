using UnityEngine;
namespace Feature.Core
{
    public class GamePauseService : IGamePauseService
    {
        public bool Paused { get; private set; }
        
        public void SetPause()
        {
            if (Paused) return;

            Paused = true;

            Time.timeScale = 0f;
        }
        
        public void SetPlay()
        {
            if (Paused == false) return;

            Paused = false;

            Time.timeScale = 1f;
        }
    }
}