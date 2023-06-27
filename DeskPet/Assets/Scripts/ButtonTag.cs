using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class ButtonTag : MonoBehaviour
{
    public int buttonNum;

    public bool isDatingSim = false;

    public Choice choice;

    private StoryController story;
    private void Start()
    {
        if (isDatingSim) { story = FindObjectOfType<StoryController>(); }
    }

    public void SetChoice(Choice c)
    {
        choice = c;
    }

    public void HandleChoice()
    {
        story.HandleChoice(choice);
        story.Refresh();
    }
    
}
