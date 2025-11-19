using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void StartGame()
    {
        AudioManager.instance.Play("GameSound");
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Debug.Log("Sair do jogo");
        Application.Quit();
    }
}
