using UnityEngine;
using System.Collections;

public static class DirectionRowHelper
{
    public static int GetRowFromDirection(Direction direction)
    {
        if(direction == Direction.Up) {
            return 1;
        }else if(direction == Direction.Down) {
            return 2;
        }else if(direction == Direction.Left) {
            return 0;
        }else if(direction == Direction.Right) {
            return 3;
        }

        return -1;
    }
}

