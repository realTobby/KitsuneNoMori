using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject objectToFollow;

    public Vector3 offset;
    public float RotationAngle;

    void FixedUpdate()
    {
        // rotate on the x => (up and down)
        #region FollowTheObject
        Vector3 cameraPosition = new Vector3(objectToFollow.transform.position.x - offset.x, objectToFollow.transform.position.y + offset.y, objectToFollow.transform.position.z - offset.z);
        this.transform.position = cameraPosition;
        #endregion

        #region RotateTheCamera
        this.transform.rotation = Quaternion.Euler(RotationAngle, 0, 0);
        #endregion
    }
}
