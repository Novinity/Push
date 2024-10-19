using InputAPI;
using UnityEngine;

namespace Push.Input {
    internal class PushInput : BaseCWInput, IExposedSetting {
        public override KeyCode GetDefaultKey() {
            return KeyCode.E;
        }

        public string GetDisplayName() {
            return "Push";
        }

        public SettingCategory GetSettingCategory() {
            return SettingCategory.Controls;
        }

        protected override void OnHeld(Player player) {}

        protected override void OnKeyDown(Player player) {
            Transform cam = player.refs.headPos;

            if (Physics.Raycast(cam.position + (cam.forward * 0.3f), cam.forward, out RaycastHit hit, 7f)) {
                Player foundPlayer = hit.transform.GetComponent<Player>();
                if (!foundPlayer) {
                    Transform p = hit.transform.parent;
                    while (p != null && !p.GetComponent<Player>()) {
                        p = p.parent;
                    }
                    if (p == null) return;
                    foundPlayer = p.GetComponent<Player>();
                    Plugin.Logger.LogDebug(foundPlayer);
                    if (foundPlayer == player) return;
                    Plugin.Logger.LogDebug("not my player");
                }
                if (!Plugin.BoundConfig.canPushMonsters.Value && foundPlayer.ai) return;
                Plugin.Logger.LogDebug("cAN PUSH MONSTER");
                if (!Plugin.BoundConfig.canPushPlayers.Value && !foundPlayer.ai) return;
                Plugin.Logger.LogDebug("CanPush");
                foundPlayer.CallTakeDamageAndAddForceAndFall(0, 
                    (foundPlayer.refs.headPos.position - player.refs.headPos.position).normalized * Plugin.BoundConfig.pushForce.Value, 3);
            }
        }

        protected override void OnKeyUp(Player player) {}
    }
}
