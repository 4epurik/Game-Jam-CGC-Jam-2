using UnityEngine;

public class GameExitTrigger : MonoBehaviour
{
    [Tooltip("��� �������, ������� ������ ������������ �����")]
    [SerializeField] private string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ����� �� ������ � ������ �����
        if (other.CompareTag(playerTag))
        {
            Debug.Log("����� ����� � �������. ����� �� ����...");

            // ������������� ���� (� ���������)
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // ��������� ���������� � �����
            Application.Quit();
#endif
        }
    }
}