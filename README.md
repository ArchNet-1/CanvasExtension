<p align="center">
  <a href="https://archnet-plateform.fr"><img src="https://archnet-plateform.fr/ArchNetLogoWithBck.png" alt="ArchNet" width="240"></a>
</p>

# ArchNet · Canvas Extension

Extension methods for Canvas and CanvasGroup: bring a canvas to the front, reset its sorting, show or hide a group, rebuild a layout.

- Unity **6000.0** or newer · No dependency.
- Part of [ArchNet](https://archnet-plateform.fr), free and open-source Unity packages

## Install

*Window > Package Manager > + > Install package from git URL…*

```
https://github.com/ArchNet-1/CanvasExtension.git
```

## Use

```csharp
using ArchNet.Extension;

popupCanvas.BringToFront();
menuGroup.Hide();
canvas.RebuildLayout();
```

## License

See [LICENSE.md](LICENSE.md).
