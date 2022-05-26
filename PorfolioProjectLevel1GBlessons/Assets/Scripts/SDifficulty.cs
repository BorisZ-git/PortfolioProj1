using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SDifficulty
{
    public static int lvlDif;
    public static int SetDifficulty(int a, int b, int c)
    {
        int value = 0;
        if (SDifficulty.lvlDif == 0)
            value = a;
        else if (SDifficulty.lvlDif == 1)
            value = b;
        else if (SDifficulty.lvlDif == 2)
            value = c;
        return value;
    }
}
