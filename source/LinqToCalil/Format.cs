using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// API の書式を指定します。
    /// </summary>
    [DataContract()]
    public enum Format {

        /// <summary>
        /// XML 形式を指定します。
        /// </summary>
        [EnumMember(Value="xml")]
        Xml = 0,

        /// <summary>
        /// JSON 形式を指定します。
        /// </summary>
        [EnumMember(Value = "json")]
        Json = 1,

    }

}
