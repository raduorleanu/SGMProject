using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utillity;

public class Fireorb : Spell
{
    private Vector3 _orbPosition;
    private bool _casted = true;
    private bool _splitting;
    private bool _shooting;
    private GameObject _target;

    public int Count;

    // Use this for initialization
    private void Start()
    {
        transform.position = new Vector3(CastOrigin.transform.position.x, CastOrigin.transform.position.y + 2,
            CastOrigin.transform.position.z);
        _orbPosition = new Vector3(CastOrigin.transform.position.x, CastOrigin.transform.position.y + 7,
            CastOrigin.transform.position.z);

        transform.localScale = new Vector3(0.1f * SpellLevel, 0.1f * SpellLevel, 0.1f * SpellLevel);
        Invoke("Die",4);
        Invoke("Transit", Random.Range(0.3f, 1.2f));
    }

    private void LevelUp()
    {
    }


    // Update is called once per frame
    private void Update()
    {
        if (_casted)
        {
            transform.position = Vector3.Lerp(transform.position, _orbPosition, Time.deltaTime * 5);
        }

        if (_splitting)
        {
            if (Count <= SpellLevel * 2)
            {
                var x = Instantiate(this);
                x.CastOrigin = CastOrigin;
                x.Count++;
                _splitting = false;
                _shooting = true;
            }
        }

        if (_shooting)
        {
            if (_target == null)
            {
                _target = ProximityProvider.FindClosestEnemyAsPlayer(CastOrigin);
            }
            else
            {
                transform.position =
                    Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * 100);
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    
    private void Transit() {
        _casted = false;
        _splitting = true;
    }
}