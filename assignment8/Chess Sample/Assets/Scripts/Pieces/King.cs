using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// King.cs
public class King : Piece
{
    public override MoveInfo[] GetMoves()
    {
        List<MoveInfo> moves = new List<MoveInfo>();

        moves.Add(new MoveInfo(1, 0, 1));  // 오른쪽
        moves.Add(new MoveInfo(-1, 0, 1)); // 왼쪽
        moves.Add(new MoveInfo(0, 1, 1)); // 위쪽
        moves.Add(new MoveInfo(0, -1, 1));// 아래쪽

        moves.Add(new MoveInfo(1, 1, 1));  // 우상
        moves.Add(new MoveInfo(-1, 1, 1)); // 좌상
        moves.Add(new MoveInfo(1, -1, 1)); // 우하
        moves.Add(new MoveInfo(-1, -1, 1));// 좌하

        return moves.ToArray();
    }
}
