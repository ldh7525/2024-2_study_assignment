using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    public override MoveInfo[] GetMoves()
    {
        List<MoveInfo> moves = new List<MoveInfo>();

        // ����� ���� ���� (��, ��, ��, ��)
        moves.Add(new MoveInfo(1, 0, Utils.FieldWidth));  // ������
        moves.Add(new MoveInfo(-1, 0, Utils.FieldWidth)); // ����
        moves.Add(new MoveInfo(0, 1, Utils.FieldHeight)); // ����
        moves.Add(new MoveInfo(0, -1, Utils.FieldHeight));// �Ʒ���

        // �밢�� ���� (�»�, ���, ����, ����)
        moves.Add(new MoveInfo(1, 1, Utils.FieldWidth));  // ���
        moves.Add(new MoveInfo(-1, 1, Utils.FieldWidth)); // �»�
        moves.Add(new MoveInfo(1, -1, Utils.FieldWidth)); // ����
        moves.Add(new MoveInfo(-1, -1, Utils.FieldWidth));// ����

        return moves.ToArray();
    }
}