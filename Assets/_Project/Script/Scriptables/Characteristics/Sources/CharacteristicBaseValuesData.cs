using System.Collections.Generic;
using UnityEngine;

namespace Characteristics
{
  /// <summary>
  /// заполняется с помощью CharacteristicBaseValuesDataEditor из енама CharacteristicNames
  /// чтобы добавить новую характеристику добавьте новое имя в енам и она автоматически подтянется в инспектор
  /// </summary>
    [CreateAssetMenu(fileName = "CharacteristicBaseValuesData", menuName = "Characteristics/CharacteristicBaseValuesData")]
    public class CharacteristicBaseValuesData : ScriptableObject
    {
        public List<Characteristic> Characteristics = new List<Characteristic>();
    }
}