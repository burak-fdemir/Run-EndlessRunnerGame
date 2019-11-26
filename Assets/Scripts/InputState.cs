using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputState : MonoBehaviour {

    public bool actionButton;
    public float absValX = 0;
    public float absValY = 0;
    public bool standing;
    public float standingTreshold = 1;

    private Rigidbody2D body2D;


	void Awake () {
        body2D = GetComponent<Rigidbody2D>();
	}
	
    void Update() {
        actionButton = Input.anyKeyDown;
    
    }

	void FixedUpdate () {
        absValX = Mathf.Abs(body2D.velocity.x);
        absValY = Mathf.Abs(body2D.velocity.y);

        standing = absValY <= standingTreshold;
    }
}
