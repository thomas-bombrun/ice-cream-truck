using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrashCan : MonoBehaviour
{

    AudioSource throwAwaySound;
    [SerializeField] Transform disposeSpot;
    [SerializeField] float arc;
    [SerializeField] float speed;

    void Awake()
    {
        throwAwaySound = GetComponent<AudioSource>();
    }
    public void ThrowAway(GameObject go)
    {
        go.transform.DOJump(disposeSpot.position, arc, 1, speed).OnComplete(() =>
          {
              Destroy(go);
              throwAwaySound.Play();
          });
    }
}
