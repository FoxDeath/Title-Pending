using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ControlsOverlay : MonoBehaviour
{
#region Attributes

public GameController gameController;
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
        GameObject bar = Instantiate(loadBarRight, Vector3.zero, Quaternion.identity, transform);
        
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
        GameObject bar = Instantiate(loadBarLeft, transform);

        RectTransform barTransform = bar.GetComponent<RectTransform>();

        barTransform.localPosition = position.transform.localPosition;
        
        barWalkClones.Add(bar);
        lastWalkBar = barWalkClones.Last();
        loadingBar = bar.GetComponent<LoadingBar>();
        loadingBar.loading = true;
        }

        else if (barWalkClones.Count != 0 && !loadingBar.loading)
        {
        GameObject bar = Instantiate(loadBarLeft, transform);
        
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
        GameObject bar = Instantiate(slideImage, transform);
        
        RectTransform barTransform = bar.GetComponent<RectTransform>();

        barTransform.localPosition = position.transform.localPosition + new Vector3(0, -27.5f, 0);
        
        barSkillClones.Add(bar);
        lastSkillBar = barSkillClones.Last();
        }
        else
        {
        GameObject bar = Instantiate(slideImage, transform);
        
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
        GameObject bar = Instantiate(jumpImage, transform);
        
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
        position.localPosition = new Vector3(-600, -275, 0);

        foreach(var clone in barWalkClones)
        {
            if(!clone)
            {
                continue;
            }
            
            foreach(Transform child in clone.transform)
            {
                Image component = child.GetComponent<Image>();

                if(component)
                {
                    if(component.color != new Color(1f, 1f, 1f, 0.5f))
                    {
                        component.color = new Color(1f, 1f, 1f, 0.5f);
                    }
                    else
                    {
                        Destroy(clone);

                        break;
                    }
                }
            }
        }

        foreach(var clone in barSkillClones)
        {
            if(!clone)
            {
                continue;
            }
            
            Image component = clone.GetComponent<Image>();

            if(component)
            {
                if(component.color != new Color(1f, 1f, 1f, 0.5f))
                {
                    component.color = new Color(1f, 1f, 1f, 0.5f);
                }
                else
                {
                    Destroy(clone);
                }
            }
        }
    }

    public void Continue()
    {
        position.localPosition = new Vector3(-600, -275, 0);

        foreach(var clone in barWalkClones)
        {
            if(!clone)
            {
                continue;
            }
            
            Destroy(clone);
        }

        foreach(var clone in barSkillClones)
        {
            if(!clone)
            {
                continue;
            }
            
            Destroy(clone);
        }

        barSkillClones.Clear();
        
        barWalkClones.Clear();
    }
}

