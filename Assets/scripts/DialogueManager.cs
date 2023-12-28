using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject[] buttonList;

    private Story story;
    private static DialogueManager instance;

    private bool isChoiceActive = false;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of DialogueManager found!");
            return;
        }
        foreach (GameObject button in buttonList)
        {
            button.gameObject.SetActive(false);
        }

        StartDialogue(inkJSON);

        instance = this;
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    public void StartDialogue(TextAsset inkJSON)
    {
        story = new Story(inkJSON.text);

        if (story.canContinue)
        {
            dialogueText.text += story.Continue();
            do
            {
                ContinueStory();
            } while (!isChoiceActive);
        }
    }

    public void ContinueStory()
    {
        if (story.canContinue)
        {
            DisplayNextSentence();
        }
        else
        {
            DisplayChoices();
            isChoiceActive = true;
        }
    }

    public void DisplayNextSentence()
    {
        string sentence = story.Continue();
        if (sentence.Equals(""))
        {
            EndDialogue();
        }
        dialogueText.text += sentence;
    }

    public void DisplayChoices()
    {
        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                buttonList[i].gameObject.SetActive(true);
                buttonList[i].GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
            }


        }
    }

    void EndDialogue()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ChooseChoice(int choiceIndex)
    {
        foreach (GameObject button in buttonList)
        {
            button.gameObject.SetActive(false);
        }
        story.ChooseChoiceIndex(choiceIndex);
        isChoiceActive = false;
        do
        {
            ContinueStory();
        } while (!isChoiceActive);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }
}
