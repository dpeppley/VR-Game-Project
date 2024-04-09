using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float cooldownTime = 3f;
	public GameObject prefabToSpawn;

	[SerializeField]
	private GameObject crystalBall;
	[SerializeField]
	private DungeonRoom room;
	[SerializeField]
	private Material mat;
	[SerializeField]
	private Material cooldownMat;
	[SerializeField]
	private Material alertMat;

	private float nextSpawnTime = 0;
	private bool cooldown = false;
	private bool isActive;

	private void Start() {
		ActivateCamera();
	}

	private void Update()
    {
		// Debug.Log(Time.time +", " + nextSpawnTime);
		// crystalBall.GetComponent<Renderer>().material = cooldownMat;
		if (Time.time > nextSpawnTime)
		{
			if (cooldown)
			{
				cooldown = false;
				crystalBall.GetComponent<Renderer>().material = mat;
			} 
			// else {
				
			// }
		}
    }


	public void ActivateCamera() {
		if(!cooldown) {
			// Vector3 fwd = transform.TransformDirection(Vector3.forward);
			// RaycastHit hit;
			// if (Physics.Raycast(transform.position, fwd, out hit, Mathf.Infinity)) {
			cooldown = true;

			// Raycast from camera to plane
			GameObject goblin = Instantiate(prefabToSpawn, VariedPosition(room.GetRoomPosition()) , transform.rotation);
			room.AddGoblin(goblin);

			crystalBall.GetComponent<Renderer>().material = cooldownMat;
			nextSpawnTime = Time.time + cooldownTime;
			// }
		}
	}

	public void DeactivateCamera() {
		isActive = false;
	}

	private Vector3 VariedPosition(Vector3 pos) {
		float newX = pos.x + Random.Range(-5f, 5f);
		float newY = pos.y + 5f;
		float newZ = pos.z + Random.Range(-5f, 5f);
		Vector3 newPos = new Vector3(newX, newY, newZ);
		return newPos;
	}

	public IEnumerator AlertFlash() {
		for(int i = 0; i < 3; i++) {
			crystalBall.GetComponent<Renderer>().material = alertMat;
			crystalBall.GetComponent<Light>().enabled = true;
			yield return new WaitForSeconds(0.5f);
			crystalBall.GetComponent<Renderer>().material = mat;
			crystalBall.GetComponent<Light>().enabled = false;
			yield return new WaitForSeconds(0.5f);
		}
	}
}