using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{
    public static SpriteController Instance;
    /*public Sprite fishHappy;
    public Sprite fishMad;
    public Sprite fishEh;
    public Sprite catHappy;
    public Sprite catMad;
    public Sprite catEh;
    private Sprite happy;
    private Sprite mad;
    private Sprite eh;*/
    public Image fishHappy;
    public Image fishMad;
    public Image fishEh;
    public Image catHappy;
    public Image catMad;
    public Image catEh;
    private Image happy;
    private Image mad;
    private Image eh;
    private Image curImage;
    private bool isFish = false;
    private void Awake()
    {
        Instance = this;
        curImage = GetComponent<Image>();
    }

    void Start()
    {

        if (GameManager.instance.finalForm == 3) 
        {
            happy = fishHappy;
            mad = fishMad;
            eh = fishEh;
        } 
        else
        {
            happy = catHappy;
            mad = catMad;
            eh = catEh;
        }

        SetImage("eh");
    }

    public void SetImage(string mood)
    {
        if(mood == "happy")
        {
            curImage.sprite = happy.sprite;
            HappySFX();
        }

        if(mood == "mad")
        {
            curImage.sprite = mad.sprite;
            MadSFX();
        }

        if(mood == "eh")
        {
            curImage.sprite = eh.sprite;
        }
    }

    private void HappySFX()
    {
        AudioManager.instance.PlaySFX(2);
    }

    private void MadSFX()
    {
        float dice = Random.Range(0, 20);
        if(dice < 9) { AudioManager.instance.PlaySFX(3); return; }

        if (isFish) { AudioManager.instance.PlaySFX(0); return; }

        AudioManager.instance.PlaySFX(1);
    }

    /*public void SetSprite(string mood) {
        GetComponent<SpriteRenderer>().sprite = mood == "happy" 
            ? happy
            : mood == "mad"
                ? mad : eh;
    }*/
}
