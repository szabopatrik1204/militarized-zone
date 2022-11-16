using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombManager : MonoBehaviour
{
    public static BombManager Instance;

    public GameObject cellPrefab;

    public Bomb bombPrefab;

    public Bomb option1;
    public Bomb option2;
    public Bomb option3;

    private void Awake()
    {
        Instance = this;
    }

    /*
        var bomb = Instantiate(bombPrefab, new Vector3(0, 0), Quaternion.identity);

        bomb.Init(Bomb.Pattern.xPattern, 100, 3);

        Debug.Log(bomb.bombPattern);

        foreach (int cell in bomb.bombPattern)
         }
        BombManager.Instance.DrawPattern(bomb,1);
    */

    public void GenerateOptions()
    {

        var values = Bomb.Pattern.GetValues(typeof(Bomb.Pattern));

        //string randomBar = (string)values.GetValue(Random.Range(0,values.Length));
        Bomb.Pattern randomBar = (Bomb.Pattern)values.GetValue(Random.Range(0, values.Length));

        var option1 = Instantiate(bombPrefab, new Vector3(0, 0), Quaternion.identity);
        option1.Init(randomBar, 100, 3);
        DrawPattern(option1, 1);

        //randomBar = (string)values.GetValue(Random.Range(0, values.Length));

        randomBar = (Bomb.Pattern) values.GetValue(Random.Range(0, values.Length));

        option2 = Instantiate(bombPrefab, new Vector3(0, 0), Quaternion.identity);
        option2.Init(randomBar, 100, 3);
        DrawPattern(option2, 2);

        randomBar = (Bomb.Pattern)values.GetValue(Random.Range(0, values.Length));

        option3 = Instantiate(bombPrefab, new Vector3(0, 0), Quaternion.identity);
        option3.Init(randomBar, 100, 3);
        DrawPattern(option3, 3);

        GameManager.Instance.UpdateGameState(GameManager.GameState.FlagCarrierSpawningAllies);

    }
    public void DrawPattern(Bomb bomb,int option) // Option 1,2,3 
    {

        for (int i = 0; i < bomb.size; i++)
        {
            for (int j = 0; j < bomb.size; j++)
            {
                if (bomb.bombPattern[i,j] == 1)
                {
                    var chosen = GameObject.Find($"Block{option} {i} {j}");
                    chosen.GetComponent<Image>().color = Color.red;
                    Debug.Log("Itt vok");
                }
                
            }
        }
        /*
        for (int x = 0; x < bomb.size; x++)
        {
            for (int y = 0; y < bomb.size; y++)
            {
                if (bomb.bombPattern[x, y] == 1)
                {
                    var spawnedTile = Instantiate(cellPrefab, new Vector3(x/2 -1 - 3.5f, y/2 - 1 + 2.5f), Quaternion.identity);
                    spawnedTile.name = $"BombCell {x-1} {y-1}";
                }

            }
        }*/

    }

    
    public void ShootBomb(Bomb bomb)
    {

    }

}
