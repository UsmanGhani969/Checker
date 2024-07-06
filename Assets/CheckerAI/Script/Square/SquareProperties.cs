using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CheckerAI.Objects
{
    [CreateAssetMenu(fileName = "Square Properties", menuName = "Square Properties")]
    public class SquareProperties : ScriptableObject
    {
        [SerializeField]
        [ColorUsage(true, true)]
        private Color32 m_Color;

        [SerializeField]
        private Sprite m_Sprite;


        public Color32 GetColor() => m_Color;
        public Sprite GetSprite() => m_Sprite;
    }
}