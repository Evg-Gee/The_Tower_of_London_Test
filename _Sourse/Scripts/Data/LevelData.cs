using System.Collections.Generic;
using UnityEngine;

namespace Data
{   
    [CreateAssetMenu(fileName = "LevelData", menuName = "TowerOfLondon/LevelData")]
    public class LevelData : ScriptableObject
    {
        public int levelIndex; 
        public int numberOfRings; 
        public int maxMoves; 

        public List<Color> initialColors;

        public List<PoleTargetState> targetStates;
    }
}