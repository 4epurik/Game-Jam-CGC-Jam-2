using System;
using UnityEngine;
using System.Collections.Generic;

public class LocalBootstrapManager : MonoBehaviour
{
    /// нужен чтобы загрузить префаб с синглтонами раньше других.
    /// Убедитесь что в Edit->ProjectSettings->ScriptSortOrder имеет приоритет
    /// Например -100
    ///
    private void Awake()
    {
        Debug.Log("LocalBootstrapManager");
        
    }
}