using UnityEngine;

namespace Spells {
    public class StormCall : Spell {

        private ParticleSystem _particleSystem;
        private bool _updated;
        
        private void Start() {
            transform.parent = CastOrigin.transform;
            transform.position = Origin;
            _particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
            _updated = false;
            transform.localScale = new Vector3(SpellLevel, 0.01f, SpellLevel);

        }

        private void Update() {
            if (!_updated && _particleSystem != null) {
                MoreRadius();
                _updated = true;
            }
        }
        
        private void MoreRadius() {
            var model = _particleSystem.shape;
            model.radius = SpellLevel;
        }

    }
}