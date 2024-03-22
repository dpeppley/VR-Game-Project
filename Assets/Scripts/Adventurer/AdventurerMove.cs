using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerMove : AdventurerState {
    private DungeonRoom currentLocation;
    private GameObject adventurer;
    private DungeonPath selectedPath;

    private Transform nextPoint;
    private int pointIndex;

    private MonoBehaviour mono;

    public AdventurerMove(AdventurerStateController asc) : base(asc) {
        mono = asc;
    }

    public override void Act() {
        if(currentLocation.GetComponent<DungeonRoom>().HasPaths()) {
            if(Vector3.Distance(adventurer.transform.position, nextPoint.position) > 0.1f) {
                adventurer.transform.position = Vector3.MoveTowards(adventurer.transform.position, nextPoint.position, 0.03f);
            } else {
                if(pointIndex == selectedPath.pathPoints.Count-1) {
                    asc.SetState(new AdventurerExplore(asc));
                } else {
                    pointIndex++;
                    nextPoint = selectedPath.pathPoints[pointIndex];
                }
            }
        }
    }

    public override void CheckTransitions() {}

    public override void OnStateEnter() {
        asc.GetAnimator().SetBool("isWalking", true);
        // asc.SetWalkAvatar();
        currentLocation = asc.GetCurrentRoom();
        adventurer = asc.gameObject;
        if(selectedPath != null) {
            currentLocation = selectedPath.room;
        }
        DungeonPath[] paths = currentLocation.GetPathOptions();
        int selection = Random.Range(0, paths.Length);
        if(paths.Length > 0) {
            selectedPath = paths[selection].GetComponent<DungeonPath>();
            pointIndex = 0;
            nextPoint = selectedPath.pathPoints[pointIndex];
        } else {
            selectedPath = null;
        }
    }

    public override void OnStateExit() {
        asc.GetAnimator().SetBool("isWalking", false);
        asc.SetCurrentRoom(selectedPath.room);
    }
}
