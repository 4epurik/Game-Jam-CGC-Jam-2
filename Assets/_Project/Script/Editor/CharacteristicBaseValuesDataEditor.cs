using UnityEditor;
using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;
using Characteristics;

namespace Characteristics
{
    [CustomEditor(typeof(CharacteristicBaseValuesData))]
    public class CharacteristicBaseValuesDataEditor : Editor
    {
        private SerializedProperty characteristicsProperty;

        private void OnEnable()
        {
            // Получаем SerializedProperty для нашего списка
            characteristicsProperty = serializedObject.FindProperty("Characteristics");
        }

        public override void OnInspectorGUI()
        {
            // Всегда начинаем с обновления, чтобы получить свежие данные с диска
            serializedObject.Update();

            var dataSO = (CharacteristicBaseValuesData)target;
            
            // Получаем все возможные значения Enum
            CharacteristicNames[] enumValues = (CharacteristicNames[])Enum.GetValues(typeof(CharacteristicNames));
            
            // --- Защита от Null и Структурная Синхронизация (Add/Remove) ---
            
            // Защита от Null (для старых ассетов)
            if (dataSO.Characteristics == null)
            {
                dataSO.Characteristics = new List<Characteristic>();
                EditorUtility.SetDirty(dataSO);
                Debug.Log("Применено SetDirty");
            }
            
            bool structuralChanged = false;
            
            // 1. Проверяем и добавляем НОВЫЕ элементы из Enum
            foreach (CharacteristicNames enumStat in enumValues)
            {
                if (!dataSO.Characteristics.Any(c => c.ID == enumStat))
                {
                    // Используем SerializedProperty для структурных изменений
                    characteristicsProperty.arraySize++;
                    SerializedProperty newElement = characteristicsProperty.GetArrayElementAtIndex(characteristicsProperty.arraySize - 1);
                    
                    newElement.FindPropertyRelative("ID").enumValueIndex = (int)enumStat;
                    newElement.FindPropertyRelative("BaseValue").floatValue = 0f;
                    
                    structuralChanged = true;
                }
            }
            
            // 2. Очищаем УСТАРЕВШИЕ элементы
            var idsToRemove = dataSO.Characteristics
                                    .Where(c => !enumValues.Contains(c.ID))
                                    .Select(c => c.ID)
                                    .ToList();
            
            if (idsToRemove.Any())
            {
                dataSO.Characteristics.RemoveAll(c => idsToRemove.Contains(c.ID));
                structuralChanged = true;
            }

            // 3. Сортируем и пересинхронизируем SerializedObject, если была изменена структура
            if (structuralChanged)
            {
                // Сортируем dataSO для стабильности и порядка в Inspector
                dataSO.Characteristics = dataSO.Characteristics
                                             .OrderBy(c => c.ID.ToString())
                                             .ToList();
                                             
                EditorUtility.SetDirty(dataSO);
                
                // Применяем изменения структуры и считываем заново
                serializedObject.ApplyModifiedProperties(); 
                serializedObject.Update(); 
            }
            
            // ----------------------------------------------------------------------------------
            // ОТРОВКА В ИНСПЕКТОРЕ и ОПТИМИЗАЦИЯ СОХРАНЕНИЯ
            // ----------------------------------------------------------------------------------
            
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Base Values Configuration (Synced with Enum)", EditorStyles.boldLabel);
            
            // *** КЛЮЧЕВОЕ ИЗМЕНЕНИЕ: Начинаем отслеживание изменений, внесенных пользователем ***
            EditorGUI.BeginChangeCheck();

            // Рисуем каждый элемент
            for (int i = 0; i < characteristicsProperty.arraySize; i++)
            {
                SerializedProperty element = characteristicsProperty.GetArrayElementAtIndex(i);
                
                SerializedProperty idProperty = element.FindPropertyRelative("ID");
                SerializedProperty baseValueProperty = element.FindPropertyRelative("BaseValue");
                
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                
                // ID (Только для чтения)
                GUI.enabled = false;
                EditorGUILayout.LabelField(((CharacteristicNames)idProperty.enumValueIndex).ToString(), GUILayout.Width(120));
                GUI.enabled = true;

                // BaseValue (Редактируемое поле)
                EditorGUILayout.PropertyField(baseValueProperty, new GUIContent("Base Value"));
                
                EditorGUILayout.EndHorizontal();
            }

            // *** ОПТИМИЗАЦИЯ: Вызываем сохранение ТОЛЬКО, если EndChangeCheck() обнаружил изменение ***
            if (EditorGUI.EndChangeCheck())
            {
                // Это сработает, только если пользователь изменил BaseValue
                serializedObject.ApplyModifiedProperties();
                Debug.Log("Применено сохранение BaseValue: Обнаружено изменение пользователем.");
            }
        }
    }
}