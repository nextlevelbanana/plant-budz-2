using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetEvolution : MonoBehaviour
{
    private Animator anim;
    private PetBehavior pb;
    private PetInteractionReaction reaction;
    public RuntimeAnimatorController[] animators;
    public int nextAnimatorController = 0;
    [SerializeField] float plantToBlobScore = 0f;
    [SerializeField] float blobToAnimalScore = 0f;
    [SerializeField] float finalEvolutionScore = 0f;

    public ParticleSystem evoParticles;

    //Idea is the evolution animation will contain a function that triggers the next anim swap.

    private void Awake()
    {
        anim = GetComponent<Animator>();
        pb = GetComponent<PetBehavior>();
        reaction = GetComponent<PetInteractionReaction>();
        anim.runtimeAnimatorController = animators[nextAnimatorController];
        anim.SetBool("Evolve", false);
    }

    public void InitEvolve()
    {
        anim.Rebind();
        anim.Update(0f);
        pb.SetExternalControl(4f);
        anim.SetBool("Evolve", true);
        if (!evoParticles.isEmitting) { evoParticles.Play(); }
        pb.RBZero();
        //animation will call swap controller function
    }

    public void SwapController()
    {
        //Called through animator!
        //evolution is finished
        pb.EndExternalControl();
        //anim.SetBool("Evolve", false);
        anim.runtimeAnimatorController = animators[nextAnimatorController];
        if (!evoParticles.isStopped) { evoParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting); }
    }

    public void PlantToBlobAudio()
    {
        //called through animator
        AudioManager.instance.PlaySFX(1);
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
        AudioManager.instance.PlayBKG(1);
    }

    private void SetBlobStats()
    {
        GameManager.instance.SetNewStats(15, 7, 3);
    }

    private void SetFishStats()
    {
        GameManager.instance.SetNewStats(20, 10, 6);
    }

    private void SetCatStats()
    {
        GameManager.instance.SetNewStats(20, 8, 4);
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
                    SetBlobStats();
                    return true;
                }
                return false;

            case 1:
                if(happiness >= blobToAnimalScore -1)
                {
                    //CAT
                    //GameManager.instance.SetDebugMessage("Cat Evo");
                    nextAnimatorController = 2;
                    reaction.petType = PetInteractionReaction.whatIsPet.Cat;
                    pb.EnableCatMode();
                    InitEvolve();
                    SetCatStats();
                    return true;
                }

                if(happiness < blobToAnimalScore -1)
                {
                    //FISH
                    //GameManager.instance.SetDebugMessage("Fish Evo");
                    nextAnimatorController = 3;
                    reaction.petType = PetInteractionReaction.whatIsPet.Fish;
                    pb.EnableFishMode();
                    InitEvolve();
                    SetFishStats();
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
