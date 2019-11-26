using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject[] prefabs;
    public float delay = 2f;
    public bool isActive = true;
    public Vector2 randomDelay = new Vector2(1,2);


	void Start () {
        ResetDelay();
        StartCoroutine(EnemyGenerator());
	}
    IEnumerator EnemyGenerator() {

        yield return new WaitForSeconds(delay);

        if (isActive) {

            Transform objectTransform = transform;

            GameObjectUtil.Instantiate(prefabs[Random.Range(0, prefabs.Length)], objectTransform.position);
           
            ResetDelay();
        }

        StartCoroutine(EnemyGenerator());


    }

    private void ResetDelay() {
        delay = Random.Range(randomDelay.x,randomDelay.y);   
        
    }



}
