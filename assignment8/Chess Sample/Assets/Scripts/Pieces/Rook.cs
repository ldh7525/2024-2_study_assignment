using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rook.cs
public class Rook : Piece
{
    public override MoveInfo[] GetMoves()
    {
        return new MoveInfo[]
        {
        new MoveInfo(1, 0, Utils.FieldWidth),   
        new MoveInfo(-1, 0, Utils.FieldWidth), 
        new MoveInfo(0, 1, Utils.FieldWidth),  
        new MoveInfo(0, -1, Utils.FieldWidth)  
        };
    }
}
