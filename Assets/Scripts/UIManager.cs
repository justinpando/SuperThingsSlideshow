using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public Button previousButton;
    public Button nextButton;
    public Button quitButton;

    public static float timeOfLastInput = 0f;

    // Use this for initialization
    void Awake()
    {
        previousButton.onClick.AddListener(ShowPreviousImage);
        nextButton.onClick.AddListener(ShowNextImage);
        quitButton.onClick.AddListener(Quit);
    }

    public static void ShowPreviousImage()
    {
        timeOfLastInput = Time.time;
    }

    public static void ShowNextImage()
    {
        timeOfLastInput = Time.time;
    }

    public static void Quit()
    {
        
    }
}
