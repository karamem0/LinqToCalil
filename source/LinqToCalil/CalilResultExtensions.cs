using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// <see cref="Karamem0.LinqToCalil.ICalilResult"/> インターフェースの拡張メソッドを定義します。
    /// </summary>
    internal static class CalilResultExtensions {

        /// <summary>
        /// <see cref="Karamem0.LinqToCalil.ICalilResult"/> のインスタンスを JSON 形式の文字列に変換します。
        /// </summary>
        /// <param name="target">変換対象の <see cref="Karamem0.LinqToCalil.ICalilResult"/>。</param>
        /// <returns>JSON 形式の <see cref="System.String"/>。</returns>
        public static string ToJsonString(this ICalilResult target) {
            var serializer = new JsonSerializer();
            using (var stream = new MemoryStream()) {
                using (var writer = new JsonTextWriter(new StreamWriter(stream))) {
                    serializer.Serialize(writer, target);
                }
                var buffer = stream.ToArray();
                return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }
        }

    }

}
