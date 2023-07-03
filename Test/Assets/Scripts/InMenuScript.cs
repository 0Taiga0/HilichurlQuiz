using UnityEngine;
using UnityEngine.SceneManagement;

public class InMenuScript : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
