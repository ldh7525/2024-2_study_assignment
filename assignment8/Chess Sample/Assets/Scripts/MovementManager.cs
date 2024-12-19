using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject effectPrefab;
    private Transform effectParent;
    private List<GameObject> currentEffects = new List<GameObject>();   // 현재 effect들을 저장할 리스트
    
    public void Initialize(GameManager gameManager, GameObject effectPrefab, Transform effectParent)
    {
        this.gameManager = gameManager;
        this.effectPrefab = effectPrefab;
        this.effectParent = effectParent;
    }

    private bool TryMove(Piece piece, (int, int) targetPos, MoveInfo moveInfo)
    {
        // 이동 방향 및 거리 초기화
        int dx = moveInfo.dirX;
        int dy = moveInfo.dirY;
        int maxDistance = moveInfo.distance;

        // 현재 위치
        var currentPos = piece.MyPos;

        for (int i = 1; i <= maxDistance; i++) //나이트가 아닐때만 작동하도록해야함.------------------------------------------------------------------------------------------------------------------------
        {
            // 다음 위치 계산
            var nextPos = (currentPos.Item1 + dx * i, currentPos.Item2 + dy * i);

            // 보드 안에 있는지 확인
            if (!Utils.IsInBoard(nextPos)) return false;

            // 목표 위치에 다른 말이 있는지 확인
            var targetPiece = gameManager.Pieces[nextPos.Item1, nextPos.Item2];
            if (targetPiece != null)
            {
                // 같은 팀 말이면 이동 불가
                if (targetPiece.PlayerDirection == piece.PlayerDirection) return false;

                // 다른 팀 말이면 현재 위치가 목표 위치인지 확인
                return nextPos == targetPos;
            }

            // 목표 위치와 일치하면 이동 가능
            if (nextPos == targetPos) return true;
        }

        return false;
    }


    // 체크를 제외한 상황에서 가능한 움직임인지를 검증
    private bool IsValidMoveWithoutCheck(Piece piece, (int, int) targetPos)
    {
        if (!Utils.IsInBoard(targetPos) || targetPos == piece.MyPos) return false;

        foreach (var moveInfo in piece.GetMoves())
        {
            if (TryMove(piece, targetPos, moveInfo))
                return true;
        }
        
        return false;
    }

    // 체크를 포함한 상황에서 가능한 움직임인지를 검증
    public bool IsValidMove(Piece piece, (int, int) targetPos)
    {
        if (!IsValidMoveWithoutCheck(piece, targetPos)) return false;

        // 체크 상태 검증을 위한 임시 이동
        var originalPiece = gameManager.Pieces[targetPos.Item1, targetPos.Item2];
        var originalPos = piece.MyPos;

        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = piece;
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = null;
        piece.MyPos = targetPos;

        bool isValid = !IsInCheck(piece.PlayerDirection);

        // 원상 복구
        gameManager.Pieces[originalPos.Item1, originalPos.Item2] = piece;
        gameManager.Pieces[targetPos.Item1, targetPos.Item2] = originalPiece;
        piece.MyPos = originalPos;

        return isValid;
    }

    // 체크인지를 확인
    private bool IsInCheck(int playerDirection)
    {
        (int, int) kingPos = (-1, -1); 
        
        for (int i = 0; i < Utils.FieldWidth; i++)
        {
            for (int j = 0; j < Utils.FieldHeight; j++)
            {
                var piece = gameManager.Pieces[i, j];
                if (piece is King && piece.PlayerDirection == playerDirection)
                {
                    kingPos = (i, j);
                    break; 
                }
            }
            if (kingPos.Item1 != -1 && kingPos.Item2 != -1) break;
        }

        // 왕이 지금 체크 상태인지를 리턴
        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                var piece = gameManager.Pieces[x, y];

                // 상대편 말만 검사
                if (piece != null && piece.PlayerDirection != playerDirection)
                {
                    // 상대편 말이 왕의 위치로 이동 가능한지 확인
                    if (IsValidMoveWithoutCheck(piece, kingPos))
                    {
                        return true; // 체크 상태
                    }
                }
            }
        }

        return false; // 체크 상태가 아님
    }


    public void ShowPossibleMoves(Piece piece)
    {
        ClearEffects();

        for (int x = 0; x < Utils.FieldWidth; x++)
        {
            for (int y = 0; y < Utils.FieldHeight; y++)
            {
                (int, int) targetPos = (x, y);

                if (IsValidMove(piece, targetPos))
                {
                    GameObject effect = Instantiate(effectPrefab, effectParent);

                    effect.transform.position = Utils.ToRealPos(targetPos);

                    currentEffects.Add(effect);
                }
            }
        }
    }


    // 효과 비우기
    public void ClearEffects()
    {
        foreach (var effect in currentEffects)
        {
            if (effect != null) Destroy(effect);
        }
        currentEffects.Clear();
    }
}