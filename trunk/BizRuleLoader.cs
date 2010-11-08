using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	internal static class BizRuleLoader
	{
		public static string LoadScript(string path)
		{
			return System.IO.File.ReadAllText(path);
		}
	}
}
