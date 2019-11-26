using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectUtil{

    private static Dictionary<RcycleGameObject, ObjectPool> pools = new Dictionary<RcycleGameObject, ObjectPool>();
	
	public static GameObject Instantiate(GameObject prefab, Vector3 pos) {

        GameObject instance = null;

        var rcyledScript = prefab.GetComponent<RcycleGameObject>();
        if (rcyledScript != null)
        {
            var pool = GetObjectPool(rcyledScript);
            instance = pool.ReturnInstance(pos).gameObject;

        }
        else{
            instance = GameObject.Instantiate(prefab);
            instance.transform.position = pos;
        }
        return instance;
    }

    public static void Destroy(GameObject prefab) {

        var gameRecycleObject = prefab.GetComponent<RcycleGameObject>();

        if (gameRecycleObject != null){
            
            gameRecycleObject.ShutDown();

        }
        else {

            GameObject.Destroy(prefab);
        }
    }

    private static ObjectPool GetObjectPool(RcycleGameObject reference) {
        ObjectPool pool = null;

        if (pools.ContainsKey(reference)) {
            pool = pools[reference];        
        } else {

            var poolContainer = new GameObject(reference.gameObject.name + "ObjectPool");
            pool = poolContainer.AddComponent<ObjectPool>();
            pool.prefab = reference;
            pools.Add(reference, pool);
        
        }

        return pool;
    
    }

}
