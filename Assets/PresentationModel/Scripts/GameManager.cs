using System;
using UnityEngine;
using UnityEngine.UI;

namespace PresentationModel.Scripts
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private UserConfig config;
        [SerializeField] private PopupView view;
        [SerializeField] private Button showButton;
        private PopupPresenter _presenter;


        private void Start()
        {
            _presenter = new PopupPresenter(view, config);
            showButton.onClick.AddListener(ShowPresenter);
        }

        private void ShowPresenter()
        {
            _presenter.Show();
            showButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _presenter.Disable();
        }
    }
}