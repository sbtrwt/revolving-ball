using GAMEHIGAME.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GAMEHIGAME.BouncingBall
{
    public class GameService : MonoBehaviour
    {
        public static GameService Instance { get; private set; }

        //Services
        public PlayerService PlayerService { get; private set; }
        public PowerService PowerService { get; private set; }

        //Scriptable inputs
        [SerializeField] private MiniGameSO miniGameSO;

        [SerializeField] private PlayerSO playerSO;
        [SerializeField] private PowerSO powerSO;
        [SerializeField] public List<Transform> allCenter;
        [SerializeField] public List<SpriteRenderer> allCenterSpreiteRenderer;
        //Properpties
        public int MiniGameID { get { return miniGameSO.MiniGameID; } }
        public int MiniGameHighScore { get { return miniGameSO.MiniGameHighScore; } set { miniGameSO.MiniGameHighScore = value; } }
        public MiniGameSO MiniGameSO => miniGameSO;
        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }
        private void Start()
        {
            InitService();
        }
        private void InitService() 
        {
            PlayerService = new PlayerService(playerSO, allCenter, allCenterSpreiteRenderer);
            PowerService = new PowerService(powerSO);
        }
        private void Update()
        {
            PowerService.Update();
            PlayerService.PlayerMove();
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("mouse down");
                PlayerService.SetCenter();
            }
        }


    }
}
