using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedTexture : MonoBehaviour {

    public Vector2 speed = Vector2.zero;
    public bool scaleHorizontally = true;
    public bool scaleVertically = true;

    
    private Material material;
    private Vector2 offset = Vector2.zero;
	
    

	void Start () {
        material = GetComponent<Renderer>().material;

        offset = material.GetTextureOffset("_MainTex");
        
        

	}
	

	void Update () {
        offset += speed * Time.deltaTime;

        material.SetTextureOffset("_MainTex",offset);

        
	}

  

}
