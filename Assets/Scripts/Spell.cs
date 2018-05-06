using UnityEngine;

public class Spell : MonoBehaviour {
    protected Vector3 Origin;
    
    public GameObject CastOrigin;
    public float Speed;
    public int SpellLevel;
    public int Damage;
    
    public void AssignPosition(GameObject player) {
        //TargetPosition = target;
        var origin = player.transform.position;
        origin.y += 1;
        Origin = origin;
        CastOrigin = player;
    }
}