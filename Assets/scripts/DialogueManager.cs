using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    [Header("Dialogue UI")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private GameObject[] buttonList;

    private Story story;
    private static DialogueManager instance;

    private bool isChoiceActive = false;
    public bool isGameActive = false;

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
            LoopOptions();
        }
    }

    public void ContinueStory()
    {
        if (story.canContinue)
        {
            Debug.Log(story.variablesState["Game"]);
            if (story.variablesState["Game"] != null)
            {
                if (!story.variablesState["Game"].ToString().Equals(""))
                {
                    string game = story.variablesState["Game"].ToString();
                    if (game.Equals("RockPaperScissors"))
                    {
                        StartRockPaperScissors();
                    }
                    else if (game.Equals("TicTacToe"))
                    {
                        StartTicTacToe();
                    }
                }
            }
            else
            {
                return;
            }

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
        if (story.variablesState["Replay"].ToString().Equals("ReplayTrue"))
        {
            dialogueText.text = "";
        }
        {string sentence = story.Continue();
        if (sentence.Equals(""))
        {
            EndDialogue();
        }
        dialogueText.text += sentence;}
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
        LoopOptions();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    void StartRockPaperScissors()
    {
        SceneManager.LoadScene("RockPaperScissors", LoadSceneMode.Additive);
        gameObject.SetActive(false);
        isGameActive = true;
        object name = "";

        story.variablesState["Game"] = name;
    }

    void StartTicTacToe()
    {
        SceneManager.LoadScene("TicTacToe", LoadSceneMode.Additive);
        gameObject.SetActive(false);
        isGameActive = true;
        object name = "";

        story.variablesState["Game"] = name;

    }

    public void LoopOptions()
    {
        while (!isChoiceActive && !isGameActive)
        {
            ContinueStory();
        }
    }
}
