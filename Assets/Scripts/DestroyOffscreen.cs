using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOffscreen : MonoBehaviour {

    public float offset = 16;
    public delegate void OnDestroy();
    public event OnDestroy DestroyCallBack;

    private bool offscreen;
    private float offscreenx = 0;
    private Rigidbody2D body2D;

	void Awake () {
        body2D = GetComponent<Rigidbody2D>();
	}

    void Start() {
        offscreenx = (Screen.width / PixelPerfectCamera.pixelsPerUnit) / 2 + offset;
    }
    

    void Update () {
        var posX = transform.position.x;
        var dirX = body2D.velocity.x;

        if(Mathf.Abs(posX) > offscreenx) { 
        
            if(dirX < 0 && posX < -offscreenx) {
                offscreen = true;
            }
            else if(dirX> 0 && posX > offscreenx) {
                offscreen = true;
            }

        }
        else {
            offscreen = false;
        }

        if (offscreen) {
            OutOfBounds();
            
        }
	
    }

    public void OutOfBounds() {
        offscreen = false;
        GameObjectUtil.Destroy(gameObject);    


        if(DestroyCallBack != null) {
            DestroyCallBack();
        
        }
    }



}
