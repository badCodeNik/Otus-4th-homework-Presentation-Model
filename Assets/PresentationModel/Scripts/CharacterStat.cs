using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PresentationModel.Scripts
{
    [Serializable]
    public class CharacterStat
    {
        public event Action<int> OnValueChanged;
        [SerializeField, ReadOnly] private string name;
        [SerializeField, ReadOnly] private int value;

        public string Name => name;
        public int Value => value;

        [Button]
        public void ChangeValue(int value)
        {
            this.value = value;
            OnValueChanged?.Invoke(value);
        }
        
        public CharacterStat(string name)
        {
            this.name = name;
        }
    }
}