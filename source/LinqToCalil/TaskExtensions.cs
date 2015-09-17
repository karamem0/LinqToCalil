using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqToCalil {

    /// <summary>
    /// <see cref="T:System.Threading.Tasks.Task`1"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class TaskExtensions {

        /// <summary>
        /// 指定した <see cref="T:System.Threading.Tasks.Task`1"/> の実行を待機して実行結果を返します。
        /// </summary>
        /// <typeparam name="T">実行結果の型。</typeparam>
        /// <param name="target">対象の <see cref="T:System.Threading.Tasks.Task`1"/>。</param>
        /// <returns>実行結果を示す値。</returns>
        public static T Wait<T>(this Task<T> target) {
            var result = default(T);
            target.ContinueWith(task => {
                if (task.Exception == null) {
                    result = task.Result;
                }
            }).Wait();
            return result;
        }

    }

}
