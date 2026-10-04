using System;
using UnityEngine;

public class UpgradeManager: MonoBehaviour
{
    public Action<int, int> OnMilestoneUpdated;
    [SerializeField] private AnimationCurve upgradeMilestones;

    private int tier = 0;
    public int requiredScoreForMilestone
    {
        get;
        private set;
    }

    private void CalculateRequiredScore(int score)
    {
        requiredScoreForMilestone = (int)upgradeMilestones.Evaluate(score);
    }

    void Awake()
    {
        CalculateRequiredScore(0);
    }

    void Start()
    {
        var scoreTracker = FindFirstObjectByType<ScoreTracker>();
        scoreTracker.OnScoreUpdated += OnScoreUpdated;
        CalculateRequiredScore(scoreTracker.Score);
    }

    void OnScoreUpdated(int score, int delta)
    {
        if(score < requiredScoreForMilestone) return;

        //Time.timeScale = 0f;
        tier++;
        CalculateRequiredScore(score);
        OnMilestoneUpdated(tier, requiredScoreForMilestone);

        print(tier);
    }
}