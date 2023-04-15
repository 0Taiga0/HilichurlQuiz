using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] GameManagerScript gameManagerScript;

    public void SetQuest()
    {
        gameManagerScript.SetQuestion();
    }

}
