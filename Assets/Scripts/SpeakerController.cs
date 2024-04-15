using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeakerController : MonoBehaviour {

    [SerializeField]
    private List<AudioSource> sounds;

    private bool isInDungeon = false;

    void Update() {
        if(isInDungeon) {
            foreach(AudioSource sound in sounds) {
                if(sound.spatialBlend >= 0.0f) {
                    sound.spatialBlend -= 0.02f;
                }
            }
        } else {
            foreach(AudioSource sound in sounds) {
                if(sound.spatialBlend <= 1.0f) {
                    sound.spatialBlend += 0.02f;
                }
            }
        }
    }

    public void Spatialize2D() {
        isInDungeon = true;
    }

    public void Spatialize3D() {
        isInDungeon = false;
    }
}
