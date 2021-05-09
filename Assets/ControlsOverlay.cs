using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ControlsOverlay : MonoBehaviour
{
#region Attributes
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

[SerializeField] bool isLoading = false;

private LoadingBar loadingBar;

#endregion

void Start()
{
    loadingBar = FindObjectOfType<LoadingBar>();
}

    public void SetFaceBar()
    {
        if(PlayerInputs.GetMoveInput() > 0f)
        {
            ActionRight();
        }
        else if(PlayerInputs.GetMoveInput() < 0f)
        {
            ActionLeft();
        }

        if(PlayerInputs.GetMoveInput() == 0f)
        {
            isLoading = false;
        }
    }
    
    public void ActionRight()
    {
       if(barWalkClones.Count == 0)
        {
        GameObject bar = Instantiate(loadBarRight, new Vector3(60, 120, 0), Quaternion.identity, transform);
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
       if(barWalkClones.Count == 0)
        {
        GameObject bar = Instantiate(loadBarLeft, new Vector3(60, 120, 0), Quaternion.identity, transform);
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
        GameObject bar = Instantiate(slideImage, new Vector3(60, 60, 0), Quaternion.identity, transform);
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
        else
        {
        GameObject bar = Instantiate(slideImage, new Vector3(lastSkillBar.transform.position.x +100,60, 0), Quaternion.identity, transform);
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
    }

    public void JumpActionCreated()
    {
        if(barSkillClones.Count == 0)
        {
        GameObject bar = Instantiate(jumpImage, new Vector3(60, 60, 0), Quaternion.identity, transform);
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
        else
        {
        GameObject bar = Instantiate(jumpImage, new Vector3(lastSkillBar.transform.position.x +100,60, 0), Quaternion.identity, transform);
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
    }

    public bool GetIsLoading()
    {
        return isLoading;
    }
}

