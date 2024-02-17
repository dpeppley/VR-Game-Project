using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToStand : MonoBehaviour
{
    private Vector3 startPos;
    private Rigidbody rb;

    private CrystalBallManager cbm;

    private bool returning = false;

    void Start() {
        cbm = gameObject.GetComponent<CrystalBallManager>();
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if(Vector3.Distance(transform.position, startPos) > 0.2f) returning = true;
        if(returning) {
            transform.position = Vector3.MoveTowards(transform.position, startPos, 0.5f);
            if(Vector3.Distance(transform.position, startPos) < 0.1f) {
                transform.position = startPos;
                returning = false;
                rb.velocity = new Vector3(0, 0, 0);
            }
        }
    }

    public void OnRelease() {
        if(cbm.CameraInDungeon()) cbm.MoveToOffice();
        returning = true;
    }
}
