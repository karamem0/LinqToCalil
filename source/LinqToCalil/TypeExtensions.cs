using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// <see cref="System.Type"/> の拡張メソッドを定義します。
    /// </summary>
    internal static class TypeExtensions {

        /// <summary>
        /// 指定した型がシーケンスの場合、その要素の型を返します。
        /// </summary>
        /// <param name="target">検索対象の <see cref="System.Type"/>。</param>
        /// <returns>要素の型を示す <see cref="System.Type"/>。</returns>
        public static Type GetEnumerableType(this Type target) {
            if (target == typeof(string)) {
                return null;
            }
            if (target.IsArray == true) {
                return target.GetElementType();
            }
            var typeInfo = target.GetTypeInfo();
            if (typeInfo.IsGenericType == true) {
                foreach (var type in target.GenericTypeArguments) {
                    if (typeof(IEnumerable<>)
                        .MakeGenericType(type)
                        .GetTypeInfo()
                        .IsAssignableFrom(typeInfo) == true) {
                        return type;
                    }
                }
            }
            foreach (var type in typeInfo.ImplementedInterfaces) {
                var result = type.GetEnumerableType();
                if (result != null) {
                    return result;
                }
            }
            if ((typeInfo.BaseType != null) &&
                (typeInfo.BaseType != typeof(object))) {
                return typeInfo.BaseType.GetEnumerableType();
            }
            return null;
        }

        /// <summary>
        /// 指定した型が <see cref="System.Nullable{T}"/> である場合、その要素の型を返します。
        /// </summary>
        /// <param name="target">検索対象の <see cref="System.Type"/>。</param>
        /// <returns>要素の型を示す <see cref="System.Type"/>。</returns>
        public static Type GetNullableType(this Type target) {
            var typeInfo = target.GetTypeInfo();
            if (typeInfo.IsGenericType == true) {
                if (typeInfo.GetGenericTypeDefinition() == typeof(Nullable<>)) {
                    return typeInfo.GenericTypeArguments[0];
                }
            }
            return target;
        }

    }

}
