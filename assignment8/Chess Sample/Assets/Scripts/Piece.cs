using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public (int, int) MyPos;    // 자신의 좌표
    public int PlayerDirection = 1; // 자신의 방향 1 - 백, 2 - 흑
    
    public Sprite WhiteSprite;  // 백일 때의 sprite
    public Sprite BlackSprite;  // 흑일 때의 sprite
    
    protected GameManager MyGameManager;
    protected SpriteRenderer MySpriteRenderer;

    void Awake()
    {
        MyGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Piece의 초기 설정 함수
    public void initialize((int, int) targetPos, int direction)
    {
        PlayerDirection = direction;
        initSprite(PlayerDirection);
        MoveTo(targetPos);
    }

    // sprite 초기 설정 함수
    void initSprite(int direction)
    {
        // 방향에 따라 Sprite를 설정
        if (direction == 1)
        {
            // 백 플레이어 방향
            MySpriteRenderer.sprite = WhiteSprite;
        }
        else if (direction == -1)
        {
            // 흑 플레이어 방향
            MySpriteRenderer.sprite = BlackSprite;
        }

        // 방향에 따른 로컬 스케일 설정 (필요 시 상하 대칭)
        transform.localScale = new Vector3(1, direction, 1);
    }


    // piece의 실제 이동 함수
    public void MoveTo((int, int) targetPos)
    {
        // MyPos를 업데이트
        (int oldX, int oldY) = MyPos;
        MyPos = targetPos;

        // GameManager의 Pieces 배열 업데이트
        MyGameManager.Pieces[oldX, oldY] = null; // 기존 위치 비우기
        MyGameManager.Pieces[targetPos.Item1, targetPos.Item2] = this; // 새로운 위치에 현재 Piece 저장

        // 실제 월드 좌표로 이동
        transform.position = Utils.ToRealPos(targetPos);
    }


    public abstract MoveInfo[] GetMoves();
}
