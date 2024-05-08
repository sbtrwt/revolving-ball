

using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace GAMEHIGAME.BouncingBall
{
    public class PowerController
    {
       
        protected PowerView powerView;
        protected PowerSO powerSO;
        private float activeDuration;
        private bool isActive;
        public PowerController(PowerSO powerSO)
        {
            this.powerSO = powerSO;
            SpawnPower();
        }
        public void SpawnPower()
        {
            powerView = Object.Instantiate(powerSO.PowerView);
            powerView.SetController(this);
            powerView.gameObject.SetActive(false);
        }
        

        public void Configure(Vector2 spawnPosition)
        {
            isActive = false;
            powerView.transform.position = spawnPosition;
            powerView.gameObject.SetActive(true);
        }
        public void PowerUpTriggerEntered(GameObject collidedObject)
        {
            if (collidedObject.GetComponent<PlayerView>() != null)
                Activate();
        }
        public virtual void Activate()
        {
            isActive = true;
            DestroyPower();
            //StartTimer();
        }
        public async void StartTimer()
        {
            if (isActive)
            {
                await Task.Delay(Mathf.RoundToInt(activeDuration * 1000));
                Deactivate();
            }
        }
        public virtual void Deactivate() => isActive = false;

        public void DestroyPower()
        {
            if (powerView == null) return;
            powerView.gameObject.SetActive(false);
            GameService.Instance.PowerService.ReturnToPool(this);
        }
        public void SetPowerText(string text)
        {
            powerView.SetText(text);
        }
    }
}
