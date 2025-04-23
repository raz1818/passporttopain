using UnityEngine;

public class client1 : MonoBehaviour
{
    public SpriteRenderer Image;
    public MainMenu menu;
    void Start()
    {
        Disappear();
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
                menu.Selection();

            }
        }
    }
    public void Disa()
    {
        Debug.Log("disa");
        Disappear();

    }

    public void App()
    {
        Debug.Log("app");
        appear();
    }

    public void appear()
    {
        Image.enabled = true;
    }

    public void Disappear()
    {
        Debug.Log("disappeared");
        Image.enabled = false;
    }
}
