using UnityEngine;
using TMPro;
using UnityEngine.UI;
using YG;

public class TNVirtualKeyboard : MonoBehaviour
{
	public static TNVirtualKeyboard instance;
	
	public string words = "";
	
	public GameObject vkCanvas;

	public GameManagerScript gmScript;
	
	public TMP_InputField targetText;

	public TMP_Text[] letters;

	public GameObject specialSymb;

	public GameObject abc;

	private bool isUpper = false;

    public GameObject image;

    public GameObject inputF;

    void Start()
    {
        instance = this;
		HideVirtualKeyboard();
		
    }
	
	public void KeyPress(string k){
		if (targetText.text.Length <= 13)
		{
			if (isUpper)
			{
				k = k.ToUpper();
			}
			words += k;
			targetText.text = words;
		}

    }
	
	public void Del(){
		if (targetText.text != "")
		{
			words = words.Remove(words.Length - 1, 1);
			targetText.text = words;
		}
	}
	
	public void ShowVirtualKeyboard(){
		vkCanvas.SetActive(true);
	}
	
	public void HideVirtualKeyboard(){
		if (YandexGame.EnvironmentData.deviceType != "desktop" && gmScript.GetCurQ() >= 90)
		{
			inputF.transform.localPosition = new Vector3(8f, -425f, 0f);
			image.SetActive(true);
			vkCanvas.SetActive(false);
		}
	}

	public void UpperCase()
	{
		if (!isUpper)
		{
			for (int i = 0; i < letters.Length; i++)
			{
				letters[i].text = letters[i].text.ToUpper();
			}
		}
		else
		{
            for (int i = 0; i < letters.Length; i++)
            {
                letters[i].text = letters[i].text.ToLower();
            }
        }
		isUpper = !isUpper;
	}

	public void SpecialSymb()
	{
		specialSymb.SetActive(true);
		abc.SetActive(false);
	}

    public void ABC()
    {
        abc.SetActive(true);
        specialSymb.SetActive(false);
    }
}
