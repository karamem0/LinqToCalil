using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// 列挙体と <see cref="System.Runtime.Serialization.EnumMemberAttribute"/>
    /// 属性で指定された文字列との変換メソッドを提供します。
    /// </summary>
    internal static class EnumConverter {

        /// <summary>
        /// 指定した文字列と一致する <see cref="System.Runtime.Serialization.EnumMemberAttribute"/>
        /// 属性を持つ列挙体の値を返します。
        /// </summary>
        /// <typeparam name="T">変換する列挙体。</typeparam>
        /// <param name="value">
        /// 列挙体の値に適用された <see cref="System.Runtime.Serialization.EnumMemberAttribute"/> 
        /// 属性の文字列を示す <see cref="System.String"/>。
        /// </param>
        /// <returns>変換された列挙体の値。</returns>
        public static T GetEnumValue<T>(string value) {
            var typeInfo = typeof(T).GetTypeInfo();
            if (typeInfo.IsEnum == false) {
                throw new InvalidOperationException();
            }
            foreach (var fi in typeInfo.DeclaredFields) {
                var attr = fi.GetCustomAttribute<EnumMemberAttribute>();
                if (attr != null) {
                    if (value == attr.Value) {
                        return (T)Enum.Parse(typeof(T), fi.Name, false);
                    }
                }
            }
            return default(T);
        }

        /// <summary>
        /// 列挙体の値に適用された <see cref="System.Runtime.Serialization.EnumMemberAttribute"/>
        /// 属性の文字列を返します。
        /// </summary>
        /// <typeparam name="T">変換する列挙体。</typeparam>
        /// <param name="value">変換する列挙体の値。</param>
        /// <returns>
        /// 列挙体の値に適用された <see cref="System.Runtime.Serialization.EnumMemberAttribute"/>
        /// 属性の文字列を示す <see cref="System.String"/>。
        /// </returns>
        public static string GetEnumName<T>(T value) {
            var typeInfo = typeof(T).GetTypeInfo();
            if (typeInfo.IsEnum == false) {
                throw new InvalidOperationException();
            }
            var fi = typeInfo.GetDeclaredField(value.ToString());
            var attr = fi.GetCustomAttribute<EnumMemberAttribute>();
            if (attr != null) {
                return attr.Value;
            }
            return null;
        }

        /// <summary>
        /// 指定した文字列を等価な列挙オブジェクトに変換します。
        /// </summary>
        /// <typeparam name="T">列挙体の型。</typeparam>
        /// <param name="value">列挙定数の名前または数値の文字列形式を示す <see cref="System.String"/>。</param>
        /// <param name="ignoreCase">大文字と小文字の区別を無視する場合は true、それ以外の場合は false。</param>
        /// <returns>列挙オブジェクトの値。</returns>
        public static T Parse<T>(string value, bool ignoreCase) {
            var typeInfo = typeof(T).GetTypeInfo();
            if (typeInfo.IsEnum == false) {
                throw new InvalidOperationException();
            }
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

    }

}
