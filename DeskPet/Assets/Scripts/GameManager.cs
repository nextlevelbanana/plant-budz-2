using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //current phase
    //non-player objects on screen (food, dookie, etc.)
    //player objects on screen

    //mutation / progression

    //stats - hunger, happiness, non-descript thing
    public static GameManager instance;
    public HUD display;
    public int curPhase = 0;

    public List<GameObject> staticObjectsOnScreen = new List<GameObject>();
    public List<GameObject> dynamicObjectsOnScreen = new List<GameObject>();


    private void Awake()
    {
        instance = this;
    }


}
