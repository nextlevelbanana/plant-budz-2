using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    //mutation / progression
    public static GameManager instance;
    public HUD display;
    public int curPhase = 0;
    public PetEvolution petEvo;
    public List<GameObject> foodObjectsOnScreen = new List<GameObject>();
    public List<GameObject> dynamicObjectsOnScreen = new List<GameObject>();
    [SerializeField] InputManager input;
    public TextMeshProUGUI debugText;

    [Header("Stats Tracked")]
    public int foodEaten = 0;
    public float happiness = 0f;
    public int timesWatered = 0;
    public int timesFlung = 0;
    public int timesPet = 0;
    public float happyMod = 1f;
    public float sadMod = 1f;
    private int totalInteractions = 0;
    public int requiredInteractionsForEvolution = 1;

    [Header("Play Timer")]
    public bool shouldTime = true;
    [SerializeField] float phaseTimer = 0f;
    [SerializeField] float[] progressionTimesPerPhase;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (shouldTime)
        {
            phaseTimer += Time.deltaTime;
        }
    }

    public void SetDebugMessage(string message)
    {
        debugText.text = message;
        Invoke("ClearDebugMessage", 3f);
    }

    private void ClearDebugMessage()
    {
        debugText.text = "";
    }

    public GameObject GetClosestFood(Transform petTransform)
    {
        //however this is initialized --- make sure list contains food! (otherwise nullrefs)
        GameObject closest = null;
        float closestDisSqr = 1000f;
        foreach(GameObject food in foodObjectsOnScreen)
        {
            Vector2 dirToTarget = food.transform.position - petTransform.position;
            float disSqrToTarget = dirToTarget.sqrMagnitude;
            if(disSqrToTarget < closestDisSqr)
            {
                closestDisSqr = disSqrToTarget;
                closest = food;
            }
        }

        return closest;
    }

    public void RemoveFood(GameObject food)
    {
        foodObjectsOnScreen.Remove(food);
    }

    public void HappinessCalc()
    {
        //pretty rudimentary - feel free to revise!
        totalInteractions = foodEaten + timesPet + timesFlung + timesWatered;
        happiness = totalInteractions / (timesFlung + timesWatered);
        print("happiness is: " + happiness);
    }

    public void EvolutionCheck()
    {
        if (shouldTime)
        {
            if(phaseTimer <= progressionTimesPerPhase[curPhase])
            {
                SetDebugMessage("Time threshold for phase not met.");
                return;
            }
        }

        HappinessCalc();

        //ensure minimum interaction?
        if(totalInteractions < requiredInteractionsForEvolution) 
        {
            return;
        }

        if(curPhase == 0)
        {
            happiness = timesWatered;
        }

        if(petEvo.ShouldEvolve(happiness, curPhase))
        {
            ClearTrackedStats();
            curPhase++;
            input.highestToolAllowed = 4;
        }
    }

    private void ClearTrackedStats()
    {
        happiness = 0f;
        foodEaten = 0;
        timesPet = 0;
        timesWatered = 0;
        timesFlung = 0;
        totalInteractions = 0;
    }


}
