using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override MoveInfo[] GetMoves()
    {
        List<MoveInfo> moves = new List<MoveInfo>();

        // 수평과 수직 방향 (상, 하, 좌, 우)
        moves.Add(new MoveInfo(1, 0, Utils.FieldWidth));  // 오른쪽
        moves.Add(new MoveInfo(-1, 0, Utils.FieldWidth)); // 왼쪽
        moves.Add(new MoveInfo(0, 1, Utils.FieldHeight)); // 위쪽
        moves.Add(new MoveInfo(0, -1, Utils.FieldHeight));// 아래쪽

        // 대각선 방향 (좌상, 우상, 좌하, 우하)
        moves.Add(new MoveInfo(1, 1, Utils.FieldWidth));  // 우상
        moves.Add(new MoveInfo(-1, 1, Utils.FieldWidth)); // 좌상
        moves.Add(new MoveInfo(1, -1, Utils.FieldWidth)); // 우하
        moves.Add(new MoveInfo(-1, -1, Utils.FieldWidth));// 좌하

        return moves.ToArray();
    }
}