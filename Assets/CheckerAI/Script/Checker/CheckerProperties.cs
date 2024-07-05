using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CheckerAI.Objects
{
    [CreateAssetMenu(fileName = "Checker Properties", menuName = "Checker Properties")]
    public class CheckerProperties : ScriptableObject
    {

        [SerializeField]
        private PlayerType m_PlayerType;

        [SerializeField]
        [ColorUsage(true, true)]
        private Color32 m_Color;

        [SerializeField]
        private Sprite m_Sprite;

        public Color32 GetColor() => m_Color;
        public Sprite GetSprite() => m_Sprite;
        public PlayerType GetPlayerType => m_PlayerType;






        
    }
    [System.Serializable]
    public enum PlayerType
    {
        Player,
        Opponent
    }
}