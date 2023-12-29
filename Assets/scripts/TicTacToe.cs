using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TicTacToe : MonoBehaviour
{
    [SerializeField] private GameObject ButtonPrefab;

    private GameObject[] Buttons = new GameObject[9];
    DialogueManager dialogueManager;

    public  int[] board = new int[9];

    public int turn = 1;

    public int winner = 0;

    private int[,] winConditions = new int[8, 3]
    {
        {0, 1, 2}, // Horizontal
        {3, 4, 5},
        {6, 7, 8},
        {0, 3, 6}, // Vertical
        {1, 4, 7},
        {2, 5, 8},
        {0, 4, 8}, // Diagonal
        {2, 4, 6}
    };

    void Awake()
    {
        dialogueManager = DialogueManager.GetInstance();
    }

    private void Start()
    {
        for (int i = 0; i < Buttons.Length; i++)
        {
            GameObject newButton = Instantiate(ButtonPrefab, transform);
            Buttons[i] = newButton;
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = "";
            newButton.AddComponent<TicTacToeButton>();
            newButton.GetComponent<TicTacToeButton>().Index = i;
            newButton.name = "Button " + i;
        }
    }

    private void Update()
    {
        while(turn % 2 == 0 && winner == 0 && turn <= 9){
            InputRandom();
        }
    }

    public void CheckWinConditions()
    {
        for (int i = 0; i < winConditions.GetLength(0); i++)
        {
            int a = winConditions[i, 0];
            int b = winConditions[i, 1];
            int c = winConditions[i, 2];

            if (board[a] != 0 && board[a] == board[b] && board[a] == board[c])
            {
                winner = board[a];
                Debug.Log("Player " + winner + " wins!");
                SceneManager.UnloadSceneAsync("TicTacToe");
                dialogueManager.gameObject.SetActive(true);
                dialogueManager.isGameActive = false;
                dialogueManager.LoopOptions();
            }
        }
    }

    void InputRandom()
    {
        int random = Random.Range(0, 9);
        Buttons[random].GetComponent<Button>().onClick.Invoke();
    }
}
