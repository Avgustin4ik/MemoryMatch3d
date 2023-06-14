using System.Linq;
using Core.Configs;
using Core.UI;
using Entitas;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace Ui.Screens
{
    public class DebugScreen : UIScreen, IUiShowListener, IUiHideListener
    {
        [SerializeField] private Toggle displayFPSToggle;
        [SerializeField] private TMP_Dropdown _dropdownLevelLoader;
        [SerializeField] private Volume _gloablVolume;
        [SerializeField] private Toggle grayscalePosteffectToggle;
        [SerializeField] private Toggle endlessTurnsToggle;
        [SerializeField] private Toggle endlessBoostersToggle;
        private IGroup<LevelEntity> _blueprintsGroup;
        private bool _isGloablVolumeNull;

        private void Start()
        {
            _isGloablVolumeNull = _gloablVolume == null;
        }

        public override void Init(IEntity iEntity)
        {
            base.Init(iEntity);
            UIEntity.isDebugScreen = true;
            UIEntity.AddUiHideListener(this);
            UIEntity.AddUiShowListener(this);
            UIEntity.triggerHide = true;
            
            _dropdownLevelLoader.ClearOptions();

            _blueprintsGroup = Contexts.sharedInstance.level.GetGroup(LevelMatcher.Blueprint);
            var index = 1;
            var gameLevelsNames = _blueprintsGroup.GetEntities().Select(e => $"Level-{index++}").ToList();
            _dropdownLevelLoader.AddOptions(gameLevelsNames);
        }

        public void OnShow(UiEntity entity)
        {
            // Open();
            gameObject.SetActive(true);
        }

        public void OnHide(UiEntity entity)
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            var gameDebugManagerEntity = Contexts.sharedInstance.game.debugManagerEntity;
            if(gameDebugManagerEntity == null) return;
            if (gameDebugManagerEntity.isDisplayFPSAlways != displayFPSToggle.isOn)
            {
                gameDebugManagerEntity.isDisplayFPSAlways = displayFPSToggle.isOn;
            }
            GrayScaleHandler();
        }

        public void DropdownHandler()
        {
            var levelLoader = Contexts.sharedInstance.level.levelLoaderEntity;
            levelLoader.ReplaceNextLevelNumber(_dropdownLevelLoader.value);
            Contexts.sharedInstance.level.CreateEntity().eventLoadNextGameLevel = true;
        }

        public void WinLevelButtonHandler()
        {
            Contexts.sharedInstance.game.stateManagerEntity.stateVictory = true;
        }

        public void LooseLevelButtonHandler()
        {
            Contexts.sharedInstance.game.stateManagerEntity.stateLoose = true;
        }

        public void DeleteAllSave()
        {
            Contexts.sharedInstance.data.CreateEntity().isRequestDeleteSaveData = true;
        }

        private void GrayScaleHandler()
        {
            ColorAdjustments colorAdjustments;
            if (_isGloablVolumeNull) return;
            if (_gloablVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments) == false) return;
            if(colorAdjustments.active == grayscalePosteffectToggle.isOn) return;
            colorAdjustments.active = grayscalePosteffectToggle.isOn;
        }

        public void EndlessTurnsHandler()
        {
            var turnControllerEntity = Contexts.sharedInstance.game.turnControllerEntity;
            if(turnControllerEntity == null) return;
            turnControllerEntity.isTurnCounterDisabled = endlessTurnsToggle.isOn;
        }

        public void EndlessBoostersHandler()
        {
            var playersGroup = Contexts.sharedInstance.game.GetGroup(GameMatcher.Player);
            foreach (var player in playersGroup)
            {
                player.isBoostersEndless = endlessBoostersToggle.isOn;
            }
        }
    }
}