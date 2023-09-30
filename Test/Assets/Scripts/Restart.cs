using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Restart : MonoBehaviour
{
    [SerializeField] private SavesData saves;

    public void RestartButton()
    {
        saves.SaveQuestion(0, 0, 0);
        SceneManager.LoadScene(1);
    }
}
