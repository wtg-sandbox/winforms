﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Runtime.InteropServices;

internal static partial class Interop
{
    internal static partial class ComCtl32
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public unsafe struct LVITEMW
        {
            public LIST_VIEW_ITEM_FLAGS mask;
            public int iItem;
            public int iSubItem;
            public LIST_VIEW_ITEM_STATE_FLAGS state;
            public LIST_VIEW_ITEM_STATE_FLAGS stateMask;
            public char* /* LPWSTR */ pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
            public int iGroupId;
            public int cColumns; // tile view columns
            public IntPtr puColumns;
            public LVCOLUMNW_FORMAT* piColFmt;
            public int iGroup; // readonly. only valid for owner data.

            /// <summary>
            /// Set the new text. The text length is limited by <see cref="cchTextMax"/>.
            /// A value of <see cref="cchTextMax"/> will be updated to the length of <paramref name="text"/> + 1.
            /// </summary>
            /// <param name="text">The text to set.</param>
            public void UpdateText(ReadOnlySpan<char> text)
            {
                if (cchTextMax <= text.Length)
                {
                    text = text.Slice(0, cchTextMax - 1);
                }
                else
                {
                    cchTextMax = text.Length + 1;
                }

                text.CopyTo(new Span<char>(pszText, cchTextMax));
                pszText[text.Length] = '\0';
            }
        }
    }
}
