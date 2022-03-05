using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCaculator
{
    public static void Move(int startSquare ,int endSquare)
    {
        bool takes = false;
        bool promotion = false;
        if (Board.Square[endSquare] != 0)
        {
            takes = true;
        }
        if (Board.Square[startSquare] == 12 && endSquare / 8 == 7)
        {
            Board.Square[endSquare] = 16;
            promotion = true;
            //need fix (promotion select)
        }
        else if (Board.Square[startSquare] == 22 && endSquare / 8 == 0)
        {
            Board.Square[endSquare] = 26;
            promotion = true;
            //need fix (promotion select)
        }
        else
        {
            if ((Board.Square[startSquare] == 12 && endSquare - startSquare == 16) || (Board.Square[startSquare] == 22 && startSquare - endSquare == 16))
            {
                Board.EPablePawn = endSquare;
            }
            else
            {
                Board.EPablePawn = 99;
            }

            if (Board.Square[startSquare] == 12 && (endSquare - startSquare == 7 || endSquare - startSquare == 9) && Board.Square[endSquare] == 0)
            {
                Board.Square[endSquare - 8] = 0;
                takes = true;
            }
            if (Board.Square[startSquare] == 22 && (startSquare - endSquare == 7 || startSquare - endSquare == 9) && Board.Square[endSquare] == 0)
            {
                Board.Square[endSquare + 8] = 0;
                takes = true;
            }  

            Board.Square[endSquare] = Board.Square[startSquare];
        }
        Board.Square[startSquare] = 0;

        List<int> myAttackingSquares = (Board.whiteToMove) ? CheckAttackingSquaresW() : CheckAttackingSquaresB();
        int check = 0;

        foreach (int i in myAttackingSquares)
        {
            Debug.Log(i);
            if (Board.whiteToMove ? Board.Square[i] == 21 : Board.Square[i] == 11)
            {
                check++;
            }
        }

        if (check != 0)
        {
            if (Board.whiteToMove)
            {
                Board.whiteChecksBlack = true;
                Board.blackChecksWhite = false;
            }
            else
            {
                Board.whiteChecksBlack = false;
                Board.blackChecksWhite = true;
            }
        }
        else
        {
            Board.whiteChecksBlack = false;
            Board.blackChecksWhite = false;
        }
            


        if (Board.whiteToMove)
        {
            if(takes)
                if(Board.Square[endSquare] == 12)
                    Board.movePGN += Board.moveCount.ToString() + ". " + Piece.pieceName[Board.Square[endSquare] % 10] + BoardRepresentation.FullNumToIndex(startSquare)[0] + "x" + BoardRepresentation.FullNumToIndex(endSquare);
                else
                    Board.movePGN += Board.moveCount.ToString() + ". " + Piece.pieceName[Board.Square[endSquare] % 10] + "x" + BoardRepresentation.FullNumToIndex(endSquare);
            else
                Board.movePGN += Board.moveCount.ToString() + ". " + Piece.pieceName[Board.Square[endSquare]%10] + BoardRepresentation.FullNumToIndex(endSquare);
        }
        else
        {
            if (takes)
                if(Board.Square[endSquare] == 22)
                    Board.movePGN += Piece.pieceName[Board.Square[endSquare] % 10] + BoardRepresentation.FullNumToIndex(startSquare)[0] + "x" + BoardRepresentation.FullNumToIndex(endSquare);
                else
                    Board.movePGN += Piece.pieceName[Board.Square[endSquare] % 10] + "x" + BoardRepresentation.FullNumToIndex(endSquare);
            else
                Board.movePGN += Piece.pieceName[Board.Square[endSquare] % 10] + BoardRepresentation.FullNumToIndex(endSquare);
            Board.moveCount++;
        }
        if (promotion)
        {
            Board.movePGN += "=" + Piece.pieceName[Board.Square[endSquare] % 10];
        }
        else
        {
            Board.movePGN += " ";
        }
        Board.whiteToMove = !Board.whiteToMove;
        Debug.Log(Board.movePGN);
    }

    public static void Castle(int whichCastle)
    {
        if(whichCastle == 100)
        {
            Board.Square[7] = 0;
            Board.Square[6] = 11;
            Board.Square[5] = 15;
            Board.Square[4] = 0;
            Board.whiteKCalste = false;
            Board.whiteQCaslte = false;
            Board.movePGN += Board.moveCount.ToString() + ". O-O ";
        }
        else if(whichCastle == 101)
        {
            Board.Square[0] = 0;
            Board.Square[2] = 11;
            Board.Square[3] = 15;
            Board.Square[4] = 0;
            Board.whiteKCalste = false;
            Board.whiteQCaslte = false;
            Board.movePGN += Board.moveCount.ToString() + ". O-O-O ";
        }
        else if(whichCastle == 102)
        {
            Board.Square[63] = 0;
            Board.Square[62] = 21;
            Board.Square[61] = 25;
            Board.Square[60] = 0;
            Board.blackKCastle = false;
            Board.blackQCastle = false;
            Board.movePGN += "O-O ";
            Board.moveCount++;
        }
        else if(whichCastle == 103)
        {
            Board.Square[56] = 0;
            Board.Square[58] = 21;
            Board.Square[59] = 25;
            Board.Square[60] = 0;
            Board.blackKCastle = false;
            Board.blackQCastle = false;
            Board.movePGN += "O-O-O ";
            Board.moveCount++;
        }
        Board.whiteToMove = !Board.whiteToMove;
    }

    public static List<int> CheckLegalSquare(int startSquare)
    {
        int startFile = (startSquare % 8) +1;
        int startRank = (startSquare / 8) +1;
        List<int> legalSquares = new List<int>();
        List<int> enemyAttackingSquares = (Board.whiteToMove)? CheckAttackingSquaresB() : CheckAttackingSquaresW();
        //White Pawn
        if(Board.Square[startSquare] == 12)
        {
            if (startRank == 2)
            {
                if (Board.Square[startSquare + 8] == 0 && Board.Square[startSquare + 16] == 0)
                {
                    legalSquares.Add(startSquare + 16);
                }
            }
            if (Board.Square[startSquare + 8] == 0)
            {
                legalSquares.Add(startSquare + 8);
            }
            if(startFile!=1 && Board.Square[startSquare+7] / 10 == 2)
            {
                legalSquares.Add(startSquare + 7);
            }
            if (startFile != 8 && Board.Square[startSquare + 9] / 10 == 2)
            {
                legalSquares.Add(startSquare + 9);
            }
            if (startSquare == Board.EPablePawn + 1 && startRank == 5)
            {
                legalSquares.Add(startSquare + 7);
            }
            if (startSquare == Board.EPablePawn - 1 && startRank == 5)
            {
                legalSquares.Add(startSquare + 9);
            }
        }
        //Black Pawn
        if(Board.Square[startSquare] == 22)
        {
            if (startRank == 7)
            {
                if (Board.Square[startSquare - 8] == 0 && Board.Square[startSquare - 16] == 0)
                {
                    legalSquares.Add(startSquare - 16);
                }
            }
            if (Board.Square[startSquare - 8] == 0)
            {
                legalSquares.Add(startSquare - 8);
            }
            if (startFile != 1 && Board.Square[startSquare - 9] / 10 == 1)
            {
                legalSquares.Add(startSquare - 9);
            }
            if (startFile != 8 && Board.Square[startSquare - 7] / 10 == 1)
            {
                legalSquares.Add(startSquare - 7);
            }
            if (startSquare == Board.EPablePawn + 1 && startRank == 4)
            {
                legalSquares.Add(startSquare - 9);
            }
            if (startSquare == Board.EPablePawn - 1 && startRank == 4)
            {
                legalSquares.Add(startSquare - 7);
            }
        }
        //Knight
        if(Board.Square[startSquare] % 10 == 3)
        {
            if(startFile > 1 && startRank < 7 && Board.Square[startSquare + 15] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare + 15);
            if (startFile < 8 && startRank < 7 && Board.Square[startSquare + 17] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare + 17);
            if (startFile < 7 && startRank < 8 && Board.Square[startSquare + 10] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare + 10);
            if (startFile < 7 && startRank > 1 && Board.Square[startSquare - 6] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare - 6);
            if (startFile < 8 && startRank > 2 && Board.Square[startSquare - 15] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare - 15);
            if (startFile > 1 && startRank > 2 && Board.Square[startSquare - 17] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare - 17);
            if (startFile > 2 && startRank > 1 && Board.Square[startSquare - 10] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare - 10);
            if (startFile > 2 && startRank < 8 && Board.Square[startSquare + 6] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare + 6);
        }
        //King
        if (Board.Square[startSquare] % 10 == 1)
        {
            if(Board.Square[startSquare] == 11 && Board.whiteKCalste && Board.Square[5] == 0 && Board.Square[6] == 0)
            {
                legalSquares.Add(100); //white kingside castle
            }
            if(Board.Square[startSquare] == 11 && Board.whiteQCaslte && Board.Square[1] == 0 && Board.Square[2] == 0 && Board.Square[3] == 0)
            {
                legalSquares.Add(101); //white queenside castle
            }
            if (Board.Square[startSquare] == 21 && Board.blackKCastle && Board.Square[61] == 0 && Board.Square[61] == 0)
            {
                legalSquares.Add(102); //black kingside castle
            }
            if (Board.Square[startSquare] == 21 && Board.blackQCastle && Board.Square[57] == 0 && Board.Square[58] == 0 && Board.Square[59] == 0)
            {
                legalSquares.Add(103); //black queenside castle
            }
            if (startFile > 1 && startRank < 8 && Board.Square[startSquare + 7] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare + 7);
            if (startFile < 8 && startRank < 8 && Board.Square[startSquare + 9] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare + 9);
            if (startFile < 8 && startRank > 1 && Board.Square[startSquare - 7] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare - 7);
            if (startFile > 1 && startRank > 1 && Board.Square[startSquare - 9] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare - 9);
            if (startFile > 1 && Board.Square[startSquare - 1] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare - 1);
            if (startRank < 8 && Board.Square[startSquare + 8] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare + 8);
            if (startFile < 8 && Board.Square[startSquare + 1] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare + 1);
            if (startRank > 1 && Board.Square[startSquare - 8] / 10 != Board.Square[startSquare] / 10)
                legalSquares.Add(startSquare - 8);
        }
        //Queen Rook
        if (Board.Square[startSquare] % 10 == 6 || Board.Square[startSquare] % 10 == 5)
        {
            for(int n = 1; n < 9 - startFile; n++)
            {
                if (Board.Square[startSquare + n] == 0)
                {
                    legalSquares.Add(startSquare + n);
                }
                else if (Board.Square[startSquare + n] / 10 != Board.Square[startSquare] / 10)
                {
                    legalSquares.Add(startSquare + n);
                    break;
                }
                else
                    break;
            }
            for (int n = -1; n > -startFile; n--)
            {
                if (Board.Square[startSquare + n] == 0)
                {
                    legalSquares.Add(startSquare + n);
                }
                else if (Board.Square[startSquare + n] / 10 != Board.Square[startSquare] / 10)
                {
                    legalSquares.Add(startSquare + n);
                    break;
                }
                else
                    break;
            }
            for (int n = 1; n < 9 - startRank; n++)
            {
                if (Board.Square[startSquare + n*8] == 0)
                {
                    legalSquares.Add(startSquare + n*8);
                }
                else if (Board.Square[startSquare + n*8] / 10 != Board.Square[startSquare] / 10)
                {
                    legalSquares.Add(startSquare + n*8);
                    break;
                }
                else
                    break;
            }
            for (int n = -1; n > -startRank; n--)
            {
                if (Board.Square[startSquare + n*8] == 0)
                {
                    legalSquares.Add(startSquare + n*8);
                }
                else if (Board.Square[startSquare + n*8] / 10 != Board.Square[startSquare] / 10)
                {
                    legalSquares.Add(startSquare + n*8);
                    break;
                }
                else
                    break;
            }
        }
        //Queen Bishop
        if (Board.Square[startSquare] % 10 == 6 || Board.Square[startSquare] % 10 == 4)
        {
            int rightUpDir = (9 - startFile) < (9 - startRank)? (9 - startFile) : (9 - startRank);
            int rightDownDir = (9 - startFile) < startRank ? (9 - startFile) : startRank;
            int leftDownDir = startFile < startRank ? startFile : startRank;
            int leftUpDir = startFile < (9 - startRank) ? startFile : (9 - startRank);
            for (int n = 1; n < rightUpDir; n++)
            {
                if (Board.Square[startSquare + n * 9] == 0)
                {
                    legalSquares.Add(startSquare + n * 9);
                }
                else if (Board.Square[startSquare + n * 9] / 10 != Board.Square[startSquare] / 10)
                {
                    legalSquares.Add(startSquare + n * 9);
                    break;
                }
                else
                    break;
            }
            for (int n = 1; n < rightDownDir; n++)
            {
                if (Board.Square[startSquare + n * (-7)] == 0)
                {
                    legalSquares.Add(startSquare + n * (-7));
                }
                else if (Board.Square[startSquare + n * (-7)] / 10 != Board.Square[startSquare] / 10)
                {
                    legalSquares.Add(startSquare + n * (-7));
                    break;
                }
                else
                    break;
            }
            for (int n = 1; n < leftDownDir; n++)
            {
                if (Board.Square[startSquare + n * (-9)] == 0)
                {
                    legalSquares.Add(startSquare + n * (-9));
                }
                else if (Board.Square[startSquare + n * (-9)] / 10 != Board.Square[startSquare] / 10)
                {
                    legalSquares.Add(startSquare + n * (-9));
                    break;
                }
                else
                    break;
            }
            for (int n = 1; n < leftUpDir; n++)
            {
                if (Board.Square[startSquare + n * 7] == 0)
                {
                    legalSquares.Add(startSquare + n * 7);
                }
                else if (Board.Square[startSquare + n * 7] / 10 != Board.Square[startSquare] / 10)
                {
                    legalSquares.Add(startSquare + n * 7);
                    break;
                }
                else
                    break;
            }
        }

        if(Board.whiteChecksBlack)
        {

        }
        return legalSquares;
    }

    public static List<int> CheckAttackingSquaresW()
    {
        List<int> attackingSquares = new List<int>();
        for (int n=0; n<Board.whitePiecePosition.Count; n++)
        {
            int file = (Board.whitePiecePosition[n] % 8) + 1;
            int rank = (Board.whitePiecePosition[n] / 8) + 1;
            if (Board.Square[Board.whitePiecePosition[n]] == 12)
            {
                if (file != 1)
                {
                    attackingSquares.Add(Board.whitePiecePosition[n] + 7);
                }
                if (file != 8)
                {
                    attackingSquares.Add(Board.whitePiecePosition[n] + 9);
                }
            }
            if (Board.Square[Board.whitePiecePosition[n]] == 13)
            {
                if (file > 1 && rank < 7)
                    attackingSquares.Add(Board.whitePiecePosition[n] + 15);
                if (file < 8 && rank < 7)
                    attackingSquares.Add(Board.whitePiecePosition[n] + 17);
                if (file < 7 && rank < 8)
                    attackingSquares.Add(Board.whitePiecePosition[n] + 10);
                if (file < 7 && rank > 1)
                    attackingSquares.Add(Board.whitePiecePosition[n] - 6);
                if (file < 8 && rank > 2)
                    attackingSquares.Add(Board.whitePiecePosition[n] - 15);
                if (file > 1 && rank > 2)
                    attackingSquares.Add(Board.whitePiecePosition[n] - 17);
                if (file > 2 && rank > 1)
                    attackingSquares.Add(Board.whitePiecePosition[n] - 10);
                if (file > 2 && rank < 8)
                    attackingSquares.Add(Board.whitePiecePosition[n] + 6);
            }
            if (Board.Square[Board.whitePiecePosition[n]] == 11)
            {
                if (file > 1 && rank < 8)
                    attackingSquares.Add(Board.whitePiecePosition[n] + 7);
                if (file < 8 && rank < 8)
                    attackingSquares.Add(Board.whitePiecePosition[n] + 9);
                if (file < 8 && rank > 1)
                    attackingSquares.Add(Board.whitePiecePosition[n] - 7);
                if (file > 1 && rank > 1)
                    attackingSquares.Add(Board.whitePiecePosition[n] - 9);
                if (file > 1)
                    attackingSquares.Add(Board.whitePiecePosition[n] - 1);
                if (rank < 8)
                    attackingSquares.Add(Board.whitePiecePosition[n] + 8);
                if (file < 8)
                    attackingSquares.Add(Board.whitePiecePosition[n] + 1);
                if (rank > 1)
                    attackingSquares.Add(Board.whitePiecePosition[n] - 8);
            }
            if (Board.Square[Board.whitePiecePosition[n]] == 16 || Board.Square[Board.whitePiecePosition[n]] == 15)
            {
                for (int i = 1; i < 9 - file; i++)
                {
                    attackingSquares.Add(Board.whitePiecePosition[n] + i);
                    if (Board.Square[Board.whitePiecePosition[n] + i] != 0)
                        break;
                }
                for (int i = -1; i > -file; i--)
                {
                    attackingSquares.Add(Board.whitePiecePosition[n] + i);
                    if (Board.Square[Board.whitePiecePosition[n] + i] != 0)
                        break;
                }
                for (int i = 1; i < 9 - rank; i++)
                {
                    attackingSquares.Add(Board.whitePiecePosition[n] + i*8);
                    if (Board.Square[Board.whitePiecePosition[n] + i*8] != 0)
                        break;
                }
                for (int i = -1; i > -rank; i--)
                {
                    attackingSquares.Add(Board.whitePiecePosition[n] + i*8);
                    if (Board.Square[Board.whitePiecePosition[n] + i*8] != 0)
                        break;
                }
            }
            if (Board.Square[Board.whitePiecePosition[n]] == 16 || Board.Square[Board.whitePiecePosition[n]] == 14)
            {
                int rightUpDir = (9 - file) < (9 - rank) ? (9 - file) : (9 - rank);
                int rightDownDir = (9 - file) < rank ? (9 - file) : rank;
                int leftDownDir = file < rank ? file : rank;
                int leftUpDir = file < (9 - rank) ? file : (9 - rank);
                for (int i = 1; i < rightUpDir; i++)
                {
                    attackingSquares.Add(Board.whitePiecePosition[n] + i * 9);
                    if (Board.Square[Board.whitePiecePosition[n] + i * 9] != 0)
                        break;
                }
                for (int i = 1; i < rightDownDir; i++)
                {
                    attackingSquares.Add(Board.whitePiecePosition[n] + i * (-7));
                    if (Board.Square[Board.whitePiecePosition[n] + i * (-7)] != 0)
                        break;
                }
                for (int i = 1; i < leftDownDir; i++)
                {
                    attackingSquares.Add(Board.whitePiecePosition[n] + i * (-9));
                    if (Board.Square[Board.whitePiecePosition[n] + i * (-9)] != 0)
                        break;
                }
                for (int i = 1; i < leftUpDir; i++)
                {
                    attackingSquares.Add(Board.whitePiecePosition[n] + i * 7);
                    if (Board.Square[Board.whitePiecePosition[n] + i * 7] != 0)
                        break;
                }
            }
        }
        return attackingSquares;
    }

    public static List<int> CheckAttackingSquaresB()
    {
        List<int> attackingSquares = new List<int>();
        for (int n = 0; n < Board.blackPiecePosition.Count; n++)
        {
            int file = (Board.blackPiecePosition[n] % 8) + 1;
            int rank = (Board.blackPiecePosition[n] / 8) + 1;
            if (Board.Square[Board.blackPiecePosition[n]] == 22)
            {
                if (file != 1)
                {
                    attackingSquares.Add(Board.blackPiecePosition[n] - 9);
                }
                if (file != 8)
                {
                    attackingSquares.Add(Board.blackPiecePosition[n] - 7);
                }
            }
            if (Board.Square[Board.blackPiecePosition[n]] == 23)
            {
                if (file > 1 && rank < 7)
                    attackingSquares.Add(Board.blackPiecePosition[n] + 15);
                if (file < 8 && rank < 7)
                    attackingSquares.Add(Board.blackPiecePosition[n] + 17);
                if (file < 7 && rank < 8)
                    attackingSquares.Add(Board.blackPiecePosition[n] + 10);
                if (file < 7 && rank > 1)
                    attackingSquares.Add(Board.blackPiecePosition[n] - 6);
                if (file < 8 && rank > 2)
                    attackingSquares.Add(Board.blackPiecePosition[n] - 15);
                if (file > 1 && rank > 2)
                    attackingSquares.Add(Board.blackPiecePosition[n] - 17);
                if (file > 2 && rank > 1)
                    attackingSquares.Add(Board.blackPiecePosition[n] - 10);
                if (file > 2 && rank < 8)
                    attackingSquares.Add(Board.blackPiecePosition[n] + 6);
            }
            if (Board.Square[Board.blackPiecePosition[n]] == 21)
            {
                if (file > 1 && rank < 8)
                    attackingSquares.Add(Board.blackPiecePosition[n] + 7);
                if (file < 8 && rank < 8)
                    attackingSquares.Add(Board.blackPiecePosition[n] + 9);
                if (file < 8 && rank > 1)
                    attackingSquares.Add(Board.blackPiecePosition[n] - 7);
                if (file > 1 && rank > 1)
                    attackingSquares.Add(Board.blackPiecePosition[n] - 9);
                if (file > 1)
                    attackingSquares.Add(Board.blackPiecePosition[n] - 1);
                if (rank < 8)
                    attackingSquares.Add(Board.blackPiecePosition[n] + 8);
                if (file < 8)
                    attackingSquares.Add(Board.blackPiecePosition[n] + 1);
                if (rank > 1)
                    attackingSquares.Add(Board.blackPiecePosition[n] - 8);
            }
            if (Board.Square[Board.blackPiecePosition[n]] == 26 || Board.Square[Board.blackPiecePosition[n]] == 25)
            {
                for (int i = 1; i < 9 - file; i++)
                {
                    attackingSquares.Add(Board.blackPiecePosition[n] + i);
                    if (Board.Square[Board.blackPiecePosition[n] + i] != 0)
                        break;
                }
                for (int i = -1; i > -file; i--)
                {
                    attackingSquares.Add(Board.blackPiecePosition[n] + i);
                    if (Board.Square[Board.blackPiecePosition[n] + i] != 0)
                        break;
                }
                for (int i = 1; i < 9 - rank; i++)
                {
                    attackingSquares.Add(Board.blackPiecePosition[n] + i * 8);
                    if (Board.Square[Board.blackPiecePosition[n] + i * 8] != 0)
                        break;
                }
                for (int i = -1; i > -rank; i--)
                {
                    attackingSquares.Add(Board.blackPiecePosition[n] + i * 8);
                    if (Board.Square[Board.blackPiecePosition[n] + i * 8] != 0)
                        break;
                }
            }
            if (Board.Square[Board.blackPiecePosition[n]] == 26 || Board.Square[Board.blackPiecePosition[n]] == 24)
            {
                int rightUpDir = (9 - file) < (9 - rank) ? (9 - file) : (9 - rank);
                int rightDownDir = (9 - file) < rank ? (9 - file) : rank;
                int leftDownDir = file < rank ? file : rank;
                int leftUpDir = file < (9 - rank) ? file : (9 - rank);
                for (int i = 1; i < rightUpDir; i++)
                {
                    attackingSquares.Add(Board.blackPiecePosition[n] + i * 9);
                    if (Board.Square[Board.blackPiecePosition[n] + i * 9] != 0)
                        break;
                }
                for (int i = 1; i < rightDownDir; i++)
                {
                    attackingSquares.Add(Board.blackPiecePosition[n] + i * (-7));
                    if (Board.Square[Board.blackPiecePosition[n] + i * (-7)] != 0)
                        break;
                }
                for (int i = 1; i < leftDownDir; i++)
                {
                    attackingSquares.Add(Board.blackPiecePosition[n] + i * (-9));
                    if (Board.Square[Board.blackPiecePosition[n] + i * (-9)] != 0)
                        break;
                }
                for (int i = 1; i < leftUpDir; i++)
                {
                    attackingSquares.Add(Board.blackPiecePosition[n] + i * 7);
                    if (Board.Square[Board.blackPiecePosition[n] + i * 7] != 0)
                        break;
                }
            }
        }
        return attackingSquares;
    }
}
