using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Client : MonoBehaviour
{
    [SerializeField] GameObject conePefab;
    [SerializeField] Transform OrderSpot;
    Cone orderedCone;

    public UnityEvent done;
    Animator animator;
    AudioSource audioSource;
    [SerializeField] AudioClip wrongAudio;
    [SerializeField] AudioClip correctAudio;
    [SerializeField] List<Material> skins;
    Vector3 lastPos;

    public void Reskin()
    {
        this.GetComponentInChildren<SkinnedMeshRenderer>().material = skins[Random.Range(0, skins.Count)];
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        lastPos = transform.position;
        Reskin();
    }

    void Update()
    {
        animator.SetBool("isRunning", lastPos != this.transform.position);
        lastPos = transform.position;
    }

    public void ReceiveCone(Cone cone)
    {
        if (cone.IsEqual(orderedCone))
        {
            Debug.Log("Yeah !");
            done.Invoke();
            audioSource.PlayOneShot(correctAudio);
            Destroy(cone.gameObject);
            Destroy(orderedCone.gameObject);
        }
        else
        {
            Debug.Log("No");
            audioSource.PlayOneShot(wrongAudio);
        }
    }

    public void GenerateOrder()
    {
        GameObject orderedConeGO = Instantiate(conePefab, OrderSpot);
        orderedCone = orderedConeGO.GetComponentInChildren<Cone>();
        orderedCone.RandomFill();
    }
}
