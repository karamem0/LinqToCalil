using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Linq;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// 蔵書検索の実行結果を表します。
    /// </summary>
    [DataContract()]
    public class CalilCheckResult : ICalilResult {

        /// <summary>
        /// 指定した XML を <see cref="LinqToCalil.CalilCheckResult"/> のコレクションに変換します。
        /// </summary>
        /// <param name="element">変換対象の <see cref="System.Xml.Linq.XElement"/>。></param>
        /// <returns>変換された <see cref="T:System.Collections.Generic.IEnumerable`1"/>。</returns>
        public static IEnumerable<CalilCheckResult> Parse(XElement element) {
            return element.Descendants("book")
                .SelectMany(x => x.Elements("system"))
                .Select(x => new CalilCheckResult() {
                    Isbn = (string)x.Parent.Attribute("isbn"),
                    CalilUrl = new Uri((string)x.Parent.Attribute("calilurl")),
                    SystemId = (string)x.Attribute("systemid"),
                    Status = (CheckState)Enum.Parse(typeof(CheckState), (string)x.Element("status"), false),
                    ReserveUrl = (string.IsNullOrEmpty((string)x.Element("reserveurl")) == true)
                        ? null
                        : new Uri((string)x.Element("reserveurl")),
                    Libkeys = (x.Element("libkeys") == null)
                        ? null
                        : x.Descendants("libkey").ToDictionary(
                            key => (string)key.Attribute("name"),
                            value => value.Value),
                });
        }

        /// <summary>
        /// 指定した文字列を <see cref="LinqToCalil.CalilCheckResult"/> のコレクションに変換します。
        /// </summary>
        /// <param name="text">変換対象の <see cref="System.String"/>。></param>
        /// <returns>変換された <see cref="T:System.Collections.Generic.IEnumerable`1"/>。</returns>
        public static IEnumerable<CalilCheckResult> Parse(string text) {
            if (string.IsNullOrEmpty(text) == true) {
                return null;
            }
            return CalilCheckResult.Parse(XElement.Parse(text, LoadOptions.None));
        }

        /// <summary>
        /// ISBN を取得します。
        /// </summary>
        [DataMember(Name = "Isbn")]
        public string Isbn { get; internal set; }

        /// <summary>
        /// カーリルの書籍情報ページの URL を取得します。
        /// </summary>
        [DataMember(Name = "CalilUrl")]
        public Uri CalilUrl { get; internal set; }

        /// <summary>
        /// システム ID を取得します。
        /// </summary>
        [DataMember(Name = "SystemId")]
        public string SystemId { get; internal set; }

        /// <summary>
        /// システム ID に対する検索状態を取得します。
        /// </summary>
        [DataMember(Name = "Status")]
        public CheckState Status { get; internal set; }

        /// <summary>
        /// 予約ページの URL を取得します。
        /// </summary>
        [DataMember(Name = "ReserveUrl")]
        public Uri ReserveUrl { get; internal set; }

        /// <summary>
        /// 図書館キーごとの貸出状況を示すキーと値のコレクションを取得します。
        /// </summary>
        [DataMember(Name = "Libkeys")]
        public Dictionary<string, string> Libkeys { get; internal set; }

        /// <summary>
        /// 現在のインスタンスを文字列に変換します。
        /// </summary>
        /// <returns>現在のインスタンスを示す <see cref="System.String"/>。</returns>
        public override string ToString() {
            return this.ToJsonString();
        }

    }

}
