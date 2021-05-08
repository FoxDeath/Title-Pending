using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class JumpControlClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] JumpControlBehaviour template = new JumpControlBehaviour();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<JumpControlBehaviour>.Create(graph, template);
    }
}