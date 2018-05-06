using System.Collections;
using System.Collections.Generic;
using Interfaces;
using PLayerScripts;
using UnityEngine;
using UnityEngine.Analytics;
using Utillity;

public class Player : MonoBehaviour, ITakeDamage {
	
	public float Health;

	public float OriginalHealth;

	public float RespawnTime = 10f;

	//public UnityEvent OnDamageTaken;

	private UiHealthBar _uiHealthBar;

	private void Start() {
		OriginalHealth = Health;
		_uiHealthBar = GetComponent<UiHealthBar>();
		PlayerExperience.LevelUpEvent += LevelUp;
	}

	//public GameObject Target;

	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SubstarctDamageFromLife_(float damageTaken) {
		Health -= damageTaken;
		//GetComponent<UiHealthBar>().ShrinkBar(damageTaken, _health);
		if (Health <= 0) {
			gameObject.SetActive(false);
			Invoke("ActivateRespawn", RespawnTime);
			//Destroy(gameObject);
		}
		else {
			_uiHealthBar.ShrinkBar(damageTaken, OriginalHealth);
		}
	}
	
	public void Heal(float life) {
		if (OriginalHealth <= Health) return;
		Health += life;
		_uiHealthBar.GrowBar(life, OriginalHealth);
	}

	private void ActivateRespawn() {
		gameObject.transform.position = new Vector3(26.4f, 1f, 7.5f);
		Health = OriginalHealth;
		_uiHealthBar.ResetBar();
		gameObject.SetActive(true);
	}

	private void LevelUp(GameObject player) {
		_uiHealthBar.GrowBar(OriginalHealth- Health, OriginalHealth);
		OriginalHealth += 20;
		Health = OriginalHealth;
	}
}
