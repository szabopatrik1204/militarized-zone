using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    public int[,] bombPattern;

    public int size;

    public int damage;

    public Pattern pattern;



    public enum Pattern
    {
        xPattern,
        plusPattern,
        randomPattern,
        horizontalPattern,
        verticalPattern,
        equalPattern,
        perPattern,
        perMirrorPattern,
        pointPattern,
    }
    public void Init(Pattern pattern, int damage,int size)
    {

        this.size = size;
        this.damage = damage;

        this.pattern = pattern;

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
            case Pattern.perPattern:
                this.bombPattern = PerPattern(size);
                break;
            case Pattern.perMirrorPattern:
                this.bombPattern = PerMirrorPattern(size);
                break;
            case Pattern.pointPattern:
                this.bombPattern = PointPattern(size);
                break;

            default:
                break;
        }

    }

    public int CalculateDamage(int damage)
    {
        return (int) Mathf.Floor(damage * (Random.Range(8f,11f)*0.1f));
    }

    public void isHealing()
    {
        int isHeal = Random.Range(0, 3);
        if (isHeal == 1)
        {
            for(int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    bombPattern[i, j] = bombPattern[i, j] * -1;
                }
            }
        }
    }

    public int[,] PerPattern(int size)
    {
        int[,] mtx = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i == j)
                {
                    mtx[i, j] = CalculateDamage(this.damage);
                }
                else
                {
                    mtx[i, j] = 0;
                }
            }
        }

        return mtx;
    }

    public int[,] PerMirrorPattern(int size)
    {
        int[,] mtx = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (i + j == size - 1)
                {
                    mtx[i, j] = CalculateDamage(this.damage);
                }
                else
                {
                    mtx[i, j] = 0;
                }
            }
        }

        return mtx;
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
                    mtx[i, j] = CalculateDamage(this.damage);
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
                if (mtx[i, j] == 1)
                {
                    mtx[i, j] = CalculateDamage(this.damage);
                }

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
                    mtx[i, j] = CalculateDamage(this.damage);
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
                    mtx[i, j] = CalculateDamage(this.damage);
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
                    mtx[i, j] = CalculateDamage(this.damage);
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
                    mtx[i, j] = CalculateDamage(this.damage);
                }
                else
                {
                    mtx[i, j] = 0;
                }
            }
        }

        return mtx;
    }


    public int[,] PointPattern(int size)
    {
        int[,] mtx = new int[size, size];

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if ((j == size / 2) && (i == size / 2))
                {
                    int dmg = this.damage + 100;
                    mtx[i, j] = CalculateDamage(dmg);
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
