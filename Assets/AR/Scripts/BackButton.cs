using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{

    [SerializeField]
    private GameObject panel;


    public void OnBackClick()
    {
        //Load Song Selection
        panel.SetActive(true);
    }

    public void OnYesClick()
    {
        //Load Song Selection
        SceneManager.LoadScene("SongSelection");
    }

    public void OnNoClick()
    {
        //Load Song Selection
        panel.SetActive(false);
    }


}
