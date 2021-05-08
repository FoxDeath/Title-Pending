using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class GameController : MonoBehaviour
{
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

    void Awake()
    {
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

    private IEnumerator PhaseSwitchBehaviour()
    {
        playerInputs.timer = 0f;

        yield return new WaitForSeconds(currentSequenceDuration);
        
        if(inInputPhase)
        {
            inInputPhase = false;

            director.Play();
        }
        else
        {
            director.Stop();

            inInputPhase = true;

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

        phaseSwitchCoroutine = StartCoroutine(PhaseSwitchBehaviour());
    }
}
