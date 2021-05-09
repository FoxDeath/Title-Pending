using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] AudioSource click;
    [SerializeField] AudioSource release;

    public void Click()
    {
        click.Play();
        release.PlayDelayed(.2f);
    }

    public void Restart()
    {
        FindObjectOfType<PlayerInputs>().isPaused = false;
        FindObjectOfType<ControlsOverlay>().isFirstGo = true;
        FindObjectOfType<GameController>().ResetMenu();
    }

    public void Quit()
    {
        FindObjectOfType<ControlsOverlay>().isFirstGo = false;
        FindObjectOfType<GameController>().ResetMenu();
    }
}
