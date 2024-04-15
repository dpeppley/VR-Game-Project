using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CrystalBallManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private GameObject xrOrigin;
    private Vector3 startPos;


    private bool cameraInDungeon;

    private AudioSource teleportAudio;
    private SpeakerController speaker;

    private GameController gc;

    void Start() {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        speaker = GameObject.Find("Speaker").GetComponent<SpeakerController>();
        cameraInDungeon = false;
        startPos = xrOrigin.transform.position;
        teleportAudio = GetComponent<AudioSource>();
    }

    public void SpawnGoblin() {
        spawnPoint.GetComponent<SpawnManager>().ActivateCamera();
    }

    public void MoveToCamera() {
        teleportAudio.Play();
        gc.DungeonShiftAudio();
        speaker.Spatialize2D();
        Vector3 cameraTransform = spawnPoint.transform.position;
        xrOrigin.transform.position = new Vector3(cameraTransform.x, cameraTransform.y - 8.0f, cameraTransform.z);
        cameraInDungeon = true;
    }

    public void MoveToOffice() {
        if(cameraInDungeon) {
            speaker.Spatialize3D();
            teleportAudio.Play();
        }
        gc.OfficeShiftAudio();
        xrOrigin.transform.position = startPos;
        cameraInDungeon = false;
    }

    public bool CameraInDungeon() {
        return cameraInDungeon;
    }

    public void AlertFlash() {
        spawnPoint.GetComponent<SpawnManager>().StartCoroutine("AlertFlash");
    }
}
