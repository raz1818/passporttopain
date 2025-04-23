using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu2 : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Button Click"); //when u click button
        SceneManager.LoadScene("Character Select"); //go char select
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); //when u press quit, quit :D
        Application.Quit();     
    }
}
