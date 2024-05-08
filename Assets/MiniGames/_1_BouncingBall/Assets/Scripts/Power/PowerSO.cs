using UnityEngine;

namespace GAMEHIGAME.BouncingBall
{
    [CreateAssetMenu(fileName = "New Power", menuName = "Power")]
    public class PowerSO : ScriptableObject
    {
        public int ID;
        public PowerType PowerType;
        public PowerView PowerView;
        public int MaxRange;
        public int MinRange;
        public float SpawnTime;
        public int MaxPowerCount;
    }
}
