using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public static CharacterSelect Instance;

    public string player1Character;
    public string player2Character;
    public bool player1Ready = false;
    public bool player2Ready = false;

    //important for scene changing ---------
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    //important for scene changing ----------

    //save player 1 and ready
    public void SetPlayer1(string character)
    {
        player1Character = character;
        player1Ready = true;
        Debug.Log("Player 1 selected: " + character);
    }

    //p2 ready
    public void SetPlayer2(string character)
    {
        player2Character = character;
        player2Ready = true;
        Debug.Log("Player 2 selected: " + character);
    }
}
