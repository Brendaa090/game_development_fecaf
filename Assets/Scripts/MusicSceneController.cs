using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSceneController : MonoBehaviour
{
    void Start()
    {
        PlayMusicForScene(SceneManager.GetActiveScene().buildIndex);
    }

    void PlayMusicForScene(int sceneIndex)
    {
        if (sceneIndex == 0) // Menu
        {
            AudioManager.instance.Play("MenuSound");
        }
        else if (sceneIndex > 0 && sceneIndex < 6) // Fases
        {
            AudioManager.instance.Play("GameSound");
        }
        else if (sceneIndex == 6) // Cena final
        {
            AudioManager.instance.Play("TheEndSound");
        }
    }
}
