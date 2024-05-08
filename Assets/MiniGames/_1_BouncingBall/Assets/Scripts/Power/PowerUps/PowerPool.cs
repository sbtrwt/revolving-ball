using GAMEHIGAME.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAMEHIGAME.BouncingBall
{
    public class PowerPool : GenericObjectPool<PowerController>
    {
        private PowerSO powerSO;

        public PowerPool(PowerSO powerSO)
        {
            this.powerSO = powerSO;
        }

        public PowerController GetPower()
        {
            if (GetUnusedItemCount<PowerController>() > 0)
            {
                return GetItem<PowerController>();
            }
            else
            {
                return null;
            }

        }
        protected override PowerController CreateItem<T>() {
            PowerType randomPowerUp = (PowerType)UnityEngine.Random.Range(0, Enum.GetValues(typeof(PowerType)).Length);

            switch (randomPowerUp)
            {
                case PowerType.HEALTH:
                    return new HealthPower(powerSO);

                default:
                    throw new Exception($"Failed to Create PowerUpController for: {randomPowerUp}");
            }
             
        }

        public List<PowerController> InitPowerPoolItems(int quantity)
        {
            List<PowerController> powerControllerList = new List<PowerController>();
            for (int i = 0; i < quantity; i++)
            {
                powerControllerList.Add(GetItem<PowerController>());
            }
            foreach (var powerController in powerControllerList)
            {
                ReturnItem(powerController);
            }
            return powerControllerList;
        }
    }
}
