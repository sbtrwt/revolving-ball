

using UnityEngine;

namespace GAMEHIGAME.BouncingBall
{
    public class HealthPower : PowerController
    {
        private int healthPower;
        public HealthPower(PowerSO powerUpData) : base(powerUpData) { Init(); }
        
        public override void Activate()
        {
            base.Activate();
            GameService.Instance.PlayerService.GetPlayerController().AddCurrentBounceCount(healthPower);
            GameService.Instance.PlayerService.GetPlayerController().OnPowerCollision();

        }

        public override void Deactivate()
        {
            base.Deactivate();
           
        }
        private void Init()
        {
           healthPower = Random.Range(powerSO.MinRange, powerSO.MaxRange);
            SetPowerText(healthPower.ToString());
        }
    }
}
