using CheckerAI.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CheckerAI.Utilities
{
    public class ResourceLoader : MonoBehaviour
    {
        [SerializeField]
        private GameSettings m_GameSettings;
        public GameSettings GetGameSettings { get { return m_GameSettings; } }



    }
}

