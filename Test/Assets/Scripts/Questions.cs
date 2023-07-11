using UnityEngine.Video;
using UnityEngine;

[System.Serializable]
public class Questions {
    public string qst;
    public QuestionType questionType;
    public Sprite questionImg;
    public AudioClip questionAudio;
    public VideoClip questionVideo;
    public string trueAnswer;
    public string annotation;
    public string[] answers = new string[4];
}

[System.Serializable]
public enum QuestionType
{
    TEXT,
    IMAGE,
    VIDEO,
    AUDIO
}