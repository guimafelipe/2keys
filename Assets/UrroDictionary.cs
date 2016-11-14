using UnityEngine;
using System.Collections;

public class UrroDictionary : MonoBehaviour {

	public static string GetUrro(string _urroType){  //get the urro sprite from the urroType string
		switch (_urroType){
		case "++":
			return "roar";
		case "+-":
			return "grr";
		case "--":
			return "birl";
		case "-+":

		case "+++":

		case "---":

		case "++-":

		case "--+":

		default:
			return "rawr";
		}
	
	}
}
