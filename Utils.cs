using System;
using System.IO;

namespace ZTD {

	class Utils {

		public static string GetFilePath(string filename) {
			return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
		}
	}
}
