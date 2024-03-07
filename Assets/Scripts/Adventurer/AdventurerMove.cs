using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerMove : AdventurerState {
    private GameObject currentLocation;
    private GameObject adventurer;
    private DungeonRoom selectedPath;

    public AdventurerMove(AdventurerStateController asc) : base(asc) {}

    public override void Act() {
        if(currentLocation.GetComponent<DungeonRoom>().HasPaths()) {
            if(Vector3.Distance(adventurer.transform.position, selectedPath.GetRoomPosition()) > 0.1f) {
                adventurer.transform.position = Vector3.MoveTowards(adventurer.transform.position, selectedPath.GetRoomPosition(), 0.05f);
            } else {
                asc.SetState(new AdventurerExplore(asc));
            }
        }
    }

    public override void CheckTransitions() {}

    public override void OnStateEnter() {
        asc.GetAnimator().SetBool("isWalking", true);
        currentLocation = asc.GetCurrentRoom();
        adventurer = asc.gameObject;
        if(selectedPath != null) {
            currentLocation = selectedPath.gameObject;
        }
        GameObject[] paths = currentLocation.GetComponent<DungeonRoom>().GetPathOptions();
        int selection = Random.Range(0, paths.Length);
        if(paths.Length > 0) {
            selectedPath = paths[selection].GetComponent<DungeonRoom>();
        } else {
            selectedPath = null;
        }
    }

    public override void OnStateExit() {
        asc.GetAnimator().SetBool("isWalking", false);
        asc.SetCurrentRoom(selectedPath.gameObject);
    }
}
