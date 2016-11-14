using UnityEngine;
using System.Collections;

public class BlockMap : MonoBehaviour {



	public static string GetBlock(int _round){

		if(_round <= 10){
			switch (_round){
				case 0:
					return "+-";
				case 1:
					return "-+";
				case 2:
					return "-++";
				case 3:
					return "---";
				case 4:
					return "+-+";
				case 5:
					return "++--";
				case 6:
					return "-+-+";
				case 7:
					return "----";
				case 8:
					return "+-+--+";
				case 9:
					return "-+--";
				case 10:
					return "---+";
				default:
					return "+-";
			}
		}
		return BlockMap.GetRandomRound(_round);
	}

		
	static string GetRandomRound(int _round){
		float blockSizeRndSelector;
		int blockSize;
		blockSizeRndSelector = Random.Range (-1f, 1f);

		if (blockSizeRndSelector >= 0) {
			blockSize = 5;
		} else {
			blockSize = 4;
		}

		string result;
		char[] _arrows;

		_arrows = new char[blockSize];

		for (int i = 0; i < blockSize; i++) {
			float arrowRandomSelector;
			arrowRandomSelector = Random.Range (-1f, 1f);
			if (arrowRandomSelector >= 0) {
				_arrows [i] = '+';
			} else {
				_arrows [i] = '-';
			}
		}

		result = new string (_arrows);
		Debug.Log(result);
		return result;
	}

}
