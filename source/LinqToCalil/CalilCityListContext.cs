using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// 市町村リストのコンテキストを表します。
    /// </summary>
    public class CalilCityListContext : ICalilContext<CalilCityListResult> {

        /// <summary>
        /// ベース URI 文字列を表します。
        /// </summary>
        public readonly string BaseUriString = "http://calil.jp/city_list";

        /// <summary>
        /// 市町村リストの実行結果を反復処理する列挙子を返します。
        /// </summary>
        /// <returns>実行結果を反復処理する <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        public IEnumerable<CalilCityListResult> AsEnumerable() {
            using (var client = new HttpClient()) {
                return client.GetStringAsync(BaseUriString)
                    .ContinueWith(task => CalilCityListResult.Parse(task.Result))
                    .Wait<IEnumerable<CalilCityListResult>>();
            }
        }

    }

}
