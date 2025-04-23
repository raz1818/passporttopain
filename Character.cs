using UnityEngine;

public class Character : MonoBehaviour
{
    public MainMenu menu;
    void Start()
    {
        menu = GameObject.FindGameObjectWithTag("Menu1").GetComponent<MainMenu>();
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                Debug.Log("Clicked");
                menu.Character(1);

            }
        }
    }
}
