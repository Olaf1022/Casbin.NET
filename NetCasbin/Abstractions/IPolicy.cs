﻿using System.Collections.Generic;
using Casbin.Rbac;

namespace Casbin
{
   public interface IPolicy
   {
        /// <summary>
        /// Provides incremental build the role inheritance relation.
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="policyOperation"></param>
        /// <param name="section"></param>
        /// <param name="policyType"></param>
        /// <param name="rule"></param>
        public void BuildIncrementalRoleLink(IRoleManager roleManager, PolicyOperation policyOperation,
            string section, string policyType, IEnumerable<string> rule);

        /// <summary>
        /// Provides incremental build the role inheritance relations.
        /// </summary>
        /// <param name="roleManager"></param>
        /// <param name="policyOperation"></param>
        /// <param name="section"></param>
        /// <param name="policyType"></param>
        /// <param name="rules"></param>
        public void BuildIncrementalRoleLinks(IRoleManager roleManager, PolicyOperation policyOperation,
            string section, string policyType, IEnumerable<IEnumerable<string>> rules);

        /// <summary>
        /// Initializes the roles in RBAC.
        /// </summary>
        /// <param name="roleManager"></param>
        public void BuildRoleLinks(IRoleManager roleManager);

        public void RefreshPolicyStringSet();

        public IEnumerable<IEnumerable<string>> GetPolicy(string section, string policyType);

        public IEnumerable<IEnumerable<string>> GetFilteredPolicy(string section, string policyType, int fieldIndex,
            params string[] fieldValues);

        public IEnumerable<string> GetValuesForFieldInPolicy(string section, string policyType, int fieldIndex);

        public IEnumerable<string> GetValuesForFieldInPolicyAllTypes(string section, int fieldIndex);

        public bool HasPolicy(string section, string policyType, IEnumerable<string> rule);

        public bool HasPolicies(string section, string policyType, IEnumerable<IEnumerable<string>> rules);

        public bool AddPolicy(string section, string policyType, IEnumerable<string> rule);

        public bool AddPolicies(string section, string policyType, IEnumerable<IEnumerable<string>> rules);

        public bool RemovePolicy(string section, string policyType, IEnumerable<string> rule);

        public bool RemovePolicies(string section, string policyType, IEnumerable<IEnumerable<string>> rules);

        public IEnumerable<IEnumerable<string>> RemoveFilteredPolicy(string section, string policyType, int fieldIndex, params string[] fieldValues);

        public void ClearPolicy();
   }
}
