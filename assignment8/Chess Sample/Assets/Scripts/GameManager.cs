using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 프리팹들
    public GameObject TilePrefab;
    public GameObject[] PiecePrefabs;   // King, Queen, Bishop, Knight, Rook, Pawn 순
    public GameObject EffectPrefab;

    // 오브젝트의 parent들
    private Transform TileParent;
    private Transform PieceParent;
    private Transform EffectParent;
    
    private MovementManager movementManager;
    private UIManager uiManager;
    
    public int CurrentTurn = 1; // 현재 턴 1 - 백, 2 - 흑
    public Tile[,] Tiles = new Tile[Utils.FieldWidth, Utils.FieldHeight];   // Tile들
    public Piece[,] Pieces = new Piece[Utils.FieldWidth, Utils.FieldHeight];    // Piece들

    void Awake()
    {
        TileParent = GameObject.Find("TileParent").transform;
        PieceParent = GameObject.Find("PieceParent").transform;
        EffectParent = GameObject.Find("EffectParent").transform;
        
        uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        movementManager = gameObject.AddComponent<MovementManager>();
        movementManager.Initialize(this, EffectPrefab, EffectParent);
        
        InitializeBoard();
    }

    void InitializeBoard()
    {
        // 8x8로 타일들을 배치
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                // TilePrefab을 TileParent의 자식으로 생성
                GameObject tileObj = Instantiate(TilePrefab, TileParent);

                // 타일 초기화
                Tile tile = tileObj.GetComponent<Tile>();
                tile.Set((x, y)); // 타일 위치 설정

                // Tiles 배열에 타일 저장
                Tiles[x, y] = tile; 

                // 타일을 실제 월드 좌표로 이동
                tileObj.transform.position = Utils.ToRealPos((x, y));
            }
        }

        // 흰색과 검은색 말 배치
        PlacePieces(1);  // 백
        PlacePieces(-1); // 흑
    }

    void PlacePieces(int direction)
    {
        // direction이 1이면 백, -1이면 흑
        int pawnRow = (direction == 1) ? 1 : 6;   // 폰의 배치 행
        int mainRow = (direction == 1) ? 0 : 7;   // 주요 말의 배치 행

        // 폰 배치
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            PlacePiece(5, (x, pawnRow), direction); // Pawn (PiecePrefabs 배열의 5번 인덱스)
        }

        // 주요 말 배치
        PlacePiece(4, (0, mainRow), direction); // Rook
        PlacePiece(4, (7, mainRow), direction); // Rook
        PlacePiece(3, (1, mainRow), direction); // Knight
        PlacePiece(3, (6, mainRow), direction); // Knight
        PlacePiece(2, (2, mainRow), direction); // Bishop
        PlacePiece(2, (5, mainRow), direction); // Bishop
        PlacePiece(1, (3, mainRow), direction); // Queen
        PlacePiece(0, (4, mainRow), direction); // King
    }

    Piece PlacePiece(int pieceType, (int, int) pos, int direction)
    {
        // PiecePrefabs의 원소를 사용하여 Piece 생성
        GameObject pieceObj = Instantiate(PiecePrefabs[pieceType], PieceParent);

        // 생성된 오브젝트에서 Piece 컴포넌트를 가져옴
        Piece piece = pieceObj.GetComponent<Piece>();

        // Piece 초기화
        piece.initialize(pos, direction);

        // Pieces 배열에 Piece 저장
        Pieces[pos.Item1, pos.Item2] = piece;

        // Piece를 반환
        return piece;
    }

    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        return movementManager.IsValidMove(piece, targetPos);
    }

    public void ShowPossibleMoves(Piece piece)
    {
        movementManager.ShowPossibleMoves(piece);
    }

    public void ClearEffects()
    {
        movementManager.ClearEffects();
    }


    public void Move(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMove(piece, targetPos)) return;

        // 기존 위치의 정보를 비움
        Pieces[piece.MyPos.Item1, piece.MyPos.Item2] = null;

        // 해당 위치에 다른 Piece가 있다면 삭제
        if (Pieces[targetPos.Item1, targetPos.Item2] != null)
        {
            Destroy(Pieces[targetPos.Item1, targetPos.Item2].gameObject);
        }

        // Piece를 이동
        piece.MoveTo(targetPos);

        // 새로운 위치에 저장
        Pieces[targetPos.Item1, targetPos.Item2] = piece;

        // 턴 변경
        ChangeTurn();
    }

    void ChangeTurn()
    {

        // 턴을 변경
        CurrentTurn = (CurrentTurn == 1) ? -1 : 1;

        // UIManager를 통해 턴을 업데이트
        uiManager.UpdateTurn(CurrentTurn);
    }

}
