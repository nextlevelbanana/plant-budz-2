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
    public UnityEngine.UI.Button ButtonPrefab;
    public Story story;
    public GameObject buttonPanel;

    //see implementation for notes
    private bool ignore = true;

    void Start()
    {
        story = new Story((int?) GameManager.instance?.petReaction.petType == 2 
            ? CatStoryJSON.text
            : StoryJSON.text);

        Refresh();
    }

    void HandleChoice (Choice choice) {
        story.ChooseChoiceIndex(choice.index);
      
        Refresh();
    }

    void ClearUI() 
    {
        foreach (UnityEngine.UI.Button button in buttonPanel.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            Destroy(button.gameObject);
        }
    }

    void Refresh()
    {
        ClearUI();
        
        if (story.canContinue)
        {
            var currentText = story.Continue();
            var currentTags = story.currentTags;
            displayText.text = currentText;

            HandleTags();

            if (story.currentChoices.Count == 0) {
                UnityEngine.UI.Button choiceButton = Instantiate(ButtonPrefab, buttonPanel.transform, false); //as UnityEngine.UI.Button;
                //choiceButton.transform.SetParent(canvas.transform, false);

                choiceButton.onClick.AddListener(() => {
                    Refresh();
                });
                var choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                choiceText.text = "...";
            }

            story.currentChoices.ForEach(choice => {
                //Button choiceButton = Instantiate(ButtonPrefab, canvas.transform);
                UnityEngine.UI.Button choiceButton = Instantiate(ButtonPrefab, buttonPanel.transform, false); //as UnityEngine.UI.Button;
		        //choiceButton.transform.SetParent(canvas.transform, false);
                
                choiceButton.onClick.AddListener(() => {
                    HandleChoice(choice);
                });
                var choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                choiceText.text = choice.text;
            });

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
