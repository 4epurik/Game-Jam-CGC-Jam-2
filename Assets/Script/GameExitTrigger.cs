using Script;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                PlayerDataManager.Instance.SaveCoins(CoinCollector.Instance.GetCoins());
                PlayerDataManager.Instance.SaveTime(GameTimer.Instance.GetElapsedTime());
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}