using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CheckerAI.Objects
{
    public class Square : MonoBehaviour
    {
        [SerializeField]
        private SquarePosition Position;

        public void SetPosition(int _Column,int _Row)
        {
            Position.Column = _Column;
            Position.Row = _Row;
        }
    }

    [System.Serializable]
    public class SquarePosition
    {
        public int Column;
        public int Row;
    }
}
