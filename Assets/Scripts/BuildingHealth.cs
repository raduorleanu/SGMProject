using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Utillity;

public class BuildingHealth : MonoBehaviour, ITakeDamage
{
    
    [SerializeField] private float _initialHealth;
    [SerializeField] private GameObject _healthBar;
    private HealthBar _hb;
    private float _buildingHealth;

    // Use this for initialization
    void Start()
    {
        _buildingHealth = _initialHealth;
        _hb = _healthBar.GetComponent<HealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SubstarctDamageFromLife_(float damageTaken)
    {
        _buildingHealth -= damageTaken;
        if (_buildingHealth <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            _hb.ShrinkBar(damageTaken, _initialHealth);
        }
    }
    
}