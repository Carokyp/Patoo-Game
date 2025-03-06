using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardLayout : MonoBehaviour
{

    public LayoutRow[] allRows;
    public Vector2Int[] leafToFreeze;

    public Leaf[,] GetLayout()
    {
        Leaf[,] theLayout = new Leaf[allRows[0].leafsInRow.Length, allRows.Length];

        for (int y = 0; y < allRows.Length; y++)
        {
            for (int x = 0; x < allRows[y].leafsInRow.Length; x++)
            {

                if (x < theLayout.GetLength(0))
                {
                    if (allRows[y].leafsInRow[x] != null)
                    {
                       /* if (allRows[y].leafToFreeze != null)
                        {
                            for (int i = 0; i < allRows[y].leafToFreeze.Length; i++)
                            {
                                allRows[y].leafsInRow[allRows[y].fre      leafToFreeze[i].

                            }
                        }*/
                        theLayout[x, allRows.Length - 1 - y] = allRows[y].leafsInRow[x];
                    }
                }
            }
        }


        return theLayout;
    }

}

[System.Serializable]
public class LayoutRow 
{
    public Leaf[] leafsInRow;
    //public int[] leafToFreeze;
}