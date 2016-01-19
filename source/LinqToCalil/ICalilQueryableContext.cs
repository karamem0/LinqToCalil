using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// カーリル API をクエリするためのコンテキストを表します。
    /// </summary>
    /// <typeparam name="TParam">パラメータの型。</typeparam>
    /// <typeparam name="TResult">実行結果の型。</typeparam>
    public interface ICalilQueryableContext<TParam, TResult> : ICalilContext<TResult>
        where TParam : ICalilParameter
        where TResult : ICalilResult {

        /// <summary>
        /// コンテキストをフィルタ処理します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression{T}"/>。</param>
        /// <returns>フィルタ処理された <see cref="Karamem0.LinqToCalil.ICalilQueryableContext{TParam, TResult}"/>。</returns>
        ICalilQueryableContext<TParam, TResult> Where(Expression<Func<TParam, bool>> expression);

    }

}
