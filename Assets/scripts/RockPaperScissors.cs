using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockPaperScissors : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    private TextMeshProUGUI AIText;

    private bool HasFinished = false;

    DialogueManager dialogueManager;

    void Awake()
    {
        dialogueManager = DialogueManager.GetInstance();
    }

    private void Start()
    {
        dropdown = GetComponentInChildren<TMP_Dropdown>();
        AIText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void OnButtonPress()
    {
        if (!HasFinished)
        {
            int playerChoice = dropdown.value;
            int AIChoice = InputRandom();
            string[] choices = { "Rock", "Paper", "Scissors" };
            string result = "";
            if (playerChoice == AIChoice)
            {
                result = "Tie!";
                HasFinished = true;
            }
            else if (playerChoice == 0 && AIChoice == 1)
            {
                result = "You lose!";
                HasFinished = true;
            }
            else if (playerChoice == 0 && AIChoice == 2)
            {
                result = "You win!";
                HasFinished = true;
            }
            else if (playerChoice == 1 && AIChoice == 0)
            {
                result = "You win!";
                HasFinished = true;
            }
            else if (playerChoice == 1 && AIChoice == 2)
            {
                result = "You lose!";
                HasFinished = true;
            }
            else if (playerChoice == 2 && AIChoice == 0)
            {
                result = "You lose!";
                HasFinished = true;
            }
            else if (playerChoice == 2 && AIChoice == 1)
            {
                result = "You win!";
                HasFinished = true;
            }
            AIText.text = "AI chose " + choices[AIChoice] + ". " + result;
        }
        else
        {
            SceneManager.UnloadSceneAsync("RockPaperScissors");
            dialogueManager.gameObject.SetActive(true);
            dialogueManager.isGameActive = false;
            dialogueManager.LoopOptions();
        }
    }

    public int InputRandom()
    {
        int random = Random.Range(0, 3);
        dropdown.value = random;
        AIText.text = dropdown.options[random].text;
        return dropdown.value;
    }
}