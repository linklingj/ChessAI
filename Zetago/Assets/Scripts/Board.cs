using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class Piece
{
    public const int None = 0;
    public const int King = 1;
    public const int Pawn = 2;
    public const int Knight = 3;
    public const int Bishop = 4;
    public const int Rook = 5;
    public const int Queen = 6;

    public const int White = 10;
    public const int Black = 20;

    public static string[] pieceName = new string[] { "", "K", "", "N", "B", "R", "Q" };
}

public static class Board
{
    public const string startFEN = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1";
    public static int[] Square = new int[64];
    public static List<int> whitePiecePosition = new List<int>();
    public static List<int> blackPiecePosition = new List<int>();

    public static string movePGN = "";

    public static bool isPlayerWhite;

    public static bool whiteToMove;

    public static bool whiteKCalste;
    public static bool whiteQCaslte;
    public static bool blackKCastle;
    public static bool blackQCastle;

    public static bool whiteChecksBlack;
    public static bool blackChecksWhite;

    public static int EPablePawn = 99;

    public static int moveCount;

    public static bool whiteIsBottom;

    public static void loadFromFen(string fen)
    {
        var pieceTypeFromSymbol = new Dictionary<char, int>()
        {
            ['k'] = Piece.King, ['q'] = Piece.Queen, ['n'] = Piece.Knight, ['b'] = Piece.Bishop, ['r'] = Piece.Rook, ['p'] = Piece.Pawn
        };

        string fenBoard = fen.Split(' ')[0];
        string fenTurn = fen.Split(' ')[1];
        string castlingAbility = fen.Split(' ')[2];

        int file = 0;
        int rank = 7;

        foreach (char symbol in fenBoard)
        {
            if(symbol == '/')
            {
                file = 0;
                rank--;
            }
            else
            {
                if(char.IsDigit(symbol))
                {
                    file += (int)char.GetNumericValue(symbol);
                }
                else
                {
                    int pieceColour = char.IsUpper(symbol) ? Piece.White : Piece.Black;
                    int pieceType = pieceTypeFromSymbol[char.ToLower(symbol)];
                    Square[rank * 8 + file] = pieceType + pieceColour;
                    file++;
                }
            }
        }

        if (fenTurn == "w")
            whiteToMove = true;
        else
            whiteToMove = false;

        if (castlingAbility[0] == '-')
            whiteKCalste = whiteQCaslte = blackKCastle = blackQCastle = false;
        else
        {
            whiteKCalste = false;
            whiteQCaslte = false;
            blackKCastle = false;
            blackQCastle = false;
            for (int i = 0; i< castlingAbility.Length; i++)
            {
                if (castlingAbility[i] == 'K')
                    whiteKCalste = true;
                if (castlingAbility[i] == 'Q')
                    whiteQCaslte = true;
                if (castlingAbility[i] == 'k')
                    blackKCastle = true;
                if (castlingAbility[i] == 'q')
                    blackQCastle = true;
            }
        }
    }

    public static void squareToFen(int[] square)
    {
        string FEN = "";
        int emptyCount = 0;
        bool isWhitePiece = true;
        int squareNum = 0;
        char pieceChar = 'P';
        for (int rank = 0; rank<8; rank++)
        {
            for(int file = 0; file<8; file++)
            {
                squareNum = (7 - rank) * 8 + file;
                if (square[squareNum] == 0)
                {
                    emptyCount++;
                }
                else
                {
                    if (emptyCount != 0)
                    {
                        FEN += emptyCount.ToString();
                        emptyCount = 0;
                    }

                    if (Mathf.FloorToInt(square[squareNum] / 10) == 1)
                        isWhitePiece = true;
                    else
                        isWhitePiece = false;

                    switch (square[squareNum] % 10)
                    {
                        case 1:
                            pieceChar = 'K';
                            break;
                        case 2:
                            pieceChar = 'P';
                            break;
                        case 3:
                            pieceChar = 'N';
                            break;
                        case 4:
                            pieceChar = 'B';
                            break;
                        case 5:
                            pieceChar = 'R';
                            break;
                        case 6:
                            pieceChar = 'Q';
                            break;
                    }
                    FEN += isWhitePiece ? pieceChar.ToString() : pieceChar.ToString().ToLower();
                }
            }
            if (emptyCount != 0)
            {
                FEN += emptyCount.ToString();
                emptyCount = 0;
            }
            FEN += "/";
        }
        if (emptyCount != 0)
        {
            FEN += emptyCount.ToString();
            emptyCount = 0;
        }
        //need fix
        FEN += " w ";
        if (Board.whiteKCalste)
            FEN += "K";
        if (Board.whiteQCaslte)
            FEN += "Q";
        if (Board.blackKCastle)
            FEN += "k";
        if (Board.blackQCastle)
            FEN += "q";
        FEN += " - 0 1";
    }
}

