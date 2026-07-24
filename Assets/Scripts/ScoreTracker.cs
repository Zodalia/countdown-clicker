
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreTracker: MonoBehaviour
{
    public UIDocument gameScreen;
    private Label scoreLabel;
    private int score = 0;

    public float[] numberMods;
    public float secondMod;
    public float miliSecMod;
    public float microSecMod;

    public GameObject prefab;

    public static ScoreTracker Instance;

    void Awake()
    {
        Instance = this;
        scoreLabel = gameScreen.rootVisualElement.Q<Label>("Score");
        DontDestroyOnLoad(gameObject);
    }

    int CalculateScore(float number)
    {
        int second = (int)number;
        int mili = (int)((number - second) * 10f);
        int micro = (int)((((number - second) * 10f) - mili) * 10f);

        float score = numberMods[second] * secondMod;
        score += numberMods[mili] * miliSecMod;
        score += numberMods[micro] * microSecMod;

        return (int)score;
    }

    public void Score(Vector3 pos, float timeRemaining)
    {
        var i = (int)(timeRemaining * 100f);
        var timeTrimmed = (float)i * 0.01f;

        var scoreToAdd = CalculateScore(timeRemaining);
        var scoreObj = Instantiate(prefab);
        scoreObj.GetComponent<UIDocument>().rootVisualElement.Q<Label>("Time").text = timeTrimmed.ToString();
        scoreObj.GetComponent<UIDocument>().rootVisualElement.Q<Label>("Score").text = "+ " + scoreToAdd.ToString();
        scoreObj.transform.position = pos;

        score += scoreToAdd;
        scoreLabel.text = score.ToString();
    }

    public int GetScore()
    {
        return score;
    }
}