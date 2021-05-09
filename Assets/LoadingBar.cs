using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] GameObject loadingBit;
    [SerializeField] GameObject loadingStartSpot;

    List<GameObject> barBitClones = new List<GameObject>();

    public GameObject lastBitSpot;

    private float Timer = 0.5f;

    private ControlsOverlay controlsOverlay;

    void Start()
    {
        controlsOverlay = FindObjectOfType<ControlsOverlay>();
    }

    void Update()
    {
        if(controlsOverlay.GetIsLoading())
        {
             SpawnLoadBar();
        }
    }

    public void SpawnLoadBar()
    {
        if(barBitClones.Count == 0)
        {
        GameObject barBit = Instantiate(loadingBit,loadingStartSpot.transform.position,Quaternion.identity,transform);
        barBitClones.Add(barBit);
        lastBitSpot = barBitClones.Last();
        }
        else
        {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
        GameObject barBit = Instantiate(loadingBit,GetLastSpot(),Quaternion.identity,transform);
         Timer = 0.5f;
         barBitClones.Add(barBit);
        }
        lastBitSpot = barBitClones.Last();
        }
    }

    public Vector3 GetLastSpot()
    {
        return new Vector3(lastBitSpot.transform.position.x + 1f,lastBitSpot.transform.position.y,lastBitSpot.transform.position.z);
    }
    public Vector3 GetLastSpotForLoad()
    {
        return new Vector3(lastBitSpot.transform.position.x + 1.5f,lastBitSpot.transform.position.y,lastBitSpot.transform.position.z);
    }
}
