using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    void Awake()
    {
        boardSetUp();
    }


    void boardSetUp()
    {
        Board.isPlayerWhite = true;
        Board.whiteToMove = true;
        Board.moveCount = 1;
        Board.whiteChecksBlack = false;
        Board.blackChecksWhite = false;
        Board.loadFromFen(Board.startFEN);
        //Board.loadFromFen("r1b2r1k/4qp1p/p1Nppb1Q/4nP2/1p2P3/2N5/PPP4P/2KR1BR1 b - - 5 18");
        /*for (int i = 0;i< 63;i++)
        {
            Debug.Log(Board.Square[i]);
        }*/
        //Board.squareToFen(Board.Square);
    }

}
