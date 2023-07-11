using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Sprite audioOn;
    [SerializeField] private Sprite audioOff;
    [SerializeField] private GameObject audioButton;

    [SerializeField] private Slider slider;

    [SerializeField] private AudioClip clip;
    private AudioSource source;

    public static float sliderValue = 1;

    private void Update()
    {
        sliderValue = slider.value;
        source.volume = sliderValue;
    }

    public void OnOffAudion()
    {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
            audioButton.GetComponent<Image>().sprite = audioOff;
        } else
        {
            AudioListener.volume = 1;
            audioButton.GetComponent<Image>().sprite = audioOn;
        }
    }

    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        source = objs[0].GetComponent<AudioSource>();
        source.loop = true;
        if (objs.Length == 1 && SceneManager.GetActiveScene().buildIndex == 0)
        {
            source.clip = clip;
            source.Play();
        }
        for (int i = 1; i < objs.Length; i++)
        {
            Destroy(objs[i].gameObject);
        }
        slider.value = sliderValue;
        if (AudioListener.volume == 0)
            audioButton.GetComponent<Image>().sprite = audioOff;

    }
}
