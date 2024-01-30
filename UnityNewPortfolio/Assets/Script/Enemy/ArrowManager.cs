using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public GameObject arrowPrefab; // ȭ�� ������
    public Transform launchPoint;  // ȭ�� �߻� ��ġ
    public float speed = 1000.0f;  // ȭ���� �ӵ�

    public void LaunchArrow()
    {
        Debug.Log("ȭ��");
        GameObject newArrow = Instantiate(arrowPrefab, launchPoint.position, launchPoint.rotation); // �� ȭ�� ����
        Rigidbody arrowRigidbody = newArrow.GetComponent<Rigidbody>();

        if (arrowRigidbody != null)
        {
            arrowRigidbody.velocity = launchPoint.forward * speed; // ȭ�쿡 �ӵ� �ο�
        }
        else
        {
            Debug.LogError("Rigidbody not found on arrow prefab!");
        }
    }
}