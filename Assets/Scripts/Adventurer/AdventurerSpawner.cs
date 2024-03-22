using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerSpawner : MonoBehaviour {
    [SerializeField]
    private GameObject adventurerPrefab;
    [SerializeField]
    private Transform spawnPoint;

    void Start() {
        StartCoroutine("SpawnAdventurer");
    }

    private IEnumerator SpawnAdventurer() {
        Instantiate(adventurerPrefab, spawnPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(10.0f);
        //StartCoroutine("SpawnAdventurer");
    }
}
