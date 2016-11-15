using UnityEngine;
using System.Collections;

public class EnemyTeamBehaviour : MonoBehaviour {

	public GameObject enemyChinchilaPrefab;
	public GameObject enemyTeam;
	public float chinchilaInstanceRange;
	private int teamSize;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void CreateEnemyChinchilas(int _chinchilasNumber){
		teamSize = _chinchilasNumber;
		for(int i = 0; i < _chinchilasNumber; i++){
			GameObject newChinchila = Instantiate(enemyChinchilaPrefab);
			newChinchila.transform.SetParent(gameObject.transform);
			newChinchila.transform.position = transform.position;
			newChinchila.transform.Translate (Random.Range (-chinchilaInstanceRange, chinchilaInstanceRange), Random.Range (-chinchilaInstanceRange,chinchilaInstanceRange), 0f);
		}
	}


	public void FazOUrroPartial(string _urroType, int _result){
		Transform selectedChinchila = transform.GetChild ((int)Mathf.Floor (Random.Range (0f, (float)teamSize)));
		var selectedChinchilaBehaviour = selectedChinchila.GetComponent<EnemyChinchilaController> ();
		if (selectedChinchilaBehaviour) {
			selectedChinchilaBehaviour.FazOUrro (_urroType);
			//Debug.Log ("Chamou enemy team urro partial");
		}
	}

	public void FazOUrroTotal(string _urroType){
		for (int i = 0; i < teamSize; i++) {
			var selectedChinchilaBehaviour = transform.GetChild (i).GetComponent<EnemyChinchilaController> ();
			if (selectedChinchilaBehaviour) {
				selectedChinchilaBehaviour.FazOUrro (_urroType);
				//Debug.Log ("Chamou enemy team urro total");
			}

		}
	}
}
