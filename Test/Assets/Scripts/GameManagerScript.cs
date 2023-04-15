using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private QuizUI quizUIScript;
    [SerializeField] private QuizDataScriptable quizDataScriptable;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text[] answerText = new TMP_Text[4];
    [SerializeField] Animator animator;
    private List<Questions> questionsList;
    private Questions currentQuestion;
    private int countQuestions = 1;
    private List<int> answers1 = new List<int>();

    void Start() {

        questionsList = quizDataScriptable.questions;

        currentQuestion = questionsList[0];
        questionText.text = currentQuestion.qst;
        for (int i = 0; i < 4; i++)
            answerText[i].text = currentQuestion.answers[i];
        quizUIScript.SetContentQuestion(currentQuestion);

    }

    public void SetQuestion()
    {
        if (countQuestions == questionsList.Count) {

            SceneManager.LoadScene(1);
        }
        else
        {
            currentQuestion = questionsList[countQuestions];
            StartCoroutine(TextCoroutine());
            for (int i = 0; i < 4; i++)
                answerText[i].text = currentQuestion.answers[i];
            countQuestions++;
            quizUIScript.SetContentQuestion(currentQuestion);

        }
    }

    IEnumerator TextCoroutine()
    {
        questionText.text = "";

        foreach (char abs in currentQuestion.qst)
        {
            questionText.text += abs;
            yield return new WaitForSeconds(0.05f);
        }

    }


    public void btnTest(TMP_Text answer)
    {
        if (answer.text == currentQuestion.answers[0]) {
            answers1.Add(0);
            animator.SetTrigger("End");
        }
        else if (answer.text == currentQuestion.answers[1])
        {
            answers1.Add(1);
            answerText[1].GetComponentInParent<Animator>().SetTrigger("End");
        } else if (answer.text == currentQuestion.answers[2])
        {
            answers1.Add(2);
            answerText[2].GetComponentInParent<Animator>().SetTrigger("End");
        } else if (answer.text == currentQuestion.answers[3])
        {
            answers1.Add(3);
            answerText[3].GetComponentInParent<Animator>().SetTrigger("End");
        }
    }
}

