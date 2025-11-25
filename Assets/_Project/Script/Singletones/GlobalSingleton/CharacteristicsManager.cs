using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Characteristics
{
    public enum CharacteristicNames
    {
        MovementSpeed,
        DashSpeed,
        DashLenght,
        JumpHeight
    }

    public enum CharacteristicUpgradeNames
    {
        MovementSpeedUpgrade,
        DashSpeedUpgrade,
        DashLenghtUpgrade,
        JumpHeightUpgrade,
    }

    public class CharacteristicsManager : SingletonBase<CharacteristicsManager>
    {
        public CharacteristicUpgradeSet UpgradeSet;
        public List<CharacteristicUpgrade> characteristics = new List<CharacteristicUpgrade>();
        public Dictionary<string, Characteristic> actualValues = new Dictionary<string, Characteristic>();

        
        public void LoadCharacteristics()
        {
            foreach ((string key, Characteristic characteristic) in actualValues)
            {
                characteristic.Upgrade(CalculateUpgradeValue(characteristic));
            }
        }

        private float CalculateUpgradeValue(Characteristic characteristic)
        {
            float result = 0;
            var speeds = characteristics.Where((x) => x.CharacteristicId == characteristic.ID).ToArray();
            foreach (CharacteristicUpgrade upgradableCharacteristic in speeds)
            {
                result += upgradableCharacteristic.TotalImprovement;
            }

            return result;
        }
    }

    [Serializable] 
    public class Characteristic
    {
        public CharacteristicNames ID;
        public float BaseValue;
        public float TotalValue { get; private set; }

        public void Upgrade(float improvementValue)
        {
            TotalValue = BaseValue + improvementValue;
        }
    }
    
    public class CharacteristicUpgrade 
    {
        public CharacteristicUpgradeNames ID;
        public List<CharacteristicLevel> Level = new List<CharacteristicLevel>(); // Инициализация
        public CharacteristicNames CharacteristicId;
        
        public int CurrentLevel;
        public float TotalImprovement => GetTotalImprovement();
        public int MaxLevel => Level.Count;

        private float GetTotalImprovement()
        {
            float total = 0f;
            
            for (var i = 0; i < CurrentLevel; i++)
                total += Level[i].UpgradeValue;
            
            return total;
        }
    }


    [Serializable]
    public class CharacteristicLevel 
    {
        public int ID {get; private set; } 
        public float UpgradeValue;
        public float UpgradeCost;
    }

    public class CharacteristicsSaver 
    {
        public string PlayerID;
        public Dictionary<string, int> CharacteristicLevel = new Dictionary<string, int>(); 

        public void SaveData()
        {
            
        }
    }
}