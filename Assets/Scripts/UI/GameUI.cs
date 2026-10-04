using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{
    private Label scoreLabel;
    private ProgressBar scoreProgress;
    private int score;
    private int requiredScore;

    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        scoreLabel = root.Q<Label>("Score");
        scoreProgress = root.Q<ProgressBar>("ScoreProgress");

        var scoreTracker = FindFirstObjectByType<ScoreTracker>();
        var upgradeManager = FindFirstObjectByType<UpgradeManager>();
        scoreTracker.OnScoreUpdated += OnScoreUpdate;
        upgradeManager.OnMilestoneUpdated += OnMilestoneUpdate;

        score = scoreTracker.Score;
        requiredScore = upgradeManager.requiredScoreForMilestone;
    }

    private void UpdateUI()
    {
        scoreLabel.text = score.ToString();
        scoreProgress.value = (int)(((float)score / (float)requiredScore) * 100f);
    }

    private void OnScoreUpdate(int score, int delta)
    {
        this.score = score;
        UpdateUI();
    }

    private void OnMilestoneUpdate(int tier, int requiredScoreForNextMilestone)
    {
        this.requiredScore = requiredScoreForNextMilestone;
        UpdateUI();
    }
}