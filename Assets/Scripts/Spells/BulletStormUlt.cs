using PLayerScripts;
using UnityEngine;
using UnityEngine.Analytics;

namespace Spells {
    public class BulletStormUlt : Spell {

        public GameObject BulletPrefab;

        public void Start() {
            Destroy(gameObject, 2);  
        }

        public void Update() {
            for (var i = 0; i < 3; i++) {
                var x = Instantiate(BulletPrefab);
                x.transform.position = new Vector3(CastOrigin.transform.position.x, 
                    CastOrigin.transform.position.y + 1.5f,
                    CastOrigin.transform.position.z);
                x.GetComponent<Spell>().CastOrigin = CastOrigin;
                
                // calculate formula for damage
                x.GetComponent<Spell>().Damage += SpellLevel * 2;
                
                x.transform.rotation = CastOrigin.transform.rotation;
                x.transform.Rotate(CastOrigin.transform.up, Random.Range(-30f, 30f));
            }
        }

    }
}