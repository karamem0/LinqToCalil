using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// 図書館検索の実行結果を表します。
    /// </summary>
    [DataContract()]
    public class CalilLibraryResult : ICalilResult {

        /// <summary>
        /// 指定した XML を <see cref="Karamem0.LinqToCalil.CalilLibraryResult"/> のコレクションに変換します。
        /// </summary>
        /// <param name="text">変換対象の <see cref="System.String"/>。></param>
        /// <returns>変換された <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        public static IEnumerable<CalilLibraryResult> Parse(string text) {
            if (string.IsNullOrEmpty(text) == true) {
                return null;
            }
            return XElement.Parse(text, LoadOptions.None).Elements("Library")
                .Select(x => new CalilLibraryResult() {
                    SystemId = (string)x.Element("systemid"),
                    LibKey = (string)x.Element("libkey"),
                    ShortName = (string)x.Element("short"),
                    FormalName = (string)x.Element("formal"),
                    Address = (string)x.Element("address"),
                    Post = (string)x.Element("post"),
                    Tel = (string)x.Element("tel"),
                    Pref = (string)x.Element("pref"),
                    City = (string)x.Element("city"),
                    SiteUri = new Uri((string)x.Element("url_pc"), UriKind.Absolute),
                    Geocode = (string)x.Element("geocode"),
                    Category = EnumConverter.GetEnumValue<Category>((string)x.Element("category")),
                    Distance = (x.Element("distance") != null)
                        ? (double)x.Element("distance")
                        : (double?)null,
                });
        }

        /// <summary>
        /// システム ID を取得または設定します。
        /// </summary>
        [DataMember(Name = "SystemId")]
        public string SystemId { get; set; }

        /// <summary>
        /// システムごとの図書館キーを取得または設定します。
        /// </summary>
        [DataMember(Name = "LibKey")]
        public string LibKey { get; set; }

        /// <summary>
        /// 正式な名前を取得または設定します。
        /// </summary>
        [DataMember(Name = "FormalName")]
        public string FormalName { get; set; }

        /// <summary>
        /// 短い形式の名前を取得または設定します。
        /// </summary>
        [DataMember(Name = "ShortName")]
        public string ShortName { get; set; }

        /// <summary>
        /// 住所を取得または設定します。
        /// </summary>
        [DataMember(Name = "Address")]
        public string Address { get; set; }

        /// <summary>
        /// 都道府県を取得または設定します。
        /// </summary>
        [DataMember(Name = "Pref")]
        public string Pref { get; set; }

        /// <summary>
        /// 市区町村を取得または設定します。
        /// </summary>
        [DataMember(Name = "City")]
        public string City { get; set; }

        /// <summary>
        /// 郵便番号を取得または設定します。
        /// </summary>
        [DataMember(Name = "Post")]
        public string Post { get; set; }

        /// <summary>
        /// 電話番号を取得または設定します。
        /// </summary>
        [DataMember(Name = "Tel")]
        public string Tel { get; set; }

        /// <summary>
        /// サイトの URI を取得または設定します。
        /// </summary>
        [DataMember(Name = "SiteUri")]
        public Uri SiteUri { get; set; }

        /// <summary>
        /// 位置情報を取得または設定します。
        /// </summary>
        [DataMember(Name = "Geocode")]
        public string Geocode { get; set; }

        /// <summary>
        /// カテゴリーを取得または設定します。
        /// </summary>
        [DataMember(Name = "Category")]
        public Category Category { get; set; }

        /// <summary>
        /// <see cref="Karamem0.LinqToCalil.CalilLibraryParameter.Geocode"/>
        /// で指定された地点からの距離を取得または設定します。
        /// </summary>
        [DataMember(Name = "Distance")]
        public double? Distance { get; set; }

        /// <summary>
        /// 現在のインスタンスを文字列に変換します。
        /// </summary>
        /// <returns>現在のインスタンスを示す <see cref="System.String"/>。</returns>
        public override string ToString() {
            return this.ToJsonString();
        }

    }

}
