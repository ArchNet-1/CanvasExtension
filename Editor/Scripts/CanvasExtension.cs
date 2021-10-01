using UnityEngine;

namespace ArchNet.Extension.ReverseMask2D
{
	/// <summary>
	/// [EXTENSION] - [ARCH NET] - [CANVAS] Canvas Extension Main Script
	/// author : Louis PAKEL
	/// </summary>
	public static class CanvasExtension
	{
		#region Public Methods

		/// <summary>
		/// Activate override sorting of fiven canvas and set his sorting order to max value
		/// </summary>
		/// <param name="pCanvas"></param>
		public static void SetCanvasToForeground(Canvas pCanvas)
		{
			pCanvas.overrideSorting = true;
			pCanvas.sortingOrder = 32767; // Biggest value, always in first plan
		}

		/// <summary>
		/// Reset sorting order of given canvas to 0 and stop overriding sorting
		/// </summary>
		/// <param name="pCanvas"></param>
		public static void StopOverridingCanvas(Canvas pCanvas)
		{
			pCanvas.sortingOrder = 0;
			pCanvas.overrideSorting = false;
		}
		#endregion
	}
}
