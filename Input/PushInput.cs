using InputAPI;
using Push.Settings;
using UnityEngine;

namespace Push.Input;
[ContentWarningSetting]
    internal class PushInput : BaseCWInput, IExposedSetting
    {
        private static float timeOfLastPush = -1;
        
        protected override KeyCode GetDefaultKey() {
            return KeyCode.E;
        }

        public string GetDisplayName() {
            return "Push";
        }

        public SettingCategory GetSettingCategory() {
            return SettingCategory.Mods;
        }

        protected override void OnHeld(Player player) {}

        protected override void OnKeyDown(Player player) {
            if (timeOfLastPush != -1 && Time.time - timeOfLastPush < 1.5f)
            {
                return;
            }
            Transform cam = player.refs.headPos;
            
            PushForceSetting pushForceSetting = GameHandler.Instance.SettingsHandler.GetSetting<PushForceSetting>();
            CanPushMonstersSetting canPushMonsters = GameHandler.Instance.SettingsHandler.GetSetting<CanPushMonstersSetting>();
            CanPushPlayersSetting canPushPlayersSetting = GameHandler.Instance.SettingsHandler.GetSetting<CanPushPlayersSetting>();

            if (Physics.Raycast(cam.position + (cam.forward * 0.3f), cam.forward, out RaycastHit hit, 7f)) {
                Player foundPlayer = hit.transform.GetComponent<Player>();
                if (foundPlayer == null) {
                    Transform p = hit.transform.parent;
                    while (p != null && p.GetComponent<Player>() == null) {
                        p = p.parent;
                    }
                    if (p == null) return;
                    foundPlayer = p.GetComponent<Player>();
                    Debug.Log(foundPlayer);
                    if (foundPlayer == player) return;
                    Debug.Log("not my player");
                }
                if (canPushMonsters.Value == 0 && foundPlayer.ai) return;
                Debug.Log("cAN PUSH MONSTER");
                if (canPushPlayersSetting.Value == 0 && !foundPlayer.ai) return;
                Debug.Log("CanPush");
                foundPlayer.CallTakeDamageAndAddForceAndFallWithFallof(0, 
                    (foundPlayer.refs.headPos.position - player.refs.headPos.position).normalized * pushForceSetting.Value, 1.5f,
                    player.refs.headPos.position, 5f);
                timeOfLastPush = Time.time;
            }
        }

        protected override void OnKeyUp(Player player) {}
    }
