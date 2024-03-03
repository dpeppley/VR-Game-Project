using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStateController : MonoBehaviour {
    [SerializeField]
    private GoblinState currentState;
    [SerializeField]
    private GameObject currentRoom;
    [SerializeField]
    private AudioSource footstep;

    private bool waiting = false;
    private bool exploring = false;

    void Start() {
        // currentRoom = GameObject.Find("Room 1");
        SetState(new GoblinExplore(this));
    }

    void Update() {
        currentState.CheckTransitions();
        currentState.Act();
    }

    public void SetState(GoblinState state) {
        if(currentState != null) {
            currentState.OnStateExit();
        }

        currentState = state;

        if(currentState != null) {
            currentState.OnStateEnter();
        }
    }

    public GameObject GetCurrentRoom() {
        return currentRoom;
    }

    public void SetCurrentRoom(GameObject room) {
        currentRoom = room;
    }

    public bool IsWaiting() {
        return waiting;
    }

    public bool IsExploring() {
        return exploring;
    }

    public void StartExploring() {
        exploring = true;
        StartCoroutine("ExploreRoom");
    }

    public AudioSource GetFootstepAudio() {
        return footstep;
    }

    private IEnumerator ExploreRoom() {
        yield return new WaitForSeconds(1.0f);
        if(currentRoom.GetComponent<DungeonRoom>().HasGoblins()) {
            waiting = true;
            yield return new WaitForSeconds(3.0f);
            currentRoom.GetComponent<DungeonRoom>().RemoveGoblin();
            waiting = false;
        }
    }
}