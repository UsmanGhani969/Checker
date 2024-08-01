using CheckerAI.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace CheckerAI.Objects
{
    public class Square : MonoBehaviour
    {

        [SerializeField]
        private SquareProperties m_SquareProperties;

        [SerializeField]
        private Image m_Image;

        [SerializeField]
        private SquarePosition Position;


        private void OnEnable() => AddListeners();
        private void OnDisable() => RemoveListeners();

        private void AddListeners()
        {
            EventManager.DEACTIVATE_SELECTED_CHECKERS += SetColor;
        }
        private void RemoveListeners()
        {
            EventManager.DEACTIVATE_SELECTED_CHECKERS -= SetColor;
        }





        public void SetPosition(int _Row,int _Column)
        {
            Position.Row = _Row;
            Position.Column = _Column;
        }
        public SquarePosition GetPosition() => Position;


        public void Init()
        {
            SetColor();
            SetSprite();
        }

        private void SetColor() => m_Image.color = m_SquareProperties.GetColor();
        private void SetSprite() => m_Image.sprite = m_SquareProperties.GetSprite();








    }

    [System.Serializable]
    public class SquarePosition
    {
        public int Row;
        public int Column;
    }
}
