using System;
using System.Runtime.InteropServices;

namespace PMS.Libraries.ToolControls.PMSPublicInfo.APIs
{
	public class APIsUxTheme
	{
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto)]
		static public extern IntPtr OpenThemeData(
			IntPtr hWnd, string pszClassList);
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto)]
		static public extern int CloseThemeData(IntPtr hTheme);
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto)]
		static public extern bool IsThemeActive();
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto)]
		static public extern bool IsAppThemed();
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto)]
		static public extern int DrawThemeBackground(
			IntPtr hWnd, IntPtr hDc, int iPartId, int iStateId,
			APIsStructs.RECT pRect, APIsStructs.RECT pClipRect);
		[DllImport("UxTheme.dll", CharSet=CharSet.Auto)]
		static public extern int GetCurrentThemeName(
			System.Text.StringBuilder pszThemeFileName, int dwMaxNameChars,
            System.Text.StringBuilder pszColorBuff, int cchMaxColorChars,
            System.Text.StringBuilder pszSizeBuff, int cchMaxSizeChars);
	}
}
