using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] StudioEventEmitter currentMusic;

    public void SetMusicParameter(string name, float value)
    {
        currentMusic.SetParameter(name, value);
    }

    public void ResetParamaters()
    {
        currentMusic.SetParameter("Movement", 0f);
        currentMusic.SetParameter("Jump", 0f);
        currentMusic.SetParameter("Slide", 0f);
    }
}
