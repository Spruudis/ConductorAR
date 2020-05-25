using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

sealed class GameObjectReference
{
    public Func<GameObject> Get { get; private set; }

    public Action<GameObject> Set { get; private set; }

    public GameObjectReference(Func<GameObject> getter, Action<GameObject> setter)
    {
        Get = getter;
        Set = setter;
    }
}
