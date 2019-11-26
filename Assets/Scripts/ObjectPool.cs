using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

    public RcycleGameObject prefab;

    private List<RcycleGameObject> poolInstances = new List<RcycleGameObject>();

    private RcycleGameObject CreateInstance(Vector3 pos) {

        var clone = GameObject.Instantiate(prefab);
        clone.transform.position = pos;
        clone.transform.parent = transform;

        poolInstances.Add(clone);

        return clone;
    
    }

    public RcycleGameObject ReturnInstance(Vector3 pos) {

        RcycleGameObject instance = null;

        foreach(var go in poolInstances) {
            if (!go.gameObject.activeSelf) {

                instance = go;
                instance.transform.position = pos;            
            }        
        }

        if(instance == null)
            instance = CreateInstance(pos);
        
        instance.Restart();

        return instance;
    }
}
