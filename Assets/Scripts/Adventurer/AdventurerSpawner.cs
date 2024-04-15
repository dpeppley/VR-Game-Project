using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject adventurerPrefab;
    [SerializeField]
    private Transform spawnPoint;

    private bool spawnerRunning;

    private float spawnDelay;

    private GameController gc;

    void Start() {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        spawnDelay = 10.0f;
        spawnerRunning = false;
        //StartSpawner();
    }

    private IEnumerator SpawnAdventurer() {
        Instantiate(adventurerPrefab, new Vector3(spawnPoint.position.x, 0, spawnPoint.position.z), Quaternion.identity);
        yield return new WaitForSeconds(spawnDelay);
        if(spawnDelay > 5) {
            spawnDelay -= .01f;
        }
        if(gc.IsGameRunning()) {
            StartCoroutine("SpawnAdventurer");
        }
    }

    public void StartSpawner() {
        Debug.Log("Spawner Started");
        if(!spawnerRunning) {
            // gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine("SpawnAdventurer");
            spawnerRunning = true;
        }
    }
}
