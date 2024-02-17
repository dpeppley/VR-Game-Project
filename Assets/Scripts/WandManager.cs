using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandManager : MonoBehaviour {
    [SerializeField]
    private ParticleSystem ps;

    private bool isHeld;
    private bool isActivated;

    void Start() {
        ps.Stop();
    }

    void OnCollisionEnter(Collision col) {
        if(isActivated && col.gameObject.tag == "Crystal Ball") {
            col.gameObject.GetComponent<CrystalBallManager>().SpawnGoblin();
            ps.Stop();
            isActivated = false;
        }
    }

    public void ActivateWand() {
        if(isHeld) {
            isActivated = true;
            ps.Play();
        }
    }

    public void IsHeld() {
        isHeld = !isHeld;
    }
}
