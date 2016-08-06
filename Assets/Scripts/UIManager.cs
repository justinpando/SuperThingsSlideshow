using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public Button previousButton;
    public Button nextButton;
    public Button quitButton;

    public static float timeOfLastInput = 0f;

    public PhotoViewer photoViewer;

    // Use this for initialization
    void Awake()
    {
        previousButton.onClick.AddListener(ShowPreviousImage);
        previousButton.onClick.AddListener(RegisterInput);

        nextButton.onClick.AddListener(ShowNextImage);
        nextButton.onClick.AddListener(RegisterInput);


        quitButton.onClick.AddListener(Quit);
    }

    public static void RegisterInput()
    {
        timeOfLastInput = Time.time;
        GameController.state.value = GameController.State.Guided;
    }

    public static void ShowPreviousImage()
    {
        instance.photoViewer.ShowPreviousImage();
    }

    public static void ShowNextImage()
    {
        instance.photoViewer.ShowNextImage();
    }

    public static void Quit()
    {
        Application.Quit();
    }
}
