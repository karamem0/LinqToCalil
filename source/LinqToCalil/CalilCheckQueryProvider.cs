using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Net.Http;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// 蔵書検索のクエリを作成または実行します。
    /// </summary>
    internal class CalilCheckQueryProvider : QueryProvider {

        /// <summary>
        /// ベース URI 文字列を取得または設定します。
        /// </summary>
        public string BaseUriString { get; set; }

        /// <summary>
        /// アプリケーション キーを取得します。
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 実行結果の書式を取得します。
        /// </summary>
        public Format Format { get; set; }

        /// <summary>
        /// ポーリングの待機時間を示すミリ秒数を表します。
        /// </summary>
        public int PollingInterval { get; set; }

        /// <summary>
        /// ポーリングが発生したときに実行するメソッドを取得します。
        /// </summary>
        public Func<IEnumerable<CalilCheckResult>, bool> OnPolling { get; set; }

        /// <summary>
        /// 指定した式ツリーを使用して蔵書検索を実行します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>実行結果を示す <see cref="System.Object"/>。</returns>
        public override object Execute(Expression expression) {
            using (var client = new HttpClient()) {
                var builder = new CalilCheckExpressionBuilder() {
                    AppKey = this.AppKey,
                    Format = this.Format,
                };
                var expr = builder.Create(expression);
                while (true) {
                    var xml = client.GetStringAsync(new UriQueryParser(expr).Parse(this.BaseUriString))
                        .ContinueWith(task => XElement.Parse(task.Result, LoadOptions.None))
                        .Wait<XElement>();
                    if ((bool)xml.Element("continue") == true) {
                        if (this.OnPolling == null ||
                            this.OnPolling.Invoke(CalilCheckResult.Parse(xml)) != true) {
                            return null;
                        }
                        Task.Delay(PollingInterval).Wait();
                        expr = builder.Create((string)xml.Element("session"));
                    } else {
                        return CalilCheckResult.Parse(xml);
                    }
                };
            }
        }

    }

}
