using UnityEngine;

namespace Characteristics
{
    public class UtilityManager : SingletonBase<UtilityManager>
    {
        public string TimeToText(float timeval)
        {
            int minutes = Mathf.FloorToInt(timeval / 60);
            int seconds = Mathf.FloorToInt(timeval % 60);
            return $"{minutes:00}:{seconds:00}";
        }
    }
}