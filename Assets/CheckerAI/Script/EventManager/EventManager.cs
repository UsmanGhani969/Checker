using CheckerAI.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CheckerAI.Utilities
{
    public class EventManager 
    {

        public static Action DEACTIVATE_SELECTED_CHECKERS;

        public static Func<PlayerType, List<Checker>> GET_CHECKER_EVENT;

        public static Func<Checker, List<Square>> GET_CHECKER_POSSIBLE_MOVES_EVENT;
        
    }
}