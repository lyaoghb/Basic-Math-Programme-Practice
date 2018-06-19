using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

    private Matrix3D mt = new Matrix3D();
    private Vector3D temp = new Vector3D();
    private float moveSpeed = 0.05f;
    private float rotSpeed = 0.5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        int front = 0;
        if (Input.GetKey(KeyCode.W)) {
            front = 1;
        }
        else if (Input.GetKey(KeyCode.S)) {
            front = -1;
        }
        if (front != 0) {
            Matrix3DUtil.translateZ(mt, front * moveSpeed);
            Matrix3DUtil.getPosition(mt, temp);
            this.transform.position = new Vector3(temp.x, temp.y, temp.z);
        }

        int left = 0;
        if (Input.GetKey(KeyCode.A)) {
            left = -1;
        }
        else if (Input.GetKey(KeyCode.D)) {
            left = 1;
        }
        if (left != 0) {
            Matrix3DUtil.translateX(mt, left * moveSpeed);
            Matrix3DUtil.getPosition(mt, temp);
            this.transform.position = new Vector3(temp.x, temp.y, temp.z);
        }

        int rot = 0;
        if (Input.GetKey(KeyCode.Q)) {
            rot = -1;
        }
        else if (Input.GetKey(KeyCode.E)) {
            rot = 1;
        }
        if (rot != 0) {
            Matrix3DUtil.rotateY(mt, rot * rotSpeed, true, null);
            Matrix3DUtil.getRotation(mt, temp);
            this.transform.rotation = Quaternion.Euler(temp.x, temp.y, temp.z);
        }
    }
}
