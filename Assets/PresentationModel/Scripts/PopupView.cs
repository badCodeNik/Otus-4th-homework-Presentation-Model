using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationModel.Scripts
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private Image icon;

        [SerializeField] private Button closeButton;
        [SerializeField] private Slider slider;
        [SerializeField] private Transform statContainer;


        [SerializeField] public Button levelUpButton;
        [SerializeField] private TextMeshProUGUI userID;
        [SerializeField] private TextMeshProUGUI userDescription;
        [SerializeField] private TextMeshProUGUI levelProgress;
        [SerializeField] private TextMeshProUGUI level;
        [SerializeField] private Sprite availableLevelUp;
        [SerializeField] private Sprite unAvailableLevelUp;

        public Sprite AvailableLevelUp => availableLevelUp;

        public Transform StatContainer => statContainer;

        public Sprite UnAvailableLevelUp => unAvailableLevelUp;
        
        
        private void OnEnable()
        {
            closeButton.onClick.AddListener(ClosePopup);
        }


        public void Show(PlayerLevel playerLevel, UserInfo userInfo)
        {
            SetDescription(userInfo.Description);
            SetIcon(userInfo.Icon);
            SetUserId(userInfo.Name);
            gameObject.SetActive(true);
            userID.text = userInfo.Name;
            userDescription.text = userInfo.Description;
            UpdateSliderValues(playerLevel.RequiredExperience, playerLevel.CurrentExperience);
        }

        public void SetLevel(int playerLevel)
        {
            level.text = $" Level " + " " + $"{playerLevel.ToString()}";
        }
        public void UpdateSliderValues(int requiredExp, int currentExp)
        {
            slider.maxValue = requiredExp;
            slider.value = currentExp;
            levelProgress.text = $"{currentExp}" + "/" + $"{requiredExp}";
        }


        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ClosePopup()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            closeButton.onClick.RemoveListener(ClosePopup);
        }

        public void SetDescription(string description)
        {
            userDescription.text = description;
        }
        
        public void SetUserId(string id)
        {
            userID.text = id;
        }
        public void SetIcon(Sprite icon)
        {
            this.icon.sprite = icon;
        }
        
        
    }
}