using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// カーリル API のコンテキストを表します。
    /// </summary>
    /// <typeparam name="TResult">実行結果の型。<see cref="Karamem0.LinqToCalil.ICalilResult"/> を実装します。</typeparam>
    public interface ICalilContext<TResult>
        where TResult : ICalilResult {

        /// <summary>
        /// コンテキストを <see cref="System.Collections.Generic.IEnumerable{T}"/> に変換します。
        /// </summary>
        /// <returns>変換された <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        IEnumerable<TResult> AsEnumerable();

    }

}
