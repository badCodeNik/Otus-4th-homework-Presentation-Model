using UnityEngine;

namespace PresentationModel.Scripts
{
    [CreateAssetMenu(fileName = "UserConfig", menuName = "Data/UserConfigs", order = 0)]
    public class UserConfig : ScriptableObject
    {
        [SerializeField] private CharacterInfo characterInfo;
        [SerializeField] private PlayerLevel playerLevel;
        [SerializeField] private UserInfo userInfo;


        public CharacterInfo CharacterInfo => characterInfo;
        public PlayerLevel PlayerLevel => playerLevel;
        public UserInfo UserInfo => userInfo;
    }
}