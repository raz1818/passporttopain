using System.Threading;
using UnityEngine;

public class Appear_Logo : MonoBehaviour
{
    public float timer = 0;
    public GameObject Logo;
    public float seconds = 3;
    void Start()
    {
        Logo.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < seconds)
        {
            timer = timer + Time.deltaTime;
        } else
        {
            Logo.SetActive(false);
        }
    }
}
