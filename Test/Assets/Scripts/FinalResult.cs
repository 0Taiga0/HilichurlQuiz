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
    [SerializeField] private TMP_Text finalName;
    [SerializeField] private Image finalImage;
    [SerializeField] private AudioSource finalSound;
    [SerializeField] private AudioClip clip;
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
        } else if (answersTrue < 10)
        {
            currentResult = res[1];
        } else if (answersTrue < 13)
        {
            currentResult = res[2];
        } else if (answersTrue < 16)
        { 
            currentResult = res[3]; 
        } else if (answersTrue < 19)
        {
            currentResult = res[4];
        } else
        {
            currentResult = res[5];
        }
        finalImage.sprite = currentResult.sprite;
        finalName.text = currentResult.name;
        StartCoroutine(TextCoroutine());
    }

    IEnumerator TextCoroutine()
    {
        finalText.text = "";

        foreach (char abs in currentResult.res)
        {
            finalText.text += abs;
            yield return new WaitForSeconds(0.03f);
        }

    }
}
