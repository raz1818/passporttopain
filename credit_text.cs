using UnityEngine;

public class credit_text : MonoBehaviour
{
    public SpriteRenderer Image;
    void Start()
    {
        Disappear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void App()
    {
        Appear();
    }
    public void Disa()
    {
        Disappear();
    }

    void Appear()
    {
        Image.enabled = true;
    }

    void Disappear()
    {
        Image.enabled = false;
    }
}
