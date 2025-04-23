using UnityEngine;
using UnityEngine.SceneManagement;

public class LockInChar : MonoBehaviour
{
    public void SelectCharacterForPlayer1(string characterName)
    {
        CharacterSelect.Instance.SetPlayer1(characterName); // when you click on a character it selects it for player 1
    }

    public void SelectCharacterForPlayer2(string characterName)
    {
        CharacterSelect.Instance.SetPlayer2(characterName); // same for player 2
    }

    public void Player2SelectScreen()
    {
        if (CharacterSelect.Instance.player1Ready)
        {
            Debug.Log("Player 1 ready");
            SceneManager.LoadScene("Player2CharacterSelect"); // if player 1 ready, move on to player 2 select
        }
    }
    public void StartMatch()
    {
        if (CharacterSelect.Instance.player1Ready && CharacterSelect.Instance.player2Ready)
        {
            SceneManager.LoadScene("Game"); //if both player ready start the game
        }
        else
        {
            Debug.Log("Both players must select a character first!");
        }
    }
}
