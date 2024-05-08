
using UnityEngine;

namespace GAMEHIGAME.BouncingBall
{
    [CreateAssetMenu(fileName = "New Player", menuName = "Player")]
    public class PlayerSO : ScriptableObject
    {
        public int ID;
        public int BounceCount;
        public PlayerView PlayerView;
    }
}
