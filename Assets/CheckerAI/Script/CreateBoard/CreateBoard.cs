using CheckerAI.Objects;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace CheckerAI.Utilities
{
    public class CreateBoard : MonoBehaviour
    {
        private ResourceLoader m_ResourceLoader;

        [SerializeField]
        private Transform m_EmptySquarePrefab;
        [SerializeField]
        private Transform m_SquarePrefab;
        [SerializeField]
        private Transform m_Content;
        [SerializeField]
        private GridLayoutGroup m_Layout;
        [SerializeField]
        private Transform m_OpponentChecker;
        [SerializeField]
        private Transform m_PlayerChecker;


        private void Start()
        {
            m_ResourceLoader=GameObject.FindObjectOfType<ResourceLoader>();

            GenerateBoardSquares();

            GetAllMovableSquares();
        }


        #region Summary
        /// <summary>
        /// Generate Cheeker Board
        /// </summary>
        #endregion
        private void GenerateBoardSquares()
        {
            List<GameObject> square = new List<GameObject>();

            int columnRows =(int)m_ResourceLoader.GetGameSettings.GetBoardSize;

            m_Layout.constraintCount = columnRows;

            int cellSize = m_ResourceLoader.GetGameSettings.GetCellSize();

            m_Layout.cellSize = new Vector2(cellSize, cellSize);

            for(int i = 0; i <columnRows; i++)
            {

                for(int j = 0;j<columnRows; j++)
                {
                    GameObject _Clone = (i % 2 != 0) ? ((j % 2 == 0) ? m_EmptySquarePrefab.gameObject : m_SquarePrefab.gameObject) : ((j % 2 != 0) ? m_EmptySquarePrefab.gameObject : m_SquarePrefab.gameObject); 

                    _Clone=Instantiate(_Clone);

                    _Clone.transform.SetParent(m_Content);

                    _Clone.transform.localScale = Vector3.one;

                    if (_Clone.GetComponent<Square>() != null)
                    {
                        square.Add(_Clone);

                        _Clone.GetComponent<Square>().SetPosition(i, (j/2));
                    }
                }
            }

            GenerateOpponentCheckers(square, columnRows, cellSize);
            GeneratePlayerCheckers(square, columnRows,cellSize);
        }


        #region Summar
        /// <summary>
        /// Place Opponent Cheekers On Board
        /// </summary>
        /// <param name="_Square">All Movable Squares</param>
        /// <param name="_columnRows">Number of Columns on the Board</param>
        #endregion
        private void GenerateOpponentCheckers(List<GameObject> _Square,int _columnRows,int _CellSize)
        {
            int totalRows=((_columnRows)/2)-1;

            int opponentCheckerCount=((_columnRows)/2)*totalRows;

            for (int i = 0; i < opponentCheckerCount; i++)
            {
                GameObject checker = Instantiate(m_OpponentChecker.gameObject);
                checker.transform.SetParent(_Square[i].transform);
                checker.transform.localScale = Vector3.one;

                checker.GetComponent<RectTransform>().sizeDelta=new Vector2(_CellSize, _CellSize);

            }
        }


        #region Summar
        /// <summary>
        /// Place Opponent Cheekers On Board
        /// </summary>
        /// <param name="_Square">All Movable Squares</param>
        /// <param name="_columnRows">Number of Columns on the Board</param>
        #endregion
        private void GeneratePlayerCheckers(List<GameObject> _Square, int _columnRows, int _CellSize)
        {
            int totalRows = ((_columnRows) / 2) - 1;

            int playerCheckerCount = ((_columnRows) / 2) * totalRows;

            for (int i = _Square.Count-1; i > ((_Square.Count-1)-playerCheckerCount); i--)
            {
                GameObject checker = Instantiate(m_PlayerChecker.gameObject);
                checker.transform.SetParent(_Square[i].transform);
                checker.transform.localScale = Vector3.one;

                checker.GetComponent<RectTransform>().sizeDelta = new Vector2(_CellSize, _CellSize);

            }
        }



        private void GetAllMovableSquares()
        {

            List<Square> squares = new List<Square>();

            for (int i = 0; i < m_Content.childCount; i++)
            {
                if (m_Content.GetChild(i).GetComponent<Square>() != null)
                {
                    squares.Add(m_Content.transform.GetChild(i).GetComponent<Square>());
                }
            }

            GetPlayerCheckers(squares);
            GetOpponentCheckers(squares);
        }

        private void GetPlayerCheckers(List<Square> _Squares)
        {
            List<Checker> checkers = new List<Checker>();

            foreach (Square square in _Squares)
            {
                if(square.GetComponentInChildren<Checker>() != null)
                {
                    Checker checker = square.GetComponentInChildren<Checker>();
                    if(checker.GetPlayerType() == PlayerType.Player)
                    {
                        checkers.Add(checker);
                    }
                }
            }
        }
        private void GetOpponentCheckers(List<Square> _Squares)
        {
            List<Checker> checkers = new List<Checker>();

            foreach (Square square in _Squares)
            {
                if (square.GetComponentInChildren<Checker>() != null)
                {
                    Checker checker = square.GetComponentInChildren<Checker>();
                    if (checker.GetPlayerType() == PlayerType.Opponent)
                    {
                        checkers.Add(checker);
                    }
                }
            }
        }
        





    }
}