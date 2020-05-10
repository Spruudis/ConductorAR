using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SongData
{
    public List<string> instruments;
    // Start is called before the first frame update
    public string songName;
    public SongData(string Name, List<string> instrumentsvar)
    {
        songName = Name;
        instruments = instrumentsvar;
    }

}
