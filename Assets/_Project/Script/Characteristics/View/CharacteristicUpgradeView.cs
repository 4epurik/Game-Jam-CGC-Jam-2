using System;
using UnityEngine;
using UnityEngine.UI;

namespace Characteristics
{
    public class CharacteristicUpgradeView : MonoBehaviour
    {
        public CharacteristicUpgradeNames UpgradeName;
        [NonSerialized] public CharacteristicUpgrade UpgradableCharacteristic;
        [NonSerialized] public string Description;
        [NonSerialized] public Image Icon;

        public void Init(CharacteristicUpgradeData data, CharacteristicUpgrade upgradableCharacteristic )
        {
            Description = data.Description;
            Icon = data.Icon;
            UpgradableCharacteristic = upgradableCharacteristic;
        }
    }
}