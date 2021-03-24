using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slip : MonoBehaviour
{
    public float slipFriction = 0;
    public float normalFriction = 1;
    Collider playerMaterial;
    // Start is called before the first frame update
    void Start()
    {
        playerMaterial = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        Ski();
    }

    void Ski()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMaterial.material.dynamicFriction = 0;
        }
        else
        {
            playerMaterial.material.dynamicFriction = 1;
        }
    }
}
