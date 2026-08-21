using UnityEngine;
using UnityEngine.SceneManagement;

public class ExittoTitle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>() != null)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
