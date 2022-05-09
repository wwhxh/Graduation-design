using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAndCollect : ScriptableObject
{
    Vector3 savePosition;
    public Vector3 GetPosition() {
        return savePosition;
    }
    public void setPosition(float x, float y, float z) {
        savePosition = new Vector3(x,y,z);
    }

    public void setPosition(Vector3 position) {
        savePosition = position;
    }
}
