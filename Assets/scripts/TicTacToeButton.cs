using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeButton : MonoBehaviour
{
    public int Index;

    private TicTacToe TicTacToe;
    TextMeshProUGUI buttonText;

    private void Start()
    {
        TicTacToe = transform.parent.GetComponent<TicTacToe>();
        GetComponent<Button>().onClick.AddListener(() => ButtonPress());
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "";
    }

    public void ButtonPress()
    {
        if (TicTacToe.winner == 0)
        {
            if (buttonText.text != "") return;
            if (TicTacToe.turn % 2 == 0)
            {
                buttonText.text = "O";
                TicTacToe.board[Index] = 2;
            }
            else
            {
                buttonText.text = "X";
                TicTacToe.board[Index] = 1;
            }

            TicTacToe.turn++;
            TicTacToe.CheckWinConditions();
        }
    }
}
