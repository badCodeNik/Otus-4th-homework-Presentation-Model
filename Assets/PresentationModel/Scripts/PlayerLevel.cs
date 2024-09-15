using System;
using Sirenix.OdinInspector;

namespace PresentationModel.Scripts
{
    [Serializable]
    public class PlayerLevel
    {
        public event Action<int> OnLevelUp;
        public event Action<int> OnExperienceChanged;

        [ShowInInspector, ReadOnly] public int CurrentLevel { get; private set; } = 1;

        [ShowInInspector, ReadOnly] public int CurrentExperience { get; private set; }

        [ShowInInspector, ReadOnly]
        public int RequiredExperience =>
            100 * (CurrentLevel + 1);

        [Button]
        public void AddExperience(int range)
        {
            var xp = Math.Min(CurrentExperience + range, RequiredExperience);
            CurrentExperience = xp;
            OnExperienceChanged?.Invoke(xp);
        }

        [Button]
        public void LevelUp()
        {
            if (this.CanLevelUp())
            {
                this.CurrentExperience = 0;
                this.CurrentLevel++;
                this.OnLevelUp?.Invoke(CurrentLevel);
            }
        }

        [Button]
        public void ResetLevel()
        {
            CurrentExperience = 0;
            CurrentLevel = 0;
            OnLevelUp?.Invoke(CurrentLevel);
        }

        public bool CanLevelUp()
        {
            return this.CurrentExperience == this.RequiredExperience;
        }
    }
}