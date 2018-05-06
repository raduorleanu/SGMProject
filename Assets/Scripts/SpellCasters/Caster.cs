using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Interfaces;
using PLayerScripts;
using Spells;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Caster : MonoBehaviour, ICaster {
    [SerializeField] private GameObject _spell;

    [SerializeField] private int _spellLevel;

    [SerializeField] private int _spellSpeed;

    [SerializeField] private float _cooldown = 10f;

    [SerializeField] private GameObject _guiImage;

    [SerializeField] private GameObject _guiOverlay;

    private float _nextCast;

    //private bool _onCoolDown;
    private float _initialY = 7;
    private float _secondPercentage;
    private float _per;
    private float _initialCoolDown;

    private void Start() {
        _initialCoolDown = _cooldown;
        PlayerExperience.LevelUpEvent += LevelUp;
    }

    private void LevelUp(GameObject player) {
        if (player.GetHashCode() != gameObject.GetHashCode()) return;
        // toDo: Modify your spell on lvl up with desired effects
        _spellLevel += 1;
        //Debug.Log("Spell " + _spell.gameObject.name + " is now level " + _spellLevel);
    }

    public void Cast(GameObject origin) {
        if (Time.time > _nextCast) {
            _nextCast = Time.time + _initialCoolDown;
            //_onCoolDown = true;
            var s = Instantiate(_spell);
            var c = s.GetComponent<Spell>();
            c.SpellLevel = _spellLevel;
            c.Speed = _spellSpeed;
            c.AssignPosition(origin);
            _guiOverlay.transform.localScale = _guiImage.transform.localScale;

            _guiOverlay.transform.position = _guiImage.transform.position;

            // percentage of cooldown that 1 seconds is:
            _secondPercentage = 100 / _cooldown;

            // percentage of secondPercentage from 7 (initial gui overlay height)

            _per = _secondPercentage * 7 / 100;
            InvokeRepeating("ShowTimeOnUi", 1, 1);
        }
    }

    private void ShowTimeOnUi() {
        _guiOverlay.transform.localScale = new Vector3(_guiOverlay.transform.localScale.x,
            _guiOverlay.transform.localScale.y - _per, _guiOverlay.transform.localScale.z);

        _cooldown -= 1;

        if (_cooldown <= 0) {
            _guiOverlay.transform.localScale = Vector3.zero;
            CancelInvoke("ShowTimeOnUi");
            _cooldown = _initialCoolDown;
            //_onCoolDown = false;
        }
    }

    private void OnDisable() {
        PlayerExperience.LevelUpEvent -= LevelUp;
    }
}