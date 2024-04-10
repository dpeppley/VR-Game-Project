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

    private AudioCode audioCode;
    void Start() {
        audioCode = GetComponent<AudioCode>();
        speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetHiHat());
        speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetBass());
        StartCoroutine("WaitStart");
    }
     
    public void StartGame() {
        adventurerSpawner.StartSpawner();
        speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetClick());
        speaker.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetKick());
        clock.GetComponent<ChuckSubInstance>().RunCode(audioCode.GetClock());
    }

    private IEnumerator WaitStart() {
        yield return new WaitForSeconds(3.0f);
        StartGame();
    }
}
