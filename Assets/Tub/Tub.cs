using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tub : MonoBehaviour
{
    [SerializeField] GameObject scoopPrefab;
    [SerializeField] Transform startScoopingPlace;
    [SerializeField] Transform endScoopingPlace;
    [SerializeField] float scoopingTime;
    [SerializeField] Material flavor;
    [SerializeField] MeshRenderer iceCreamInTub;
    [SerializeField] int startingScoops;

    private int _remainingScoops;
    public int remainingScoops
    {
        get
        {
            return _remainingScoops;
        }
        set
        {
            _remainingScoops = value;
            if (_remainingScoops > 0)
            {
                iceCreamInTub.gameObject.SetActive(true);
            }
            float remainingPercent = (float)remainingScoops / (float)startingScoops;
            iceCreamInTub.transform.parent.DOScaleY(remainingPercent, scoopingTime).OnComplete(() =>
            {
                if (remainingPercent == 0)
                {
                    iceCreamInTub.gameObject.SetActive(false);
                }
            });
        }
    }
    bool isScooping;

    void Start()
    {
        remainingScoops = startingScoops;
        iceCreamInTub.material = flavor;
    }

    public void StartScooping(Cone cone)
    {
        if (isScooping || remainingScoops == 0)
        {
            return;
        }
        isScooping = true;
        GameObject newScoop = Instantiate(scoopPrefab);
        newScoop.GetComponentInChildren<MeshRenderer>().material = flavor;
        newScoop.transform.position = startScoopingPlace.position;
        remainingScoops--;
        newScoop.transform.DOScale(Vector3.zero, scoopingTime).From();
        newScoop.transform.DOMove(endScoopingPlace.position, scoopingTime).OnComplete(() =>
        {

            isScooping = false;
            cone.ReceiveScoop(newScoop);
        });
    }

    public void Refill()
    {
        remainingScoops = startingScoops;
    }

}
