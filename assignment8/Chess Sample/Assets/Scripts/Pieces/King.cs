using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// King.cs
public class King : Piece
{
    public override MoveInfo[] GetMoves()
    {
        List<MoveInfo> moves = new List<MoveInfo>();

        moves.Add(new MoveInfo(1, 0, 1));  // ������
        moves.Add(new MoveInfo(-1, 0, 1)); // ����
        moves.Add(new MoveInfo(0, 1, 1)); // ����
        moves.Add(new MoveInfo(0, -1, 1));// �Ʒ���

        moves.Add(new MoveInfo(1, 1, 1));  // ���
        moves.Add(new MoveInfo(-1, 1, 1)); // �»�
        moves.Add(new MoveInfo(1, -1, 1)); // ����
        moves.Add(new MoveInfo(-1, -1, 1));// ����

        return moves.ToArray();
    }
}
