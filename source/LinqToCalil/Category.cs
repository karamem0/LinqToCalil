using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// 図書館のカテゴリを指定します。
    /// </summary>
    [DataContract()]
    public enum Category {

        /// <summary>
        /// カテゴリは指定されていません。
        /// </summary>
        [EnumMember()]
        None = 0,

        /// <summary>
        /// 図書室または公民館を示します。
        /// </summary>
        [EnumMember(Value = "SMALL")]
        Small = 1,

        /// <summary>
        /// 地域図書館を示します。
        /// </summary>
        [EnumMember(Value = "MEDIUM")]
        Medium = 2,

        /// <summary>
        /// 広域図書館を示します。
        /// </summary>
        [EnumMember(Value = "LARGE")]
        Large = 3,

        /// <summary>
        /// 大学図書館を示します。
        /// </summary>
        [EnumMember(Value = "UNIV")]
        University = 4,

        /// <summary>
        /// 専門図書館を示します。
        /// </summary>
        [EnumMember(Value = "SPECIAL")]
        Special = 5,

        /// <summary>
        /// 移動図書館を示します。
        /// </summary>
        [EnumMember(Value = "BM")]
        Bookmobile = 6,

    }

}
