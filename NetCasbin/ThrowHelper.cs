﻿using System;

namespace Casbin
{
    internal static class ThrowHelper
    {
        internal static void ThrowNameNotFoundException()
            => throw new ArgumentException("The name parameter does not exist.");

        internal static void ThrowOneOfNamesNotFoundException()
            => throw new ArgumentException("The name1 or name2 parameter does not exist.");
    }
}
