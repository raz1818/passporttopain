using UnityEngine;

public class Appear_Map : MonoBehaviour
{
    public Transform scale;

    [SerializeField] Sprite Map;
    [SerializeField] Sprite ireland;
    [SerializeField] Sprite america;
    [SerializeField] Sprite france;
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Map;
        scale.localScale = new Vector3(2.8f, 2.9f, 1);
    }

    public void Ireland()   
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ireland;
        scale.localScale = new Vector3(1, 1, 1);
    }
    public void America()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = america;
        scale.localScale = new Vector3(1, 1, 1);
    }
    public void France()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = france;
        scale.localScale = new Vector3(1, 1, 1);
    }
}
