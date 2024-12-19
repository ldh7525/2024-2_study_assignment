using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Knight.cs
public class Knight : Piece
{
    public override MoveInfo[] GetMoves()
    {
        return new MoveInfo[]
        {
        new MoveInfo(2, 1, 1),   // ©Л╩С
        new MoveInfo(2, -1, 1),  // ©Лго
        new MoveInfo(-2, 1, 1),  // аб╩С
        new MoveInfo(-2, -1, 1), // абго
        new MoveInfo(1, 2, 1),   // ╩С©Л
        new MoveInfo(-1, 2, 1),  // ╩Саб
        new MoveInfo(1, -2, 1),  // го©Л
        new MoveInfo(-1, -2, 1)  // гоаб
        };
    }

}