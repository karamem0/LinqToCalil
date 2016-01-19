using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// JSON 形式の文字列の変換メソッドを提供します。
    /// </summary>
    internal static class JsonConverter {

        /// <summary>
        /// JSONP 形式を JSON 形式に変換します。
        /// </summary>
        /// <param name="value">JSONP 形式の <see cref="System.String"/>。</param>
        /// <returns>JSON 形式の <see cref="System.String"/>。</returns>
        public static string JsonpToJson(string value) {
            var match = Regex.Match(value, @"^.+\((.+)\);?$");
            if (match.Success) {
                return match.Groups[1].Value;
            }
            return value;
        }

    }

}
