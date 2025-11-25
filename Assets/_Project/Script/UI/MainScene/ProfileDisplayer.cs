using System;
using System.Collections;
using System.Collections.Generic;
using Characteristics;
using TMPro;
using UnityEngine;

public class ProfileDisplayer : MonoBehaviour
{
   [SerializeField] private TextMeshProUGUI nameText;
   [SerializeField] private TextMeshProUGUI moneyText;
   [SerializeField] private TextMeshProUGUI totalText;

   private MetaGameplayData meta => MetaGameplayData.Instance;
   private UtilityManager utility => UtilityManager.Instance;
   
   private void OnEnable()
   { 
      nameText.text = meta.PlayerName;
      moneyText.text = meta.CurrentMoney.ToString();
      totalText.text = utility.TimeToText( meta.TotalTime);
   }
}
