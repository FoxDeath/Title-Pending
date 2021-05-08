using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class MovementRightControlClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] MovementRightControlBehaviour template = new MovementRightControlBehaviour();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        return ScriptPlayable<MovementRightControlBehaviour>.Create(graph, template);
    }
}