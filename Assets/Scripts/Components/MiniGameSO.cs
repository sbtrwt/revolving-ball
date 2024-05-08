using UnityEngine;

namespace GAMEHIGAME.Root
{
    [CreateAssetMenu(fileName = "New MiniGame", menuName = "MiniGame")]
    public class MiniGameSO : ScriptableObject
    {
        public int ID;
        public int MiniGameID;
        public string MiniGameName;
        public int MiniGameHighScore;
        public Sprite MiniGameSprite;
        public GameItemView GameItemViewPrefab;
    }
}
