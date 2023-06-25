using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetInteractionReaction : MonoBehaviour
{
    //Animations && SFX

    private PetBehavior behavior;
    private Animator anim;
    private string animEat = "Eating", animHappy = "Happy", animDazed = "Dazed", animAngry = "Angry";
    [SerializeField] private PetTrigger trigger;
    private GameManager gameMan;

    public enum whatIsPet { Plant, Blob, Cat, Fish };
    public whatIsPet petType;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        behavior = GetComponent<PetBehavior>();
        gameMan = GameManager.instance;
        petType = whatIsPet.Plant;
    }

    public void PetWatered()
    {
        StartCoroutine(trigger.IgnoreCollision(1.5f));
        behavior.SleepCheck();
        gameMan.timesWatered++;
        if(petType == whatIsPet.Plant || petType == whatIsPet.Fish)
        {
            anim.SetTrigger(animHappy);
        }
        else
        {
            anim.SetTrigger(animAngry);
        }

        gameMan.EvolutionCheck();
    }

    public void PetPetted()
    {
        StartCoroutine(trigger.IgnoreCollision(1.5f));
        behavior.SleepCheck();
        gameMan.timesPet++;
        Invoke("DelayedBrushSFX", 0.5f);
        gameMan.EvolutionCheck();
        if (petType == whatIsPet.Fish) { anim.SetTrigger(animAngry); return; }
        anim.SetTrigger(animHappy);
        
    }

    public void PetEatingAnim()
    {
        behavior.SleepCheck();
        anim.SetTrigger(animEat);
        gameMan.EvolutionCheck();
    }

    public void PetOverfed()
    {
        transform.localScale *= 1.1f;
    }
    public void PetStartFling()
    {
        //end fling handled in behavior
        behavior.SleepCheck();
        behavior.StartFling();
        gameMan.timesFlung++;
        gameMan.EvolutionCheck();
        anim.SetTrigger(animDazed);
    }
    public void DelayedBrushSFX()
    {
        AudioManager.instance.PlaySFX(2);
    }

    public void HappySFX()
    {
        AudioManager.instance.PlaySFX(8);
    }

    public void EatSFX()
    {
        AudioManager.instance.PlaySFX(3);
    }

    public void AngryCat()
    {
        AudioManager.instance.PlaySFX(0);
    }

    public void AngrySFX()
    {
        AudioManager.instance.PlaySFX(10);
    }

    public void AngryFish()
    {
        AudioManager.instance.PlaySFX(12);
    }

    public void SadSFX()
    {
        AudioManager.instance.PlaySFX(10);
    }

    public void DazedSFX()
    {
        AudioManager.instance.PlaySFX(11);
    }





}
