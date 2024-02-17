using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerStateController : MonoBehaviour {
    [SerializeField]
    private AdventurerState currentState;
    [SerializeField]
    private GameObject currentRoom;

    private bool waiting = false;
    private bool exploring = false;

    void Start() {
        currentRoom = GameObject.Find("Room 1");
        SetState(new AdventurerMove(this));
    }

    void Update() {
        currentState.CheckTransitions();
        currentState.Act();
    }

    public void SetState(AdventurerState state) {
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

    public void FightGoblin() {
        StartCoroutine("FightingGoblin");
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

    private IEnumerator ExploreRoom() {
        for(int i = 0; i < 10; i++) {
            yield return new WaitForSeconds(1.0f);
            if(currentRoom.GetComponent<DungeonRoom>().HasGoblins()) {
                waiting = true;
                yield return new WaitForSeconds(3.0f);
                currentRoom.GetComponent<DungeonRoom>().RemoveGoblin();
                waiting = false;
            }
        }
        exploring = false;
    }
}