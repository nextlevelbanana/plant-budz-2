using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PetEvolution : MonoBehaviour
{
    private Animator anim;
    public AnimatorController[] animators;
    public int nextAnimatorController = 0;
    //Idea is the evolution animation will contain a function that triggers the next anim swap.
    //This will have to pull info from gameman upon starting evolution?

    //Hold of on anims until we know exactly what parameters we're passing through and what triggers evolution

    private void Awake()
    {
        SwapController();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            nextAnimatorController++;
            InitEvolve();
        }
    }

    public void InitEvolve()
    {
        //get gameman tracked info
        anim.SetTrigger("Evolve");
        Invoke("SwapController", 1.5f);
    }

    public void SwapController()
    {
        anim.runtimeAnimatorController = animators[nextAnimatorController];
    }








}
