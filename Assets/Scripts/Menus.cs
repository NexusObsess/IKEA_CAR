using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [SerializeField] string nextSceneSolo; // assign script to a menu manager game object in scene, writing the scene name EXCATLY that you want the button to trigger

    [SerializeField] string nextMainLevel; // assign script to a menu manager game object in scene, writing the scene name EXCATLY that you want the button to trigger
    [SerializeField] string gameManager; // assign script to a menu manager game object in scene, writing the scene name EXCATLY that you want the button to trigger

    [SerializeField] string sceneToUnload;

    // AudioSource source; // nickname for the text sound effect

    // void Start()
    // {
    //     source = GetComponent<AudioSource>(); // assigns as the audiosource from the game object the script is on
    // }

    public void NextSceneFresh() // load set scene, unload all else
    {
        Debug.Log("New Scene");
        SceneManager.LoadScene(nextSceneSolo);
    }

    // IEnumerator UnloadScene()
    // {
    //     Debug.Log("Unload scene");
    //     AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneToUnload);

    //     yield return unloadOperation;
    // }

    // public void NextMainLevelScene() // load set scene on top of what is already loaded
    // {
    //     Debug.Log("Next Level");
    //     SceneManager.LoadScene(nextMainLevel, LoadSceneMode.Additive);

    //     StartCoroutine(UnloadScene());
    // }

    public void NewScoreKeeper() // load set scene on top of what is already loaded, the score in every scene
    {
        Debug.Log("New Score Keeper");
        SceneManager.LoadScene(gameManager, LoadSceneMode.Additive);
    }

    public void ExitGame() // drag the manager under the on click section on the button game object and select this function under the menus drop down
    {
        Debug.Log("Leave Game :(");
        Application.Quit();
    }

    // public void OpenAudio()
    // {
    //     if (source != null && source.clip != null) // checks if audio source was on game object and if it has an audio clip attached
    //     {
    //         source.Play();
    //     }
    //     else // debugging
    //     {
    //         Debug.LogWarning("No audio source on game object");
    //     }
    // }
}
