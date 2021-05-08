using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBarController : MonoBehaviour
{
    [SerializeField] Transform gameEnd;
    [SerializeField] Transform gameStart;

    [SerializeField] GameObject loadingDot;

    public List<GameObject> loadingDots = new List<GameObject>();

    private float conpletionPrecentage;

    private void Start() 
    {
        conpletionPrecentage = 0f;

        loadingDots.Add(Instantiate(loadingDot, new Vector3(-7.37f, -4f, 0f), Quaternion.identity, transform));
    }

    private void Update()
    {
        if(Random.Range(0,2) == 1)
        AddDot();
        else
        RemoveDot();
    }
            

    private void AddDot()
    {
        if(loadingDots.Count >= 124)
        {
            return;
        }
        
        loadingDots.Add(Instantiate(loadingDot, loadingDots[loadingDots.Count - 1].transform.position + new Vector3(0.12f, 0f, 0f), Quaternion.identity, transform));
    }

    private void RemoveDot()
    {
        if(loadingDots.Count <= 1)
        {
            return;
        }
        
        GameObject lastDot = loadingDots[loadingDots.Count-1];

        loadingDots.Remove(lastDot);

        Destroy(lastDot);
    }

}
