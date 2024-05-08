

using UnityEngine;
using UnityEngine.UI;

namespace GAMEHIGAME.Root
{
    public class GameItemController
    {
      
        private GameItemView gameItemView;
        private MiniGameSO miniGameSO;
        public bool IsSelected { get; set; }
        public GameItemController(MiniGameSO miniGameSO)
        {
            this.miniGameSO = miniGameSO;
            InitView();
        }
        private void InitView()
        {
            gameItemView = Object.Instantiate(miniGameSO.GameItemViewPrefab);
            gameItemView.SetController(this);
            gameItemView.SetHighScoreText(miniGameSO.MiniGameHighScore.ToString());
            gameItemView.SetMiniGameNameText(miniGameSO.MiniGameName);
        }
        public void SetParent(Transform parent)
        {
            gameItemView.transform.SetParent(parent);
        }
        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            gameItemView.SetToggleGroup(toggleGroup);
        }
        public MiniGameSO GetMiniGameSO() => miniGameSO;

        public void SetToggleSelection(bool isOn) => gameItemView.SetToggleSelection(isOn);
        
    }
}
