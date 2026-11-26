// ArchNet · Canvas Extension — responsive switch
// Shows its targets only on the devices and orientations ticked: a phone layout and a tablet
// layout of the same menu, a hint shown on computers only, a gamepad icon hidden on phones…
// Docs: https://archnet-plateform.fr/#CanvasExtension

using UnityEngine;

namespace ArchNet.Extension
{
    [AddComponentMenu("ArchNet/Responsive Switch")]
    public class ResponsiveSwitch : MonoBehaviour
    {
        [Tooltip("Objects shown or hidden (not this one, which keeps checking).")]
        [SerializeField] GameObject[] _targets;
        [SerializeField] bool _phone = true;
        [SerializeField] bool _tablet = true;
        [SerializeField] bool _desktop = true;
        [SerializeField] bool _portrait = true;
        [SerializeField] bool _landscape = true;

        Vector2Int _screen;

        void OnEnable() => _screen = Vector2Int.zero;

        void Update()
        {
            if (Screen.width == _screen.x && Screen.height == _screen.y) return;
            _screen = new Vector2Int(Screen.width, Screen.height);
            bool show = IsShown(DeviceInfo.Kind, DeviceInfo.IsPortrait);
            foreach (GameObject target in _targets)
                if (target != null && target != gameObject) target.SetActive(show);
        }

        public bool IsShown(DeviceKind kind, bool portrait)
        {
            bool device = kind == DeviceKind.Phone ? _phone : kind == DeviceKind.Tablet ? _tablet : _desktop;
            return device && (portrait ? _portrait : _landscape);
        }
    }
}
