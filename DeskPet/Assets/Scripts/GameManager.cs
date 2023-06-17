using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class GameManager : MonoBehaviour
{
    //mutation / progression

    //stats - hunger, happiness, obscure measurement
    public static GameManager instance;
    public HUD display;
    public int curPhase = 0;

    public List<GameObject> foodObjectsOnScreen = new List<GameObject>();
    public List<GameObject> dynamicObjectsOnScreen = new List<GameObject>();

    public TextMeshProUGUI debugText;

    private void Awake()
    {
        instance = this;
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

        debugText.text = "Hunting: " + closest.name;
        return closest;
    }

    public void RemoveFood(GameObject food)
    {
        foodObjectsOnScreen.Remove(food);
    }


}
