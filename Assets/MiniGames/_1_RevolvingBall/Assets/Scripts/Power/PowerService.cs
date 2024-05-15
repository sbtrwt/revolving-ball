using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GAMEHIGAME.BouncingBall
{
    public class PowerService
    {
        private PowerSO powerSO;
        //private List<PowerController> powerControllers;
        private bool isSpawning;
        private float spawnTimer;
        private PowerPool powerPool;
        public PowerService(PowerSO powerSO)
        {
            this.powerSO = powerSO;

            spawnTimer = this.powerSO.SpawnTime;
            isSpawning = true;
            powerPool = new PowerPool(powerSO);
            powerPool.InitPowerPoolItems(powerSO.MaxPowerCount);
        }
        public void Update()
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                SpawnPowerUps();
                ResetSpawnTimer();
            }
        }
        private void ResetSpawnTimer() => spawnTimer = powerSO.SpawnTime;
        private Vector2 CalculateRandomSpawnPosition()
        {
            // Get the boundaries of the visible game screen
            float minX = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0, 0)).x;
            float maxX = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0, 0)).x;
            float minY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.2f, 0)).y;
            float maxY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;

            // Generate random values for X and Y coordinates within the screen boundaries
            float randomX = UnityEngine.Random.Range(minX, maxX);
            float randomY = UnityEngine.Random.Range(minY, maxY);

            // Return the calculated random spawn position
            return new Vector2(randomX, randomY);
        }

        private void SpawnPowerUps()
        {

            if (isSpawning)
            {
                // Fetch the corresponding PowerUpController
                PowerController powerUp = powerPool.GetPower();
                if (powerUp != null)
                {
                 
                    // Configure the PowerUp to be spawned.
                    powerUp.Configure(CalculateRandomSpawnPosition());
                }
            }
        }


        public void SetPowerSpawning(bool setSpawningActive) => isSpawning = setSpawningActive;

        public void ReturnToPool(PowerController powerToReturn)
        {
            powerPool.ReturnItem(powerToReturn);
        }
    }
}
