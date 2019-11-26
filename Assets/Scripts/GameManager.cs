using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {
    public AudioClip restart;
    public GameObject playerPrefab;
    public Text continueText;
    public Text scoreText;

    private AudioSource audioSource;
    private float timeElapsed = 0;
    private float bestTime = 0;
    private float blinkTime = 0;
    private bool blink;
    private bool isGameStarted;
    private GameObject player;
    private GameObject floor;
    private Spawner spawner;
    private TimeManager timeManager;
    private bool isBeatTheBestTime;
	void Awake () {

        timeManager = GetComponent<TimeManager>();
        floor = GameObject.Find("ForeGround");
        spawner = GameObject.Find("Spawner").GetComponent<Spawner>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Start () {  
        float floorHeight = floor.transform.localScale.y;
        Vector3 position = floor.transform.position;

        position.x = 0;
        position.y = -((Screen.height/ PixelPerfectCamera.pixelsPerUnit) / 2) + (floorHeight/2);
        floor.transform.position = position;

        spawner.isActive = false;
        Time.timeScale = 0;
        continueText.text = "PRESS ANY BUTTON TO START";

        bestTime = PlayerPrefs.GetFloat("bestTime");

    }

    void Update() { 
    
        if(!isGameStarted && Time.timeScale == 0) {

            if (Input.anyKeyDown) {
                timeManager.ManipulateTime(1,1f);
                ResetGame();            
            }
        
        }

        if (!isGameStarted) {
            blinkTime++;
            
            if(blinkTime % 40 == 0) {
                blink = !blink;            
            }

            continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);

            var textColor = isBeatTheBestTime ? "#FF0" : "#FFF";

            scoreText.text = "SCORE: " + FormatTime(timeElapsed) + "\n<color="+textColor+">BEST: " + FormatTime(bestTime) + "</color>";
        
        }
        else {
            timeElapsed += Time.deltaTime;
            scoreText.text = "SCORE: " + FormatTime(timeElapsed);
        }
    
    }

    void OnPlayerKilled() {

        spawner.isActive = false;

        var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        playerDestroyScript.DestroyCallBack -= OnPlayerKilled;

        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        timeManager.ManipulateTime(0,5.5f);

        isGameStarted = false;
        continueText.text = "PRESS ANY BUTTON TO START";

        if(timeElapsed > bestTime) {
            bestTime = timeElapsed;
            PlayerPrefs.SetFloat("bestTime", bestTime);
            isBeatTheBestTime = true;
        }
    }

    private void ResetGame() {
        spawner.isActive = true;

        player = GameObjectUtil.Instantiate(playerPrefab, new Vector3(0, (Screen.height / PixelPerfectCamera.pixelsPerUnit) / 2 + 100, 0));

        var playerDestroyScript = player.GetComponent<DestroyOffscreen>();
        playerDestroyScript.DestroyCallBack += OnPlayerKilled;

        isGameStarted = true;
        continueText.canvasRenderer.SetAlpha(0);

        timeElapsed = 0;
        isBeatTheBestTime = false;

        audioSource.PlayOneShot(restart);
    }

    string FormatTime(float timeValue) {

        TimeSpan t = TimeSpan.FromSeconds(timeValue);
        
        return string.Format("{0:D2}:{1:D2}",t.Minutes,t.Seconds);
    
    }
}
