using UnityEngine;
using System.Collections;

public static class BoardRepresentation
{
    public static int IndexToNum (string indexFile ,int indexRank)
    {
        int file=1;
        int rank=1;
        if (indexFile[0] == 'a')
            file = 1;
        if (indexFile[0] == 'b')
            file = 2;
        if (indexFile[0] == 'c')
            file = 3;
        if (indexFile[0] == 'd')
            file = 4;
        if (indexFile[0] == 'e')
            file = 5;
        if (indexFile[0] == 'f')
            file = 6;
        if (indexFile[0] == 'g')
            file = 7;
        if (indexFile[0] == 'h')
            file = 8;
        rank = indexRank * 10;
        return file+rank;
    }
    public static int IndexToFullNum(string index)
    {
        int file = 0;
        int rank = 0;
        if (index[0] == 'a')
            file = 0;
        if (index[0] == 'b')
            file = 1;
        if (index[0] == 'c')
            file = 2;
        if (index[0] == 'd')
            file = 3;
        if (index[0] == 'e')
            file = 4;
        if (index[0] == 'f')
            file = 5;
        if (index[0] == 'g')
            file = 6;
        if (index[0] == 'h')
            file = 7;
        rank = ((int)char.GetNumericValue(index[1])-1) * 8;
        return rank + file;
    }
    public static string FullNumToIndex(int num)
    {
        string file = "a";
        int rank = 0;
        int numRank = Mathf.FloorToInt(num / 8) + 1;
        int numFile = num % 8;
        if (numFile == 0)
            file = "a";
        if (numFile == 1)
            file = "b";
        if (numFile == 2)
            file = "c";
        if (numFile == 3)
            file = "d";
        if (numFile == 4)
            file = "e";
        if (numFile == 5)
            file = "f";
        if (numFile == 6)
            file = "g";
        if (numFile == 7)
            file = "h";
        rank = numRank;
        return file + rank.ToString();
    }
    public static string NumToIndex(int numFile, int numRank)
    {
        string file = "a";
        int rank = 1;
        if (numFile == 1)
            file = "a";
        if (numFile == 2)
            file = "b";
        if (numFile == 3)
            file = "c";
        if (numFile == 4)
            file = "d";
        if (numFile == 5)
            file = "e";
        if (numFile == 6)
            file = "f";
        if (numFile == 7)
            file = "g";
        if (numFile == 8)
            file = "h";
        rank = numRank;
        return file + rank.ToString();
    }
}

