using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// 図書館検索のコンテキストを表します。
    /// </summary>
    public class CalilLibraryContext : ICalilQueryableContext<CalilLibraryParameter, CalilLibraryResult> {

        /// <summary>
        /// ベース URI 文字列を表します。
        /// </summary>
        public readonly string BaseUriString = "http://api.calil.jp/library";

        /// <summary>
        /// 実行結果の書式を表します。
        /// </summary>
        public readonly Format Format = Format.Xml;

        /// <summary>
        /// アプリケーション キーを取得または設定します。
        /// </summary>
        public string AppKey { get; private set; }

        /// <summary>
        /// 現在のインスタンスに関連付けられた式ツリーを取得します。
        /// </summary>
        public Expression Expression { get; private set; }

        /// <summary>
        /// <see cref="LinqToCalil.CalilLibraryContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="appKey">アプリケーション キーを示す <see cref="System.String"/>。</param>
        internal CalilLibraryContext(string appKey) {
            this.AppKey = appKey;
            this.Expression = null;
        }

        /// <summary>
        /// <see cref="LinqToCalil.CalilLibraryContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="appKey">アプリケーション キーを示す <see cref="System.String"/>。</param>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        internal CalilLibraryContext(string appKey, Expression expression) {
            this.AppKey = appKey;
            this.Expression = expression;
        }

        /// <summary>
        /// 現在のインスタンスをフィルタ処理します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="T:System.Linq.Expressions.Expression`1"/>。</param>
        /// <returns>フィルタ処理された <see cref="T:LinqToCalil.ICalilQueryableContext`2"/>。</returns>
        public ICalilQueryableContext<CalilLibraryParameter, CalilLibraryResult>
            Where(Expression<Func<CalilLibraryParameter, bool>> expression) {
            return new CalilLibraryContext(
                this.AppKey,
                (this.Expression == null) ?
                    expression.Body :
                    Expression.AndAlso(this.Expression, expression.Body));
        }

        /// <summary>
        /// 図書館検索の実行結果を反復処理する列挙子を返します。
        /// </summary>
        /// <returns>実行結果を反復処理する <see cref="T:System.Collections.Generic.IEnumerable`1"/>。</returns>
        public IEnumerable<CalilLibraryResult> AsEnumerable() {
            var provider = new CalilLibraryQueryProvider() {
                BaseUriString = BaseUriString,
                Format = Format,
                AppKey = this.AppKey,
            };
            return provider.CreateQuery<CalilLibraryResult>(this.Expression);
        }

    }

}
