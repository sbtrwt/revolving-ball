using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GAMEHIGAME.BouncingBall
{
    public class PlayerService
    {
        private PlayerSO playerSO;
        private PlayerController playerController;
        public PlayerService(PlayerSO playerSO, List<Transform> allCenter, List<SpriteRenderer> allCenterSpreiteRenderer)
        {
            this.playerSO = playerSO;
            playerController = new PlayerController(playerSO);
            playerController.SetAllCenter(allCenter, allCenterSpreiteRenderer);
            SetCenter();
        }

        public PlayerController GetPlayerController()
        {
            return playerController;
        }

        public void PlayerMove()
        {
            playerController.Move();
        }
        public void SetCenter()
        {
            playerController.SetCenter();
        }
    }
}
