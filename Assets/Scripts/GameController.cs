using System;
using UnityEngine;
using System.Collections;

public class GameController : Singleton<GameController>
{
    public enum State { Guided, Slideshow }
    public readonly StateManager<State> state = StateManager<State>.CreateNew(); 
    public static bool isQuitting = false;

    readonly CoroutineManager.Item stateSequence = new CoroutineManager.Item();

    public float switchToSlideshowDelay = 30f;
    public float autoTransitionDelay = 4f;     

    // Use this for initialization
    void Start()
    {
        state.OnChanged += HandleState;
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
    void HandleState(State newState)
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
    void HandleInput()
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

    IEnumerator SlideshowSequence()
    {
        //While in Slideshow state,
        while (true)
        {
            //Show the next image
            UIManager.ShowNextImage();
            //Then wait for the transition delay
            yield return new WaitForSeconds(autoTransitionDelay);
        }

    }


    IEnumerator GuidedSequence()
    {
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
