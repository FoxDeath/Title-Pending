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

    private float Timer = 0.075f;

    private ControlsOverlay controlsOverlay;

    public bool loading;

    void Start()
    {
        controlsOverlay = FindObjectOfType<ControlsOverlay>();
    }

    void Update()
    {
        if(loading)
        {
             SpawnLoadBar();
        }
    }

    public void SpawnLoadBar()
    {
        if(barBitClones.Count == 0)
        {
        GameObject barBit = Instantiate(loadingBit,loadingStartSpot.transform.localPosition,Quaternion.identity,transform);
        
        RectTransform barTransform = barBit.GetComponent<RectTransform>();
        
        barTransform.localPosition = loadingStartSpot.transform.localPosition;

        
        barBitClones.Add(barBit);
        lastBitSpot = barBitClones.Last();
        }
        else
        {
        Timer -= Time.deltaTime;
        if(Timer <= 0f)
        {
        GameObject barBit = Instantiate(loadingBit,Vector3.zero, Quaternion.identity, transform);
        
        RectTransform barTransform = barBit.GetComponent<RectTransform>();
        
        barTransform.position = controlsOverlay.position.position;
        
         barBitClones.Add(barBit);
        }
        lastBitSpot = barBitClones.Last();
        }
    }
    public Vector3 GetLastSpotForLoad()
    {
        return new Vector3(lastBitSpot.transform.position.x + 5f,lastBitSpot.transform.position.y,lastBitSpot.transform.position.z);
    }
}
