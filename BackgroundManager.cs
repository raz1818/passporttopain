using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    public Sprite normalBackground;
    public Sprite fightBackground;

    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = normalBackground;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game") // your final scene name
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
