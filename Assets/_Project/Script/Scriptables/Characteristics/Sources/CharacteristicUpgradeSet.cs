using System.Collections.Generic;
using UnityEngine;

namespace Characteristics
{
    [CreateAssetMenu(fileName = "CharacteristicUpgradeSet", menuName = "Characteristics/CharacteristicUpgradeSet")]
    public class CharacteristicUpgradeSet : ScriptableObject
    {
        public List<CharacteristicUpgradeData> UpgradeDataSet = new List<CharacteristicUpgradeData>(); // Инициализация
    }
}