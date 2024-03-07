using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerStateController : MonoBehaviour {
    [SerializeField]
    private AdventurerState currentState;
    [SerializeField]
    private GameObject currentRoom;

    private Animator anim;

    private bool waiting = false;
    private bool exploring = false;

    void Start() {
        anim = GetComponent<Animator>();
        currentRoom = GameObject.Find("Room 1");
        SetState(new AdventurerMove(this));
    }

    void Update() {
        // Debug.Log("Walking: " + anim.GetBool("isWalking") + ", fighting: " + anim.GetBool("isFighting"));
        Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
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

    public Animator GetAnimator() {
        return anim;
    }

    private IEnumerator ExploreRoom() {
        for(int i = 0; i < 10; i++) {
            yield return new WaitForSeconds(1.0f);
            if(currentRoom.GetComponent<DungeonRoom>().HasGoblins()) {
                waiting = true;
                yield return new WaitForSeconds(Random.Range(3.0f, 6.0f));
                currentRoom.GetComponent<DungeonRoom>().RemoveGoblin();
                waiting = false;
            }
        }
        exploring = false;
    }

    private IEnumerator FightingGoblin() {
        anim.SetBool("isFighting", true);
        waiting = true;

        yield return new WaitForSeconds(2.0f);

        currentRoom.GetComponent<DungeonRoom>().RemoveGoblin();

        anim.SetBool("isFighting", false);
        waiting = false;
    }
}