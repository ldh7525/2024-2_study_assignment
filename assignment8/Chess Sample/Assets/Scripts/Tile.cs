using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public (int, int) MyPos;    // Tile의 좌표
    Color tileColor = new Color(255 / 255f, 193 / 255f, 204 / 255f);    // 색깔
    SpriteRenderer MySpriteRenderer;

    private void Awake()
    {
        MySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 타일을 처음에 배치하는 함수
    public void Set((int, int) targetPos)
    {
        // MyPos를 targetPos로 지정
        MyPos = targetPos;

        // 타일 위치를 targetPos의 실제 좌표로 이동
        transform.position = Utils.ToRealPos(targetPos);

        // 배치 규칙에 따라 색깔 지정 (체스판 패턴)
        if ((targetPos.Item1 + targetPos.Item2) % 2 != 0)
        {
            // 밝은 색
            MySpriteRenderer.color = Color.white; // 기본 설정된 밝은 색
        }
        else
        {
            // 어두운 색 (분홍)
            MySpriteRenderer.color = new Color(255 / 255f, 193 / 255f, 206 / 255f, 255 / 255f);
        }
    }

}
