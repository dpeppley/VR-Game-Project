using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReorientObject : MonoBehaviour {
    [SerializeField]
    private bool setX;
    [SerializeField]
    private float baseX;
    [SerializeField]
    private bool setY;
    [SerializeField]
    private float baseY;
    [SerializeField]
    private bool setZ;
    [SerializeField]
    private float baseZ;

    // Update is called once per frame
    public void Reorient()
    {
        Quaternion objRotation = gameObject.transform.rotation;
        float xVal = setX ? objRotation.x : baseX;
        float yVal = setY ? objRotation.y : baseY;
        float zVal = setZ ? objRotation.z : baseZ;
        gameObject.transform.rotation = Quaternion.Euler(xVal, yVal, zVal);
    }
}