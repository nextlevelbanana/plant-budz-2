using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
using Ink.Runtime;
using TMPro;

public class StoryController : MonoBehaviour
{

    public TextAsset StoryJSON;
    public TextMeshProUGUI displayText;
    public UnityEngine.UI.Button ButtonPrefab;
    public Story story;
    public GameObject canvas;


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        story = new Story(StoryJSON.text);

        Refresh();
           
    }

    void HandleChoice (Choice choice) {
        Debug.Log("chosen");
        story.ChooseChoiceIndex(choice.index);
      
        Refresh();
    }

    void ClearUI() 
    {
        foreach (UnityEngine.UI.Button button in canvas.GetComponentsInChildren<UnityEngine.UI.Button>())
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

            if (story.currentTags.Count > 0)
            {
                var tag = story.currentTags[0];
                SpriteController.Instance.SetSprite(tag);
            }

            Debug.Log(story.currentChoices.Count);

            if (story.currentChoices.Count == 0) {
                UnityEngine.UI.Button choiceButton = Instantiate (ButtonPrefab) as UnityEngine.UI.Button;
		        choiceButton.transform.SetParent (canvas.transform, false);
                
                choiceButton.onClick.AddListener(() => {
                    Refresh();
                });
                var choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                choiceText.text = "...";
            }

            story.currentChoices.ForEach(choice => {
                //Button choiceButton = Instantiate(ButtonPrefab, canvas.transform);
                UnityEngine.UI.Button choiceButton = Instantiate (ButtonPrefab) as UnityEngine.UI.Button;
		        choiceButton.transform.SetParent (canvas.transform, false);
                
                choiceButton.onClick.AddListener(() => {
                    HandleChoice(choice);
                });
                var choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                choiceText.text = choice.text;
            });

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
