using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using System.Collections;
using YG;

public class FinalResult : MonoBehaviour
{
    [SerializeField] private ResultDataScriptable resultDataScriptable;
    [SerializeField] private TMP_Text finalText;
    [SerializeField] private TMP_Text finalName;
    [SerializeField] private TMP_Text ThxTxt;
    [SerializeField] private Image finalImage;
    [SerializeField] private AudioSource finalSound;
    [SerializeField] private AudioClip clip;
    [SerializeField] private SavesData saveData;
    private int answersTrue = 0;
    private List<Results> res;
    private Results currentResult;



    private void Start()
    {
        finalSound.PlayOneShot(clip);
        res = resultDataScriptable.results;
        SetResult();
    }

    public void SetTrue(int answersTrue1) { answersTrue = answersTrue1; }

    public void SetResult()
    {
        if (answersTrue < 5)
        {
            currentResult = res[0];
            saveData.Save(0);
        } else if (answersTrue < 10)
        {
            currentResult = res[1];
            saveData.Save(1);
        }
        else if (answersTrue < 13)
        {
            currentResult = res[2];
            saveData.Save(2);
        }
        else if (answersTrue < 16)
        { 
            currentResult = res[3];
            saveData.Save(3);
        }
        else if (answersTrue < 19)
        {
            currentResult = res[4];
            saveData.Save(4);
        }
        else
        {
            currentResult = res[5];
            saveData.Save(5);
        }
        finalImage.sprite = currentResult.sprite;
        finalName.text = currentResult.name;
        StartCoroutine(TextCoroutine(finalText, currentResult.res));
        StartCoroutine(TextCoroutine(ThxTxt, "Спасибо за прохождение теста <3"));
    }

    IEnumerator TextCoroutine(TMP_Text text, string necTxt)
    {
        text.text = "";

        foreach (char abs in necTxt)
        {
            text.text += abs;
            yield return new WaitForSeconds(0.03f);
        }

    }
}
