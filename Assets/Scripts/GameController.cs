using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameController : MonoBehaviour
{
    private List<Transform> CamPositions = new List<Transform>();

    private Transform cameraFollow;

    private PlayableDirector director;

    private PlayerInputs playerInputs;

    private TimelineAsset replay;

    private TrackAsset moveLeftTrack;
    
    private TrackAsset moveRightTrack;

    private TrackAsset jumpTrack;
    
    private TrackAsset slideTrack;

    private Coroutine phaseSwitchCoroutine;

    [HideInInspector] public bool inInputPhase = true;
    
    public float currentSequenceDuration = 5f;

    public float camLerpDuration = 3f;

    private int currentCamPosition = 0;

    void Awake()
    {
        Transform camPositions = GameObject.Find("CamPositions").transform;

        for(int i = 0; i < camPositions.childCount; i++)
        {
            CamPositions.Add(camPositions.GetChild(i));
        }

        cameraFollow = GameObject.Find("Mendatory").transform.Find("CameraFollow");

        director = GameObject.Find("Player").GetComponent<PlayableDirector>();

        playerInputs = FindObjectOfType<PlayerInputs>();

        replay = (TimelineAsset)director.playableAsset;
        
        moveLeftTrack = replay.GetOutputTrack(1);

        moveRightTrack = replay.GetOutputTrack(2);

        jumpTrack = replay.GetOutputTrack(3);

        slideTrack = replay.GetOutputTrack(4);

        director.playOnAwake = true;

        director.Stop();

        phaseSwitchCoroutine = StartCoroutine(PhaseSwitchBehaviour());
            
        ResetTracks();
    }

    public void NextSection()
    {
        currentCamPosition++;

        StopCoroutine(phaseSwitchCoroutine);

        director.Stop();

        inInputPhase = true;

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
        
        phaseSwitchCoroutine = StartCoroutine(PhaseSwitchBehaviour());
    }

    private IEnumerator PhaseSwitchBehaviour()
    {
        playerInputs.timer = 0f;

        yield return new WaitForSeconds(currentSequenceDuration);
        
        if(inInputPhase)
        {
            inInputPhase = false;

            director.Play();

            playerInputs.FinaliseClips();
        }
        else
        {
            director.Stop();

            inInputPhase = true;
            
            ResetTracks();
        }

        phaseSwitchCoroutine = StartCoroutine(PhaseSwitchBehaviour());
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
