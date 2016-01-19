using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// <see cref="Karamem0.LinqToCalil.CalilLibraryResult"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class CalilLibraryResultExtensions {

        /// <summary>
        /// <see cref="Karamem0.LinqToCalil.CalilLibraryResult.Geocode"/> プロパティの経度を返します。
        /// </summary>
        /// <param name="target">図書館検索の実行結果を示す <see cref="Karamem0.LinqToCalil.CalilLibraryResult"/>。</param>
        /// <returns>経度を示す null 許容型の <see cref="System.Double"/>。</returns>
        public static double? Longitude(this CalilLibraryResult target) {
            var match = Regex.Match(target.Geocode, "^(\\d+\\.?\\d*),(\\d+\\.?\\d*)$");
            if (match.Success == true) {
                return double.Parse(match.Groups[1].Value);
            }
            return null;
        }

        /// <summary>
        /// <see cref="Karamem0.LinqToCalil.CalilLibraryResult.Geocode"/> プロパティの緯度を返します。
        /// </summary>
        /// <param name="target">図書館検索の実行結果を示す <see cref="Karamem0.LinqToCalil.CalilLibraryResult"/>。</param>
        /// <returns>緯度を示す null 許容型の <see cref="System.Double"/>。</returns>
        public static double? Latitude(this CalilLibraryResult target) {
            var match = Regex.Match(target.Geocode, "^(\\d+\\.?\\d*),(\\d+\\.?\\d*)$");
            if (match.Success == true) {
                return double.Parse(match.Groups[2].Value);
            }
            return null;
        }

    }

}
