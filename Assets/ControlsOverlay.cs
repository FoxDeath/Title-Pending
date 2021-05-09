using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControlsOverlay : MonoBehaviour
{
#region Attributes
[SerializeField] RectTransform MoveY;
[SerializeField] RectTransform ActionY;
[SerializeField] RectTransform XStart;
[SerializeField] RectTransform XEnd;
[SerializeField] RectTransform X;

private Transform loadingStartSpot;
private Transform loadingNextSpot;
[SerializeField] GameObject loadBarRight;
[SerializeField] GameObject loadBarLeft;
[SerializeField] GameObject jumpImage;
[SerializeField] GameObject slideImage;

List<GameObject> barWalkClones = new List<GameObject>();
List<GameObject> barSkillClones = new List<GameObject>();

private GameObject lastWalkBar;
private GameObject lastSkillBar;

private bool isLoading = false;

private LoadingBar loadingBar;

private LoadingBarController loadingBarController;

#endregion

void Start()
{
    loadingBar = FindObjectOfType<LoadingBar>();

    loadingBarController = FindObjectOfType<LoadingBarController>();
}

private void FixedUpdate() 
{
    SetFaceBar();
}

public void ResetCanvas()
{
    barWalkClones = new List<GameObject>();

    barSkillClones = new List<GameObject>();

    foreach(Transform child in transform)
    {
        Destroy(child.gameObject);
    }
}

    public void SetFaceBar()
    {
        if(PlayerInputs.GetMovePressed() > 0f)
        {
            ActionRight();
        }
        else if(PlayerInputs.GetMovePressed() < 0f)
        {
            ActionLeft();
        }

        if(PlayerInputs.GetMovePressed() == 0f)
        {
            isLoading = false;
        }
    }
    
    public void ActionRight()
    {
       if(barWalkClones.Count == 0 && !GetIsLoading())
        {
        GameObject bar = Instantiate(loadBarRight, new Vector3(X.position.x, MoveY.position.y, 0), Quaternion.identity, transform);
        barWalkClones.Add(bar);
        lastWalkBar = barWalkClones.Last();
        isLoading = true;

            if(loadingBar == null)
            {
                loadingBar = FindObjectOfType<LoadingBar>();
            }
        }
        else if (barWalkClones.Count != 0 && !GetIsLoading())
        {
        GameObject bar = Instantiate(loadBarRight, loadingBar.GetLastSpotForLoad(), Quaternion.identity, transform);
        barWalkClones.Add(bar);
        lastWalkBar = barWalkClones.Last();
        isLoading = true;

            if(loadingBar == null)
            {
                loadingBar = FindObjectOfType<LoadingBar>();
            }
        }
    }

    public void ActionLeft()
    {
       if(barWalkClones.Count == 0 && !GetIsLoading())
        {
        GameObject bar = Instantiate(loadBarLeft, new Vector3(X.position.x, MoveY.position.y, 0), Quaternion.identity, transform);
        barWalkClones.Add(bar);
        lastWalkBar = barWalkClones.Last();
        isLoading = true;

            if(loadingBar == null)
            {
                loadingBar = FindObjectOfType<LoadingBar>();
            }
        }

        else if (barWalkClones.Count != 0 && !GetIsLoading())
        {
        GameObject bar = Instantiate(loadBarLeft,loadingBar.GetLastSpotForLoad(), Quaternion.identity, transform);
        barWalkClones.Add(bar);
        lastWalkBar = barWalkClones.Last();
        isLoading = true;

            if(loadingBar == null)
            {
                loadingBar = FindObjectOfType<LoadingBar>();
            }
        }
    }
    
    public void SlideActionCreated()
    {
        if(barSkillClones.Count == 0)
        {
        GameObject bar = Instantiate(slideImage, new Vector3(X.position.x, ActionY.position.y, 0), Quaternion.identity, transform);
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
        else
        {
        GameObject bar = Instantiate(slideImage, new Vector3(X.position.x,ActionY.position.y, 0), Quaternion.identity, transform);
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
    }

    public void JumpActionCreated()
    {
        if(barSkillClones.Count == 0)
        {
        GameObject bar = Instantiate(jumpImage, new Vector3(X.position.x, ActionY.position.y, 0), Quaternion.identity, transform);
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
        else
        {
        GameObject bar = Instantiate(jumpImage, new Vector3(X.position.x,ActionY.position.y, 0), Quaternion.identity, transform);
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
    }

    public bool GetIsLoading()
    {
        return isLoading;
    }
}

