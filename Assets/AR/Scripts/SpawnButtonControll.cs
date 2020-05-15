using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonControll : MonoBehaviour
{
    [SerializeField]
    private Button xylophoneButton;

    [SerializeField]
    private Button pianoButton;

    [SerializeField]
    private Button violinsButton;

    [SerializeField]
    private Button drumsButton;

    void Start()
    {
        //Go through the list of instruments for the selected song
        foreach (string instrument in SongLoader.instruments)
        {
            //Enable the correct button 
            switch (instrument)
            {
                case "xylophone":
                    xylophoneButton.gameObject.SetActive(true);
                    break;
                case "piano":
                    pianoButton.gameObject.SetActive(true);
                    break;
                case "violins":
                    violinsButton.gameObject.SetActive(true);
                    break;
                case "drums":
                    drumsButton.gameObject.SetActive(true);
                    break;
            }
        }
    }


}
