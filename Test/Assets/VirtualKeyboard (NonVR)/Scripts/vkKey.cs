using UnityEngine;

public class vkKey : MonoBehaviour
{
	public string k = "";
	
	public void KeyClick(){
		TNVirtualKeyboard.instance.KeyPress(k);
	}
}
