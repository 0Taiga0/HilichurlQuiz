using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class StartGame : MonoBehaviour
{
    [SerializeField] private GameObject[] buttons;

    private void Start()
    {
        if (YandexGame.EnvironmentData.deviceType != "desktop")
        {
            buttons[0].transform.localScale = new Vector3(1.3f, 1.3f, 1);
            buttons[1].transform.localScale = new Vector3(1.3f, 1.3f, 1);
            buttons[2].transform.localScale = new Vector3(1, 1.5f, 1);
            buttons[3].transform.localScale = new Vector3(1.5f, 1.5f, 1);
        }
    }

}
