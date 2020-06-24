using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    private GameObject[] CharacterList;

    // Start is called before the first frame update
    private void Start()
    {
      // array size should equal number of objects in CharacterList
      CharacterList = new GameObject[transform.childCount];

      // Fill array with models
      for(int i=0; i<transform.childCount; i++)
        CharacterList[i] = transform.GetChild(i).gameObject;

      // Toggle off renderer
      foreach(GameObject go in  CharacterList)
        go.SetActive(false);

      // Toggle on first models
      if(CharacterList[0])
        CharacterList[0].SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
