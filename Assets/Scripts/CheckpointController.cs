using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    private Collider2D myColldier;

    private GameController gameController;

    private void Awake()
    {
        myColldier = GetComponent<Collider2D>();

        gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        myColldier.enabled = false;

                FindObjectOfType<ControlsOverlay>().ResetHard();

        gameController.NextSection();
    }
}
