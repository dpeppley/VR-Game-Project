using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalBallManager : MonoBehaviour
{
    [SerializeField]
    private GameObject spawnPoint;

    [SerializeField]
    private GameObject xrOrigin;
    private Vector3 startPos;

    private bool cameraInDungeon;

    private AudioSource teleportAudio;

    void Start() {
        cameraInDungeon = false;
        startPos = xrOrigin.transform.position;
        teleportAudio = GetComponent<AudioSource>();
    }

    public void SpawnGoblin() {
        spawnPoint.GetComponent<SpawnManager>().ActivateCamera();
    }

    public void MoveToCamera() {
        teleportAudio.Play();
        xrOrigin.transform.position = spawnPoint.transform.position;
        cameraInDungeon = true;
    }

    public void MoveToOffice() {
        teleportAudio.Play();
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
