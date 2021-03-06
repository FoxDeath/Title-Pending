using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

public class GameController : MonoBehaviour
{
    private GameObject tut1;
    
    private GameObject tut2;
    
    private GameObject tut3;

    private Transform player;

    private List<Transform> CamPositions = new List<Transform>();

    private Transform cameraFollow;

    private Vector2 currentSavedPos;

    private PlayableDirector director;

    private PlayerInputs playerInputs;

    private TimelineAsset replay;

    private TrackAsset moveLeftTrack;
    
    private TrackAsset moveRightTrack;

    private TrackAsset jumpTrack;
    
    private TrackAsset slideTrack;

    private Coroutine phaseSwitchCoroutine;

    [HideInInspector] public bool inInputPhase = false;

    [HideInInspector] public bool inSegmentChange = false;

    [HideInInspector] public bool pressAnyKeyToContinue = true;

    public bool inTutorial = false;
    
    public float currentSequenceDuration = 5f;

    public float camLerpDuration = 3f;

    private int currentCamPosition = 0;

    private bool started = false;

    void Awake()
    {
        tut1 = GameObject.Find("Mendatory").transform.Find("LoadingBarCanvas 1").Find("Tutorial1").gameObject;

        tut1.SetActive(false);
        
        tut2 = GameObject.Find("Mendatory").transform.Find("LoadingBarCanvas 1").Find("Tutorial2").gameObject;

        tut2.SetActive(false);
        
        tut3 = GameObject.Find("Mendatory").transform.Find("LoadingBarCanvas 1").Find("Tutorial3").gameObject;

        tut3.SetActive(false);

        player = GameObject.Find("Player").transform;

        Transform camPositions = GameObject.Find("CamPositions").transform;

        for(int i = 0; i < camPositions.childCount; i++)
        {
            CamPositions.Add(camPositions.GetChild(i));
        }

        cameraFollow = GameObject.Find("Mendatory").transform.Find("CameraFollow");

        currentSavedPos = player.position;

        director = GameObject.Find("Player").GetComponent<PlayableDirector>();

        playerInputs = FindObjectOfType<PlayerInputs>();

        replay = (TimelineAsset)director.playableAsset;

        moveLeftTrack = replay.GetOutputTrack(1);

        moveRightTrack = replay.GetOutputTrack(2);

        jumpTrack = replay.GetOutputTrack(3);

        slideTrack = replay.GetOutputTrack(4);

        director.playOnAwake = true;

        director.Stop();

        ResetTracks();
    }
    
    private void Update()
    {
        if(!tut1.activeInHierarchy && inTutorial && !started)
        {
            started = true;

            tut1.SetActive(true);
        }
    }

    public void TutorialStep()
    {
        if(tut1.activeSelf)
        {
            tut1.SetActive(false);

            tut2.SetActive(true);
        }
        else if(tut2.activeSelf)
        {
            tut2.SetActive(false);

            tut3.SetActive(true);
        }
        else if(tut3.activeSelf)
        {
            tut3.SetActive(false);

            inTutorial = false;
        }
    }

    public void Win()
    {
        PlayerMovement.StopMoving();

        if(phaseSwitchCoroutine != null)
        {
            StopCoroutine(phaseSwitchCoroutine);
        }

        director.Stop();

        ResetTracks();

        SceneManager.LoadScene(0);
    }

    public void NextSection()
    {
        PlayerMovement.StopMoving();

        currentCamPosition++;

        currentSavedPos = player.position;

        inSegmentChange = true;

        StopCoroutine(phaseSwitchCoroutine);

        director.Stop();

        ResetTracks();

        StartCoroutine(CameraLerpBehaviour());
    }

    private IEnumerator CameraLerpBehaviour()
    {
        float time = 0;

        Vector3 startValue = cameraFollow.position;
        
        Vector3 endValue = CamPositions[currentCamPosition].position;

        while (time < camLerpDuration)
        {
            cameraFollow.position = Vector3.Lerp(startValue, endValue, time / camLerpDuration);

            time += Time.deltaTime;

            yield return null;
        }

        cameraFollow.position = endValue;
        
        inSegmentChange = false;

        pressAnyKeyToContinue = true;
    }

    public void ContinueGame()
    {
        switch(currentCamPosition)
        {
            case 0:
                currentSequenceDuration = 3f;
            break;

            case 1:
                currentSequenceDuration = 5f;
            break;

            case 2:
                currentSequenceDuration = 5f;
            break;

            case 3:
                currentSequenceDuration = 8f;
            break;

            default:
                currentSequenceDuration = 5f;
            break;
        }

        phaseSwitchCoroutine = StartCoroutine(PhaseSwitchBehaviour());

        inInputPhase = true;
    }

    private IEnumerator PhaseSwitchBehaviour()
    {
        pressAnyKeyToContinue = false;

        playerInputs.timer = 0f;

        yield return new WaitForSeconds(currentSequenceDuration);
        
        if(inInputPhase)
        {
            inInputPhase = false;

            director.Play();

            playerInputs.FinaliseClips();

            phaseSwitchCoroutine = StartCoroutine(PhaseSwitchBehaviour());
        }
        else
        {
            director.Stop();
            
            ResetTracks();

            ResetSegment();
        }
    }

    private void ResetSegment()
    {
        player.position = currentSavedPos;

        pressAnyKeyToContinue = true;
    }

    private void ResetTracks()
    {
        foreach(TimelineClip clip in moveLeftTrack.GetClips())
        {
            moveLeftTrack.DeleteClip(clip);
        }

        foreach(TimelineClip clip in moveRightTrack.GetClips())
        {
            moveRightTrack.DeleteClip(clip);
        }

        foreach(TimelineClip clip in jumpTrack.GetClips())
        {
            jumpTrack.DeleteClip(clip);
        }

        foreach(TimelineClip clip in slideTrack.GetClips())
        {
            slideTrack.DeleteClip(clip);
        }
    }
}
