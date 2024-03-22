using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerStateController : MonoBehaviour {
    [SerializeField]
    private AdventurerState currentState;
    [SerializeField]
    private DungeonRoom currentRoom;

    [SerializeField]
    private Avatar idleAvatar;
    [SerializeField]
    private Avatar walkingAvatar;
    [SerializeField]
    private Avatar attackAvatar;

    private Animator anim;

    private bool waiting = false;
    private bool exploring = false;

    void Start() {
        anim = GetComponent<Animator>();
        currentRoom = GameObject.Find("Adventurer Spawner").GetComponent<DungeonRoom>();
        SetState(new AdventurerMove(this));
    }

    void Update() {
        // Debug.Log("Walking: " + anim.GetBool("isWalking") + ", fighting: " + anim.GetBool("isFighting"));
        // Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        currentState.CheckTransitions();
        currentState.Act();
        // if(!anim.GetBool("isAttacking")) {
        //     anim.avatar = anim.GetBool("isWalking") ? walkingAvatar : idleAvatar;
        // }

        // Debug.Log("Avatar: " + anim.avatar.name + ", anim clip: " + anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
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

    public DungeonRoom GetCurrentRoom() {
        return currentRoom;
    }

    public void SetCurrentRoom(DungeonRoom room) {
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

    public void SetWalkAvatar() {
        anim.avatar = walkingAvatar;
    }

    public void setIdleAvatar() {
        anim.avatar = idleAvatar;
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
        anim.avatar = attackAvatar;
        anim.SetBool("isAttacking", true);
        Debug.Log("Is fighting: " + anim.GetBool("isAttacking"));
        
        
        waiting = true;

        yield return new WaitForSeconds(2.0f);

        currentRoom.GetComponent<DungeonRoom>().RemoveGoblin();

        anim.SetBool("isAttacking", false);
        anim.avatar = idleAvatar;
        waiting = false;
    }

    public void StartIdling() {
        StartCoroutine("Idling");
    }

    private IEnumerator Idling() {
        anim.avatar = idleAvatar;
        waiting = true;

        yield return new WaitForSeconds(3.0f);

        waiting = false;
    }
}