using CheckerAI.Utilities;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace CheckerAI.Objects
{
    public class Checker : MonoBehaviour
    {
        [SerializeField]
        private CheckerProperties m_CheckerProperties;

        [SerializeField]
        private Image m_Image;

        [SerializeField]
        private Button m_Button;

        private bool m_IsKing;


        private void OnEnable() => AddListeners();
        private void OnDisable() => RemoveListeners();

        private void AddListeners()
        {
            m_Button.onClick.AddListener(OnCheckerClicked);

            EventManager.DEACTIVATE_SELECTED_CHECKERS += SetColor;
        }
        private void RemoveListeners()
        {
            m_Button.onClick.RemoveListener(OnCheckerClicked);

            EventManager.DEACTIVATE_SELECTED_CHECKERS -= SetColor;

        }


        public void Init()
        {
            SetColor();
            SetSprite();
        }


        public PlayerType GetPlayerType() => m_CheckerProperties.GetPlayerType;
        private void SetColor()=>m_Image.color = m_CheckerProperties.GetColor();
        private void SetSprite()=>m_Image.sprite = m_CheckerProperties.GetSprite();

        private void OnCheckerClicked()
        {
            EventManager.DEACTIVATE_SELECTED_CHECKERS?.Invoke();

            this.gameObject.GetComponent<Image>().color = Color.red; 

            List<Square> possibleMoves=EventManager.GET_CHECKER_POSSIBLE_MOVES_EVENT?.Invoke(this);

            foreach (var item in possibleMoves)
            {
                item.gameObject.GetComponent<Image>().color= Color.red;
            }
        }

    }
}
