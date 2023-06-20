using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInteractionReaction : MonoBehaviour
{
    //logic is player selects tool, hits action button, based on tool selected and/or pet trigger, call method in here

    private PetBehavior behavior;
    private Animator anim;
    private string animEat = "Eating", animHappy = "Happy", animDazed = "Dazed", animAngry = "Angry", animAsleep = "Asleep";
    [SerializeField] private PetTrigger trigger;
    private GameManager gameMan;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        behavior = GetComponent<PetBehavior>();
        gameMan = GameManager.instance;
    }

    public void PetWatered()
    {
        StartCoroutine(trigger.IgnoreCollision(1.5f));
        gameMan.timesWatered++;
        if(gameMan.curPhase == 0)
        {
            anim.SetTrigger(animHappy);
        }
        else
        {
            anim.SetTrigger(animAngry);
        }

        gameMan.EvolutionCheck();
        //set angry anim
    }

    public void PetPetted()
    {
        StartCoroutine(trigger.IgnoreCollision(1.5f));
        gameMan.timesPet++;
        anim.SetTrigger(animHappy);
    }

    public void PetStartFling()
    {
        //end fling handled in behavior
        behavior.StartFling();
        gameMan.timesFlung++;
        anim.SetTrigger(animDazed);
    }






}
