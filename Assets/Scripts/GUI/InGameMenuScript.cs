using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuScript : MonoBehaviour
{
    private bool _isPaused = false;

    [SerializeField]

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F10))
        {
            Debug.Log("Menu is now active");
            gameObject.SetActive(true);
            Time.timeScale = 0.0f;
            _isPaused = true;

        }
    }
}