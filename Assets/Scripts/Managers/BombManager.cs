using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
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

    public Bomb previousOption;

    public Bomb chosenOption;

    public Color damageColor;

    public Color healColor;

    public Color zeroColor;

    public GameObject explosionPrefab;

    public Sprite question_damage;
    public Sprite no_damage;
    public Sprite low_damage;
    public Sprite medium_damage;
    public Sprite high_damage;

    private void Awake()
    {
        Instance = this;
        chosenOption = null;
    }

    public void DestroyOption(string name)
    {
        Destroy(GameObject.Find(name));
    }

    

    public void ReplaceOption()
    {
        if (chosenOption == option1)
        {
            var values = Bomb.Pattern.GetValues(typeof(Bomb.Pattern));
            Bomb.Pattern randomBar = (Bomb.Pattern)values.GetValue(Random.Range(0, values.Length));

            DestroyOption("Option 1 Bomb");

            option1 = Instantiate(bombPrefab, new Vector3(0, 0), Quaternion.identity);
            option1.Init(randomBar, 100, 3);
            if (option1.pattern != Bomb.Pattern.randomPattern)
            {
                option1.isHealing();
            }
            option1.name = $"Option 1 Bomb";
            ClearPattern(option1, 1);
            DrawPattern(option1, 1);
        } 
        else if (chosenOption == option2)
        {
            var values = Bomb.Pattern.GetValues(typeof(Bomb.Pattern));
            Bomb.Pattern randomBar = (Bomb.Pattern)values.GetValue(Random.Range(0, values.Length));

            DestroyOption("Option 2 Bomb");

            option2 = Instantiate(bombPrefab, new Vector3(0, 0), Quaternion.identity);
            option2.Init(randomBar, 100, 3);
            option2.name = $"Option 2 Bomb";
            if (option2.pattern != Bomb.Pattern.randomPattern)
            {
                option2.isHealing();
            }
            ClearPattern(option2, 2);
            DrawPattern(option2, 2);
        } 
        else if (chosenOption == option3)
        {
            var values = Bomb.Pattern.GetValues(typeof(Bomb.Pattern));
            Bomb.Pattern randomBar = (Bomb.Pattern)values.GetValue(Random.Range(0, values.Length));

            DestroyOption("Option 3 Bomb");

            option3 = Instantiate(bombPrefab, new Vector3(0, 0), Quaternion.identity);
            option3.Init(randomBar, 100, 3);
            option3.name = $"Option 3 Bomb";
            if (option2.pattern != Bomb.Pattern.randomPattern)
            {
                option3.isHealing();
            }
            ClearPattern(option3, 3);
            DrawPattern(option3, 3);
        }
    }

    public void GenerateOptions()
    {

        var values = Bomb.Pattern.GetValues(typeof(Bomb.Pattern));

        Bomb.Pattern randomBar = (Bomb.Pattern)values.GetValue(Random.Range(0, values.Length));

        option1 = Instantiate(bombPrefab, new Vector3(0, 0), Quaternion.identity);
        option1.Init(randomBar, 100, 3);
        option1.name = $"Option 1 Bomb";
        DrawPattern(option1, 1);

        randomBar = (Bomb.Pattern) values.GetValue(Random.Range(0, values.Length));

        option2 = Instantiate(bombPrefab, new Vector3(0, 0), Quaternion.identity);
        option2.Init(randomBar, 100, 3);
        option2.name = $"Option 2 Bomb";
        DrawPattern(option2, 2);

        randomBar = (Bomb.Pattern)values.GetValue(Random.Range(0, values.Length));

        option3 = Instantiate(bombPrefab, new Vector3(0, 0), Quaternion.identity);
        option3.Init(randomBar, 100, 3);
        option3.name = $"Option 3 Bomb";
        DrawPattern(option3, 3);

        GameManager.Instance.UpdateGameState(GameManager.GameState.FlagCarrierSpawningAllies);

    }

    public void DrawPattern(Bomb bomb,int option) // Option 1,2,3 
    {

        for (int i = 0; i < bomb.size; i++)
        {
            for (int j = 0; j < bomb.size; j++)
            {          
                if (bomb.pattern == Bomb.Pattern.randomPattern)
                {
                    int cellDamage = bomb.bombPattern[i, j];
                    var chosen = GameObject.Find($"Block{option} {i} {j}");
                    Image image = chosen.GetComponent<Image>();
                    image.sprite = question_damage;
                    image.color = damageColor;
                }
                else if (bomb.bombPattern[i, j] > 0)
                {
                    int cellDamage = bomb.bombPattern[i, j];
                    var chosen = GameObject.Find($"Block{option} {i} {j}");
                    Image image = chosen.GetComponent<Image>();
                    switch (cellDamage / 10)
                    {
                        case 0:
                            image.sprite = no_damage;
                            break;
                        case <= 8:
                            image.sprite = low_damage;
                            break;
                        case > 8 and <= 10:
                            image.sprite = medium_damage;
                            break;
                        case > 10:
                            image.sprite = high_damage;
                            break;
                    }
                    image.color = damageColor;

                }
                else if (bomb.bombPattern[i, j] < 0)
                {
                    int cellDamage = bomb.bombPattern[i, j];
                    var chosen = GameObject.Find($"Block{option} {i} {j}");
                    Image image = chosen.GetComponent<Image>();
                    switch (cellDamage / 10)
                    {
                        case 0:
                            image.sprite = no_damage;
                            break;
                        case <= 8:
                            image.sprite = low_damage;
                            break;
                        case > 8 and <= 10:
                            image.sprite = medium_damage;
                            break;
                        case > 10:
                            image.sprite = high_damage;
                            break;
                    }
                    image.color = healColor;

                }
                else if (bomb.bombPattern[i, j] == 0)
                {
                    var chosen = GameObject.Find($"Block{option} {i} {j}");
                    Image image = chosen.GetComponent<Image>();
                    image.sprite = no_damage;
                    image.color = zeroColor;
                }

            }
        }

    }

    public void ClearPattern(Bomb bomb, int option) // Option 1,2,3 
    {

        for (int i = 0; i < bomb.size; i++)
        {
            for (int j = 0; j < bomb.size; j++)
            {
                    var chosen = GameObject.Find($"Block{option} {i} {j}");
                    chosen.GetComponent<Image>().color = zeroColor;
                    //chosen.GetComponent<Image>().transform.GetChild(0).GetComponent<TMP_Text>().text = "0";
            }
        }
    }

    public void ChooseBomb(Button button)
    {
        if (GameManager.Instance.State == GameManager.GameState.ShootBomb)
        {
            GridManager.Instance.ClearHighlights();
            switch (button.name)
            {
                case "Option1":
                    chosenOption = option1;
                    break;
                case "Option2":
                    chosenOption = option2;
                    break;
                case "Option3":
                    chosenOption = option3;
                    break;
                default:
                    return;
            }
        }
    }


    public void ShootBomb(Bomb bomb,RaycastHit hit)
    {
        SoundManager.playBoom();
        for (int i = 0; i < bomb.size; i++)
        {
            for (int j = 0; j < bomb.size; j++)
            {
                var target = GameObject.Find(hit.transform.gameObject.name);
                if ((bomb.bombPattern[i, j] != 0)) 
                {
                    var chosen = GameObject.Find($"Soldier {target.transform.position.x + i - 1} {target.transform.position.y + j - 1}");
                    if (chosen != null)
                    {
                        Soldier chosenSoldier = chosen.GetComponent<Soldier>();
                        chosenSoldier.health = Mathf.Min(Mathf.Max(chosenSoldier.health - bomb.bombPattern[i, j], 0), 300);
                        if ((chosenSoldier.playerSide == Soldier.Side.Allies) || (chosenSoldier.playerSide == Soldier.Side.Axis))
                        {
                            AnimationManager.Instance.ExplosionPlay(chosenSoldier);
                            var hlObject = GameObject.Find($"Highlight {chosenSoldier.position.x} {chosenSoldier.position.y}");
                            Highlight hl = hlObject.GetComponent<Highlight>();
                            chosenSoldier.isDead();
                            chosenSoldier.UpdateHealthbar();

                        }
                        else if (chosenSoldier.playerSide == Soldier.Side.None)
                        {
                            chosenSoldier.ChangeSide(bomb.bombPattern[i, j] > 0 ? Soldier.Side.BombedZone : Soldier.Side.BombedZone);
                        }
                        //ANIMATION
                        var explosionObject = Instantiate(explosionPrefab, chosenSoldier.transform.position + new Vector3(0, 0, 0), chosenSoldier.transform.rotation);
                        Destroy(explosionObject, 5f);
                    }
                }
            }
        }

        ReplaceOption();

        chosenOption = null;

        GameManager.Instance.nextTurn();

    }

    public void NukeBomb()
    {
        for (int i = 0; i < GridManager.Instance.height; i++)
        {
            for (int j = 0; j < GridManager.Instance.width; j++)
            {
                var target = GameObject.Find($"Soldier {i} {j}");
                Soldier soldier = target.GetComponent<Soldier>();
                if ((soldier.playerSide == Soldier.Side.Allies) || (soldier.playerSide == Soldier.Side.Axis))
                {
                    if (Random.Range(0,2) == 1)
                    {
                        soldier.health = 0;
                        soldier.isDead();
                        soldier.UpdateHealthbar();
                    }
                }
            }
        }
    }

}
