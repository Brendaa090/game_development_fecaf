using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    int currentLevel = 1;
    int totalLevels = 6;
    int score = 0;
    int life = 3;
    int keys = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void StartGame()
    {
        currentLevel = 1;
        keys = 0;
        life = 3;
        SceneManager.LoadScene(currentLevel);
    }

    public void AddKey()
    {
        keys++;
        Debug.Log("🔑 Key added | Keys = " + keys);
    }

    public bool HasKey()
    {
        return keys > 0;
    }

    public void UseKey()
    {
        if (keys > 0)
        {
            keys--;
            Debug.Log("🗝️ Key used | Keys = " + keys);
        }
    }

    public void IncLife()
    { 
        if (life < 3) life++;
    }

    public void DecLife()
    {
        life--;
        keys = 0;

        AudioManager.instance.Play("Hit"); // 🔊 LEVOU DANO

        if (life <= 0)
            SceneManager.LoadScene(totalLevels);
        else
        {
            currentLevel = 1;   // Resetar para fase inicial
            SceneManager.LoadScene(currentLevel);
        }

    }

    public void GoToNextLevel()
    {
        currentLevel++;
        SceneManager.LoadScene(currentLevel);
    }
}
