using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongLoader : MonoBehaviour
{
    static public string songName;
    static public List<string> instruments;
    static public Dictionary<string, List<float>> instrumentCues;
    static public Dictionary<string, string> clipNames;
    static public int BPM;
}
