using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    public GameObject arrowPrefab; // 화살 프리팹
    public Transform launchPoint;  // 화살 발사 위치
    public float speed = 1000.0f;  // 화살의 속도

    public void LaunchArrow()
    {
        Debug.Log("화살");
        GameObject newArrow = Instantiate(arrowPrefab, launchPoint.position, launchPoint.rotation); // 새 화살 생성
        Rigidbody arrowRigidbody = newArrow.GetComponent<Rigidbody>();

        if (arrowRigidbody != null)
        {
            arrowRigidbody.velocity = launchPoint.forward * speed; // 화살에 속도 부여
        }
        else
        {
            Debug.LogError("Rigidbody not found on arrow prefab!");
        }
    }
}