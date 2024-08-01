using CheckerAI.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
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

        [SerializeField]
        private Square[,] m_Square;



        private void OnEnable() => AddListeners();
        private void OnDisable() => RemoveListeners();

        private void AddListeners()
        {
            EventManager.GET_CHECKER_EVENT += GetCheckers;
            EventManager.GET_CHECKER_POSSIBLE_MOVES_EVENT += GetCheckerPossibleMove;
        }
        private void RemoveListeners()
        {
            EventManager.GET_CHECKER_EVENT -= GetCheckers;
            EventManager.GET_CHECKER_POSSIBLE_MOVES_EVENT -= GetCheckerPossibleMove;
        }


        private void Start()
        {
            m_ResourceLoader=GameObject.FindObjectOfType<ResourceLoader>();

            GenerateBoardSquares();
        }

        #region Summary
        /// <summary>
        /// Generate Cheeker Board
        /// </summary>
        #endregion
        private void GenerateBoardSquares()
        {

            int columnRows =(int)m_ResourceLoader.GetGameSettings.GetBoardSize;

            m_Square = new Square[columnRows, columnRows];

            m_Layout.constraintCount = columnRows;

            int cellSize = m_ResourceLoader.GetGameSettings.GetCellSize();

            m_Layout.cellSize = new Vector2(cellSize, cellSize);

            for(int i = 0; i <columnRows; i++)
            {

                for(int j = 0;j<columnRows; j++)
                {

                    GameObject _Clone = ((i+j)%2==0) ? m_SquarePrefab.gameObject : m_EmptySquarePrefab.gameObject;

                    _Clone=Instantiate(_Clone);               
                    _Clone.gameObject.name = i + "_" + j;

                    _Clone.transform.SetParent(m_Content);
                    _Clone.transform.localScale = Vector3.one;

                    Square s= _Clone.GetComponent<Square>();
                    s.Init();
                    s.SetPosition(i, j);

                    m_Square[i,j]=(s);

                }
            }

            GenerateOpponentCheckers(columnRows, cellSize);
            GeneratePlayerCheckers(columnRows,cellSize);
        }


        #region Summary
        /// <summary>
        /// Place Opponent Cheekers On Board
        /// </summary>
        /// <param name="_Square">All Squares</param>
        /// <param name="_columnRows">Number of Columns on the Board</param>
        /// <param name="_CellSize">Square Size</param>
        #endregion
        private void GenerateOpponentCheckers(int _columnRows,int _CellSize)
        {
            int totalRows=((_columnRows)/2)-1;


            for (int i=0;i<totalRows;i++)
            {

                for (int j = 0; j < _columnRows; j++)
                {

                    if ((i + j) % 2 == 0)
                    {
                        CreateCheckerPrefab(m_OpponentChecker.gameObject, m_Square[i, j], _CellSize, i, j);
                    }
                
                }

            }

           
        }


        #region Summary
        /// <summary>
        /// Place Opponent Cheekers On Board
        /// </summary>
        /// <param name="_Square">All Movable Squares</param>
        /// <param name="_columnRows">Number of Columns on the Board</param>
        /// <param name="_CellSize">Square Size</param>
        #endregion
        private void GeneratePlayerCheckers(int _columnRows, int _CellSize)
        {

            int totalRows = ((_columnRows) / 2) - 1;

            for (int i = _columnRows - 1; i >= (_columnRows - totalRows); i--)
            {

                for (int j = 0; j < _columnRows; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        CreateCheckerPrefab(m_PlayerChecker.gameObject, m_Square[i, j], _CellSize, i, j);
                    }
                }

            }

        }


        #region Summary
        /// <summary>
        /// Instantiate Checker Prefab on the Exact Position
        /// </summary>
        /// <param name="_CheckerPrefab">Checker Prefab</param>
        /// <param name="_Square">Array of All Squares Positions</param>
        /// <param name="_CellSize">Square Size</param>
        /// <param name="_CIndex">Column Index</param>
        /// <param name="_RIndex">Row Index</param>
        #endregion
        private void CreateCheckerPrefab(GameObject _CheckerPrefab, Square _Square,int _CellSize,int _CIndex,int _RIndex)
        {
            GameObject checker = Instantiate(_CheckerPrefab);
            checker.transform.SetParent(_Square.transform);
            checker.transform.localScale = Vector3.one;
            checker.GetComponent<Checker>().Init();

            checker.GetComponent<RectTransform>().sizeDelta = new Vector2(_CellSize, _CellSize);
        }


        #region Summary
        /// <summary>
        /// Get All Available Player Checkers
        /// </summary>
        /// <returns></returns>
        #endregion
        private List<Checker> GetCheckers(PlayerType _playerType)
        {
            List<Checker> checkers = new List<Checker>();

            int columnRows = (int)m_ResourceLoader.GetGameSettings.GetBoardSize;


            for (int i = 0;i<columnRows;i++)
            {
                for(int j = 0;j<columnRows;j++)
                {
                    if ((i+j)%2==0)
                    {
                        Checker checker = m_Square[i, j].GetComponentInChildren<Checker>();

                        if (checker != null)
                        {
                            if (checker.GetPlayerType() == _playerType)
                            {
                                checkers.Add(checker);
                            }
                        }
                    }
                }
            }
            return checkers;
        }


        #region Summary
        /// <summary>
        /// Find All Avaialbe Move of the current Checker which have clicked
        /// </summary>
        /// <param name="_checker"></param>
        #endregion
        private List<Square> GetCheckerPossibleMove(Checker checker)
        {

            Square square = checker.gameObject.GetComponentInParent<Square>();

            int rowValue = square.GetPosition().Row;
            int columnValue= square.GetPosition().Column;

            List<Square> possibleSquares = new List<Square>();

            List<Square> emptySquares = new();
            emptySquares = FindEmptySquares();

            foreach (var item in emptySquares)
            {

                item.Init();

                int EmptySquareRowValue = item.GetPosition().Row;
                int EmptySquareColumnValue = item.GetPosition().Column;

                int r_diff = Mathf.Abs(rowValue - EmptySquareRowValue);
                int c_diff = Mathf.Abs(EmptySquareColumnValue - columnValue);



                if (r_diff == 1)
                {
                    if (c_diff == 1)
                    {
                        possibleSquares.Add(item);
                    }
                }
            }

            return possibleSquares;

        }

        private List<Square> FindEmptySquares()
        {
            List<Square> squares = new List<Square>();

            int columnRows = (int)m_ResourceLoader.GetGameSettings.GetBoardSize;


            for (int i = 0; i < columnRows; i++)
            {
                for (int j = 0; j < columnRows; j++)
                {
                    if ((i + j) % 2 == 0)
                    {
                        Checker checker = m_Square[i, j].GetComponentInChildren<Checker>();

                        if (checker == null)
                        {
                            squares.Add(m_Square[i,j]);
                        }
                    }
                }
            }
            return squares;
        }



    }
}