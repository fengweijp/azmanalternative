using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AzAlternative
{
	/// <summary>
	/// Specifies the type of group
	/// </summary>
	public enum GroupType
	{
		/// <summary>
		/// Basic group containing Windows members
		/// </summary>
		Basic,
		/// <summary>
		/// LDAP Query specifying entries to return
		/// </summary>
		LdapQuery
	}

	/// <summary>
	/// Specifies the language used by a biz rule
	/// </summary>
    public enum BizRuleLanguage
    {
		/// <summary>
		/// The language is not specified or used
		/// </summary>
		Undefined,
		/// <summary>
		/// The rule is written using VBScript
		/// </summary>
        VBScript,
		/// <summary>
		/// The rule is written using JScript
		/// </summary>
        JScript
    }

	public static class Constants
	{
	}
}
