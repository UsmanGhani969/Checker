using CheckerAI.Utilities;
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

        [SerializeField]
        private Image m_Image;

        [SerializeField]
        private Button m_Button;


        private void OnEnable() => AddListeners();
        private void OnDisable() => RemoveListeners();

        private void AddListeners()
        {
            m_Button.onClick.AddListener(OnCheckerClicked);
        }
        private void RemoveListeners()
        {
            m_Button.onClick.RemoveListener(OnCheckerClicked);
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
            List<Square> possibleSquares= EventManager.CHECKER_POSSIBLE_MOVES?.Invoke(this);

            
            if(possibleSquares.Count > 0 )
            {
                Square square = this.transform.GetComponentInParent<Square>();

                square.gameObject.GetComponent<Image>().color = Color.red;

                foreach (var item in possibleSquares)
                {
                    item.gameObject.GetComponent<Image>().color = Color.red;
                }
            }

           
        }

    }
}
