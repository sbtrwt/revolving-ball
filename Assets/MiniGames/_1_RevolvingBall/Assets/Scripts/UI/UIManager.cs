using GAMEHIGAME.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GAMEHIGAME.BouncingBall
{ 
    public class UIManager : GenericMonoSingleton<UIManager>
    {

        [SerializeField] private Button buttonHome;
        [SerializeField] private Button buttonRestart;
        [SerializeField] private Button buttonHome2;
        [SerializeField] private GameObject restartPanel;
        [SerializeField] private TMPro.TMP_Text scoreText;
       
        private void Start()
        {
            buttonHome.onClick.AddListener(OnHomeClick);
            buttonHome2.onClick.AddListener(OnHomeClick);
            buttonRestart.onClick.AddListener(OnRestartClick);
        }
        private void OnHomeClick()
        {
            SceneManager.LoadScene(0);
        }
        private void OnRestartClick()
        {
            SceneManager.LoadScene(1);
        }
        public void SetRestartPanel(bool isVisible)
        {
            restartPanel.SetActive(isVisible);
        }
        public void SetScoreText(string score)
        {
            scoreText.text = score;
        }
    }
}