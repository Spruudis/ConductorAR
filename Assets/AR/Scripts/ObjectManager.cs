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
    private GameObject synthReference;

    [SerializeField]
    private GameObject pianoReference;

    [SerializeField]
    private GameObject celloReference;

    [SerializeField]
    private GameObject stringsReference;


    //Instantiated objects
    //private Dictionary<string, GameObjectReference> spawnedObjects;    //Use this if the dictionary stores a copy of the object and it cannot be changed appropriately
    private Dictionary<string, GameObject> spawnedObjectsDict;

  
    private Dictionary<string, GameObject> referenceObjectsDict;






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
        objectToSpawn = synthReference;
        targetObject = "synth";

        spawnedObjectsDict = new Dictionary<string, GameObject>();

        referenceObjectsDict = new Dictionary<string, GameObject>
        {
            { "synth", synthReference },
            { "piano", pianoReference },
            { "cello", celloReference },
            { "strings", stringsReference }
        };

    }


    public void selectObject(string name)
    {
        objectToSpawn = referenceObjectsDict[name];
        targetObject = name;
    }

    //functions responsible for spawning each instrument
    public bool spawnObject(Vector3 position, Quaternion rotation)
    {
        if (!spawnedObjectsDict.ContainsKey(targetObject)){ //Object is not in the dictionary


            //Find the angle
            Vector3 rot = rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);

            spawnedObjectsDict.Add(targetObject, Instantiate(objectToSpawn, position, Quaternion.Euler(rot))); //Instantiate and add the object to the dict
            Debug.Log("New object spawned");
            return true;
        }
        else
        {
            Debug.Log("Type of object already has been spawned, moving the object instead");
            spawnedObjectsDict[targetObject].transform.Translate(Vector3.up * 0.1f);

            return false;
        }


    }

    public void despawnObject()
    {
        //REmove the object
        Debug.Log("ObjectManager: despawnObject --- Attempting to destroy " + targetObject);
        if (spawnedObjectsDict.ContainsKey(targetObject))
        {
            Destroy(spawnedObjectsDict[targetObject]);
            spawnedObjectsDict.Remove(targetObject);
            Debug.Log("ObjectManager: despawnObject --- --- Destruction successful");
        }
    }

    public Dictionary<string, GameObject> allObjectsSpawned(){
        return spawnedObjectsDict;
    }



}
