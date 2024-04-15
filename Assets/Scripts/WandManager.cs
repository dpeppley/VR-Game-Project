using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandManager : MonoBehaviour {
    [SerializeField]
    private ParticleSystem ps;

    [SerializeField]
    private AudioSource tapAudio;
    [SerializeField]
    private AudioSource drawAudio;
    [SerializeField]
    private AudioSource spellAudio;

    private bool isHeld;
    private bool isActivated;

    void Start() {
        ps.Stop();
    }

    void OnCollisionEnter(Collision col) {
        if(col.gameObject.tag == "Crystal Ball") {
            tapAudio.Play();
            if(isActivated) {
                Debug.Log("Collided and activated");
                col.gameObject.GetComponent<CrystalBallManager>().SpawnGoblin();
                spellAudio.Stop();
                ps.Stop();
                isActivated = false;
            }
        }
    }

    public void ActivateWand() {
        if(isHeld) {
            isActivated = true;
            spellAudio.Play();
            ps.Play();
        }
    }

    public void IsHeld() {
        isHeld = !isHeld;
    }

    public AudioSource GetDrawAudio() {
        return drawAudio;
    }
}
