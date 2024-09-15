using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationModel.Scripts
{
    public class PopupPresenter
    {
        private readonly PopupView _view;
        private readonly Button _levelUpButton;
        private readonly CharacterStat _characterStat;
        private readonly CharacterInfo _characterInfo;
        private readonly PlayerLevel _playerLevel;
        private readonly UserInfo _userInfo;
        private readonly StatFactory _statFactory;

        public PopupPresenter(PopupView view, UserConfig config)
        {
            _view = view;
            _statFactory = new StatFactory(6,view.StatContainer);
            _levelUpButton = view.levelUpButton;
            _characterInfo = config.CharacterInfo;
            _playerLevel = config.PlayerLevel;
            _userInfo = config.UserInfo;
            _levelUpButton.onClick.AddListener(LevelUp);
            _playerLevel.OnLevelUp += _view.SetLevel;
            _playerLevel.OnExperienceChanged += UpdateExp;
            _userInfo.OnDescriptionChanged += UpdateDescription;
            _userInfo.OnIconChanged += UpdateIcon;
            _userInfo.OnNameChanged += UpdateName;
            _characterInfo.OnStatAdded += AddStat;
            _characterInfo.OnStatRemoved += RemoveStat;
        }

        private void RemoveStat(CharacterStat stat)
        {
            _statFactory.RemoveStatUI(stat);
        }

        private void AddStat(CharacterStat characterStat)
        {
            _statFactory.AddStatUI(characterStat, _playerLevel.CurrentLevel);
        }


        private void UpdateIcon(Sprite icon)
        {
            _view.SetIcon(icon);
        }

        private void UpdateName(string id)
        {
            _view.SetUserId(id);
        }

        private void UpdateDescription(string description)
        {
            _view.SetDescription(description);
        }

        private void UpdateExp(int exp)
        {
            _view.UpdateSliderValues(_playerLevel.RequiredExperience, exp);
        }

        public void Show()
        {
            _view.Show(_playerLevel, _userInfo);
        }

        private void LevelUp()
        {
            _levelUpButton.image.sprite = _view.AvailableLevelUp;
            _playerLevel.AddExperience(100);
            
            if (_playerLevel.CanLevelUp())
            {
                _playerLevel.LevelUp();
                _statFactory.UpdateAllStats(_playerLevel.CurrentLevel * 2);
                _view.UpdateSliderValues(_playerLevel.RequiredExperience, _playerLevel.CurrentLevel);
            }

            if (_playerLevel.CurrentLevel >= 10)
            {
                _levelUpButton.enabled = false;
                _levelUpButton.image.sprite = _view.UnAvailableLevelUp;
            }
                
        }

        public void Disable()
        {
            _playerLevel.OnLevelUp -= _view.SetLevel;
            _playerLevel.OnExperienceChanged -= UpdateExp;
            _userInfo.OnDescriptionChanged -= UpdateDescription;
            _userInfo.OnIconChanged -= UpdateIcon;
            _userInfo.OnNameChanged -= UpdateName;
            _characterInfo.OnStatAdded -= AddStat;
            _characterInfo.OnStatRemoved -= RemoveStat;
        }
    }
}