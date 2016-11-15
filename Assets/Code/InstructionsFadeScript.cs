using UnityEngine;
using System.Collections;

public class InstructionsFadeScript : MonoBehaviour {

	public Texture2D instructionsTexture;  //the credits image

	public float fadeSpeed = 0.8f; //The speed of fade 

	private int drawDepth = -1000;  

	private float instructionsAlpha = 0f;
	private int instructionsFadeDir = -1;

	void OnGUI(){
		instructionsAlpha += instructionsFadeDir * fadeSpeed * Time.deltaTime;

		instructionsAlpha = Mathf.Clamp01 (instructionsAlpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, instructionsAlpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0,0,Screen.width, Screen.height), instructionsTexture);

	}

	public float ShowInstructions (){
		instructionsFadeDir = 1;
		return (fadeSpeed);
	}

	public float HideInstructions(){
		instructionsFadeDir = -1;
		return (fadeSpeed);
	}

}
