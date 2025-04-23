using NUnit.Framework;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MainMenu : MonoBehaviour
{
    public Appear_Menu1 solo;
    public Appear_Menu1 online;
    public Appear_Menu1 quit;
    public Appear_Menu1 options;
    public Appear_Menu2 volumeUp;
    public Appear_Menu2 volumeDown;
    public Appear_Menu2 back;
    public Appear_Menu2 credits;
    public credit_text credittext;
    public Appear_Selection USA;
    public Appear_Selection UK;
    public Appear_Selection France;
    public Appear_Selection Ireland;
    public Appear_Selection Romania;
    public Appear_Map Map;
    public client1 client1;
    public Host Host;

    public float seconds = 1;
    public float timer = 0;
    public int RandomNo;
    private void Start()
    {
        solo = GameObject.FindGameObjectWithTag("solo").GetComponent<Appear_Menu1>();
        online = GameObject.FindGameObjectWithTag("online").GetComponent<Appear_Menu1>();
        quit = GameObject.FindGameObjectWithTag("quit").GetComponent<Appear_Menu1>();
        options = GameObject.FindGameObjectWithTag("options").GetComponent<Appear_Menu1>();
        volumeUp = GameObject.FindGameObjectWithTag("volumeUp").GetComponent<Appear_Menu2>();
        volumeDown = GameObject.FindGameObjectWithTag("volumeDown").GetComponent<Appear_Menu2>();
        back = GameObject.FindGameObjectWithTag("back").GetComponent<Appear_Menu2>();
        credits = GameObject.FindGameObjectWithTag("credits").GetComponent<Appear_Menu2>();
        credittext = GameObject.FindGameObjectWithTag("credittext").GetComponent<credit_text>();
        USA = GameObject.FindGameObjectWithTag("usa").GetComponent<Appear_Selection>();
        UK = GameObject.FindGameObjectWithTag("uk").GetComponent<Appear_Selection>();
        France = GameObject.FindGameObjectWithTag("france").GetComponent<Appear_Selection>();
        Ireland = GameObject.FindGameObjectWithTag("ireland").GetComponent<Appear_Selection>();
        Romania = GameObject.FindGameObjectWithTag("romanian").GetComponent<Appear_Selection>();
        Map = GameObject.FindGameObjectWithTag("map").GetComponent<Appear_Map>();
        client1 = GameObject.FindGameObjectWithTag("Client").GetComponent<client1>();
        Host = GameObject.FindGameObjectWithTag("Host").GetComponent<Host>();
        int RandomNo = Random.Range(0, 3);


    }
    public void Update()
    {
        if (timer < seconds)
        {
            timer = timer + Time.deltaTime;
            
        }
        else
        {
            RandomNo++;

        }
        if(RandomNo == 3)
        {
            RandomNo = 0;
        }

    }
    public void Options()
    {
        Debug.Log("Communicated");
        solo.Disa();
        online.Disa();
        quit.Disa();
        options.Disa();
        volumeUp.App();
        volumeDown.App();
        back.App();
        credits.App();
    }
    [ContextMenu("AYOO")]
    public void Credits()
    {
        volumeUp.Disa();
        volumeDown.Disa();
        back.Disa();
        credits.Disa();
        credittext.App();
    }
    public void Back()
    {
        Debug.Log("Communicated");
        solo.App();
        online.App();
        quit.App();
        options.App();
        volumeUp.Disa();
        volumeDown.Disa();
        back.Disa();
        credits.Disa();
    }
    public void play()
    {
        Debug.Log("Online");
        solo.Disa();
        online.Disa();
        quit.Disa();
        options.Disa();
        volumeUp.Disa();
        volumeDown.Disa();
        back.Disa();
        credits.Disa();

        client1.App();
        Host.App();
        

    }

    public void Selection()
    {
        client1.Disa();
        Host.Disa();
        USA.App();
        UK.App();
        France.App();
        Ireland.App();
        Romania.App();
    }


    public void Character(int Character)
    {
        USA.Disa();
        UK.Disa();
        France.Disa();
        Ireland.Disa();
        Romania.Disa();
        
        if (RandomNo == 0) { Map.Ireland(); }
        if(RandomNo == 1) { Map.America(); }
        if(RandomNo == 2) { Map.France(); }
        
    }
}
