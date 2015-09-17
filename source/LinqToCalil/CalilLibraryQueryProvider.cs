using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinqToCalil {

    /// <summary>
    /// 図書館検索のクエリを作成または実行します。
    /// </summary>
    internal class CalilLibraryQueryProvider : QueryProvider {

        /// <summary>
        /// ベース URI 文字列を取得または設定します。
        /// </summary>
        public string BaseUriString { get; set; }

        /// <summary>
        /// アプリケーション キーを取得または設定します。
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 実行結果の書式を取得または設定します。
        /// </summary>
        public Format Format { get; set; }

        /// <summary>
        /// 指定した式ツリーを使用して図書館検索を実行します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>実行結果を示す <see cref="System.Object"/>。</returns>
        public override object Execute(Expression expression) {
            using (var client = new HttpClient()) {
                var builder = new CalilLibraryExpressionBuilder() {
                    AppKey = this.AppKey,
                    Format = this.Format,
                };
                var expr = builder.Create(expression);
                var uri = new UriQueryParser(expr).Parse(this.BaseUriString);
                return client.GetStringAsync(uri)
                    .ContinueWith(task => CalilLibraryResult.Parse(task.Result))
                    .Wait<IEnumerable<CalilLibraryResult>>();
            }
        }

    }

}
