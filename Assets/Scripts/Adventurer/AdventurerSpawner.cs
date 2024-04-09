using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject adventurerPrefab;
    [SerializeField]
    private Transform spawnPoint;

    private bool spawnerRunning;

    void Start() {
        spawnerRunning = false;
        StartSpawner();
    }

    private IEnumerator SpawnAdventurer() {
        Instantiate(adventurerPrefab, spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(10.0f);
        StartCoroutine("SpawnAdventurer");
    }

    public void StartSpawner() {
        Debug.Log("Spawner Started");
        if(!spawnerRunning) {
            gameObject.GetComponent<AudioSource>().Play();
            StartCoroutine("SpawnAdventurer");
            spawnerRunning = true;
        }
    }
}
