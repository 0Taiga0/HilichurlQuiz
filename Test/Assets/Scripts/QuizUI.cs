using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using Plugins.Audio.Core;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private static GameManagerScript gmScript;
    [SerializeField] private Image questionImage;
    [SerializeField] private VideoPlayer questionVideo;
    [SerializeField] private SourceAudio questionAudio;
    [SerializeField] private AudioSource questionAudio1;
    [SerializeField] private TMP_Text qText;
    [SerializeField] private GameObject audioButton;
    [SerializeField] private Slider questSlider;
    private double timer = 10;
    Questions question;
    public static float questSliderValue = 1;

    private void Start()
    {
        questSlider.value = questSliderValue;
        questionAudio.Volume = questSliderValue;
    }

    private void Update()
    {
        questSliderValue = questSlider.value;
        questionAudio.Volume = questSliderValue;
        timer += Time.deltaTime;
    }

    public void SetContentQuestion(Questions question)
    {
        QuestionContent();
        this.question = question;

        switch (question.questionType)
        {
            case QuestionType.TEXT:
                QuestionContent();
                qText.transform.localPosition = new Vector3(0f, 385f, 0f);
                break;
            case QuestionType.IMAGE:
                QuestionContent();
                questionImage.transform.gameObject.SetActive(true);
                questionImage.sprite = question.questionImg;
                break;
            case QuestionType.VIDEO:
                QuestionContent();
                questionVideo.transform.gameObject.SetActive(true);
                questionVideo.clip = question.questionVideo;
                //questionAudio.Play();
                break;
            case QuestionType.AUDIO:
                QuestionContent();
                qText.transform.localPosition = new Vector3(0f, 310f, 0f);
                questionAudio.transform.gameObject.SetActive(true);
                audioButton.SetActive(true);
                questionAudio1.clip = question.questionAudio;
                timer = 10;
                PlayAudio();
                break;
        }

    }

    public void PlayAudio()
    {
        if (timer > questionAudio1.clip.length)
        {
            questionAudio.PlayOneShot(question.questionAudio.name);
            timer = 0;
        }
        
    }

    void QuestionContent()
    {
        qText.transform.localPosition = new Vector3(0f, 400f, 0f);
        questionImage.transform.gameObject.SetActive(false);
        questionVideo.transform.gameObject.SetActive(false);
        questionAudio.transform.gameObject.SetActive(false);
        audioButton.SetActive(false);
    }

}
