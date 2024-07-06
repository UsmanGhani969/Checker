using CheckerAI.Objects;
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


        private void OnEnable() => AddListeners();
        private void OnDisable() => RemoveListeners();

        private void AddListeners()
        {
            EventManager.CHECKER_POSSIBLE_MOVES += GetCheckerPossibleMove;

            EventManager.PLAYER_TURN += Turn;
        }
        private void RemoveListeners()
        {
            EventManager.CHECKER_POSSIBLE_MOVES -= GetCheckerPossibleMove;

            EventManager.PLAYER_TURN -= Turn;
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

            Square[,] square = new Square[columnRows, columnRows];

            m_Layout.constraintCount = columnRows;

            int cellSize = m_ResourceLoader.GetGameSettings.GetCellSize();

            m_Layout.cellSize = new Vector2(cellSize, cellSize);

            for(int i = 0; i <columnRows; i++)
            {

                for(int j = 0;j<columnRows; j++)
                {
                    GameObject _Clone = (i % 2 != 0) ? ((j % 2 == 0) ? m_EmptySquarePrefab.gameObject : m_SquarePrefab.gameObject) : ((j % 2 != 0) ? m_EmptySquarePrefab.gameObject : m_SquarePrefab.gameObject); 

                    _Clone=Instantiate(_Clone);               
                    _Clone.gameObject.name = i + "_" + j;

                    _Clone.transform.SetParent(m_Content);
                    _Clone.transform.localScale = Vector3.one;

                    Square s= _Clone.GetComponent<Square>();
                    s.Init();
                    s.SetPosition(i, j);

                    square[i,j]=(s);

                }
            }

            GenerateOpponentCheckers(square, columnRows, cellSize);
            GeneratePlayerCheckers(square, columnRows,cellSize);
        }


        #region Summary
        /// <summary>
        /// Place Opponent Cheekers On Board
        /// </summary>
        /// <param name="_Square">All Squares</param>
        /// <param name="_columnRows">Number of Columns on the Board</param>
        /// <param name="_CellSize">Square Size</param>
        #endregion
        private void GenerateOpponentCheckers(Square[,] _Square,int _columnRows,int _CellSize)
        {
            int totalRows=((_columnRows)/2)-1;

            bool flag = true;

            for (int i = 0; i < totalRows; i++)
            {
                for (int j = 0; j < _columnRows; j++)
                {
                    if(flag)
                    {
                        if(j % 2 == 0)
                        {
                            CreateCheckerPrefab(m_OpponentChecker.gameObject, _Square, _CellSize, i, j);
                        }
                    }
                    else
                    {
                        if(j%2!=0)
                        {
                            CreateCheckerPrefab(m_OpponentChecker.gameObject, _Square, _CellSize, i, j);
                        }
                    }
                }
                flag ^= true;
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
        private void CreateCheckerPrefab(GameObject _CheckerPrefab, Square[,] _Square,int _CellSize,int _CIndex,int _RIndex)
        {
            GameObject checker = Instantiate(_CheckerPrefab);
            checker.transform.SetParent(_Square[_CIndex, _RIndex].transform);
            checker.transform.localScale = Vector3.one;
            checker.GetComponent<Checker>().Init();

            checker.GetComponent<RectTransform>().sizeDelta = new Vector2(_CellSize, _CellSize);
        }


        #region Summary
        /// <summary>
        /// Place Opponent Cheekers On Board
        /// </summary>
        /// <param name="_Square">All Movable Squares</param>
        /// <param name="_columnRows">Number of Columns on the Board</param>
        /// <param name="_CellSize">Square Size</param>
        #endregion
        private void GeneratePlayerCheckers(Square[,] _Square, int _columnRows, int _CellSize)
        {

            int totalRows = ((_columnRows) / 2) - 1;

            bool flag = false;

            for (int i = 0; i < totalRows; i++)
            {
                for (int j = _columnRows-1; j>=0; j--)
                {
                    if (flag)
                    {
                        if (j % 2 == 0)
                        {
                            CreateCheckerPrefab(m_PlayerChecker.gameObject, _Square, _CellSize, (_columnRows - i)-1, j);
                        }
                    }
                    else
                    {
                        if (j % 2 != 0)
                        {
                            CreateCheckerPrefab(m_PlayerChecker.gameObject, _Square, _CellSize,(_columnRows-i)-1, j);
                        }
                    }
                }
                flag ^= true;
            }

        }


        #region Summary
        /// <summary>
        /// Which Turn is This Player Or Opponent
        /// </summary>
        /// <param name="playerType">Player Or Opponent</param>
        /// <returns></returns>
        #endregion
        private void Turn(PlayerType playerType)
        {
            List<Checker> checkerList = new List<Checker>();    

            switch (playerType)
            {
                case PlayerType.Player:
                    checkerList=GetPlayerCheckers();
                    break;
                case PlayerType.Opponent:
                    checkerList=GetOpponentCheckers();
                    break;
            }

           FindMovableCheckers(checkerList);
        }


        #region Summary
        /// <summary>
        /// Get All Available Path/Squares for Move.
        /// </summary>
        /// <returns></returns>
        #endregion
        private List<Square> GetAllMovableSquares()
        {
            List<Square> squares = new List<Square>();

            for (int i = 0; i < m_Content.childCount; i++)
            {
               squares.Add(m_Content.transform.GetChild(i).GetComponent<Square>());
            }

            return squares;
        }


        #region Summary
        /// <summary>
        /// Get All Available Player Checkers
        /// </summary>
        /// <returns></returns>
        #endregion
        private List<Checker> GetPlayerCheckers()
        {
            List<Checker> checkers = new List<Checker>();

            foreach (Square square in GetAllMovableSquares())
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
            return checkers;
        }


        #region Summary
        /// <summary>
        /// Get All Available Opponent Checkers
        /// </summary>
        /// <returns></returns>
        #endregion
        private List<Checker> GetOpponentCheckers()
        {
            List<Checker> checkers = new List<Checker>();

            foreach (Square square in GetAllMovableSquares())
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
            return checkers;
        }

        #region Summary
        /// <summary>
        /// Get All Available Empty Squares Which Have no checker on it
        /// </summary>
        /// <returns></returns>
        #endregion
        private List<Square> GetEmptySquares()
        {
            List<Square> squares = new List<Square>();

            foreach (Square square in GetAllMovableSquares())
            {
                square.Init();

                if (square.GetComponentInChildren<Checker>() == null)
                {
                    squares.Add(square);
                }
            }

            return squares;
        }


        #region Summary
        /// <summary>
        /// Get All Movable Checkers
        /// </summary>
        /// <param name="_checkers">search data</param>
        #endregion
        private void FindMovableCheckers(List<Checker> _checkers)
        {

            List<Square> sq = GetEmptySquares();

            foreach (Checker checker in _checkers)
            {
                Square square = checker.gameObject.GetComponentInParent<Square>();
                int rowValue = square.GetPosition().Row;
                int columnValue = square.GetPosition().Column;

                foreach (var item in sq)
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
                            square.gameObject.GetComponent<Image>().color= Color.red;
                            break;
                        }
                    }
                }
            }
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

            foreach (var item in GetEmptySquares())
            {

                item.Init();

                int EmptySquareRowValue = item.GetPosition().Row;
                int EmptySquareColumnValue= item.GetPosition().Column;

                int r_diff = Mathf.Abs(rowValue - EmptySquareRowValue);
                int c_diff = Mathf.Abs(EmptySquareColumnValue - columnValue);



                if (r_diff == 1)
                {
                    if (c_diff==1)
                    {
                        possibleSquares.Add(item);
                    }
                }
            }

            return possibleSquares;

        }




    }
}