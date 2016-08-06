using System;
using UnityEngine;
using System.Collections;

public class GameController : Singleton<GameController>
{
    public enum State { Guided, Slideshow }
    public static readonly StateManager<State> state = StateManager<State>.CreateNew(); 
    public static bool isQuitting = false;

    readonly CoroutineManager.Item stateSequence = new CoroutineManager.Item();

    public float switchToSlideshowDelay = 30f;
    public float autoTransitionDelay = 4f;     

    // Use this for initialization
    void Start()
    {
        //Subscribe HandleState to state changes
        state.OnChanged += HandleState;
        //Begin the slideshow
        state.value = State.Slideshow;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// Handles state changes and assigns a new state IEnumerator.
    /// </summary>
    /// <param name="newState"></param>
    private void HandleState(State newState)
    {
        switch(newState)
        {
            case State.Guided:
                stateSequence.value = GuidedSequence();
                break;
            case State.Slideshow:
                stateSequence.value = SlideshowSequence();
                break;
        }
    }

    /// <summary>
    /// Handles keyboard input.
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            UIManager.ShowPreviousImage();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            UIManager.ShowNextImage();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Quit();
        }
    }

    private IEnumerator SlideshowSequence()
    {
        Debug.Log("Entered slideshow state at : " + Time.time);
        //While in Slideshow state,
        while (true)
        {
            //Show the next image
            UIManager.ShowNextImage();
            //Then wait for the transition delay
            yield return new WaitForSeconds(autoTransitionDelay);
        }

    }

    private IEnumerator GuidedSequence()
    {
        Debug.Log("Entered guided state at : " + Time.time);
        //When the time since the last user input is longer than the delay time,
        while (Time.time - UIManager.timeOfLastInput < switchToSlideshowDelay)
        {
            yield return null;
        }
        //Re-enter slideshow state
        state.value = State.Slideshow;
    }

    public void OnApplicationQuit()
    {
        //Set isQuitting to true to prevent Singleton from showing missing reference logs
        isQuitting = true;
    }
}
