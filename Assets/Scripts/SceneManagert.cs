using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagert : MonoBehaviour
{
    [SerializeField] BoxCollider ExitCollider;
    [SerializeField] GameObject Player;
    [SerializeField] string SceneName;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void OnTriggerEnter(Collider other)
    {
        
            Debug.Log("Leaving Maze");
            SceneManager.LoadScene(SceneName);
        
        
    }
}
