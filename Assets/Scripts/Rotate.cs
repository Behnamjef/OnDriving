using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float RotateSpeed;
    public Vector3 Axis = new Vector3(0, 1, 0);
    
    void Update()
    {
        transform.Rotate(Axis * RotateSpeed * Time.deltaTime);
    }
}