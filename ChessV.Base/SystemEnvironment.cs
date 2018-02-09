
/***************************************************************************

                                 ChessV

                  COPYRIGHT (C) 2012-2017 BY GREG STRONG

This file is part of ChessV.  ChessV is free software; you can redistribute
it and/or modify it under the terms of the GNU General Public License as 
published by the Free Software Foundation, either version 3 of the License, 
or (at your option) any later version.

ChessV is distributed in the hope that it will be useful, but WITHOUT ANY 
WARRANTY; without even the implied warranty of MERCHANTABILITY or 
FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for 
more details; the file 'COPYING' contains the License text, but if for
some reason you need a copy, please visit <http://www.gnu.org/licenses/>.

****************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ChessV
{
	public class SystemEnvironment
	{
		[DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
		public extern static IntPtr LoadLibrary(string libraryName);

		[DllImport("kernel32", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
		public extern static IntPtr GetProcAddress(IntPtr hwnd, string procedureName);

		private delegate bool IsWow64ProcessDelegate([In] IntPtr handle, [Out] out bool isWow64Process);

		public static bool IsOS64Bit()
		{
			if( IntPtr.Size == 8 || (IntPtr.Size == 4 && Is32BitProcessOn64BitProcessor()) )
				return true;
			return false;
		}

		private static IsWow64ProcessDelegate GetIsWow64ProcessDelegate()
		{
			IntPtr handle = LoadLibrary( "kernel32" );

			if( handle != IntPtr.Zero )
			{
				IntPtr fnPtr = GetProcAddress( handle, "IsWow64Process" );

				if( fnPtr != IntPtr.Zero )
				return (IsWow64ProcessDelegate) Marshal.GetDelegateForFunctionPointer( (IntPtr) fnPtr, typeof(IsWow64ProcessDelegate) );
			}

			return null;
		}

		private static bool Is32BitProcessOn64BitProcessor()
		{
			IsWow64ProcessDelegate fnDelegate = GetIsWow64ProcessDelegate();

			if( fnDelegate == null )
				return false;
	
			bool isWow64;
			bool retVal = fnDelegate.Invoke( Process.GetCurrentProcess().Handle, out isWow64 );

			if( retVal == false )
				return false;

			return isWow64;
		}

	}
}
