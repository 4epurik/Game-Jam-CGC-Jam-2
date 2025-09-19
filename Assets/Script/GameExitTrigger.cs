using Script;
using TMPro;
using UnityEngine;

public class GameExitTrigger : MonoBehaviour
{
    [Tooltip("��� �������, ������� ������ ������������ �����")]
    [SerializeField] private string playerTag = "Player";

    private bool isHurt;

    private void OnTriggerEnter(Collider other)
    {
        // ���������, ����� �� ������ � ������ �����
        if (other.CompareTag(playerTag) && !isHurt)
        {
            isHurt = true;
            LifeController.Instance.UpdateLife(true);
            var countLife = LifeController.Instance.GetLife();
            if (countLife <= 0)
            {
                LifeController.Instance.SetInitialLife();
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
}