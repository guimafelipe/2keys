using UnityEngine;
using System.Collections;

public class CreditsFadeScript : MonoBehaviour {

	public Texture2D creditsTexture;  //the credits image

	public float fadeSpeed = 0.8f; //The speed of fade 

	private int drawDepth = -1000;  

	private float creditsAlpha = 0f;
	private int creditsFadeDir = -1;

	void OnGUI(){
		creditsAlpha += creditsFadeDir * fadeSpeed * Time.deltaTime;

		creditsAlpha = Mathf.Clamp01 (creditsAlpha);

		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, creditsAlpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture (new Rect (0,0,Screen.width, Screen.height), creditsTexture);

	}

	public float ShowCredits (){
		creditsFadeDir = 1;
		return (fadeSpeed);
	}

	public float HideCredits(){
		creditsFadeDir = -1;
		return (fadeSpeed);
	}

}
