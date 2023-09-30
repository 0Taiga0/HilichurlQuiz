using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace YG
{
    public class SavesData : MonoBehaviour
    {
        private bool[] collections = new bool[6];
        private Image[] collectionImg = new Image[6];
        [SerializeField] private ResultDataScriptable resultDataScriptable;


        private void OnEnable() => YandexGame.GetDataEvent += GetLoad;
        private void OnDisable() => YandexGame.GetDataEvent -= GetLoad;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                GetLoad();
        }

        public void Save(int i)
        {
            YandexGame.savesData.collection[i] = true;

            YandexGame.SaveProgress();
        }

        public void SaveQuestion(int qst, int trueAnsw, int condition)
        {
            YandexGame.savesData.question = qst;
            YandexGame.savesData.trueQuestion = trueAnsw;
            YandexGame.savesData.condition = condition;

            YandexGame.SaveProgress();
        }

        public void SaveCond(int condition)
        {
            YandexGame.savesData.condition = condition;

            YandexGame.SaveProgress();
        }

        public int[] LoadQuestion()
        {
            int[] b = new int[3];
            b[0] = YandexGame.savesData.question;
            b[1] = YandexGame.savesData.trueQuestion;
            b[2] = YandexGame.savesData.condition;
            return b;
        }

        public void Load() => YandexGame.LoadProgress();

        public void GetLoad()
        {
            for (int i = 0; i < 6; i++)
            {
                collections[i] = YandexGame.savesData.collection[i];
            }

        }

        public void Collections()
        {
            collectionImg = GameObject.Find("Collections").GetComponentsInChildren<Image>();
            for (int i = 0; i < 6; i++)
            {
                if (collections[i])
                    collectionImg[i].sprite = resultDataScriptable.results[i].sprite;
            }
        }

    }
}