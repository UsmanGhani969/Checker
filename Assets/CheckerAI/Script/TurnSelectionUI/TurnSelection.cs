using CheckerAI.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CheckerAI.Objects;

namespace CheckerAI.UI
{
    public class TurnSelection : MonoBehaviour
    {
        [SerializeField]
        private Button m_PlayerTurnBtn;
        [SerializeField]
        private Button m_OpponentTurnBtn;


        private void OnEnable() => AddListeners();
        private void OnDisable() => RemoveListeners();

        private void AddListeners()
        {
            m_PlayerTurnBtn.onClick.AddListener(OnPlayerTurnClicked);
            m_OpponentTurnBtn.onClick.AddListener(OnOpponentTurnClicked);
        }
        private void RemoveListeners()
        {
            m_PlayerTurnBtn.onClick.RemoveListener(OnPlayerTurnClicked);
            m_OpponentTurnBtn.onClick.RemoveListener(OnOpponentTurnClicked);
        }

        private void OnPlayerTurnClicked()
        {
            EventManager.DEACTIVATE_SELECTED_CHECKERS?.Invoke();

            List<Checker> checkers= EventManager.GET_CHECKER_EVENT?.Invoke(PlayerType.Player);

            foreach (Checker checker in checkers)
            {
                checker.gameObject.GetComponent<Image>().color = Color.red;
            }
        }
        private void OnOpponentTurnClicked()
        {
            EventManager.DEACTIVATE_SELECTED_CHECKERS?.Invoke();

            List<Checker> checkers = EventManager.GET_CHECKER_EVENT?.Invoke(PlayerType.Opponent);

            foreach (Checker checker in checkers)
            {
                checker.gameObject.GetComponent<Image>().color = Color.red;
            }
        }
    }
}