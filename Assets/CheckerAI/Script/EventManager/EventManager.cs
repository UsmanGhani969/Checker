using CheckerAI.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CheckerAI.Utilities
{
    public class EventManager 
    {

        public static Func<Checker,List<Square>> CHECKER_POSSIBLE_MOVES;

        public static Action<PlayerType> PLAYER_TURN;
        
    }
}