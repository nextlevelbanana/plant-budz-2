using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetEvolution : MonoBehaviour
{
    private Animator anim;
    private PetBehavior pb;
    //0 = plant, 1 = blob, 2 = cat, 3 = bunny. Not sure about endings yet.
    public RuntimeAnimatorController[] animators;
    public int nextAnimatorController = 0;
    [SerializeField] float plantToBlobScore = 0f;
    [SerializeField] float blobToAnimalScore = 0f;
    [SerializeField] float finalEvolutionScore = 0f;

    //Idea is the evolution animation will contain a function that triggers the next anim swap.

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pb = GetComponent<PetBehavior>();
        anim.runtimeAnimatorController = animators[nextAnimatorController];
    }

    public void InitEvolve()
    {
        //turn off trigger, ignore input
        pb.SetExternalControl(4f);
        anim.SetTrigger("Evolve");
        //animation will call swap controller function
    }

    public void SwapController()
    {
        //Called through animator!
        //evolution is finished
        pb.EndExternalControl();
        anim.runtimeAnimatorController = animators[nextAnimatorController];
    }

    public bool ShouldEvolve(float happiness, int phase)
    {
        switch (phase)
        {
            case 0:
                //plant to blob
                if(happiness >= plantToBlobScore)
                {
                    nextAnimatorController = 1;
                    InitEvolve();
                    return true;
                }
                return false;

            case 1:
                //blob to cat / bunny
                if(happiness <= blobToAnimalScore)
                {
                    nextAnimatorController = 2;
                    InitEvolve();
                    print("Set next anim controller to cat");
                    return true;
                }

                if(happiness > blobToAnimalScore)
                {
                    nextAnimatorController = 3;
                    //InitEvolve();
                    print("Set next anim controller to bunny");
                    return true;
                }

                return false;

            case 2:
                //final evo
                if(happiness >= finalEvolutionScore)
                {
                    return true;
                }
                return false;
        }

        return false;
    }








}
