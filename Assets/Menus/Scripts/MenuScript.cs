using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.33f;

    void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>(); //Might break if multiple canvas group are present
        fadeGroup.alpha = 1;

    }

    void Update()
    {
        //Fade in
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    }


    //Buttons
    public void OnPlayClick()
    {
        //Load AR Scene
        SceneManager.LoadScene("Orchestra");
    }

    public void OnComposeClick()
    {
        Debug.Log("Compose button has been cliked");
    }
}
