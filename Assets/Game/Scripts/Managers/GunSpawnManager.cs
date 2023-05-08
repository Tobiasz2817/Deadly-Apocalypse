using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawnManager : MonoBehaviour
{
    [SerializeField]
    private List<Transform> gunPositions = new List<Transform>();
    void Start() {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn() {
        while (!SinglePoller.Instance || !SinglePoller.Instance.PollerCreated) {
            yield return new WaitForSeconds(0.1f);
        }
        
        SpawnGuns();
    }

    private void SpawnGuns() {
        foreach (var pos in gunPositions) {
            /*var gun = SinglePoller.Instance.GetObject(Owner.Player, ObjectPollTypes.Guns);
            gun.transform.parent = null;
            gun.transform.position = pos.position;
            gun.gameObject.SetActive(true);*/
        }
    }
}
