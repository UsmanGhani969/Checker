using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CheckerAI.Objects
{
    public class Checker : MonoBehaviour
    {
        [SerializeField]
        private CheckerProperties m_CheckerProperties;

        private Image m_Image;

        private void Start()
        {

            m_Image = GetComponent<Image>();

            SetColor();
            SetSprite();
        }

        public PlayerType GetPlayerType() => m_CheckerProperties.GetPlayerType;
        private void SetColor()=>m_Image.color = m_CheckerProperties.GetColor();
        private void SetSprite()=>m_Image.sprite = m_CheckerProperties.GetSprite();

    }
}
