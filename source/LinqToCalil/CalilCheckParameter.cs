﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// 蔵書検索のフィルタを指定します。
    /// </summary>
    [DataContract()]
    public class CalilCheckParameter : ICalilParameter {

        /// <summary>
        /// アプリケーション キーを取得または設定します。
        /// </summary>
        [DataMember()]
        [UriQuery("appkey")]
        public string AppKey { get; set; }

        /// <summary>
        /// 実行結果の書式を取得または設定します。
        /// </summary>
        [DataMember()]
        [UriQuery("format")]
        public Format? Format { get; set; }

        /// <summary>
        /// セッション ID を取得または設定します。
        /// </summary>
        [DataMember()]
        [UriQuery("session")]
        public string Session { get; set; }

        /// <summary>
        /// ISBN を複数指定するカンマ区切りの文字列を取得または設定します。
        /// </summary>
        [DataMember()]
        [UriQuery("isbn")]
        public string Isbn { get; set; }

        /// <summary>
        /// システム ID を複数指定するカンマ区切りの文字列を取得または設定します。
        /// </summary>
        [DataMember()]
        [UriQuery("systemid")]
        public string SystemId { get; set; }

    }

}
