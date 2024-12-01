using UnityEngine;

namespace Kuhpik
{
    [CreateAssetMenu(menuName = "Config/GameConfig")]
    public sealed class GameConfig : ScriptableObject
    {
        public float[] FirstGameChance;
        public Sprite[] FirstGameSprites;
        public float[] WinMultipliers;
        
        public Sprite[] SecondGameSprites;

    }
}