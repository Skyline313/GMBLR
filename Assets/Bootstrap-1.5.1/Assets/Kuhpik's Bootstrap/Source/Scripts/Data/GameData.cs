using System;
using UnityEngine;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using _Source.Code.Components;

namespace Kuhpik
{
    /// <summary>
    /// Used to store game data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class GameData
    {
        public int CoinsPerRound;
        public GameStateID Game;

        public int Bet;
        public LevelComponent Level;
        public LevelComponent[] Levels;
        public float FirstGameWinChance;
        public int CurrentRow;
        
        public SecondGameLevelComponent SecondLevel;
        public SecondGameLevelComponent[] SecondLevels;
    }
}