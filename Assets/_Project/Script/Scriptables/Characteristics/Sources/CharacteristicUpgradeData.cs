using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Characteristics
{
    [CreateAssetMenu(fileName = "CharacteristicUpgradeData", menuName = "Characteristics/CharacteristicUpgradeData")]
    public class CharacteristicUpgradeData: ScriptableObject
    {
        public CharacteristicUpgradeNames UpgradeID;
        public CharacteristicNames CharacteristicId;
        public List<CharacteristicLevel> Level = new List<CharacteristicLevel>(); 
        
        public string Description;
        public Image Icon;
    }
}