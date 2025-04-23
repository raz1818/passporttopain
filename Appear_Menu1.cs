using UnityEngine;

public class Appear_Menu1 : MonoBehaviour
{
    
    public SpriteRenderer Image;
    public float timer = 0;
    public float seconds = 3;
    public bool hat = true;
    
    void Start()
    {

        
        Disappear();
    }

    // Update is called once per frame
    void Update()
    {
        if (hat)
        {
            if (timer < seconds)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                appear();
                hat = !hat;
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

    public  void appear()
    {
        Image.enabled = true;
    }

    public  void Disappear()
    {
        Debug.Log("disappeared");
        Image.enabled = false;
    }



}
