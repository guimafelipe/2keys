using UnityEngine;
using System.Collections;

public class HealthIndicator : MonoBehaviour {

	[SerializeField]
	private RectTransform healthBar;

	// Use this for initialization
	void Start () {
		if (healthBar == null) {
			Debug.Log ("there is no health bar!");
		}
	}
	
	// Update is called once per frame
	public void SetHealth (int _currentHealth, int _maxHealth) {
		float _value = (float)_currentHealth / _maxHealth;
		_value = Mathf.Clamp01 (_value);
		healthBar.localScale = new Vector3 (_value, healthBar.localScale.y, healthBar.localScale.z); 
	}
}
