using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// 市町村リストの実行結果を表します。
    /// </summary>
    [DataContract()]
    public class CalilCityListResult : ICalilResult {

        /// <summary>
        /// 指定した JSON 文字列を <see cref="Karamem0.LinqToCalil.CalilCityListResult"/> のコレクションに変換します。
        /// </summary>
        /// <param name="text">変換対象の <see cref="System.String"/>。></param>
        /// <returns>変換された <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        public static IEnumerable<CalilCityListResult> Parse(string text) {
            if (string.IsNullOrEmpty(text) == true) {
                return null;
            }
            var serializer = new JsonSerializer();
            using (var reader = new JsonTextReader(new StringReader(JsonConverter.JsonpToJson(text)))) {
                return ((Dictionary<string, Dictionary<string, string[]>>)
                    serializer.Deserialize(reader, typeof(Dictionary<string, Dictionary<string, string[]>>)))
                        .SelectMany(x => x.Value.SelectMany(
                            y => y.Value.Select(
                                z => new CalilCityListResult() {
                                    Pref = x.Key,
                                    CityIndex = y.Key,
                                    CityName = z,
                                })));
            }
        }

        /// <summary>
        /// 都道府県を取得します。
        /// </summary>
        [DataMember()]
        public string Pref { get; internal set; }

        /// <summary>
        /// 市町村のインデックスを取得します。
        /// </summary>
        [DataMember()]
        public string CityIndex { get; internal set; }

        /// <summary>
        /// 市町村の名前を取得します。
        /// </summary>
        [DataMember()]
        public string CityName { get; internal set; }

        /// <summary>
        /// 現在のインスタンスを文字列に変換します。
        /// </summary>
        /// <returns>現在のインスタンスを示す <see cref="System.String"/>。</returns>
        public override string ToString() {
            return this.ToJsonString();
        }

    }

}
