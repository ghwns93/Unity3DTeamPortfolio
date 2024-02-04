using UnityEngine;

public class EndPoint : MonoBehaviour
{
    // 애니메이션의 마지막 위치를 저장할 변수
    public Vector3 endPosition;

    void Start()
    {
        // 초기화나 필요한 작업을 수행
    }

    // 애니메이션 이벤트에서 호출될 함수
    public void OnAnimationEnd()
    {
        // 애니메이션의 마지막 위치로 오브젝트 이동
        transform.position = endPosition;
    }

    // 필요한 경우 애니메이션의 마지막 위치를 업데이트하는 다른 메서드
    public void UpdateEndPosition(Vector3 newPosition)
    {
        endPosition = newPosition;
    }
}