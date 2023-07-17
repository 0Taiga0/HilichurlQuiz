using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using YG;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] private QuizUI quizUIScript;
    [SerializeField] private QuizDataScriptable quizDataScriptable;
    [SerializeField] private FinalResult fr;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text counter;
    [SerializeField] private TMP_Text[] answerText = new TMP_Text[4];
    [SerializeField] private AudioClip clickButtonSound;
    [SerializeField] private AudioSource clickButtonSource;
    [SerializeField] Animator animator;
    [SerializeField] private GameObject continueButton;
    [SerializeField] GameObject finalRes;
    [SerializeField] GameObject mainCanvas;


    private List<Questions> questionsList;
    private Questions currentQuestion;
    private int countQuestions = 1;
    private int countTrueAnswers = 0;

    private bool active = false;
    private bool coroutineText= false;



    void Start() {

        counter.text = countQuestions.ToString() + "/20";
        finalRes.SetActive(false);
        mainCanvas.SetActive(true);
        countTrueAnswers = 0;
        questionsList = quizDataScriptable.questions;

        currentQuestion = questionsList[0];
        questionText.text = currentQuestion.qst;
        for (int i = 0; i < 4; i++)
            answerText[i].text = currentQuestion.answers[i];
        quizUIScript.SetContentQuestion(currentQuestion);

    }


    public void QuizEnd()
    {
        mainCanvas.SetActive(false);
        finalRes.SetActive(true);

    }

    public void SetQuestion()
    {
        continueButton.SetActive(false);
        coroutineText = true;
        if (countQuestions == questionsList.Count) 
        {
            fr.SetTrue(countTrueAnswers);
            counter.GetComponentInParent<Animator>().SetTrigger("End");
            questionText.GetComponentInParent<Animator>().SetTrigger("End");
            return;
        }
        else
        {
            counter.text = (countQuestions + 1).ToString() + "/20";
            currentQuestion = questionsList[countQuestions];
            StopAllCoroutines();
            StartCoroutine(TextCoroutine());
            for (int i = 0; i < 4; i++)
                answerText[i].text = currentQuestion.answers[i];
            countQuestions++;
            quizUIScript.SetContentQuestion(currentQuestion);
        }
        active = false;
        coroutineText = false;
    }

    IEnumerator TextCoroutine()
    {
        if (coroutineText)
        {
            questionText.text = "";

            foreach (char abs in currentQuestion.qst)
            {
                questionText.text += abs;
                yield return new WaitForSeconds(0.03f);
            }
        } else
        {
            questionText.text = "";

            foreach (char abs in currentQuestion.annotation)
            {
                questionText.text += abs;
                yield return new WaitForSeconds(0.03f);
            }
        }

    }

    public void AudioButton()
    {
        clickButtonSource.PlayOneShot(clickButtonSound);
    }

    public void btnTest(TMP_Text answer)
    {
        AudioButton();
        if (active == false)
        {
            if (answer.text == currentQuestion.answers[0])
            {
                StopCoroutine(TextCoroutine());
                if (answer.text == currentQuestion.trueAnswer)
                {
                    countTrueAnswers++;
                    animator.SetTrigger("True");
                } else
                {
                    animator.SetTrigger("False");
                    StopAllCoroutines();
                    StartCoroutine(TextCoroutine());
                    continueButton.SetActive(true);
                }
            }
            else if (answer.text == currentQuestion.answers[1])
            {
                if (answer.text == currentQuestion.trueAnswer)
                {
                    countTrueAnswers++;
                    answerText[1].GetComponentInParent<Animator>().SetTrigger("True");
                } else
                {
                    answerText[1].GetComponentInParent<Animator>().SetTrigger("False");
                    StopAllCoroutines();
                    StartCoroutine(TextCoroutine());
                    continueButton.SetActive(true);
                }
            }
            else if (answer.text == currentQuestion.answers[2])
            {
                if (answer.text == currentQuestion.trueAnswer)
                {
                    countTrueAnswers++;
                    answerText[2].GetComponentInParent<Animator>().SetTrigger("True");
                } else
                {
                    answerText[2].GetComponentInParent<Animator>().SetTrigger("False");
                    StopAllCoroutines();
                    StartCoroutine(TextCoroutine());
                    continueButton.SetActive(true);
                }

            }
            else if (answer.text == currentQuestion.answers[3])
            {
                if (answer.text == currentQuestion.trueAnswer)
                {
                    countTrueAnswers++;
                    answerText[3].GetComponentInParent<Animator>().SetTrigger("True");
                } else
                {
                    answerText[3].GetComponentInParent<Animator>().SetTrigger("False");
                    StopAllCoroutines();
                    StartCoroutine(TextCoroutine());
                    continueButton.SetActive(true);
                }
            }
            active = true;
        }
    }
}

