using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GAMEHIGAME.Root
{
    public class GameSelectionUIManager : MonoBehaviour
    {
        [SerializeField] private Transform gameListContainer;
        [SerializeField] private ToggleGroup toggleGroup;
        [SerializeField] private Button selectionOKButton;
     

        private List<GameItemController> allGameItems;
        private void Start()
        {
            allGameItems = new List<GameItemController>();
            InitGameList();
            selectionOKButton.onClick.AddListener(OnClickSelectionOK);
        }

        private void InitGameList()
        {
           foreach(var game in MainGameService.Instance.AllMiniGameSO)
            {
                GameItemController gameItemController = new GameItemController(game);
                gameItemController.SetParent(gameListContainer);
                gameItemController.SetToggleGroup(toggleGroup);
                gameItemController.IsSelected = (MainGameService.Instance.CurrentMiniGameID == game.MiniGameID);
                gameItemController.SetToggleSelection(gameItemController.IsSelected);
                allGameItems.Add(gameItemController);
                
            }
        }

        private void OnClickSelectionOK() 
        {
            GameItemController selectedGame = allGameItems.Find(x => x.IsSelected);
            if (selectedGame != null) 
            {
                MiniGameSO miniGameSO = selectedGame.GetMiniGameSO();
                MainGameService.Instance.SelectMiniGame(miniGameSO.MiniGameID);
            }
        }
    }
}
