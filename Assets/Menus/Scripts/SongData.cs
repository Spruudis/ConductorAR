﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SongData
{
    public List<string> instruments;
    // Start is called before the first frame update
    public string songName;
    public List<float> cues;
    public List<string> clipNames;
    public int BPM;
    public SongData(string Name, List<string> instrumentsvar, List<float> inputCues, List<string> inputClipNames, int inputBPM)
    {
        songName = Name;
        instruments = instrumentsvar;
        cues = inputCues;
        clipNames = inputClipNames;
        BPM = inputBPM;
    }

}
