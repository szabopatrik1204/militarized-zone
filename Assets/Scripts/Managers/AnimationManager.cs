using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    public const string AXIS_IDLE = "idle_axis";
    public const string AXIS_IDLE2 = "idle_axis2";
    public const string ALLIES_IDLE = "idle_allies";
    public const string EXPLOSION = "explosion";

    public AnimationClip animclip;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    public IEnumerator ExplosionPlay(Soldier soldier)
    {
        var explosionObject = GameObject.Find($"Highlight {soldier.position.x} {soldier.position.y}");
        Highlight explosion = explosionObject.GetComponent<Highlight>();
        var highlightColor = Color.white;
        //highlightColor.a = 1;
        explosion.GetComponent<SpriteRenderer>().color = highlightColor;
        Animator animator = explosion.GetComponent<Animator>();
        Debug.Log(animator);
        animator.enabled = true;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
        //animator.Play(EXPLOSION);
        //float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        //WaitForAnimation(explosionAnim);

    }

    public void IdleSide(Soldier.Side side)
    {
        if (side == Soldier.Side.Axis)
        {
            List<Soldier> axis = Soldier.listAxis();
            axis.ForEach(x => AnimationManager.Instance.IdlePlay(x));
            List<Soldier> allies = Soldier.listAllies();
            allies.ForEach(x => AnimationManager.Instance.IdleStop(x));
        }
        else if (side == Soldier.Side.Allies)
        {
            List<Soldier> allies = Soldier.listAllies();
            allies.ForEach(x => AnimationManager.Instance.IdlePlay(x));
            List<Soldier> axis = Soldier.listAxis();
            axis.ForEach(x => AnimationManager.Instance.IdleStop(x));
        }
    }

    public void IdlePlay(Soldier soldier)
    {
        Animator animator = soldier.GetComponent<Animator>();

        if (soldier.playerSide == Soldier.Side.Allies)
        {
            soldier.GetComponent<Animator>().enabled = true;
            animator.Play(ALLIES_IDLE);
        }
        else if (soldier.playerSide == Soldier.Side.Axis)
        {
            soldier.GetComponent<Animator>().enabled = true;
            animator.Play(AXIS_IDLE2);
        }
    }

    public void IdleStop(Soldier soldier)
    {
        Animator animator = soldier.GetComponent<Animator>();
        soldier.GetComponent<Animator>().enabled = false;
    }




}
