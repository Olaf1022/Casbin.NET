﻿using System;
using System.Collections.Generic;

namespace NetCasbin.Persist
{
    public interface IAdapter
    {
        void LoadPolicy(Model.Model model);
        void SavePolicy(Model.Model model);

        void AddPolicy(String sec, String ptype, IList<String> rule);

        void RemovePolicy(String sec, String ptype, IList<String> rule);

        void RemoveFilteredPolicy(String sec, String ptype, int fieldIndex, params string[] fieldValues);
    }
}
