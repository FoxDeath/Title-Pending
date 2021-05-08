using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBarController : MonoBehaviour
{   
    [SerializeField] Transform playerPossition;
    [SerializeField] Transform gameStart;
    [SerializeField] Transform gameEnd;

    [SerializeField] GameObject loadingDot;

    [SerializeField] int maxDots = 74;
    private int targetDots;

    public List<GameObject> loadingDots = new List<GameObject>();

    private float conpletionPrecentage;

    private void Start() 
    {
        InvokeRepeating("Loading", 0f, 0.1f);    
    }

    private void Loading()
    {
        conpletionPrecentage = (gameStart.position.x - playerPossition.position.x) / (gameStart.position.x - gameEnd.position.x);

        targetDots = (int)(conpletionPrecentage * maxDots);

        for (int i = 0; i < loadingDots.Count; i++)
        {
            if (i < targetDots)
            {
                loadingDots[i].SetActive(true);
            }
            else if (i > targetDots)
            {
                loadingDots[i].SetActive(false);
            }
        }
    }
}
