using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioCode))]
public class GameController : MonoBehaviour {
    [SerializeField]
    private GameObject speaker;
    [SerializeField]
    private GameObject clock;
    [SerializeField]
    private AdventurerSpawner adventurerSpawner;

    private bool gameRunning;

    private AudioCode audioCode;

    void Start() {
        gameRunning = false;
        audioCode = GetComponent<AudioCode>();
        // speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetHiHat());
        // speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetBass());
        StartCoroutine("WaitStart");
    }
     
    public void StartGame() {
        gameRunning = true;
        adventurerSpawner.StartSpawner();
        // speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetClick());
        // speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetKick());
        clock.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetClock());
    }

    public void StopGame() {
        gameRunning = false;
    }

    public bool IsGameRunning() {
        return gameRunning;
    }

    private IEnumerator WaitStart() {
        yield return new WaitForSeconds(3.0f);
        StartGame();
        StartCoroutine("WaitEnd");
    }

    private IEnumerator WaitEnd() {
        yield return new WaitForSeconds(3.0f);
        StopGame();
    }
}
