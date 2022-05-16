using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollectionEvent : UnityEvent<string> {}
public class CollectionController : MonoBehaviour
{
    public CollectionEvent collectionEvent;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player")){
            collectionEvent?.Invoke(gameObject.name);
            Destroy(gameObject);
        }
    }
}
