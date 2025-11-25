using System.Collections.Generic;
using UnityEngine;

namespace Characteristics
{
    public class CharacteristicUpgradeViewFabric : MonoBehaviour
    {
        private CharacteristicsManager characteristicsManager => CharacteristicsManager.Instance;
        private List<CharacteristicUpgradeView> characteristicUpgradeViews = new();
        private void Awake()
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent(out CharacteristicUpgradeView upgradeView))
                {
                    characteristicUpgradeViews.Add(upgradeView);
                    
                }
            }
        }
    }
}