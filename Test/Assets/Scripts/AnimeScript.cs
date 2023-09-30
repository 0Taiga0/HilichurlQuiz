using UnityEngine;
using YG;

public class AnimeScript : MonoBehaviour
{
    [SerializeField] private GameObject collections;
    [SerializeField] private GameObject start;
    [SerializeField] private SavesData saveData;
    private bool collAnimStart;

    public void CollectionOn()
    {
        saveData.Collections();
        foreach (var i in collections.GetComponentsInChildren<Animator>()) 
        {
            i.SetBool("CloseAnim", collAnimStart);
        }
    }


    public void CollAnimStart() 
    {
        start.SetActive(collAnimStart);
        gameObject.GetComponent<Animator>().SetBool("CollAnimStart", !collAnimStart);
        collAnimStart = !collAnimStart;
    }

    public void ClickerOn()
    {
        start.SetActive(collAnimStart);
        collections.SetActive(collAnimStart);
        gameObject.GetComponent<Animator>().SetBool("ClickAnim", !collAnimStart);
        GameObject.Find("PlayClicker").GetComponent<Animator>().SetBool("UpDown", !collAnimStart);
        collAnimStart = !collAnimStart;
    }
}
