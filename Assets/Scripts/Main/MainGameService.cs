using GAMEHIGAME.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GAMEHIGAME.Root
{
    public class MainGameService : GenericMonoSingleton<MainGameService>
    {
        [SerializeField] private List<MiniGameSO> allMiniGameSO;
        [SerializeField] private int currentMiniGameID;
        [SerializeField] private MiniGameSO currentMiniGame;

        public int CurrentMiniGameID { get { return currentMiniGameID; } }
        public int CurrentMiniGameHighScore { get { return currentMiniGame.MiniGameHighScore; } }
        public string CurrentMiniGameName { get { return currentMiniGame.MiniGameName; } }
        public List<MiniGameSO> AllMiniGameSO { get { return allMiniGameSO; } }
       
        private void Start()
        {
            LoadAllGame();
            currentMiniGameID = PlayerPrefs.GetInt(GlobalConstant.MINIGAMEID_KEY);
            Debug.Log(currentMiniGameID);
            if (currentMiniGameID == 0) { currentMiniGameID = 1; }
            SelectMiniGame(currentMiniGameID);
        }
        public void SelectMiniGame(int miniGameID)
        {
            currentMiniGameID = miniGameID;
            PlayerPrefs.SetInt(GlobalConstant.MINIGAMEID_KEY, miniGameID);
            currentMiniGame = allMiniGameSO.Find(x => x.MiniGameID == miniGameID);
            GameData gameData = SaveSystem.LoadGameData(miniGameID);
            if (gameData != null)
            {
                currentMiniGame.MiniGameHighScore = gameData.HighScore;
            }

            UIManager.Instance.SetGameUI(currentMiniGame);
        }
        public void SetHighScore(int miniGameID, int highScore)
        {
            MiniGameSO miniGameSO = allMiniGameSO.Find(x => x.MiniGameID == miniGameID);
            miniGameSO.MiniGameHighScore = highScore;
            SaveGameData(miniGameSO);
        }
        public int GetHighScore(int miniGameID)
        {
            MiniGameSO miniGameSO = allMiniGameSO.Find(x => x.MiniGameID == miniGameID);
            GameData gameData = SaveSystem.LoadGameData(miniGameID);
            if (gameData != null)
            {
                miniGameSO.MiniGameHighScore = gameData.HighScore;
            }
            return miniGameSO.MiniGameHighScore;
        }

        public void SaveGameData(MiniGameSO miniGameSO)
        {
            SaveSystem.SaveGameData(new GameData(miniGameSO));
        }
        public void LoadAllGame()
        {
            foreach (var game in AllMiniGameSO)
            {
                GameData gameData = SaveSystem.LoadGameData(game.MiniGameID);
                if (gameData != null)
                {
                    game.MiniGameHighScore = gameData.HighScore;
                }
            }
        }
    }
}
