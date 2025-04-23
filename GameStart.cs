using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public Transform player1Spawn;
    public Transform player2Spawn;

    public GameObject romanianPrefab;
    public GameObject irishPrefab;
    public GameObject americanPrefab;
    public GameObject frenchPrefab;


    void Start()
    {
        //some debug logic to ensure everything works
        Debug.Log("GameStart complete");
        Debug.Log("Player 1 character: " + CharacterSelect.Instance.player1Character);
        Debug.Log("Player 2 character: " + CharacterSelect.Instance.player2Character);
        
        //store p1 character and p2 character
        GameObject p1Char = SpawnCharacter(CharacterSelect.Instance.player1Character, player1Spawn, true);
        GameObject p2Char = SpawnCharacter(CharacterSelect.Instance.player2Character, player2Spawn, false);

        //spawns and stores to wait for opponent
        Movement p1Movement = p1Char.GetComponent<Movement>();
        Movement p2Movement = p2Char.GetComponent<Movement>();

        p1Movement.opponent = p2Char;
        p2Movement.opponent = p1Char;

    }

    GameObject SpawnCharacter(string characterName, Transform spawnPoint, bool isPlayer1)
    {
        
        GameObject prefab = null;

        //select the correct character prefab depending on who we select
        switch (characterName)
        {
            case "Romanian": prefab = romanianPrefab; break;
            case "Irish": prefab = irishPrefab; break;
            case "American": prefab = americanPrefab; break;
            case "French": prefab = frenchPrefab; break;
        }
        if (prefab != null)
        {
            GameObject character = Instantiate(prefab, spawnPoint.position, Quaternion.identity); //spawns character at spawn point

            character.layer = isPlayer1 ? LayerMask.NameToLayer("Player1") : LayerMask.NameToLayer("Player2"); // select layer 1 for player 1 and layer 2 for player 2

            //applies the same for the gun point,kick point, and punch point
            foreach (Transform child in character.transform)
            {
                child.gameObject.layer = character.layer;
            }
            
            //player 1 labeled P1 player 2 labeled P2
            character.name = isPlayer1 ? "P1" : "P2";


            //makes sure character has movement
            Movement movement = character.GetComponent<Movement>();
            movement.characterName = characterName;
            movement.spawnPoint = spawnPoint;

            if (isPlayer1) // if player 1 then assign left hand side of the game for health etc.
            {
                movement.health = GameObject.Find("Healthbar_Front_left").GetComponent<Image>();
                movement.ability = GameObject.Find("Abilitybar_front_left").GetComponent<Image>();
                movement.roundIcons = new Image[] {
                GameObject.Find("Round1Left").GetComponent<Image>(),
                GameObject.Find("Round2Left").GetComponent<Image>()
                };
                movement.characterPortrait = GameObject.Find("P1 Portrait").GetComponent<Image>(); //<< doesn't work <<<<<
            }
            else //if player 2 then assign right hand side of the game
            {
                movement.health = GameObject.Find("Healthbar_Front_right").GetComponent<Image>();
                movement.ability = GameObject.Find("Abilitybar_front_right").GetComponent<Image>();
                movement.roundIcons = new Image[] {
                GameObject.Find("Round1Right").GetComponent<Image>(),
                GameObject.Find("Round2Right").GetComponent<Image>()
                };
                movement.characterPortrait = GameObject.Find("P2 Portrait").GetComponent<Image>(); //<< doesn't work <<<<<
                
            }


            return character;
        }
        return null;
    }

}
