using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// <see cref="Karamem0.LinqToCalil.ICalilParameter"/> インターフェースの拡張メソッドを定義します。
    /// </summary>
    internal static class CalilParameterExtensions {

        /// <summary>
        /// <see cref="Karamem0.LinqToCalil.ICalilParameter"/> から式ツリーを作成します。
        /// </summary>
        /// <param name="target">変換元の <see cref="Karamem0.LinqToCalil.ICalilParameter"/>。</param>
        /// <returns>作成された <see cref="System.Linq.Expressions.Expression"/>。</returns>
        public static Expression CreateExpression(this ICalilParameter target) {
            return target.GetType().GetTypeInfo().DeclaredProperties
                .Where(x => x.GetValue(target, null) != null)
                .Select(x => Expression.Equal(
                    Expression.Property(Expression.Constant(target), x),
                    Expression.Constant(x.GetValue(target, null))))
                .Aggregate((x, y) => Expression.AndAlso(x, y));
        }

    }

}
