using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// カーリル API のコンテキストを表します。
    /// </summary>
    /// <typeparam name="TResult">実行結果の型。<see cref="LinqToCalil.ICalilResult"/> を実装します。</typeparam>
    public interface ICalilContext<TResult>
        where TResult : ICalilResult {

        /// <summary>
        /// コンテキストを <see cref="T:System.Collections.Generic.IEnumerable`1"/> に変換します。
        /// </summary>
        /// <returns>変換された <see cref="T:System.Collections.Generic.IEnumerable`1"/>。</returns>
        IEnumerable<TResult> AsEnumerable();

    }

}
