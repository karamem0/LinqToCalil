using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// 蔵書検索の結果を指定します。
    /// </summary>
    [DataContract()]
    public enum CheckState {

        /// <summary>
        /// 取得が完了したことを示します。
        /// </summary>
        [EnumMember(Value = "OK")]
        OK = 0,

        /// <summary>
        /// キャッシュを使用して取得が完了したことを示します。
        /// </summary>
        [EnumMember(Value = "Cache")]
        Cache = 1,

        /// <summary>
        /// 実行中であることを示します。
        /// </summary>
        [EnumMember(Value = "Running")]
        Running = 2,

        /// <summary>
        /// エラーが発生したことを示します。
        /// </summary>
        [EnumMember(Value = "Error")]
        Error = 3,

    }

}
