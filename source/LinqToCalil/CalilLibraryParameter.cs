using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// 図書館検索のフィルタを指定します。
    /// </summary>
    [DataContract()]
    public class CalilLibraryParameter : ICalilParameter {

        /// <summary>
        /// アプリケーション キーを取得または設定します。
        /// </summary>
        [UriQuery("appkey")]
        public string AppKey { get; set; }

        /// <summary>
        /// 実行結果の書式を取得または設定します。
        /// </summary>
        [UriQuery("format")]
        public Format? Format { get; set; }

        /// <summary>
        /// システム ID を取得または設定します。
        /// </summary>
        [UriQuery("systemid")]
        public string SystemId { get; set; }

        /// <summary>
        /// 都道府県を取得または設定します。
        /// </summary>
        [UriQuery("pref")]
        public string Pref { get; set; }

        /// <summary>
        /// 市区町村を取得または設定します。
        /// </summary>
        [UriQuery("city")]
        public string City { get; set; }

        /// <summary>
        /// 位置情報を取得または設定します。
        /// </summary>
        [UriQuery("geocode")]
        public string Geocode { get; set; }

    }

}
