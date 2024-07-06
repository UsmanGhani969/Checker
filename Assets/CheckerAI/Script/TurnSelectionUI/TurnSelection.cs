using CheckerAI.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
            EventManager.PLAYER_TURN?.Invoke(Objects.PlayerType.Player);
        }
        private void OnOpponentTurnClicked()
        {
            EventManager.PLAYER_TURN?.Invoke(Objects.PlayerType.Opponent);
        }
    }
}