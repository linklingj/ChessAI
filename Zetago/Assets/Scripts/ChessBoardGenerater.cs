using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardGenerater : MonoBehaviour {

	MeshRenderer[,] squareRenderers;

	public Material lightCol;
	public Material darkCol;

	public Shader squareShader;

	Camera cam;

	public Sprite KingW;
	public Sprite QueenW;
	public Sprite PawnW;
	public Sprite RookW;
	public Sprite KnightW;
	public Sprite BishopW;

	public Sprite KingB;
	public Sprite QueenB;
	public Sprite PawnB;
	public Sprite RookB;
	public Sprite KnightB;
	public Sprite BishopB;

	GameObject trackingPiece;
	bool isTrackingPieceWhite;

	int clickedSquare;

    private void Awake()
    {
		cam = Camera.main;
    }
    void Start ()
	{
		CreateBoard();
		UpdatePieces();
		UpdateAllPiecePosition();
	}

    private void Update()
    {
		Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
		if (trackingPiece!=null)
        {
			trackingPiece.transform.position = new Vector3(mousePos.x,mousePos.y, -5);
        }	
    }

    void CreateBoard()
    {
		squareRenderers = new MeshRenderer[8, 8];

		for (int file=0; file<8; file++)
        {
			for(int rank=0; rank<8; rank++)
            {
                bool isLightSquare = (file + rank) % 2 != 0;

				Transform square = GameObject.CreatePrimitive(PrimitiveType.Quad).transform;
				square.parent = transform;
				square.name = BoardRepresentation.NumToIndex(file+1, rank+1);
				square.position = new Vector3(file-3.5f, rank-3.5f);

				squareRenderers[file, rank] = square.gameObject.GetComponent<MeshRenderer>();
				squareRenderers[file, rank].material = isLightSquare? lightCol : darkCol;
				
			}
        }
    }

	void UpdatePieces()
    {
		for (int file = 0; file < 8; file++)
		{
			for (int rank = 0; rank < 8; rank++)
			{
				if (Board.Square[rank * 8 + file] % 10 != 0)
				{
					if(GameObject.Find(BoardRepresentation.NumToIndex(file + 1, rank + 1)).transform.childCount!=0)
						Destroy(GameObject.Find(BoardRepresentation.NumToIndex(file + 1, rank + 1)).transform.GetChild(0).gameObject);
					GameObject piece;
					piece = new GameObject("piece");
					SpriteRenderer sr = piece.AddComponent<SpriteRenderer>();
					piece.transform.position = GameObject.Find(BoardRepresentation.NumToIndex(file + 1, rank + 1)).transform.position;
					piece.transform.parent = GameObject.Find(BoardRepresentation.NumToIndex(file + 1, rank + 1)).transform;

					switch (Board.Square[rank * 8 + file])
					{
						case 0:
							Destroy(piece.gameObject);
							break;
						case 11:
							sr.sprite = KingW;
							piece.name = "KingW";
							break;
						case 12:
							sr.sprite = PawnW;
							piece.name = "PawnW";
							break;
						case 13:
							sr.sprite = KnightW;
							piece.name = "KnightW";
							break;
						case 14:
							sr.sprite = BishopW;
							piece.name = "BishopW";
							break;
						case 15:
							sr.sprite = RookW;
							piece.name = "RookW";
							break;
						case 16:
							sr.sprite = QueenW;
							piece.name = "QueenW";
							break;
						case 21:
							sr.sprite = KingB;
							piece.name = "KingB";
							break;
						case 22:
							sr.sprite = PawnB;
							piece.name = "PawnB";
							break;
						case 23:
							sr.sprite = KnightB;
							piece.name = "KnightB";
							break;
						case 24:
							sr.sprite = BishopB;
							piece.name = "BishopB";
							break;
						case 25:
							sr.sprite = RookB;
							piece.name = "RookB";
							break;
						case 26:
							sr.sprite = QueenB;
							piece.name = "QueenB";
							break;
					}
				}
			}
		}
	}

	void UpdateSqecificSquare(int square)
    {
		if (GameObject.Find(BoardRepresentation.FullNumToIndex(square)).transform.childCount != 0)
			Destroy(GameObject.Find(BoardRepresentation.FullNumToIndex(square)).transform.GetChild(0).gameObject);
		GameObject piece;
		piece = new GameObject("piece");
		SpriteRenderer sr = piece.AddComponent<SpriteRenderer>();
		piece.transform.position = GameObject.Find(BoardRepresentation.FullNumToIndex(square)).transform.position;
		piece.transform.parent = GameObject.Find(BoardRepresentation.FullNumToIndex(square)).transform;

		switch (Board.Square[square])
		{
			case 0:
				Destroy(piece.gameObject);
				break;
			case 11:
				sr.sprite = KingW;
				piece.name = "KingW";
				break;
			case 12:
				sr.sprite = PawnW;
				piece.name = "PawnW";
				break;
			case 13:
				sr.sprite = KnightW;
				piece.name = "KnightW";
				break;
			case 14:
				sr.sprite = BishopW;
				piece.name = "BishopW";
				break;
			case 15:
				sr.sprite = RookW;
				piece.name = "RookW";
				break;
			case 16:
				sr.sprite = QueenW;
				piece.name = "QueenW";
				break;
			case 21:
				sr.sprite = KingB;
				piece.name = "KingB";
				break;
			case 22:
				sr.sprite = PawnB;
				piece.name = "PawnB";
				break;
			case 23:
				sr.sprite = KnightB;
				piece.name = "KnightB";
				break;
			case 24:
				sr.sprite = BishopB;
				piece.name = "BishopB";
				break;
			case 25:
				sr.sprite = RookB;
				piece.name = "RookB";
				break;
			case 26:
				sr.sprite = QueenB;
				piece.name = "QueenB";
				break;
		}
	}

	void UpdateAllPiecePosition()
    {
		Board.whitePiecePosition.Clear();
		Board.blackPiecePosition.Clear();
		Board.whitePiecePosition.TrimExcess();
		Board.blackPiecePosition.TrimExcess();
		for (int n=0; n<64; n++)
        {
			if(Board.Square[n]/10 == 1)
            {
				Board.whitePiecePosition.Add(n);
            }
			else if(Board.Square[n]/10 == 2)
            {
				Board.blackPiecePosition.Add(n);
			}
        }
    }

	public void DragPiece(int targetSquare)
    {
		clickedSquare = targetSquare;
		if (Board.Square[targetSquare]!=0)
        {
			trackingPiece = GameObject.Find(BoardRepresentation.FullNumToIndex(targetSquare)).transform.GetChild(0).gameObject;
			if (Board.Square[BoardRepresentation.IndexToFullNum(trackingPiece.transform.parent.name)] / 10 == 1)
				isTrackingPieceWhite = true;
			else
				isTrackingPieceWhite = false;
        }
    }

	public void ReleasePiece(int targetSquare)
    {
		if(targetSquare != clickedSquare && targetSquare >= 0 && targetSquare <= 63 && trackingPiece != null && isTrackingPieceWhite == Board.whiteToMove)
        {
			int trackingPieceNum = BoardRepresentation.IndexToFullNum(trackingPiece.transform.parent.name);
			if (targetSquare == 6 && MoveCaculator.CheckLegalSquare(trackingPieceNum).Contains(100))
			{
				MoveCaculator.Castle(100);
				UpdateSqecificSquare(4);
				UpdateSqecificSquare(5);
				UpdateSqecificSquare(6);
				UpdateSqecificSquare(7);
			}
			else if (targetSquare == 2 && MoveCaculator.CheckLegalSquare(trackingPieceNum).Contains(101))
			{
				MoveCaculator.Castle(101);
				UpdateSqecificSquare(0);
				UpdateSqecificSquare(2);
				UpdateSqecificSquare(3);
				UpdateSqecificSquare(4);
			}
			else if (targetSquare == 62 && MoveCaculator.CheckLegalSquare(trackingPieceNum).Contains(102))
			{
				MoveCaculator.Castle(102);
				UpdateSqecificSquare(60);
				UpdateSqecificSquare(61);
				UpdateSqecificSquare(62);
				UpdateSqecificSquare(63);
			}
			else if (targetSquare == 58 && MoveCaculator.CheckLegalSquare(trackingPieceNum).Contains(103))
			{
				MoveCaculator.Castle(103);
				UpdateSqecificSquare(56);
				UpdateSqecificSquare(58);
				UpdateSqecificSquare(59);
				UpdateSqecificSquare(60);
			} 
			else if (MoveCaculator.CheckLegalSquare(trackingPieceNum).Contains(targetSquare))
            {
				//deprive castling right
				if (Board.Square[trackingPieceNum] == 11)
				{
					Board.whiteKCalste = false;
					Board.whiteQCaslte = false;
				}
				else if (Board.Square[trackingPieceNum] == 21)
				{
					Board.blackKCastle = false;
					Board.blackQCastle = false;
				}
				else if (Board.Square[trackingPieceNum] == 15)
				{
					if (trackingPieceNum == 0)
						Board.whiteQCaslte = false;
					else if (trackingPieceNum == 7)
						Board.whiteKCalste = false;
				}
				else if (Board.Square[trackingPieceNum] == 25)
				{
					if (trackingPieceNum == 56)
						Board.blackQCastle = false;
					else if (trackingPieceNum == 63)
						Board.blackKCastle = false;
				}

				if(Board.whiteToMove)
                {
					Board.whitePiecePosition.Remove(trackingPieceNum);
					Board.blackPiecePosition.Remove(targetSquare);
					Board.whitePiecePosition.Add(targetSquare);
				}
                else
                {
					Board.blackPiecePosition.Remove(trackingPieceNum);
					Board.whitePiecePosition.Remove(targetSquare);
					Board.blackPiecePosition.Add(targetSquare);
				}

				MoveCaculator.Move(trackingPieceNum, targetSquare);
				UpdateSqecificSquare(trackingPieceNum);
				UpdateSqecificSquare(targetSquare);
				
				if (Board.Square[targetSquare] == 12 && (targetSquare - trackingPieceNum == 7 || targetSquare - trackingPieceNum == 9) && trackingPieceNum / 8 == 4)
					UpdateSqecificSquare(targetSquare - 8);
				if (Board.Square[targetSquare] == 22 && (trackingPieceNum - targetSquare == 7 || trackingPieceNum - targetSquare == 9) && trackingPieceNum / 8 == 3)
					UpdateSqecificSquare(targetSquare + 8);
				Board.squareToFen(Board.Square);
				/*foreach(int i in Board.whitePiecePosition)
                {
					Debug.Log(i);
                }*/
			}
            else
            {
				UpdateSqecificSquare(clickedSquare);
				//illegal move
			}
		}
		else
        {
			UpdateSqecificSquare(clickedSquare);
        }
		if(trackingPiece != null)
			Destroy(trackingPiece);
	}
}
