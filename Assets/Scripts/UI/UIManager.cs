using GAMEHIGAME.Utilities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GAMEHIGAME.Root
{
    public class UIManager : GenericMonoSingleton<UIManager>
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button selectionButton;
        [SerializeField] private Button selectOKButton;
        [SerializeField] private GameObject gameSelectionPanel;
        [SerializeField] private int selectedGame;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private TMP_Text miniGameNameText;
        [SerializeField] private Button exitButton;
        private void Start()
        {
            selectOKButton.onClick.AddListener(OnSelectOKButton);
            selectionButton.onClick.AddListener(OnSelectionClick);
            playButton.onClick.AddListener(OnPlayClick);
            exitButton.onClick.AddListener(OnExitButton);

            SetHighScoreText(MainGameService.Instance.CurrentMiniGameHighScore.ToString());
            SetMiniGameNameText(MainGameService.Instance.CurrentMiniGameName);
        }

        private void OnPlayClick() { SceneManager.LoadScene(selectedGame); }
        private void OnSelectionClick()
        {
            gameSelectionPanel.SetActive(true);
        }
        private void OnSelectOKButton()
        {
            gameSelectionPanel.SetActive(false);
        }
        private void OnExitButton()
        {
            Application.Quit();
        }

        public void SetHighScoreText(string text)
        {
            highScoreText.text = text;
        }
        public void SetMiniGameNameText(string text)
        {
            miniGameNameText.text = text;
        }

        public void SetSelectedGame(int gameID) => selectedGame = gameID;

        public void SetGameUI(MiniGameSO miniGameSO)
        {
           SetSelectedGame(miniGameSO.MiniGameID);
           SetHighScoreText(miniGameSO.MiniGameHighScore.ToString());
           SetMiniGameNameText(miniGameSO.MiniGameName);
        }
    }
}