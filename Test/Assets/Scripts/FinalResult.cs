using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.Collections;

public class FinalResult : MonoBehaviour
{
    [SerializeField] private ResultDataScriptable resultDataScriptable;
    [SerializeField] private TMP_Text finalText;
    [SerializeField] private Image finalImage;
    private static int[] answers;
    private List<Results> res;
    private Results currentResult;
    private double answerSum = 0;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            res = resultDataScriptable.results;
            SetResult();
        }
    }

    public void SetAnswers(List<int> answers1) { answers = answers1.ToArray(); }

    public void SetResult()
    {
        answerSum = (double)answers.Sum() / 6;
        currentResult = res[(int)Math.Floor(answerSum)];
        finalImage.sprite = currentResult.sprite;
        StartCoroutine(TextCoroutine());
    }

    IEnumerator TextCoroutine()
    {
        finalText.text = "";

        foreach (char abs in currentResult.res)
        {
            finalText.text += abs;
            yield return new WaitForSeconds(0.05f);
        }

    }
}
