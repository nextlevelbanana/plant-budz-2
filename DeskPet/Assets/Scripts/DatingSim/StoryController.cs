using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Ink.Runtime;
using TMPro;

public class StoryController : MonoBehaviour
{

    public TextAsset StoryJSON;
    public TextAsset CatStoryJSON;
    public TextMeshProUGUI displayText;
    public TextMeshProUGUI speakerText;
    //public UnityEngine.UI.Button ButtonPrefab;

    public Story story;
    public GameObject buttonPanel;

    public GameObject testButton;
    public bool testing = true;

    private bool ignore = true;

    public Transform[] threeButtonLayout;
    public Transform[] twoButtonLayout;
    private int curButtonPlace = 0;


    void Start()
    {
        story = new Story((int?) GameManager.instance?.petReaction.petType == 2 
            ? CatStoryJSON.text
            : StoryJSON.text);

        Refresh();
    }

    public void HandleChoice (Choice choice)
    {
        if (choice == null) { Refresh(); return; }

        story.ChooseChoiceIndex(choice.index);

        Refresh();
    }

    void ClearUI() 
    {
        /*foreach (UnityEngine.UI.Button button in buttonPanel.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            Destroy(button.gameObject);
        }*/

        foreach(ButtonTag button in buttonPanel.GetComponentsInChildren<ButtonTag>())
        {
            Destroy(button.gameObject);
        }
    }

    public void Refresh()
    {
        ClearUI();
        
        if (story.canContinue)
        {
            var currentText = story.Continue();
            var currentTags = story.currentTags;
            displayText.text = currentText;

            HandleTags();

            if (story.currentChoices.Count == 0) 
            {

                if (testing)
                {
                    GameObject choiceButton = Instantiate(testButton, threeButtonLayout[1], false);
                    var choiceText = choiceButton.GetComponent<ButtonTag>().choiceTxt;
                    choiceText.text = "...";
                }

                /*else
                {
                    UnityEngine.UI.Button choiceButton = Instantiate(ButtonPrefab, buttonPanel.transform, false);

                    choiceButton.onClick.AddListener(() => { Refresh(); });

                    var choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                    choiceText.text = "...";
                }*/



            }

            bool is3 = false;

            if (story.currentChoices.Count > 2)
            {
                is3 = true;
                //3 button layout
            }

            story.currentChoices.ForEach(choice => {

                if (testing)
                {
                    GameObject choiceButton = null;
                    if (is3)
                    {
                        choiceButton = Instantiate(testButton, threeButtonLayout[curButtonPlace], false);
                    }
                    else
                    {
                        choiceButton = Instantiate(testButton, twoButtonLayout[curButtonPlace], false);
                    }

                    curButtonPlace++;

                    choiceButton.GetComponent<ButtonTag>().SetChoice(choice);

                    var choiceText = choiceButton.GetComponent<ButtonTag>().choiceTxt;

                    choiceText.text = choice.text;
                }
                /*else
                {
                    UnityEngine.UI.Button choiceButton = Instantiate(ButtonPrefab, buttonPanel.transform, false); //as UnityEngine.UI.Button;

                    choiceButton.onClick.AddListener(() => {
                        HandleChoice(choice);
                    });

                    var choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                    choiceText.text = choice.text;
                }*/

                

            });
            curButtonPlace = 0;
        }
    }

    void HandleTags()
    {
        foreach (var t in story.currentTags)
        {
            var tag = t.Split(".");

            Debug.Log(tag);

            if (tag[0] == "who")
            {
                SetSpeaker(tag[1]);

            }
            else if (tag[0] == "mood")
            {
                if (ignore) { ignore = false; return; }
                //The first time set image is called happens before sprite controller is fully set up
                //gives a null ref and won't execute button code :/
                //ignore = temp workaround

                //SpriteController.Instance.SetSprite(tag[1]);
                SpriteController.Instance.SetImage(tag[1]);
            }
        }
    }

    void SetSpeaker(string who)
    {
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        speakerText.text = textInfo.ToTitleCase(who);
    }
}
