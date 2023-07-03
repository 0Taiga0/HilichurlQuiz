using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using System.Collections;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private static GameManagerScript gmScript;
    [SerializeField] private Image questionImage;
    [SerializeField] private VideoPlayer questionVideo;
    [SerializeField] private AudioSource questionAudio;
    [SerializeField] private TMP_Text qText;
    [SerializeField] private GameObject audioButton;
    [SerializeField] private Slider questSlider;
    private double timer = 10;
    Questions question;
    public static float questSliderValue = 1;


    private void Update()
    {
        questSliderValue = questSlider.value;
        questionAudio.volume = questSliderValue;
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
                qText.transform.localPosition = new Vector3(0f, 80f, 0f);
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
                questionAudio.Play();
                break;
            case QuestionType.AUDIO:
                QuestionContent();
                qText.transform.localPosition = new Vector3(0f, 122f, 0f);
                questionAudio.transform.gameObject.SetActive(true);
                audioButton.SetActive(true);
                questionAudio.clip = question.questionAudio;
                PlayAudio();
                break;
        }

    }

    public void PlayAudio()
    {
        if (timer > questionAudio.clip.length)
        {
            questionAudio.PlayOneShot(question.questionAudio);
            timer = 0;
        }
        
    }

    //IEnumerator PlayAudio()
    //{
    //    if (question.questionType == QuestionType.AUDIO)
    //    {
    //        questionAudio.PlayOneShot(question.questionAudio);

    //        yield return new WaitForSeconds(question.questionAudio.length + 1f);

    //        StartCoroutine(PlayAudio());
    //    } else
    //    {
    //        StopCoroutine(PlayAudio());
    //        yield return null;
    //    }
    //}

    void QuestionContent()
    {
        qText.transform.localPosition = new Vector3(0f, 185f, 0f);
        questionImage.transform.gameObject.SetActive(false);
        questionVideo.transform.gameObject.SetActive(false);
        questionAudio.transform.gameObject.SetActive(false);
        audioButton.SetActive(false);
    }

}
