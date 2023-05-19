using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnObjectsPoller : MonoBehaviour
{
    [Serializable]
    public class ObjectsSpawnAttributes
    {
        [SerializeField] private List<Transform> transformPoints = new List<Transform>();
        [field: SerializeField] public Owner owner { private set; get; }
        [field: SerializeField] public ObjectPollTypes objectPollTypes { private set; get; }
        [field: SerializeField] public string nameType { private set; get; }
        [SerializeField] public Type type { private set; get; }

        public void InitType() {
            if (string.IsNullOrEmpty(nameType)) throw new Exception("nameType is empty");
            type = Type.GetType(nameType);

            if (type == null) throw new Exception($"Dont find type {nameType}"); 
        }

        public List<Transform> GetPoints() => transformPoints;
        public int GetCountPoints() => transformPoints.Count;
    }

    [SerializeField]
    private List<ObjectsSpawnAttributes> objectsSpawnAttributesList = new List<ObjectsSpawnAttributes>();

    private void Awake() {
        foreach (var object_ in objectsSpawnAttributesList) 
            object_.InitType();
    }

    void Start() {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        while (!NetworkPoller.Instance || !NetworkPoller.Instance.PollerCreated) {
            yield return new WaitForSeconds(0.1f);
        }
        
        SpawnGuns();
    }

    private void SpawnGuns() {
        foreach (var object_ in objectsSpawnAttributesList) {
            var points = object_.GetPoints();
            var objects = NetworkPoller.Instance.GetListObjects(object_.owner, object_.objectPollTypes, object_.type, object_.GetCountPoints());

            for (int i = 0; i < objects.Count; i++) {
                var gun = objects[i];
                gun.transform.position = points[i].position;
                gun.gameObject.SetActive(true);
            }
        }
    }
}
