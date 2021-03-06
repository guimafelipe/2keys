﻿using UnityEngine;
using System.Collections;

public class GameOverFade : MonoBehaviour {

	public Texture2D gameOverTexture;  //the game over image

	public float fadeSpeed = 0.8f; //The speed of fade 

	private int drawDepth = -1000;  

	private float alpha = 0f;
	private int fadeDir = -1;

	void OnGUI(){
		alpha += fadeDir * fadeSpeed * Time.deltaTime;

		alpha = Mathf.Clamp01 (alpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0,0,Screen.width, Screen.height), gameOverTexture);
	
	}

	public float BeginFade (int direction){
		fadeDir	= direction;
		return (fadeSpeed);
	}
}
