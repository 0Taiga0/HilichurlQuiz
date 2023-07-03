using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using System;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private FinalResult fr;
    [SerializeField] private QuizUI quizUIScript;
    [SerializeField] private QuizDataScriptable quizDataScriptable;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text[] answerText = new TMP_Text[4];
    [SerializeField] Animator animator;
    private List<Questions> questionsList;
    private Questions currentQuestion;
    private int countQuestions = 1;
    private List<int> answers1 = new List<int>();

    private bool active = false;

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
        if (countQuestions == questionsList.Count) 
        {
            fr.SetAnswers(answers1);
            SceneManager.LoadScene(2);
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
        active = false;
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
        if (active == false)
        {
            if (answer.text == currentQuestion.answers[0])
            {
                answers1.Add(0);
                animator.SetTrigger("End");
                active = true;
            }
            else if (answer.text == currentQuestion.answers[1])
            {
                answers1.Add(1);
                answerText[1].GetComponentInParent<Animator>().SetTrigger("End");
                active = true;
            }
            else if (answer.text == currentQuestion.answers[2])
            {
                answers1.Add(2);
                answerText[2].GetComponentInParent<Animator>().SetTrigger("End");
                active = true;
            }
            else if (answer.text == currentQuestion.answers[3])
            {
                answers1.Add(3);
                answerText[3].GetComponentInParent<Animator>().SetTrigger("End");
                active = true;
            }
        }
    }
}

