using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinStateController : MonoBehaviour {
    [SerializeField]
    private GoblinState currentState;
    [SerializeField]
    private DungeonRoom currentRoom;
    [SerializeField]
    private AudioSource footstep;

    private Animator anim;

    private bool waiting = false;
    private bool exploring = false;

    void Start() {
        anim = GetComponent<Animator>();
        // currentRoom = GameObject.Find("Room 1");
        SetState(new GoblinExplore(this));
        Debug.Log(currentRoom);
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

    public DungeonRoom GetCurrentRoom() {
        return currentRoom;
    }

    public void SetCurrentRoom(DungeonRoom room) {
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

    public Animator GetAnim() {
        return anim;
    }

    private IEnumerator ExploreRoom() {
        yield return new WaitForSeconds(1.0f);
        waiting = true;
        yield return new WaitForSeconds(3.0f);
        waiting = false;
    }
}