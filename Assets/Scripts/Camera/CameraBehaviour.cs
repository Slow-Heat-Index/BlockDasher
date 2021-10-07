using System;
using System.Collections;
using System.Collections.Generic;
using Player.Data;
using UnityEngine;
using DG.Tweening;

public class CameraBehaviour : MonoBehaviour {
    private GameObject player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float distance;
    
    void Awake() {
        player = FindObjectOfType<PlayerData>().gameObject;
        
    }

    private void Start() {
        UpdateCameraPosition();
        transform.LookAt(player.transform);
    }


    public void RotateRight() {
        var newRotation = Quaternion.AngleAxis(-90, Vector3.up) * transform.rotation;

        if (offset.x == 0) {
            if (offset.z == distance) {
                offset.x = -distance;
                offset.z = 0;
            }
            else {
                offset.x = distance;
                offset.z = 0;
            }
            
        }
        else {
            if (offset.x == distance) {
                offset.z = distance;
                offset.x = 0;
            }
            else {
                offset.z = -distance;
                offset.x = 0;
            }
        }

        transform.DOMove(player.transform.position + offset, 0.5f);
        transform.DORotateQuaternion(newRotation, 0.5f);
    }

    public void RotateLeft() {
        var newRotation = Quaternion.AngleAxis(90, Vector3.up) * transform.rotation;
        
        if (offset.x == 0) {
            if (offset.z == distance) {
                offset.x = +distance;
                offset.z = 0;
            }
            else {
                offset.x = -distance;
                offset.z = 0;
            }
            
        }
        else {
            if (offset.x == distance) {
                offset.z = -distance;
                offset.x = 0;
            }
            else {
                offset.z = distance;
                offset.x = 0;
            }
        }
        

        transform.DOMove(player.transform.position + offset, 0.5f);
        transform.DORotateQuaternion(newRotation, 0.5f);
    }

    public void UpdateCameraPosition() {
        transform.DOMove(player.transform.position + offset, 0.5f);
    }
}
