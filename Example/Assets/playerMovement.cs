using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    private Rigidbody rb;

    [Header("Movement")]
    [SerializeField]
    private float movementSpd;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update () {
        CheckInput();
	}

    private void CheckInput()
    {
        rb.velocity = new Vector3(Input.GetAxis("Horizontal") * movementSpd, rb.velocity.y, rb.velocity.z);
    }
}
