using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    private Story story;
    private Font font;
    public UnityEngine.UI.Button buttonPrefab; 
    
    void Start()
    {   
        story = new Story(inkJSONAsset.text);
        font = Resources.Load("Roboto") as Font;
        refresh();
    }

    void ClearUI()
    {
        int childCount = this.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    void refresh() 
    {
        Debug.Log("refreshing");
        ClearUI();

        GameObject newGameObject = new GameObject("TextChunk");
        // Set its transform to the Canvas (this)
        newGameObject.transform.SetParent(this.transform, false);
        // Add a new Text component to the new GameObject
        Text newTextObject = newGameObject.AddComponent<Text>();
        // Set the fontSize larger
        newTextObject.fontSize = 24;
        // Set the text from new story block
        newTextObject.text = getNextStoryBlock();
        // Load Arial from the built-in resources
        newTextObject.font = font;

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab, this.transform);

            // Set listener
            choiceButton.onClick.AddListener(delegate {
                OnClickChoiceButton(choice);
            });

            // Gets the text from the button prefab
            var choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choice.text;
        }
    }

    string getNextStoryBlock()
    {
        string text = "";

        if (story.canContinue)
        {
            text = story.Continue();
            Debug.Log(text);
        } else {
            Debug.Log("no more story");
        }

        return text;
    }

     // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        Debug.Log("clicked! " + choice.index);
        story.ChooseChoiceIndex(choice.index);
        refresh();
    }
}
