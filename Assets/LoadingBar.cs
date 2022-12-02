using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] GameObject loadingBit;

    private List<GameObject> barBitClones = new();
    
    private float Timer = 0.0345f;

    private ControlsOverlay controlsOverlay;

    public bool loading;

    void Start()
    {
        controlsOverlay = FindObjectOfType<ControlsOverlay>();
        
        Timer = 0.0115f * (controlsOverlay.gameController.currentSequenceDuration * 1.25f);
    }

    void FixedUpdate()
    {
        if(controlsOverlay.gameController.inSegmentChange)
        {
            Timer = 0.0115f * (controlsOverlay.gameController.currentSequenceDuration * 1.25f);
        }
        
        if(loading)
        {
             SpawnLoadBar();
        }
    }

    public void SpawnLoadBar()
    {
        Timer -= Time.fixedDeltaTime;
        
        if(Timer <= 0f)
        {
            Timer = 0.0115f * (controlsOverlay.gameController.currentSequenceDuration * 1.25f);
            
            GameObject barBit = Instantiate(loadingBit, transform);
            
            RectTransform barTransform = barBit.GetComponent<RectTransform>();
            
            barTransform.position = controlsOverlay.position.position;
            
             barBitClones.Add(barBit);
        }
    }
}
