using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagert : MonoBehaviour
{
    [SerializeField] BoxCollider ExitCollider;
    [SerializeField] GameObject Player;
  

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            Debug.Log("Leaving Maze");
            SceneManager.LoadScene("Third level building");
        }
        else
        {
            Debug.Log("NO NO");
        }
    }
}
