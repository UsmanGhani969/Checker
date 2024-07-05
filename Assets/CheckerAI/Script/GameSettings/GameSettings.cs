using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CheckerAI.Settings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Game Settings")]
    public class GameSettings : ScriptableObject
    {


        [SerializeField]
        private BoardSize m_BoardSize;

        [SerializeField]
        private List<BoardSizes> m_BoardSizes;


        #region Properties
        public BoardSize GetBoardSize { get { return m_BoardSize; } }
        #endregion


        #region CustomMethods

        #region Summary
        /// <summary>
        /// Get Cell Size Respect to Board Size
        /// </summary>
        /// <returns></returns>
        #endregion
        public int GetCellSize()
        {
            foreach (var item in m_BoardSizes)
            {
                if(item.BoardSize==m_BoardSize)
                {
                    return item.CellSize;
                }
            }
            return 0;
        }

        #endregion


    }

    public enum BoardSize
    {
        _4X=4,
        _6X=6,
        _8X=8,
        _10x=10
    }

    [System.Serializable]
    public class BoardSizes
    {
        public BoardSize BoardSize;
        public int CellSize;
    }
}