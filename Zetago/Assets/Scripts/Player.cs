using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Camera cam;
    public GameObject boardUI;
    ChessBoardGenerater chessBoard;

    public void Awake()
    {
        cam = Camera.main;
        chessBoard = boardUI.GetComponent<ChessBoardGenerater>();
    }

    public void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            chessBoard.DragPiece(TryGetSquareUnderMouse(mousePos));
        }
        else if (Input.GetMouseButtonUp(0))
        {
            chessBoard.ReleasePiece(TryGetSquareUnderMouse(mousePos));
        }
    }

    public int TryGetSquareUnderMouse(Vector2 mouseWorld)
    {
        if(mouseWorld.x<-4 || mouseWorld.y<-4)
        {
            return 99;
        }
        else
        {
            int file = (int)(4 + mouseWorld.x);
            int rank = (int)(4 + mouseWorld.y);
            if (Board.whiteIsBottom)
            {
                file = 7 - file;
                rank = 7 - rank;
            }
            if (file >= 0 && file < 8 && rank >= 0 && rank < 8)
                return rank * 8 + file;
            else
                //mouse not on square
                return 99;
        }
    }
}