using TMPro;
using UnityEngine;
using YG;

public class vkEnabler : MonoBehaviour
{
	public GameObject keyboard;
	public GameObject image;
	public GameObject inputF;
	public void ShowVirtualKeyboard(){
		if (keyboard.activeSelf && YandexGame.EnvironmentData.deviceType != "desktop")
		{
			image.SetActive(false);
			inputF.transform.localPosition = new Vector3(0f, 200, 0f);
            TNVirtualKeyboard.instance.ShowVirtualKeyboard();
			TNVirtualKeyboard.instance.targetText = gameObject.GetComponent<TMP_InputField>();
		}
	}
}
