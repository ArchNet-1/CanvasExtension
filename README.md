<p align="center">
  <a href="https://archnet-plateform.fr"><img src="https://archnet-plateform.fr/ArchNetLogoWithBck.png" alt="ArchNet" width="240"></a>
</p>

# ArchNet · Canvas Extension

Responsive UI for phones, tablets and computers, plus extension methods for Canvas and CanvasGroup.

- Unity **6000.0** or newer · Needs `com.unity.ugui`.
- Part of [ArchNet](https://archnet-plateform.fr), free and open-source Unity packages

## Install

*Window > Package Manager > + > Install package from git URL…*

```
https://github.com/ArchNet-1/CanvasExtension.git
```

## Responsive UI (one click)

*ArchNet > Hub > Services > Responsive UI > Add to game* goes through every prefab and scene under `Assets/ArchNet` and, on each canvas:

- adds a **Responsive Canvas**: the whole 1920×1080 design stays visible on any screen shape, portrait or landscape (Canvas Scaler in *Expand* mode), and the UI is larger on phones (×1.3) and tablets (×1.1) so texts stay readable;
- moves its content into a **Safe Area**, away from notches, rounded corners and home bars (full-screen backgrounds stay outside).

Click **Update** on the card after adding screens. **Open tool**: open Unity's Device Simulator to preview phones and tablets, make a canvas of your own responsive, choose the mobile orientations (portrait, landscape or both).

| Component | Use |
|---|---|
| **ArchNet/Safe Area** | On a full-screen RectTransform: it shrinks to the safe part of the screen, and follows rotations. |
| **ArchNet/Responsive Canvas** | Next to a Canvas Scaler: whole design visible, larger UI on phones and tablets. |
| **ArchNet/Responsive Switch** | Shows its targets only on the devices (phone / tablet / desktop) and orientations ticked. |
| `DeviceInfo.Kind`, `DeviceInfo.IsPortrait` | Phone, tablet (7" and more) or desktop, from code. |

## Extension methods

```csharp
using ArchNet.Extension;

popupCanvas.BringToFront();
menuGroup.Hide();
canvas.RebuildLayout();
```

## License

MIT, see [LICENSE.md](LICENSE.md).
