using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// <see cref="System.Threading.Tasks.Task{TResult}"/> クラスの拡張メソッドを定義します。
    /// </summary>
    internal static class TaskExtensions {

        /// <summary>
        /// 指定した <see cref="System.Threading.Tasks.Task{TResult}"/> の実行を待機して実行結果を返します。
        /// </summary>
        /// <typeparam name="T">実行結果の型。</typeparam>
        /// <param name="target">対象の <see cref="System.Threading.Tasks.Task{TResult}"/>。</param>
        /// <returns>実行結果を示す値。</returns>
        public static T Wait<T>(this Task<T> target) {
            var result = default(T);
            target.ContinueWith(task => {
                if (task.IsCanceled != true && task.IsFaulted != true) {
                    result = task.Result;
                }
            }).Wait();
            return result;
        }

    }

}
