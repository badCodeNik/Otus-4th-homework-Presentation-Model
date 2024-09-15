using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace PresentationModel.Scripts
{
    [Serializable]
    public class CharacterInfo
    {
        public event Action<CharacterStat> OnStatAdded;
        public event Action<CharacterStat> OnStatRemoved;

        [ShowInInspector] private readonly HashSet<CharacterStat> _stats = new();

        [Button]
        public void AddStat(string name)
        {
            var stat = new CharacterStat(name);
            if (_stats.Add(stat))
            {
                OnStatAdded?.Invoke(stat);
            }
        }

        [Button]
        public void RemoveStat(CharacterStat stat)
        {
            if (_stats.Remove(stat))
            {
                this.OnStatRemoved?.Invoke(stat);
            }
        }

        public CharacterStat GetStat(string name)
        {
            foreach (var stat in this._stats)
            {
                if (stat.Name == name)
                {
                    return stat;
                }
            }

            throw new Exception($"Stat {name} is not found!");
        }

        public CharacterStat[] GetStats()
        {
            return this._stats.ToArray();
        }
    }
}