using UnityEngine;

public class UIParalax : MonoBehaviour
{

    private Vector2 pz;

    public float modifier;

    void Update()
    {
        var pz = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        transform.position = new Vector2(Screen.width / 2 + (pz.x * modifier), Screen.height / 2 + (pz.y * modifier));
    }
}
