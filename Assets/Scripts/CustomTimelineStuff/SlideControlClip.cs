using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SlideControlClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] SlideControlBehaviour template = new SlideControlBehaviour();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<SlideControlBehaviour>.Create(graph, template);
    }
}