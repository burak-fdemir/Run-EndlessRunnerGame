using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    private InputState inputState;

    private Animator animator;

	void Awake () {
        inputState = GetComponent<InputState>();
        animator = GetComponent<Animator>();
	}
	
	
	void Update () {

        var isRunning = true;

        if (inputState.absValX > 0 && inputState.absValY < inputState.standingTreshold)
            isRunning = false;

        animator.SetBool("isRunning", isRunning);
        
        
        
	}
}
