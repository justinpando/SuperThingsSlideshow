using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{

    public Button previousButton;
    public Button nextButton;
    public Button quitButton;

    public float ui_fadeDelay = 3f;

    public static float timeOfLastInput = 0f;

    public PhotoViewer photoViewer;

    public CanvasGroupFader fader;
    CoroutineManager.Item fadeSequence = new CoroutineManager.Item();

    // Use this for initialization
    void Start()
    {
        //Add all input listeners to UI buttons
        previousButton.onClick.AddListener(ShowPreviousImage);
        previousButton.onClick.AddListener(RegisterInput);

        nextButton.onClick.AddListener(ShowNextImage);
        nextButton.onClick.AddListener(RegisterInput);


        quitButton.onClick.AddListener(Quit);

        //Start a fade sequence
        instance.fadeSequence.value = instance.FadeSequence();
    }

    void Update()
    {
        //If the mouse is moving, restart the fade sequence
        if (Utility.MouseIsMoving())
        {
            instance.fadeSequence.value = instance.FadeSequence();
        }

    }

    public static void RegisterInput()
    {
        timeOfLastInput = Time.time;
        GameController.state.value = GameController.State.Guided;
        instance.fadeSequence.value = instance.FadeSequence();
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

    /// <summary>
    /// Fades the UI in, then automatically fades out after the fade delay
    /// </summary>
    /// <returns></returns>
    IEnumerator FadeSequence()
    {
        Cursor.visible = true;
        fader.FadeIn();

        yield return new WaitForSeconds(ui_fadeDelay);

        fader.FadeOut();
        Cursor.visible = false;
    }
}
