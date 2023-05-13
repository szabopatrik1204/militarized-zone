using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{

    public static FlagManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void FlagStep()
    {
        Soldier newFlag = null;
        //Allies Flag
        for (int i = 0; i < Soldier.diedAxisThisTurn; i++)
        {
            GameObject originalObject = GameObject.Find($"Soldier {Spawner.Instance.AlliesFlag.position.x} {Spawner.Instance.AlliesFlag.position.y}");
            Soldier original = originalObject.GetComponent<Soldier>();

            newFlag = null;
            int step = 1;
            GameObject newObject = GameObject.Find($"Soldier {Spawner.Instance.AlliesFlag.position.x} {Spawner.Instance.AlliesFlag.position.y}");

            Debug.Log(newObject.GetComponent<Soldier>().position.y - 1);

            //Jump above Allies
            do
            {
                newObject = GameObject.Find($"Soldier {Spawner.Instance.AlliesFlag.position.x} {Spawner.Instance.AlliesFlag.position.y - step}");
                if (newObject != null)
                {
                    newFlag = newObject.GetComponent<Soldier>();
                    step++;
                }
                Debug.Log($"Allies {Spawner.Instance.AlliesFlag.position.y - step} {Spawner.Instance.AlliesFlag.position.y}");

            } while ((newFlag.position.y != 0) && (newFlag.playerSide == Soldier.Side.Allies));



            //Soldier at empty space
            if (newFlag.playerSide == Soldier.Side.None)
            {
                Spawner.Instance.AlliesFlag = newFlag;
                original.ChangeSide(Soldier.Side.None);
                newFlag.ChangeSide(Soldier.Side.FlagCarrierAllies);
            }

            //Soldier cant move to the position and later neither
            else if ((newFlag.playerSide == Soldier.Side.Axis) || 
                (newFlag.playerSide == Soldier.Side.BombedZone) || 
                (newFlag.playerSide == Soldier.Side.HealedZone))
            {
                break;
            }
            //Allies got to the end point and won
            if(newFlag.position.y == 0)
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.EndGame);
                GameManager.Instance.WinnerText.text = "Kék nyert";
                break;
            }
            Tile.ColorTile(newFlag);
        }


        //Axis Flag
        for (int i = 0; i < Soldier.diedAlliesThisTurn; i++)
        {
            GameObject originalObject = GameObject.Find($"Soldier {Spawner.Instance.AxisFlag.position.x} {Spawner.Instance.AxisFlag.position.y}");
            Soldier original = originalObject.GetComponent<Soldier>();

            newFlag = null;
            int step = 1;
            GameObject newObject = GameObject.Find($"Soldier {Spawner.Instance.AxisFlag.position.x} {Spawner.Instance.AxisFlag.position.y}");

            Debug.Log(newObject.GetComponent<Soldier>().position.y);

            //Jump above Axis
            do
            {
                newObject = GameObject.Find($"Soldier {Spawner.Instance.AxisFlag.position.x} {Spawner.Instance.AxisFlag.position.y + step}");
                if (newObject != null)
                {
                    newFlag = newObject.GetComponent<Soldier>();
                    step++;
                }
                Debug.Log($"Axis {Spawner.Instance.AxisFlag.position.x} {Spawner.Instance.AxisFlag.position.y + step}");

            } while ((newFlag.position.y != GridManager.Instance.width - 1) && (newFlag.playerSide == Soldier.Side.Axis));

            //Soldier at empty space
            if (newFlag.playerSide == Soldier.Side.None)
            {
                Spawner.Instance.AxisFlag = newFlag;
                original.ChangeSide(Soldier.Side.None);
                newFlag.ChangeSide(Soldier.Side.FlagCarrierAxis);
            }

            //Soldier cant move to the position and later neither
            else if ((newFlag.playerSide == Soldier.Side.Allies) ||
                (newFlag.playerSide == Soldier.Side.BombedZone) ||
                (newFlag.playerSide == Soldier.Side.HealedZone))
            {
                break;
            }
            //Axis got to the end point and won
            if (newFlag.position.y == GridManager.Instance.height - 1)
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.EndGame);
                GameManager.Instance.WinnerText.text = "Sárga nyert";
                break;
            }
            Tile.ColorTile(newFlag);
        }

        GridManager.ClearZones();

        Soldier.ResetDeathCounters();

    }
   
}
