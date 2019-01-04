﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace Library.Core.DataTypes
{

    /// <summary>
    /// Gives you the base types. The PropertyInfo.PropertyType.IsPrimitive doesnt give you string and the nullable types. This class will give you all the primitive types include nullable values
    /// </summary>
    public static class PrimitiveTypes
    {

        /// <summary>
        /// Gives you the base types. The PropertyInfo.PropertyType.IsPrimitive doesnt give you string and the nullable types. This class will give you all the primitive types include nullable values
        /// </summary>
        /// <returns>List Of Types</returns>
        public static ImmutableHashSet<Type> PrimitiveTypeLookup { get; } = ImmutableHashSet.Create(
            typeof(string),
            typeof(bool),
            typeof(bool?),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(Int16),
            typeof(Int16?),
            typeof(Int32),
            typeof(Int32?),
            typeof(Int64),
            typeof(Int64?),
            typeof(double),
            typeof(double?),
            typeof(float),
            typeof(float?),
            typeof(decimal),
            typeof(decimal?)
            );

    }

}
