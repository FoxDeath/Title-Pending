using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ControlsOverlay : MonoBehaviour
{
#region Attributes
[SerializeField] RectTransform MoveY;
[SerializeField] RectTransform ActionY;
[SerializeField] RectTransform XStart;
[SerializeField] RectTransform XEnd;
[SerializeField] public RectTransform X;

private Transform loadingStartSpot;
private Transform loadingNextSpot;
[SerializeField] GameObject loadBarRight;
[SerializeField] GameObject loadBarLeft;
[SerializeField] GameObject jumpImage;
[SerializeField] GameObject slideImage;

List<GameObject> barWalkClones = new List<GameObject>();
List<GameObject> barSkillClones = new List<GameObject>();

public List<GameObject> replayClones = new List<GameObject>();

public GameObject lastWalkBar;
private GameObject lastSkillBar;

private bool isLoading = false;

private LoadingBar loadingBar;

private GameController gameController;

public bool isFirstGo;

#endregion

void Start()
{
    loadingBar = FindObjectOfType<LoadingBar>();

    gameController = FindObjectOfType<GameController>();

    isFirstGo = true;
}

private void FixedUpdate() 
{
    SetFaceBar();
}

public IEnumerator MoveX()
{
        float time = 0;

        while (time < gameController.currentSequenceDuration)
        {
            X.position = Vector3.Lerp(XStart.position, XEnd.position, time / gameController.currentSequenceDuration);

            time += Time.deltaTime;

            yield return null;
        }
}

public void Reset()
{
    if(isFirstGo)
    {
        isFirstGo = false;
        FirstGo();
    }
    else
    {
        NotFirstGo();
    }
}

public void ResetHard()
{
    foreach(GameObject gameObject1 in replayClones)
    {
        Destroy(gameObject1);
    }

    replayClones = new List<GameObject>();

    NotFirstGo();
}

private void FirstGo()
{
    CreateReplay();
}

private void NotFirstGo()
{
    ResetCanvas();
}

private void ResetCanvas()
{
    foreach(GameObject gameObject1 in replayClones)
    {
        Destroy(gameObject1);
    }

    replayClones = new List<GameObject>();

    foreach(GameObject gameObject in barWalkClones)
    {
        foreach(Transform child in gameObject.transform)
        {
            replayClones.Add(child.gameObject);
        }
    }

    foreach(GameObject gameObject in barSkillClones)
    {
        replayClones.Add(gameObject);
    }

    barWalkClones = new List<GameObject>();

    barSkillClones = new List<GameObject>();

    X.position = XStart.position;

    Color transparent = new Color(1, 1, 1, .1f);

    foreach(GameObject gameObject in replayClones)
    {
        Image image;

        gameObject.TryGetComponent<Image>(out image);

        if(image != null)
        {
            image.color = transparent;
        }
        else
        {
            image = gameObject.GetComponentInChildren<Image>();
            
            if(image != null)
            {
                image.color = transparent;
            }
        }
    }
}

private void CreateReplay()
{
    replayClones = new List<GameObject>();

    foreach(GameObject gameObject in barWalkClones)
    {
        foreach(Transform child in gameObject.transform)
        {
            replayClones.Add(child.gameObject);
        }
    }

    foreach(GameObject gameObject in barSkillClones)
    {
        replayClones.Add(gameObject);
    }

    barWalkClones = new List<GameObject>();

    barSkillClones = new List<GameObject>();

    X.position = XStart.position;

    Color transparent = new Color(1, 1, 1, .1f);

    foreach(GameObject gameObject in replayClones)
    {
        Image image;

        gameObject.TryGetComponent<Image>(out image);

        if(image != null)
        {
            image.color = transparent;
        }
        else
        {
            image = gameObject.GetComponentInChildren<Image>();
            
            if(image != null)
            {
                image.color = transparent;
            }
        }
    }
}

    public void SetFaceBar()
    {
        if(gameController.inTutorial)
        {
            return;
        }

        if(FindObjectOfType<PlayerInputs>().isPaused)
        {
            return;
        }

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
        GameObject bar = Instantiate(loadBarRight, new Vector3(X.position.x, MoveY.position.y, 0), Quaternion.identity, transform);
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
        GameObject bar = Instantiate(loadBarLeft,new Vector3(X.position.x, MoveY.position.y, 0), Quaternion.identity, transform);
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
        if(gameController.inTutorial)
        {
            return;
        }

        if(FindObjectOfType<PlayerInputs>().isPaused)
        {
            return;
        }

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
        if(gameController.inTutorial)
        {
            return;
        }

        if(FindObjectOfType<PlayerInputs>().isPaused)
        {
            return;
        }

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

