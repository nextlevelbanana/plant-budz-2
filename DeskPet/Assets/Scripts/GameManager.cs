using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int curPhase = 0;
    public PetEvolution petEvo;
    public List<GameObject> foodObjectsOnScreen = new List<GameObject>();
    [SerializeField] InputManager input;
    public TextMeshProUGUI debugText;
    public PetInteractionReaction petReaction;

    [Header("Stats Tracked")]
    public int foodEaten = 0;
    public float happiness = 0f;
    public int timesWatered = 0;
    public int timesFlung = 0;
    public int timesPet = 0;
    [SerializeField] private int totalInteractions = 0;
    public int requiredInteractionsForEvolution = 1;

    [Header("Play Timer")]
    public bool shouldTime = true;
    [SerializeField] float phaseTimer = 0f;
    [SerializeField] float[] progressionTimesPerPhase;

    
    public Vector2 pos;

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
        totalInteractions = foodEaten + timesPet + timesFlung + timesWatered;

        if(petReaction.petType == PetInteractionReaction.whatIsPet.Plant) { PlantHappyCalc(); return; }
        if(petReaction.petType == PetInteractionReaction.whatIsPet.Fish) { FishHappyCalc(); return; }
        //blob and cat follow same logic
        BlobAndCatHappyCalc();
    }

    private void PlantHappyCalc()
    {
        happiness = timesWatered;
        SetDebugMessage("Happiness: " + happiness);
    }

    private void FishHappyCalc()
    {
        happiness = (foodEaten + timesWatered) - (timesFlung + timesPet);
        SetDebugMessage("Happiness: " + happiness);
    }

    private void BlobAndCatHappyCalc()
    {
        happiness = (foodEaten + timesPet) - (timesFlung + timesWatered);
        SetDebugMessage("Happiness: " + happiness);
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
        if (totalInteractions < requiredInteractionsForEvolution) 
        {
            return;
        }

        if (curPhase == 0)
        {
            //passes timer check and minimum interaction check - should always trigger evolution
            happiness = timesWatered;
            input.UnlockAllButtons();
        }

        if(petEvo.ShouldEvolve(happiness, curPhase))
        {
            ClearTrackedStats();
            curPhase++;
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
        DestroyAllFood();
    }

    private void DestroyAllFood()
    {
        foreach(GameObject food in foodObjectsOnScreen)
        {
            Destroy(food);
        }

        foodObjectsOnScreen.Clear();
    }


}
