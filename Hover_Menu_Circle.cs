using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class Hover_Menu_Circle : MonoBehaviour
{
    public float seconds;
    private float timer;
    public Camera cam;
    public CircleCollider2D box;
    private void Start()
    {
        cam = Camera.main;
        box.enabled = false;
        seconds = 0;
    }

    [SerializeField] Sprite HiLiSprite;
    [SerializeField] Sprite NormSprite;


    void Update()
    {
        if (seconds == 10)
        {
            box.enabled = true;
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider.transform == transform)
        {

            Sprite2();

        }
        else
        {

            Sprite1();

        }

        seconds++;

    }
    [ContextMenu("Sprite1")]
    void Sprite1()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = NormSprite;
    }
    [ContextMenu("Sprite2")]
    void Sprite2()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = HiLiSprite;
    }
}

