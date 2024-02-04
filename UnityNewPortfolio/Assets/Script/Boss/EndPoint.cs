using UnityEngine;

public class EndPoint : MonoBehaviour
{
    // �ִϸ��̼��� ������ ��ġ�� ������ ����
    public Vector3 endPosition;

    void Start()
    {
        // �ʱ�ȭ�� �ʿ��� �۾��� ����
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��� �Լ�
    public void OnAnimationEnd()
    {
        // �ִϸ��̼��� ������ ��ġ�� ������Ʈ �̵�
        transform.position = endPosition;
    }

    // �ʿ��� ��� �ִϸ��̼��� ������ ��ġ�� ������Ʈ�ϴ� �ٸ� �޼���
    public void UpdateEndPosition(Vector3 newPosition)
    {
        endPosition = newPosition;
    }
}