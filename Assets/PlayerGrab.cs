using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] Transform coneSpot;
    [SerializeField] Transform tubSpot;
    Cone cone;
    Tub tub;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit hitInfo;
            Physics.Raycast(this.transform.position, this.transform.forward, out hitInfo, 10);
            if (hitInfo.collider == null)
            {
                return;
            }

            Debug.Log(hitInfo.collider.name);

            if (hitInfo.collider.GetComponent<Tub>() != null)
            {
                Tub grabbedTub = hitInfo.collider.GetComponent<Tub>();
                if (grabbedTub.remainingScoops == 0 && tub == null)
                {
                    tub = grabbedTub;
                    tub.transform.parent = tubSpot;
                    tub.transform.DOLocalMove(Vector3.zero, 1f);
                    tub.transform.DOLocalRotate(Vector3.zero, 1f);
                    return;
                }
                if (cone != null && cone.CanReceiveScoop())
                {
                    hitInfo.collider.GetComponent<Tub>().StartScooping(cone);
                }
            }
            else if (hitInfo.collider.GetComponent<Client>() != null)
            {
                if (cone == null)
                {
                    return;
                }
                hitInfo.collider.GetComponent<Client>().ReceiveCone(cone);
            }
            else if (hitInfo.collider.GetComponent<TrashCan>() != null)
            {
                hitInfo.collider.GetComponent<TrashCan>().ThrowAway(cone.gameObject);
                Debug.Log(cone);
            }
            else if (hitInfo.collider.GetComponent<ConeDispenser>() != null)
            {
                if (cone == null)
                {
                    cone = hitInfo.collider.GetComponent<ConeDispenser>().GetNewCone();
                    cone.transform.SetParent(coneSpot);
                    cone.transform.DOLocalMove(Vector3.zero, 1f);
                    cone.transform.localRotation = Quaternion.identity;
                    cone.transform.localScale = Vector3.one;
                }
            }
            else if (hitInfo.collider.GetComponent<Freezer>() != null)
            {
                if (tub == null)
                {
                    return;
                }
                tub.Refill();
            }
            else if (hitInfo.collider.GetComponent<TubSpot>() != null)
            {
                if (tub == null)
                {
                    return;
                }
                tub.transform.parent = hitInfo.collider.GetComponent<TubSpot>().transform;
                tub.transform.DOLocalMove(Vector3.zero, 1f);
                tub.transform.DOLocalRotate(Vector3.zero, 1f);
                tub = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            cone.Reset();
        }
    }
}
