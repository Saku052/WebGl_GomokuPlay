using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
}

public class CEnemyAI
{
    //同じ色が二つ並んでいるかどうかを判定する
    public static bool Is2SameColor(int x, int y, Map.map color)
    {
        //横方向の判定
        if (x < 3)
        {
            if (Map.GameMap[x + 1, y] == color && Map.GameMap[x + 2, y] == color)
            {
                return true;
            }
        }

        if (x > 1)
        {
            if (Map.GameMap[x - 1, y] == color && Map.GameMap[x - 2, y] == color)
            {
                return true;
            }
        }

        if (x > 0 && x < 4)
        {
            if (Map.GameMap[x - 1, y] == color && Map.GameMap[x + 1, y] == color)
            {
                return true;
            }
        }

        //縦方向の判定
        if (y < 3)
        {
            if (Map.GameMap[x, y + 1] == color && Map.GameMap[x, y + 2] == color)
            {
                return true;
            }
        }

        if (y > 1)
        {
            if (Map.GameMap[x, y - 1] == color && Map.GameMap[x, y - 2] == color)
            {
                return true;
            }
        }

        if (y > 0 && y < 4)
        {
            if (Map.GameMap[x, y - 1] == color && Map.GameMap[x, y + 1] == color)
            {
                return true;
            }
        }

        //斜め方向の判定
        if (x < 3 && y < 3)
        {
            if (Map.GameMap[x + 1, y + 1] == color && Map.GameMap[x + 2, y + 2] == color)
            {
                return true;
            }
        }

        if (x > 1 && y > 1)
        {
            if (Map.GameMap[x - 1, y - 1] == color && Map.GameMap[x - 2, y - 2] == color)
            {
                return true;
            }
        }

        if (x > 0 && x < 4 && y > 0 && y < 4)
        {
            if (Map.GameMap[x - 1, y - 1] == color && Map.GameMap[x + 1, y + 1] == color)
            {
                return true;
            }
        }

        //斜め方向の判定
        if (x < 3 && y > 1)
        {
            if (Map.GameMap[x + 1, y - 1] == color && Map.GameMap[x + 2, y - 2] == color)
            {
                return true;
            }
        }

        if (x > 1 && y < 3)
        {
            if (Map.GameMap[x - 1, y + 1] == color && Map.GameMap[x - 2, y + 2] == color)
            {
                return true;
            }
        }

        if (x > 0 && x < 4 && y > 0 && y < 4)
        {
            if (Map.GameMap[x - 1, y + 1] == color && Map.GameMap[x + 1, y - 1] == color)
            {
                return true;
            }
        }

        return false;
    }

    //同じ色が三つ並んでいるかどうかを判定する
    public static bool Is3SameColor(int x, int y, Map.map color)
    {
        //横方向の判定
        if(x < 2)
        {
            if(Map.GameMap[x + 1, y] == color && Map.GameMap[x + 2, y] == color && Map.GameMap[x + 3, y] == color)
            {
                return true;
            }
        }

        if(x > 2)
        {
            if (Map.GameMap[x - 1, y] == color && Map.GameMap[x - 2, y] == color && Map.GameMap[x - 3, y] == color)
            {
                return true;
            }
        }

        if(x > 0 && x < 3)
        {
            if (Map.GameMap[x - 1, y] == color && Map.GameMap[x + 1, y] == color && Map.GameMap[x + 2, y] == color)
            {
                return true;
            }
        }

        if(x > 1 && x < 4)
        {
            if (Map.GameMap[x - 1, y] == color && Map.GameMap[x - 2, y] == color && Map.GameMap[x + 1, y] == color)
            {
                return true;
            }
        }

        //横の判定
        if (y < 2)
        {
            if (Map.GameMap[x, y + 1] == color && Map.GameMap[x, y + 2] == color && Map.GameMap[x, y + 3] == color)
            {
                return true;
            }
        }

        if (y > 2)
        {
            if (Map.GameMap[x, y - 1] == color && Map.GameMap[x, y - 2] == color && Map.GameMap[x, y - 3] == color)
            {
                return true;
            }
        }

        if (y > 0 && y < 3)
        {
            if (Map.GameMap[x, y - 1] == color && Map.GameMap[x, y + 1] == color && Map.GameMap[x, y + 2] == color)
            {
                return true;
            }
        }

        if (y > 1 && y < 4)
        {
            if (Map.GameMap[x, y - 1] == color && Map.GameMap[x, y - 2] == color && Map.GameMap[x, y + 1] == color)
            {
                return true;
            }
        }

        //斜めの判定
        if (x < 2 && y < 2)
        {
            if (Map.GameMap[x + 1, y + 1] == color && Map.GameMap[x + 2, y + 2] == color && Map.GameMap[x + 3, y + 3] == color)
            {
                return true;
            }
        }

        if (x > 2 && y > 2)
        {
            if (Map.GameMap[x - 1, y - 1] == color && Map.GameMap[x - 2, y - 2] == color && Map.GameMap[x - 3, y - 3] == color)
            {
                return true;
            }
        }

        if (x > 0 && x < 2 && y > 0 && y < 2)
        {
            if (Map.GameMap[x - 1, y - 1] == color && Map.GameMap[x + 1, y + 1] == color && Map.GameMap[x + 2, y + 2] == color)
            {
                return true;
            }
        }

        if (x > 1 && x < 3 && y > 1 && y < 3)
        {
            if (Map.GameMap[x - 1, y - 1] == color && Map.GameMap[x - 2, y - 2] == color && Map.GameMap[x + 1, y + 1] == color)
            {
                return true;
            }
        }

        return false;
    }

}
