using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// ポーリングを監視可能なカーリル API をクエリするためのコンテキストを表します。
    /// </summary>
    /// <typeparam name="TParam">パラメーターの型。<see cref="Karamem0.LinqToCalil.ICalilParameter"/> を実装します。</typeparam>
    /// <typeparam name="TResult">実行結果の型。<see cref="Karamem0.LinqToCalil.ICalilResult"/> を実装します。</typeparam>
    public interface ICalilPollingContext<TParam, TResult> : ICalilQueryableContext<TParam, TResult>
        where TParam : ICalilParameter
        where TResult : ICalilResult {

        /// <summary>
        /// コンテキストをフィルタ処理します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression{TDelegate}"/>。</param>
        /// <returns>フィルタ処理された <see cref="Karamem0.LinqToCalil.ICalilPollingContext{TParam, TResult}"/>。</returns>
        new ICalilPollingContext<TParam, TResult> Where(Expression<Func<TParam, bool>> expression);

        /// <summary>
        /// コンテキストを <see cref="System.Collections.Generic.IEnumerable{T}"/> に変換します。
        /// </summary>
        /// <param name="canPolling">
        /// ポーリングが発生すると実行され、処理を続行するかどうかを評価する 
        /// <see cref="System.Func{T, TResult}"/>。メソッドが false
        /// を返すとポーリングはキャンセルされます。
        /// </param>
        /// <returns>変換された <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        IEnumerable<TResult> AsEnumerable(Func<IEnumerable<TResult>, bool> canPolling);

    }

}
