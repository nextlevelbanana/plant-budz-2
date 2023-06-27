using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int curPhase = 0;
    public PetEvolution petEvo;
    public List<GameObject> foodObjectsOnScreen = new List<GameObject>();
    [SerializeField] InputManager input;
    public TextMeshProUGUI debugText;
    public PetInteractionReaction petReaction;
    private int maxFoodAllowed = 60;

    [Header("Stats Tracked")]
    public int foodEaten = 0;
    public float happiness = 0f;
    public int timesWatered = 0;
    public int timesFlung = 0;
    public int timesPet = 0;
    [SerializeField] private int totalInteractions = 0;
    public int requiredInteractionsForEvolution = 10;
    private int underfedThresh = 0, overfedThresh = 0;
    

    [Header("Play Timer")]
    public bool shouldTime = true;
    [SerializeField] float phaseTimer = 0f;
    [SerializeField] float[] progressionTimesPerPhase;

    //This WAS needed (my bad). GameMan is the only thing that persists through scenes so it needs to store what the form is.
    public int finalForm = 0;
    //3 == fish
    public GameObject desktopia;
    
    public Vector2 pos;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(instance);
    }

    private void Update()
    {
        if (shouldTime)
        {
            phaseTimer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadDatingSim();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadBulletHell();
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

    public void LoadDatingSim()
    {
        shouldTime = false;
        SceneManager.LoadScene(2);
    }

    public void LoadBulletHell()
    {
        shouldTime = false;
        SceneManager.LoadScene(3);
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
        foodEaten++;

        if(foodEaten > overfedThresh) { petReaction.PetOverfed(); }
    }

    public void AddFood(GameObject food)
    {
        foodObjectsOnScreen.Add(food);
        if (foodObjectsOnScreen.Count >= maxFoodAllowed)
        {
            DestroyAllFood();
            //SetDebugMessage("Too much food...");
        }
    }

    public void HappinessCalc()
    {
        totalInteractions = foodEaten + timesPet + timesFlung + timesWatered;

        if(petReaction.petType == PetInteractionReaction.whatIsPet.Plant) { PlantHappyCalc(); return; }
        if(petReaction.petType == PetInteractionReaction.whatIsPet.Fish) { FishHappyCalc(); return; }
        //blob and cat follow same logic
        BlobAndCatHappyCalc();
    }

    private void FatCalc()
    {
        if(foodEaten <= underfedThresh) { happiness -= 5; return; }

        if(foodEaten > underfedThresh && foodEaten <= overfedThresh) { happiness += 5; }

        if(foodEaten > overfedThresh) { happiness -= 3; }
    }

    private void PlantHappyCalc()
    {
        happiness = timesWatered;
        //SetDebugMessage("Happiness: " + happiness);
    }

    private void FishHappyCalc()
    {
        happiness = (foodEaten + (timesWatered * 2)) - (timesFlung + timesPet);
        FatCalc();
        //SetDebugMessage("Fish Happiness: " + happiness);
    }

    private void BlobAndCatHappyCalc()
    {
        happiness = (foodEaten + (timesPet * 2)) - (timesFlung + timesWatered);
        FatCalc();
        //SetDebugMessage("Blob / Cat Happiness: " + happiness);
    }

    public void EvolutionCheck()
    {
        if (shouldTime)
        {
            if(phaseTimer <= progressionTimesPerPhase[curPhase])
            {
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

    public void SetNewStats(int requiredInteractions, int overfed, int underfed)
    {
        requiredInteractionsForEvolution = requiredInteractions;
        overfedThresh = overfed;
        underfedThresh = underfed;
    }

    public void DestroyAllFood()
    {
        foreach(GameObject food in foodObjectsOnScreen)
        {
            Destroy(food);
        }

        foodObjectsOnScreen.Clear();
    }

    public void ResetFoodScore()
    {
        foodEaten = 0;
    }




}
