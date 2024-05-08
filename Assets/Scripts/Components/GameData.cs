namespace GAMEHIGAME.Root
{
    [System.Serializable]
    public class GameData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public  int HighScore { get; set; }
        public GameData() { }
        public GameData(MiniGameSO miniGameSO)
        {
            ID = miniGameSO.MiniGameID;
            Name = miniGameSO.MiniGameName;
            HighScore = miniGameSO.MiniGameHighScore;
        }
       
    }

}
