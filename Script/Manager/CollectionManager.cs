using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollectionEvent : UnityEvent {}
public class CollectionManager : MonoBehaviour
{
    public CollectionEvent collectionEvent;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            collectionEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
