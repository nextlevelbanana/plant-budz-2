using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetEvolution : MonoBehaviour
{
    private Animator anim;
    private PetBehavior pb;
    private PetInteractionReaction reaction;
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
        reaction = GetComponent<PetInteractionReaction>();
        anim.runtimeAnimatorController = animators[nextAnimatorController];
    }

    public void InitEvolve()
    {
        //turn off trigger, ignore input
        pb.RBZero();
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

    public void PlantToBlobAudio()
    {
        //called through animator
        AudioManager.instance.PlaySFX(1);
        AudioManager.instance.PlayBKG(1);
    }

    public void BlobEvoCheck()
    {
        //anim trigger
        if (reaction.petType == PetInteractionReaction.whatIsPet.Cat) { BlobToCatAudio(); }
        if (reaction.petType == PetInteractionReaction.whatIsPet.Fish) { BlobToFishAudio(); }
    }

    private void BlobToCatAudio()
    {
        AudioManager.instance.PlaySFX(4);
        AudioManager.instance.PlayBKG(2);
    }

    private void BlobToFishAudio()
    {
        AudioManager.instance.PlaySFX(14);
        AudioManager.instance.PlayBKG(2);
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
                    reaction.petType = PetInteractionReaction.whatIsPet.Blob;
                    InitEvolve();
                    return true;
                }
                return false;

            case 1:
                if(happiness >= blobToAnimalScore -1)
                {
                    //CAT
                    nextAnimatorController = 2;
                    reaction.petType = PetInteractionReaction.whatIsPet.Cat;
                    GameManager.instance.SetDebugMessage("Cat Evolution");
                    pb.EnableCatMode();
                    InitEvolve();
                    return true;
                }

                if(happiness < blobToAnimalScore -1)
                {
                    //FISH
                    nextAnimatorController = 3;
                    reaction.petType = PetInteractionReaction.whatIsPet.Fish;
                    pb.EnableFishMode();
                    InitEvolve();
                    GameManager.instance.SetDebugMessage("Fish Evolution");
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
