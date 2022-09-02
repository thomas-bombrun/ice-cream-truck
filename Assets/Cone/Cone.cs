using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Cone : MonoBehaviour
{
    public static T SafeDestroy<T>(T obj) where T : Object
    {
        if (Application.isEditor)
            Object.DestroyImmediate(obj);
        else
            Object.Destroy(obj);

        return null;
    }

    [SerializeField] GameObject scoopPrefab;
    [SerializeField] List<Material> flavors;

    [SerializeField] private Transform scoopSpots;
    int currentFreeSpot;

    public bool CanReceiveScoop()
    {
        return currentFreeSpot < scoopSpots.childCount;
    }
    public void ReceiveScoop(GameObject scoop, bool animate = true)
    {
        scoop.transform.parent = scoopSpots.GetChild(currentFreeSpot);
        if (animate)
        {
            //scoop.transform.DOLocalMove(Vector3.zero, 1);
            scoop.transform.localPosition = Vector3.zero;
        }
        else
        {
            scoop.transform.localPosition = Vector3.zero;
        }
        scoop.transform.localRotation = Quaternion.identity;
        scoop.transform.localScale = Vector3.one;
        currentFreeSpot++;
    }

    public void Reset()
    {
        foreach (Transform spot in scoopSpots)
        {
            foreach (Transform child in spot.transform)
            {
                SafeDestroy(child.gameObject);
            }
        }
        currentFreeSpot = 0;
    }

    public bool IsEqual(Cone cone)
    {
        for (int i = 0; i < scoopSpots.childCount; i++)
        {
            if (scoopSpots.GetChild(i).childCount == 0 && cone.scoopSpots.GetChild(i).childCount == 0)
            {
                return true;
            }
            if (scoopSpots.GetChild(i).childCount == 0 ^ cone.scoopSpots.GetChild(i).childCount == 0)
            {
                return false;
            }
            if (scoopSpots.GetChild(i).GetChild(0).GetComponentInChildren<MeshRenderer>().sharedMaterial != cone.scoopSpots.GetChild(i).GetChild(0).GetComponentInChildren<MeshRenderer>().sharedMaterial)
            {
                return false;
            };
        }
        return true;
    }

    public void RandomFill()
    {
        Reset();
        int randomNumberScoops = Random.Range(2, scoopSpots.childCount + 1);
        for (int i = 0; i < randomNumberScoops; i++)
        {
            GameObject scoop = Instantiate(scoopPrefab);
            scoop.GetComponentInChildren<MeshRenderer>().material = flavors[Random.Range(0, flavors.Count)];
            ReceiveScoop(scoop, false);
        }
    }
}
