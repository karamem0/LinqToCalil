using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// 蔵書検索のコンテキストを表します。
    /// </summary>
    public class CalilCheckContext : ICalilPollingContext<CalilCheckParameter, CalilCheckResult> {

        /// <summary>
        /// ベース URI 文字列を表します。
        /// </summary>
        public readonly string BaseUriString = "http://api.calil.jp/check";

        /// <summary>
        /// 実行結果の書式を表します。
        /// </summary>
        public readonly Format Format = Format.Xml;

        /// <summary>
        /// ポーリングの待機時間を示すミリ秒数を表します。
        /// </summary>
        public readonly int PollingInterval = 2000;

        /// <summary>
        /// アプリケーション キーを取得または設定します。
        /// </summary>
        public string AppKey { get; private set; }

        /// <summary>
        /// 現在のインスタンスに関連付けられた式ツリーを取得します。
        /// </summary>
        public Expression Expression { get; private set; }

        /// <summary>
        /// ポーリングが発生したときに実行するメソッドを取得します。
        /// </summary>
        public Action<IEnumerable<CalilCheckResult>> OnPolling { get; private set; }

        /// <summary>
        /// <see cref="Karamem0.LinqToCalil.CalilCheckContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="appKey">アプリケーション キーを示す <see cref="System.String"/>。</param>
        internal CalilCheckContext(string appKey) {
            this.AppKey = appKey;
            this.Expression = null;
        }

        /// <summary>
        /// <see cref="Karamem0.LinqToCalil.CalilCheckContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="appKey">アプリケーション キーを示す <see cref="System.String"/>。</param>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        internal CalilCheckContext(string appKey, Expression expression) {
            this.AppKey = appKey;
            this.Expression = expression;
        }

        /// <summary>
        /// <see cref="Karamem0.LinqToCalil.CalilCheckContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="appKey">アプリケーション キーを示す <see cref="System.String"/>。</param>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <param name="onPolling">ポーリングが発生したときに実行するメソッドを示す <see cref="System.Action{T}"/>。</param>
        internal CalilCheckContext(string appKey, Expression expression, Action<IEnumerable<CalilCheckResult>> onPolling) {
            this.Expression = expression;
            this.AppKey = appKey;
            this.OnPolling = onPolling;
        }

        /// <summary>
        /// 現在のインスタンスをフィルタ処理します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression{T}"/>。</param>
        /// <returns>フィルタ処理された <see cref="Karamem0.LinqToCalil.ICalilQueryableContext{TParam, TResult}"/>。</returns>
        ICalilQueryableContext<CalilCheckParameter, CalilCheckResult>
            ICalilQueryableContext<CalilCheckParameter, CalilCheckResult>.Where(Expression<Func<CalilCheckParameter, bool>> expression) {
            return this.Where(expression);
        }

        /// <summary>
        /// 現在のインスタンスをフィルタ処理します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression{T}"/>。</param>
        /// <returns>フィルタ処理された <see cref="Karamem0.LinqToCalil.ICalilPollingContext{TParam, TResult}"/>。</returns>
        public ICalilPollingContext<CalilCheckParameter, CalilCheckResult>
            Where(Expression<Func<CalilCheckParameter, bool>> expression) {
            return new CalilCheckContext(
                this.AppKey,
                (this.Expression == null) ?
                    expression.Body :
                    Expression.AndAlso(this.Expression, expression.Body));
        }

        /// <summary>
        /// 蔵書検索の実行結果を反復処理する列挙子を返します。
        /// </summary>
        /// <returns>実行結果を反復処理する <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        public IEnumerable<CalilCheckResult> AsEnumerable() {
            var provider = new CalilCheckQueryProvider() {
                BaseUriString = BaseUriString,
                Format = Format,
                PollingInterval = PollingInterval,
                AppKey = this.AppKey,
                CanPolling = null,
            };
            return provider.CreateQuery<CalilCheckResult>(this.Expression);
        }

        /// <summary>
        /// 蔵書検索の実行結果を反復処理する列挙子を返します。
        /// </summary>
        /// <param name="canPolling">ポーリングが発生したときに実行される <see cref="System.Func{T, TResult}"/>。</param>
        /// <returns>実行結果を反復処理する <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        public IEnumerable<CalilCheckResult> AsEnumerable(Func<IEnumerable<CalilCheckResult>, bool> canPolling) {
            var provider = new CalilCheckQueryProvider() {
                BaseUriString = BaseUriString,
                Format = Format,
                PollingInterval = PollingInterval,
                AppKey = this.AppKey,
                CanPolling = canPolling,
            };
            return provider.CreateQuery<CalilCheckResult>(this.Expression);
        }

    }

}
