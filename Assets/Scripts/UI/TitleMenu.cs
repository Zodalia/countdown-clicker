using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleMenu : MonoBehaviour
{
    private VisualElement root;

    void Awake()
    {
        root = GetComponent<UIDocument>()?.rootVisualElement;

        root.Q<Button>("PlayButton").clicked += Play;
    }

    void Play()
    {
        SceneManager.LoadScene("MainScene");
    }
}