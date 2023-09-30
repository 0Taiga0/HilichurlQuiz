using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Collections;
using YG;
using Plugins.Audio.Core;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] private QuizUI quizUIScript;
    [SerializeField] private QuizDataScriptable quizDataScriptable;
    [SerializeField] private FinalResult fr;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text counter;
    [SerializeField] private TMP_Text message;
    [SerializeField] private TMP_Text[] answerText = new TMP_Text[4];
    [SerializeField] private SourceAudio clickButtonSource;
    [SerializeField] Animator animator;
    [SerializeField] Animator animatorL;
    [SerializeField] private GameObject continueButton;
    [SerializeField] GameObject finalRes;
    [SerializeField] GameObject mainCanvas;
    [SerializeField] GameObject buttonNext;
    [SerializeField] GameObject buttonNext1;
    [SerializeField] GameObject buttonStartText;
    [SerializeField] GameObject applyButton;
    [SerializeField] GameObject[] answersFalse;
    [SerializeField] TMP_InputField inputAnswer;
    [SerializeField] TNVirtualKeyboard keyboard;
    [SerializeField] GameObject image;
    [HideInInspector] public int buttonNum = 0;

    [SerializeField] private YandexGame sdk;
    [SerializeField] private SavesData saves;


    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject[] settingsButtons;



    private List<Questions> questionsList;
    private Questions currentQuestion;
    private int countQuestions = 0;
    private int countTrueAnswers = 0;
    private int condition = 0;

    private bool active = false;



    void Start() {
        if (YandexGame.EnvironmentData.deviceType != "desktop")
        {
            for (int i = 0; i < buttons.Length - 2; i++)
            {
                buttons[i].transform.localScale = new Vector3(1.3f, 1.3f, 1);
            }
            buttons[7].transform.localScale = new Vector3(1.5f, 1.5f, 1);
            buttons[8].transform.localScale = new Vector3(1.5f, 1.5f, 1);

            settingsButtons[0].transform.localScale = new Vector3(1.3f, 1.3f, 1);
            settingsButtons[1].transform.localScale = new Vector3(1.5f, 1.5f, 1);
            settingsButtons[2].transform.localScale = new Vector3(1.3f, 1.3f, 1);
            settingsButtons[3].transform.localScale = new Vector3(1f, 1.5f, 1);
            settingsButtons[4].transform.localScale = new Vector3(1.5f, 1.5f, 1);
            settingsButtons[5].transform.localScale = new Vector3(1f, 1.5f, 1);
            settingsButtons[6].transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }

        var b = saves.LoadQuestion();
        countQuestions = b[0];
        countTrueAnswers = b[1];
        condition = b[2];

        counter.text = "1/100";
        finalRes.SetActive(false);
        questionsList = quizDataScriptable.questions;


        if (countQuestions == 0)
        {
            message.gameObject.SetActive(true);
            message.text = "Дорогой игрок, ты начинаешь проходить викторину!\n\r" +
                    "Я надеюсь, количество вопросов тебя не смутит, и ты осилишь их все! \n\rПомни, если ты " +
                    "ошибешься, то всегда сможешь увидеть правильный ответ!\n\rУдачи!";
            mainCanvas.SetActive(false);
            buttonStartText.SetActive(true);
        }
        else
        {
            StartSetQuest();
        }

        

    }
    public int GetCurQ()
    {
        return countQuestions;
    }


    public void QuizEnd()
    {
        mainCanvas.SetActive(false);
        finalRes.SetActive(true);

    }

    public void Next()
    {
        message.gameObject.SetActive(false);
        mainCanvas.SetActive(true);
        buttonNext.SetActive(false);
        currentQuestion = questionsList[0];
        questionText.text = currentQuestion.qst;
        for (int i = 0; i < 4; i++)
            answerText[i].text = currentQuestion.answers[i];
        quizUIScript.SetContentQuestion(currentQuestion);
        countQuestions++;
    }

    public void Next1()
    {
        message.gameObject.SetActive(false);
        mainCanvas.SetActive(true);
        buttonNext1.SetActive(false);

    }

    public void StartMessage()
    {
        buttonNext.SetActive(true);
        buttonStartText.SetActive(false);
        message.text = "Вы - обычный хиличурл своего маленького племени в Мондштадте! " +
            "Вам предстоит пройти специальный экзамен - ЕГЭ по Хиличурловедению! " +
            "Этот экзамен определит, какую роль вы будете занимать в своем племени - будете вы " +
            "обычным хиличурлом или займете место высших чинов - шамачурла или лавачурла! Удачи!";
    }
    
    public void StartSetQuest()
    {
        if (countQuestions == questionsList.Count)
        {
            fr.SetTrue(countTrueAnswers);
            image.SetActive(false);
            counter.GetComponentInParent<Animator>().SetTrigger("End");
            questionText.GetComponentInParent<Animator>().SetTrigger("End");
            return;
        }

        if (countQuestions >= 90 && answersFalse[0].activeSelf == true)
        {
            for (int i = 0; i < 4; i++)
            {
                answersFalse[i].SetActive(false);
            }
            continueButton.transform.localPosition = new Vector2(-409, -426);
            applyButton.SetActive(true);
            inputAnswer.gameObject.SetActive(true);
        }
        else if (answersFalse[0].activeSelf == false && countQuestions < 90)
        {
            for (int i = 0; i < 4; i++)
            {
                answersFalse[i].SetActive(true);
            }
            continueButton.transform.localPosition = new Vector2(0, 114);
            applyButton.SetActive(false);
            inputAnswer.gameObject.SetActive(false);


        }

        if (countQuestions >= 90)
        {
            applyButton.SetActive(true);
            counter.text = (countQuestions + 1).ToString() + "/100";
            currentQuestion = questionsList[countQuestions];
            StopAllCoroutines();
            StartCoroutine(TextCoroutine(currentQuestion.qst));
            countQuestions++;
            quizUIScript.SetContentQuestion(currentQuestion);

            if (condition != 0)
            {
                animatorL.SetTrigger("False");
                StopAllCoroutines();
                StartCoroutine(TextCoroutine(currentQuestion.annotation));
                continueButton.SetActive(true);
                applyButton.SetActive(false);
                active = true;
            }

        }

        if (countQuestions < 90)
        {
            counter.text = (countQuestions + 1).ToString() + "/100";
            currentQuestion = questionsList[countQuestions];
            StopAllCoroutines();
            StartCoroutine(TextCoroutine(currentQuestion.qst));
            for (int i = 0; i < 4; i++)
                answerText[i].text = currentQuestion.answers[i];
            countQuestions++;
            quizUIScript.SetContentQuestion(currentQuestion);

            if (condition == 1)
            {
                animator.SetTrigger("False");
                StopAllCoroutines();
                StartCoroutine(TextCoroutine(currentQuestion.annotation));
                continueButton.SetActive(true);
                active = true;
            }
            else if (condition == 2)
            {
                answerText[1].GetComponentInParent<Animator>().SetTrigger("False");
                StopAllCoroutines();
                StartCoroutine(TextCoroutine(currentQuestion.annotation));
                continueButton.SetActive(true);
                active = true;
            }
            else if (condition == 3)
            {
                answerText[2].GetComponentInParent<Animator>().SetTrigger("False");
                StopAllCoroutines();
                StartCoroutine(TextCoroutine(currentQuestion.annotation));
                continueButton.SetActive(true);
                active = true;
            }
            else if (condition == 4)
            {
                answerText[3].GetComponentInParent<Animator>().SetTrigger("False");
                StopAllCoroutines();
                StartCoroutine(TextCoroutine(currentQuestion.annotation));
                continueButton.SetActive(true);
                active = true;
            }
        }

    }

    public void SetQuestion()
    {
        condition = 0;
        saves.SaveQuestion(countQuestions, countTrueAnswers, condition);
        if (countQuestions == questionsList.Count)
        {
            fr.SetTrue(countTrueAnswers);
            counter.GetComponentInParent<Animator>().SetTrigger("End");
            questionText.GetComponentInParent<Animator>().SetTrigger("End");
            return;
        }

        if (countQuestions >= 90 && answersFalse[0].activeSelf == true)
        {
            for (int i = 0; i < 4; i++)
            {
                answersFalse[i].SetActive(false);
            }
            continueButton.transform.localPosition = new Vector2(-409, -426);
            applyButton.SetActive(true);
            inputAnswer.gameObject.SetActive(true);
        }
        else if (answersFalse[0].activeSelf == false && countQuestions < 90)
        {
            for (int i = 0; i < 4; i++)
            {
                answersFalse[i].SetActive(true);
            }
            continueButton.transform.localPosition = new Vector2(0, 114);
            applyButton.SetActive(false);
            inputAnswer.gameObject.SetActive(false);
        }

        if (countQuestions >= 90)
        {
            applyButton.SetActive(true);
            counter.text = (countQuestions + 1).ToString() + "/100";
            currentQuestion = questionsList[countQuestions];
            StopAllCoroutines();
            StartCoroutine(TextCoroutine(currentQuestion.qst));
            countQuestions++;
            quizUIScript.SetContentQuestion(currentQuestion);

        }

        if (countQuestions == 20)
        {
            StopAllCoroutines();
            buttonNext1.SetActive(true);
            message.gameObject.SetActive(true);
            message.text = "Вы прошли первый блок теста: Хиличурлы!\n\r Следующий блок - Персонажи!";
            mainCanvas.SetActive(false);
        } else if (countQuestions == 55) {
            StopAllCoroutines();
            buttonNext1.SetActive(true);
            message.gameObject.SetActive(true);
            message.text = "Вы прошли второй блок теста: Персонажи!\n\r Следующий блок - Тейват!";
            mainCanvas.SetActive(false);
        }
        continueButton.SetActive(false);

        if (countQuestions < 90)
        {
            counter.text = (countQuestions + 1).ToString() + "/100";
            currentQuestion = questionsList[countQuestions];
            StopAllCoroutines();
            StartCoroutine(TextCoroutine(currentQuestion.qst));
            for (int i = 0; i < 4; i++)
                answerText[i].text = currentQuestion.answers[i];
            countQuestions++;
            quizUIScript.SetContentQuestion(currentQuestion);
        }

        active = false;
    }

    public void checkAnswer()
    {
        if (inputAnswer.text.ToLower() == currentQuestion.trueAnswer)
        {
            applyButton.SetActive(false);
            countTrueAnswers++;
            animatorL.SetTrigger("True");
            condition = 0;
        }
        else
        {
            animatorL.SetTrigger("False");
            StopAllCoroutines();
            StartCoroutine(TextCoroutine(currentQuestion.annotation));
            continueButton.SetActive(true);
            applyButton.SetActive(false);
            condition = 1;
            saves.SaveCond(condition);
        }
    }

    IEnumerator TextCoroutine(string text)
    {
        questionText.text = "";

        foreach (char abs in text)
        {
            questionText.text += abs;
            yield return new WaitForSeconds(0.02f);
        }
    }

    public void AdButton()
    {
        sdk._RewardedShow(0);
    }

    public void RewardAd()
    {
        keyboard.HideVirtualKeyboard();
        StopAllCoroutines();
        StartCoroutine(TextCoroutine(currentQuestion.annotation));
    }




    public void AudioButton()
    {
        clickButtonSource.PlayOneShot("ButtonSound");
    }

    public void btnTest(TMP_Text answer)
    {
        AudioButton();
        if (active == false)
        {
            if (answer.text == currentQuestion.answers[0])
            {
                StopCoroutine(TextCoroutine(currentQuestion.annotation));
                if (answer.text == currentQuestion.trueAnswer)
                {
                    countTrueAnswers++;
                    animator.SetTrigger("True");
                    condition = 0;
                }
                else
                {
                    animator.SetTrigger("False");
                    StopAllCoroutines();
                    StartCoroutine(TextCoroutine(currentQuestion.annotation));
                    continueButton.SetActive(true);
                    condition = 1;
                    saves.SaveCond(condition);
                }
            }
            else if (answer.text == currentQuestion.answers[1])
            {
                if (answer.text == currentQuestion.trueAnswer)
                {
                    countTrueAnswers++;
                    answerText[1].GetComponentInParent<Animator>().SetTrigger("True");
                    condition = 0;
                } else
                {
                    answerText[1].GetComponentInParent<Animator>().SetTrigger("False");
                    StopAllCoroutines();
                    StartCoroutine(TextCoroutine(currentQuestion.annotation));
                    continueButton.SetActive(true);
                    condition = 2;
                    saves.SaveCond(condition);
                }
            }
            else if (answer.text == currentQuestion.answers[2])
            {
                if (answer.text == currentQuestion.trueAnswer)
                {
                    countTrueAnswers++;
                    answerText[2].GetComponentInParent<Animator>().SetTrigger("True");
                    condition = 0;
                } else
                {
                    answerText[2].GetComponentInParent<Animator>().SetTrigger("False");
                    StopAllCoroutines();
                    StartCoroutine(TextCoroutine(currentQuestion.annotation));
                    continueButton.SetActive(true);
                    condition = 3;
                    saves.SaveCond(condition);
                }

            }
            else if (answer.text == currentQuestion.answers[3])
            {
                if (answer.text == currentQuestion.trueAnswer)
                {
                    countTrueAnswers++;
                    answerText[3].GetComponentInParent<Animator>().SetTrigger("True");
                    condition = 0;
                } else
                {
                    answerText[3].GetComponentInParent<Animator>().SetTrigger("False");
                    StopAllCoroutines();
                    StartCoroutine(TextCoroutine(currentQuestion.annotation));
                    continueButton.SetActive(true);
                    condition = 4;
                    saves.SaveCond(condition);
                }
            }
            active = true;
        }
    }
}

