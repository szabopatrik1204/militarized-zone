using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public int[,] bombPattern;

    public int size;

    public int damage;

    public enum Pattern
    {
        xPattern,
        plusPattern,
        randomPattern,
        horizontalPattern,
        verticalPattern,
        equalPattern,
    }
    public void Init(Pattern pattern, int damage,int size)
    {

        switch (pattern)
        {
            case Pattern.xPattern:
                this.bombPattern = XPattern(size);
                break; 
            case Pattern.randomPattern:
                this.bombPattern = RandomPattern(size);
                break;
            case Pattern.plusPattern:
                this.bombPattern = PlusPattern(size);
                break;
            case Pattern.horizontalPattern:
                this.bombPattern = HorizontalPattern(size);
                break;
            case Pattern.verticalPattern:
                this.bombPattern = VerticalPattern(size);
                break;
            case Pattern.equalPattern:
                this.bombPattern = EqualPattern(size);
                break;

            default:
                break;
        }

        this.size = size;
        this.damage = damage;

    }

    public int[,] XPattern(int size)
    {
        int[,] mtx = new int[size,size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if ((i == j) || (i + j == size-1))
                {
                    mtx[i, j] = 1;
                }
                else
                {
                    mtx[i, j] = 0;
                }
            }
        }

        return mtx;
    }

    public int[,] RandomPattern(int size)
    {
        int[,] mtx = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                mtx[i, j] = Random.Range(0,2);
            }
        }

        return mtx;
    }

    public int[,] PlusPattern(int size)
    {
        int[,] mtx = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if ( (i == Mathf.Floor(size / 2)) || (j == Mathf.Floor(size / 2)) )
                {
                    mtx[i, j] = 1;
                }
                else
                {
                    mtx[i, j] = 0;
                }
            }
        }

        return mtx;
    }

    public int[,] HorizontalPattern(int size)
    {
        int[,] mtx = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == Mathf.Floor(size / 2))
                {
                    mtx[i, j] = 1;
                }
                else
                {
                    mtx[i, j] = 0;
                }
            }
        }

        return mtx;
    }

    public int[,] VerticalPattern(int size)
    {
        int[,] mtx = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (j == Mathf.Floor(size / 2))
                {
                    mtx[i, j] = 1;
                }
                else
                {
                    mtx[i, j] = 0;
                }
            }
        }

        return mtx;
    }

    public int[,] EqualPattern(int size)
    {
        int[,] mtx = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if ((j == 0) || (j == size-1))
                {
                    mtx[i, j] = 1;
                }
                else
                {
                    mtx[i, j] = 0;
                }
            }
        }

        return mtx;
    }



}
