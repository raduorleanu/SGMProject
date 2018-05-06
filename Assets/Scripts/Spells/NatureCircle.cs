using PLayerScripts;
using UnityEngine;

namespace Spells {
    public class NatureCircle : Spell {
        
        private GameObject _player;
        private int _lifespan = 10;
        private bool _mooving;
        private Vector3 _fwd;
        

        private void Start() {
            _player = CastOrigin;
            _fwd = _player.transform.forward;
            transform.position = _player.transform.position;
            _lifespan += SpellLevel * 2;
            Invoke("StopMoving", 3);
            Destroy(gameObject, _lifespan);
            Damage += Damage + SpellLevel * 2;
        }

        private void Update() {
            if (!_mooving) {
                transform.position += _fwd * Time.deltaTime * Speed * 3;
            }
        }

        private void StopMoving() {
            _mooving = true;
        }
        
    }
}