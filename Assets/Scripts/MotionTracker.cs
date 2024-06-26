using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using PDollarGestureRecognizer;
using UnityEngine.VFX;
using System.IO;

public class MotionTracker : MonoBehaviour {
    public XRNode inputSource;
    public UnityEngine.XR.Interaction.Toolkit.InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;
    public Transform movementSource;

    public float newPositionThresholdDistance = 0.05f;
    //public GameObject debugCubePrefab;
    public bool creationMode = true;
    public string newGestureName;

    private List<Gesture> trainingSet = new List<Gesture>();
    private bool isMoving = false;
    private List<Vector3> positionsList = new List<Vector3>();

    [SerializeField]
    private WandManager wand;
    [SerializeField]
    private GameObject vfx;

    private AudioSource audio;

    void Start() {
        vfx.SetActive(false);
        Debug.Log(Resources.Load<TextAsset>("Circle"));
        string textFile = Resources.Load<TextAsset>("Circle").ToString();
        trainingSet.Add(GestureIO.ReadGestureFromXML(textFile));

        audio = wand.GetDrawAudio();
    }

    void Update() {
        UnityEngine.XR.Interaction.Toolkit.InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

        if(!isMoving && isPressed) { // Start movement
            StartMovement();
        } else if(isMoving && !isPressed) { // End movement
            EndMovement();
        } else if(isMoving && isPressed) { // Updating movement
            UpdateMovement();
        }
    }

    void StartMovement() {
        audio.Play();
        vfx.SetActive(true);
        isMoving = true;
        positionsList.Clear();
        positionsList.Add(movementSource.position);
    }

    void EndMovement() {
        audio.Stop();
        vfx.GetComponent<TrailRenderer>().Clear();
        vfx.SetActive(false);
        isMoving = false;

        // Create gesture from position list
        Point[] pointArray = new Point[positionsList.Count];

        for(int i = 0; i < positionsList.Count; i++) {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(positionsList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }

        Gesture newGesture = new Gesture(pointArray);

        if(creationMode) {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);

            string fileName = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, fileName);
            Debug.Log(fileName);
        } else {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(result.GestureClass + result.Score);
            if (result.Score > 0.8f) {
                Debug.Log("Wand activated");
                wand.ActivateWand();
            }
        }
    }

    void UpdateMovement() {
        Vector3 lastPosition = positionsList[positionsList.Count - 1];
        if(Vector3.Distance(movementSource.position, lastPosition) > newPositionThresholdDistance) {
            positionsList.Add(movementSource.position);
        }
    }
}
