using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class EndMenu : MonoBehaviour
{
    private VisualElement root;

    void Awake()
    {
        root = GetComponent<UIDocument>()?.rootVisualElement;

        root.Q<Button>("RetryButton").clicked += Retry;
        root.Q<Button>("MainMenuButton").clicked += MainMenu;

        var scoreTracker = GameObject.Find("ScoreTracker");
        root.Q<Label>("Score").text = "Score: " + scoreTracker.GetComponent<ScoreTracker>().GetScore().ToString();
        Destroy(scoreTracker);
    }

    void Retry()
    {
        SceneManager.LoadScene("MainScene");
    }

    void MainMenu()
    {
        SceneManager.LoadScene("TitleScene");
    }
}