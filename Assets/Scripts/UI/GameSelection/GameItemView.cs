using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GAMEHIGAME.Root
{
    public class GameItemView : MonoBehaviour
    {
        private GameItemController gameItemController;
        [SerializeField] private TMP_Text highScoreText;
        [SerializeField] private TMP_Text miniGameNameText;
        [SerializeField] private Toggle gameSelection;
        private void Start()
        {
            gameSelection.onValueChanged.AddListener(OnClickGameSelection);
        }
        public void SetHighScoreText(string text)
        {
            highScoreText.text = text;
        }
        public void SetMiniGameNameText(string text)
        {
            miniGameNameText.text = text;
        }

        public void SetToggleSelection(bool isOn)
        {
            gameSelection.isOn = isOn;
        }
        public void SetController(GameItemController controller) { gameItemController = controller; }

        public void SetToggleGroup(ToggleGroup toggleGroup) { gameSelection.group = toggleGroup; }

        private void OnClickGameSelection(bool isSelected) { gameItemController.IsSelected = isSelected; }
    }
}
