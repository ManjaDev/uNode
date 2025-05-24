using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices; // Required for ComVisibleAttribute

namespace MaxyGames.UNode {
    public static class AttributeUtils {
        /// <summary>
        /// Filters a collection of types to find those that have the ComVisibleAttribute
        /// and its value is set to false.
        /// </summary>
        /// <param name="types">The collection of types to filter.</param>
        /// <returns>An enumerable of types that have [ComVisible(false)].</returns>
        public static IEnumerable<Type> GetTypesWithComVisibleAttributeFalse(IEnumerable<Type> types) {
            if (types == null) {
                throw new ArgumentNullException(nameof(types));
            }

            List<Type> result = new List<Type>();
            foreach (Type type in types) {
                if (type == null) continue;

                // Get ComVisibleAttribute, inheriting from base types
                object[] attributes = type.GetCustomAttributes(typeof(ComVisibleAttribute), true);
                
                if (attributes != null && attributes.Length > 0) {
                    ComVisibleAttribute comVisibleAttr = attributes.OfType<ComVisibleAttribute>().FirstOrDefault();
                    if (comVisibleAttr != null && !comVisibleAttr.Value) {
                        result.Add(type);
                    }
                }
            }
            return result;
        }
    }
}
