using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject settings;
    [SerializeField] private Animator animator;

    public void ActiveSettings()
    {
        content.SetActive(true);
    }

    public void CloseSettings()
    {
        settings.SetActive(false);
    }

    public void AnimSettings()
    {
        animator.SetTrigger("FadeEnd");
        gameObject.GetComponent<Animator>().SetTrigger("SetEnd");
        content.SetActive(false);

    }
}
