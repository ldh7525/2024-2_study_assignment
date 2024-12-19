using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    public override MoveInfo[] GetMoves()
    {
        return new MoveInfo[]
        {
        new MoveInfo(1, 1, Utils.FieldWidth),   
        new MoveInfo(-1, 1, Utils.FieldWidth),  
        new MoveInfo(1, -1, Utils.FieldWidth),
        new MoveInfo(-1, -1, Utils.FieldWidth)  
        };
    }
}