using UnityEngine;

// ScriptableObject для хранения данных
[CreateAssetMenu(fileName = "ReloadGame", menuName = "Game/ReloadGame")]
public class ReloadGame : ScriptableObject
{
    public bool needReloadGame;
}