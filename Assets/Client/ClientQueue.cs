using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ClientQueue : MonoBehaviour
{
    [SerializeField] GameObject clientPrefab;
    [SerializeField] float spacing;
    [SerializeField] float speed;
    [SerializeField] int numberClients;
    Client[] clients;
    int firstClientIndex = 0;
    void Start()
    {
        clients = new Client[numberClients];
        for (int i = 0; i < numberClients; i++)
        {
            GameObject client = Instantiate(clientPrefab, transform);
            client.transform.localPosition = new Vector3(0, 0, i * spacing); ;
            client.transform.localRotation = Quaternion.Euler(0, 180f, 0);
            clients[i] = client.GetComponent<Client>();
            clients[i].done.AddListener(ClientDone);

            if (i == 0)
            {
                clients[i].GenerateOrder();
            }
        }
    }

    void ClientDone()
    {
        Score.Increment();
        MoveFirstToLast();
        MoveEveryoneUp();
        firstClientIndex++;
        if (firstClientIndex == numberClients)
        {
            firstClientIndex = 0;
        }
        clients[firstClientIndex].GenerateOrder();

    }
    void MoveEveryoneUp()
    {
        for (int i = 0; i < numberClients; i++)
        {
            if (i == firstClientIndex)
            {
                continue;
            }
            clients[i].gameObject.transform.DOLocalMoveZ(-spacing, 1).SetRelative().SetSpeedBased(true);
        }
    }

    void MoveFirstToLast()
    {
        Client firstClient = clients[firstClientIndex];
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(firstClient.transform.DOLocalMoveX(5, 1).SetSpeedBased(true));
        mySequence.Append(firstClient.transform.DOLocalMove(new Vector3(0, 0, (numberClients - 1) * spacing), 0)).OnComplete(() => firstClient.Reskin());
    }

}
