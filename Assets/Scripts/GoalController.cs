using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private Collider2D myColldier;

    private GameController gameController;

    private AudioSource audioSource;

    private bool fired = false;

    private void Awake()
    {
        myColldier = GetComponent<Collider2D>();

        gameController = FindObjectOfType<GameController>();

        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!fired)
        {
            fired = true;
            
            myColldier.enabled = false;

            audioSource.Play();

            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);

        gameController.Win();
    }
}
