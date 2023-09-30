using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Plugins.Audio.Core;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Sprite audioOn;
    [SerializeField] private Sprite audioOff;
    [SerializeField] private GameObject audioButton;
    [SerializeField] private Slider slider;
    private SourceAudio source;

    public static float sliderValue = 1;

    private void Update()
    {
        if (!source.Mute) { 
            sliderValue = slider.value;
            source.Volume = sliderValue;
        }

    }

    public void OnOffAudion()
    {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
            source.Mute = true;
            audioButton.GetComponent<Image>().sprite = audioOff;
        } else
        {
            AudioListener.volume = 1;
            audioButton.GetComponent<Image>().sprite = audioOn;
            source.Mute = false;
        }
    }

    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        source = objs[0].GetComponent<SourceAudio>();
        if (objs.Length == 1 && SceneManager.GetActiveScene().buildIndex == 0)
        {
            source.Play("BackSound");
            source.Loop = true;
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
