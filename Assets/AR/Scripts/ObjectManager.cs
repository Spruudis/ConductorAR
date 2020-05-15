using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    //Singleton behaviour
    private static ObjectManager _instance;
    public static ObjectManager Instance { get { return _instance; } }


    //Selected object to spawn
    private GameObject objectToSpawn;

    //Pointer to the appropriate object in memory depending on selected object to spawn
    private string targetObject;


    //GameObject asset references to be instantiated
    [SerializeField]
    private GameObject xylophoneReference;

    [SerializeField]
    private GameObject pianoReference;

    [SerializeField]
    private GameObject violinsReference;

    [SerializeField]
    private GameObject drumsReference;


    //Instantiated objects
    //private Dictionary<string, GameObjectReference> spawnedObjects;    //Use this if the dictionary stores a copy of the object and it cannot be changed appropriately
    private Dictionary<string, GameObject> spawnedObjectsDict;







    //Ensuring this class ins a singleton
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

    }


    private void Start()
    {
        objectToSpawn = xylophoneReference;
        targetObject = "xylophone";

        spawnedObjectsDict = new Dictionary<string, GameObject>();
    }


    //functions to select spawnable object
    public void selectXylophone()
    {
        objectToSpawn = xylophoneReference;
        targetObject = "xylophone";
    }

    public void selectPiano()
    {
        objectToSpawn = pianoReference;
        targetObject = "piano";
    }

    public void selectViolins()
    {
        objectToSpawn = violinsReference;
        targetObject = "violins";
    }

    public void selectDrums()
    {
        objectToSpawn = drumsReference;
        targetObject = "drums";
    }



    //functions responsible for spawning each instrument
    public bool spawnObject(Vector3 position, Quaternion rotation)
    {
        if (!spawnedObjectsDict.ContainsKey(targetObject)){ //Object is not in the dictionary

            spawnedObjectsDict.Add(targetObject, Instantiate(objectToSpawn, position, rotation)); //Instantiate and add the object to the dict
            Console.WriteLine("New object spawned");
            return true;
        }
        else
        {
            Console.WriteLine("Type of object already has been spawned, moving the object instead");
            spawnedObjectsDict[targetObject].transform.Translate(Vector3.up * 0.1f);

            return false;
        }


    }



}
