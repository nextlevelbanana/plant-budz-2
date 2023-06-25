using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    public static SpriteController Instance;
    public Sprite fishHappy;
    public Sprite fishMad;
    public Sprite fishEh;
    public Sprite catHappy;
    public Sprite catMad;
    public Sprite catEh;
    private Sprite happy;
    private Sprite mad;
    private Sprite eh;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //this is so clumsy but whatever, it's Sunday and I got better things to do than figure out abstractions
        if ((int?)GameManager.instance?.petReaction?.petType == null || (int?)GameManager.instance?.petReaction?.petType == 3) 
        {
            happy = fishHappy;
            mad = fishMad;
            eh = fishEh;
        }  else {
            happy = catHappy;
            mad = catMad;
            eh = catEh;
        }
    }

    public void SetSprite(string mood) {
        GetComponent<SpriteRenderer>().sprite = mood == "happy" 
            ? happy
            : mood == "mad"
                ? mad : eh;
    }
}
