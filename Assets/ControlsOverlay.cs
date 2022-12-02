using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControlsOverlay : MonoBehaviour
{
#region Attributes

private GameController gameController;
private Transform loadingStartSpot;
private Transform loadingNextSpot;
[SerializeField] GameObject loadBarRight;
[SerializeField] GameObject loadBarLeft;
[SerializeField] GameObject jumpImage;
[SerializeField] GameObject slideImage;

List<GameObject> barWalkClones = new List<GameObject>();
List<GameObject> barSkillClones = new List<GameObject>();

[SerializeField] GameObject lastWalkBar;
private GameObject lastSkillBar;

private LoadingBar loadingBar;

[SerializeField] public RectTransform position;

#endregion

private void Awake()
{
    gameController = FindObjectOfType<GameController>();
}

    private void FixedUpdate()
    {
        if(!gameController.inInputPhase)
        {
            if(loadingBar && loadingBar.loading)
            {
                loadingBar.loading = false;
            }
            
            return;
        }

        position.localPosition = Vector3.MoveTowards(position.localPosition, new Vector3(600, -275, 0), 1200f / (50f * gameController.currentSequenceDuration));

        SetFaceBar();
    }

    public void SetFaceBar()
    {
        if(PlayerInputs.GetMovePressed() < 0f)
        {
            ActionRight();
        }
        else if(PlayerInputs.GetMovePressed() > 0f)
        {
            ActionLeft();
        }

        if(PlayerInputs.GetMovePressed() == 0f && loadingBar)
        {
            loadingBar.loading = false;
        }
    }
    
    public void ActionRight()
    {
       if(barWalkClones.Count == 0)
        {
        GameObject bar = Instantiate(loadBarRight, new Vector3(60, 120, 0), Quaternion.identity, transform);
        
        RectTransform barTransform = bar.GetComponent<RectTransform>();

        barTransform.anchoredPosition = position.anchoredPosition;
        
        barWalkClones.Add(bar);
        lastWalkBar = barWalkClones.Last();
        loadingBar = bar.GetComponent<LoadingBar>();
        loadingBar.loading = true;
        }
        else if (barWalkClones.Count != 0 && !loadingBar.loading)
        {
        GameObject bar = Instantiate(loadBarRight, loadingBar.GetLastSpotForLoad(), Quaternion.identity, transform);
        
        RectTransform barTransform = bar.GetComponent<RectTransform>();

        barTransform.anchoredPosition = position.anchoredPosition;
        
        barWalkClones.Add(bar);
        lastWalkBar = barWalkClones.Last();
        loadingBar = bar.GetComponent<LoadingBar>();
        loadingBar.loading = true;
        }
    }

    public void ActionLeft()
    {
       if(barWalkClones.Count == 0)
        {
        GameObject bar = Instantiate(loadBarLeft, new Vector3(-600, -275, 0), Quaternion.identity, transform);

        RectTransform barTransform = bar.GetComponent<RectTransform>();

        barTransform.localPosition = position.transform.localPosition;
        
        barWalkClones.Add(bar);
        lastWalkBar = barWalkClones.Last();
        loadingBar = bar.GetComponent<LoadingBar>();
        loadingBar.loading = true;
        }

        else if (barWalkClones.Count != 0 && !loadingBar.loading)
        {
        GameObject bar = Instantiate(loadBarLeft,loadingBar.GetLastSpotForLoad(), Quaternion.identity, transform);
        
        RectTransform barTransform = bar.GetComponent<RectTransform>();

        barTransform.localPosition = position.transform.localPosition;
        
        barWalkClones.Add(bar);
        lastWalkBar = barWalkClones.Last();
        loadingBar = bar.GetComponent<LoadingBar>();
        loadingBar.loading = true;
        }
    }
    
    public void SlideActionCreated()
    {
        if(barSkillClones.Count == 0)
        {
        GameObject bar = Instantiate(slideImage, new Vector3(-600, -275, 0), Quaternion.identity, transform);
        
        RectTransform barTransform = bar.GetComponent<RectTransform>();

        barTransform.localPosition = position.transform.localPosition + new Vector3(0, -27.5f, 0);
        
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
        else
        {
        GameObject bar = Instantiate(slideImage, new Vector3(lastSkillBar.transform.position.x +100,60, 0), Quaternion.identity, transform);
        
        RectTransform barTransform = bar.GetComponent<RectTransform>();

        barTransform.localPosition = position.transform.localPosition + new Vector3(0, -27.5f, 0);
        
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
    }

    public void JumpActionCreated()
    {
        if(barSkillClones.Count == 0)
        {
        GameObject bar = Instantiate(jumpImage, new Vector3(-600, -275, 0), Quaternion.identity, transform);
        
        RectTransform barTransform = bar.GetComponent<RectTransform>();

        barTransform.localPosition = position.transform.localPosition + new Vector3(0, -27.5f, 0);
        
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
        else
        {
        GameObject bar = Instantiate(jumpImage, new Vector3(lastSkillBar.transform.position.x +100,60, 0), Quaternion.identity, transform);
        
        RectTransform barTransform = bar.GetComponent<RectTransform>();

        barTransform.localPosition = position.transform.localPosition + new Vector3(0, -27.5f, 0);
        
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
    }

    public void Reset()
    {
        
    }
}

