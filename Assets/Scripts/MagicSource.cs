using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicSource : MonoBehaviour {

//	[SerializeField]
//	private ChainLightning _spell;

	private ICaster[] _casters;
	private GameObject _player;

	public KeyCode Spell1;
	public KeyCode Spell2;
	public KeyCode Spell3;
	public KeyCode Spell4;

	private void Awake() {
		_casters = GetComponents<ICaster>();
		_player = gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(Spell1)) {
			if (_casters.Length > 0) {
				CastSpell(0);
			}
		}
		
		if (Input.GetKeyDown(Spell2)) {
			if (_casters.Length > 1) {
				CastSpell(1);
			}
		}
		
		if (Input.GetKeyDown(Spell3)) {
			if (_casters.Length > 2) {
				CastSpell(2);
			}
		}
		
		if (Input.GetKeyDown(Spell4)) {
			if (_casters.Length > 3) {
				CastSpell(3);
			}
		}
	}

	private void CastSpell(int spellNumber) {
		//var spell = Instantiate(_spell);
//		foreach (var caster in _casters) {
//			caster.Cast(GetComponent<Player>().Target.transform.position, transform.position);
//		}
		_casters[spellNumber].Cast(_player);
	}
}
