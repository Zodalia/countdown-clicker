
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreTracker: MonoBehaviour
{
    public Action<int, int> OnScoreUpdated;
    public UIDocument gameScreen;
    private int score;
    public int Score
    {
        get{return score;}
        private set
        {
            int delta = value - score;
            score = value;
            OnScoreUpdated?.Invoke(score, delta);
        }
    }

    public AnimationCurve scoreCurve;
    public float[] numberMods;
    public float secondMod;
    public float miliSecMod;
    public float microSecMod;

    public GameObject prefab;

    public static ScoreTracker Instance;

    void Awake()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    int CalculateScore(float number)
    {

        return (int)scoreCurve.Evaluate(number);

        //TODO - Extra mod score
        int second = (int)number;
        int mili = (int)((number - second) * 10f);
        int micro = (int)((((number - second) * 10f) - mili) * 10f);

        float score = numberMods[second] * secondMod;
        score += numberMods[mili] * miliSecMod;
        score += numberMods[micro] * microSecMod;

        return (int)score;
    }

    public void AddScore(Vector3 pos, float timeRemaining)
    {
        var i = (int)(timeRemaining * 100f);
        var timeTrimmed = (float)i * 0.01f;

        var scoreToAdd = CalculateScore(timeRemaining);
        var scoreObj = Instantiate(prefab);
        scoreObj.GetComponent<UIDocument>().rootVisualElement.Q<Label>("Time").text = timeTrimmed.ToString();
        scoreObj.GetComponent<UIDocument>().rootVisualElement.Q<Label>("Score").text = "+ " + scoreToAdd.ToString();
        scoreObj.transform.position = pos;

        Score += scoreToAdd;
    }
}