using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioCode))]
public class GameController : MonoBehaviour {
    [SerializeField]
    private GameObject speaker;
    [SerializeField]
    private GameObject clock;
    [SerializeField]
    private AdventurerSpawner adventurerSpawner;

    [SerializeField]
    private AudioMixerSnapshot gameNotRunningSnapshot;
    [SerializeField]
    private AudioMixerSnapshot gameRunningSnapshot;
    [SerializeField]
    private AudioMixerSnapshot gameRunningAlertSnapshot;
    [SerializeField]
    private AudioMixerSnapshot gameRunningDungeonSnapshot;
    [SerializeField]
    private AudioMixerSnapshot dungeonSnapshotNotStarted;
    [SerializeField]
    private AudioMixerSnapshot dungeonSnapshotStarted;

    private bool gameRunning;

    private AudioCode audioCode;

    void Start() {
        gameNotRunningSnapshot.TransitionTo(0.0f);
        gameRunning = false;
        // audioCode = GetComponent<AudioCode>();
        // speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetHiHat());
        // speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetBass());
        // speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetClick());
        // speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetKick());
        // clock.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetClock());
        // StartGame();
    }
     
    public void StartGame() {
        gameRunningSnapshot.TransitionTo(1.0f);
        gameRunning = true;
        adventurerSpawner.StartSpawner();
    }

    public void StopGame() {
        gameNotRunningSnapshot.TransitionTo(1.0f);
        gameRunning = false;
    }

    public bool IsGameRunning() {
        return gameRunning;
    }

    public void DungeonShiftAudio() {
        if(gameRunning) {
            dungeonSnapshotStarted.TransitionTo(1.0f);
        } else {
            dungeonSnapshotNotStarted.TransitionTo(1.0f);
        }
    }

    public void OfficeShiftAudio() {
        if(gameRunning) {
            gameRunningSnapshot.TransitionTo(1.0f);
        } else {
            gameNotRunningSnapshot.TransitionTo(1.0f);
        }
    }
}
