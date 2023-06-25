using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public static SpriteController Instance;
    public Sprite happy;
    public Sprite sad;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSprite(string mood) {
        Debug.Log(mood);
        GetComponent<SpriteRenderer>().sprite = mood == "happy" 
            ? happy
            : sad;
    }
}
