using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationModel.Scripts
{
    public class StatFactory
    {
        private Dictionary<CharacterStat, GameObject> _activeStats;
        private readonly Queue<GameObject> _stats = new();

        public StatFactory(int requiedStats, Transform container)
        {
            for (int index = 0; index < requiedStats; index++)
            {
                GameObject statObject = new GameObject();
                statObject.SetActive(false);
                statObject.transform.SetParent(container, false);
                _stats.Enqueue(statObject);
            }

            _activeStats = new Dictionary<CharacterStat, GameObject>();
        }

        public void AddStatUI(CharacterStat stat, int currentLevel)
        {
            var statObject = _stats.Dequeue();
            
            statObject.name = stat.Name;
            TextMeshProUGUI text = statObject.AddComponent<TextMeshProUGUI>();
            text.text = $"{stat.Name}: {stat.Value}";
            text.fontSize = 22;
            text.color = Color.black;
            LayoutElement layoutElement = statObject.AddComponent<LayoutElement>();
            layoutElement.preferredHeight = 30;
            layoutElement.preferredWidth = 130;
            statObject.SetActive(true);
            stat.OnValueChanged += (newValue) => UpdateStatValue(statObject, newValue);
            stat.ChangeValue(currentLevel * 2);
            _activeStats[stat] = statObject;
        }

        public void UpdateAllStats(int value)
        {
            foreach (var stat in _activeStats.Keys)
            {
                stat.ChangeValue(value);
            }
        }

        private void UpdateStatValue(GameObject statObject, int newValue)
        {
            var text = statObject.GetComponent<TextMeshProUGUI>();
            text.text = $"{statObject.name}" + " " + $"{newValue}";
        }

        public void RemoveStatUI(CharacterStat stat)
        {
            if (!_activeStats.Remove(stat, out GameObject statGameObj)) return;

            statGameObj.SetActive(false);
            _stats.Enqueue(statGameObj);
        }
    }
}