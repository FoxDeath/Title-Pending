using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] StudioEventEmitter currentMusic;

    public void SetMusicParameter(string name, float value)
    {
        ResetParamaters();
        
        currentMusic.SetParameter(name, value);
    }

    public void ResetParamaters()
    {
        SetMusicParameter("Movement", 0f);
        SetMusicParameter("Jump", 0f);
        SetMusicParameter("Slide", 0f);
    }
}
