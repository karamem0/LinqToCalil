using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// URI クエリに変換可能なプロパティであることを指定します。
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    internal class UriQueryAttribute : Attribute {

        /// <summary>
        /// キー文字列を取得します。
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// 書式文字列を取得します。
        /// </summary>
        public string Format { get; private set; }

        /// <summary>
        /// <see cref="LinqToCalil.UriQueryAttribute"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="key">キー文字列を示す <see cref="System.String"/>。</param>
        public UriQueryAttribute(string key) {
            this.Key = key;
        }

        /// <summary>
        /// <see cref="LinqToCalil.UriQueryAttribute"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="key">キー文字列を示す <see cref="System.String"/>。</param>
        /// <param name="format">書式文字列を <see cref="System.String"/>。</param>
        public UriQueryAttribute(string key, string format) {
            this.Key = key;
            this.Format = format;
        }

    }

}
