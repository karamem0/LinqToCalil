using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Karamem0.LinqToCalil {

    /// <summary>
    /// 蔵書検索の内部パラメーターを式ツリーに変換します。
    /// </summary>
    public class CalilCheckExpressionBuilder : CalilExpressionBuilder<CalilCheckParameter> {

        /// <summary>
        /// アプリケーション キーを取得します。
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 実行結果の書式を取得します。
        /// </summary>
        public Format Format { get; set; }


        /// <summary>
        /// 内部的に使用されるパラメーターを指定する式ツリーを作成します。
        /// </summary>
        /// <param name="expression">式ツリーを示す <see cref="System.Linq.Expressions.Expression"/>。</param>
        /// <returns>作成された <see cref="System.Linq.Expressions.Expression"/>。</returns>
        public override Expression Create(Expression expression) {
            var typeInfo = this.ElementType.GetTypeInfo();
            var instance = Activator.CreateInstance(this.ElementType);
            return Expression.AndAlso(
                expression,
                Expression.AndAlso(
                    Expression.Equal(
                        Expression.Property(
                            Expression.Constant(instance), typeInfo.GetDeclaredProperty("AppKey")),
                        Expression.Constant(this.AppKey)),
                    Expression.Equal(
                        Expression.Property(
                            Expression.Constant(instance), typeInfo.GetDeclaredProperty("Format")),
                            Expression.Convert(
                                Expression.Constant(this.Format), typeof(Nullable<Format>)))));
        }

        /// <summary>
        /// 内部的に使用されるパラメーターを指定する式ツリーを作成します。
        /// </summary>
        /// <param name="session">セッションを示す <see cref="System.String"/>。</param>
        /// <returns>作成された <see cref="System.Linq.Expressions.Expression"/>。</returns>
        public Expression Create(string session) {
            var typeInfo = this.ElementType.GetTypeInfo();
            var instance = Activator.CreateInstance(this.ElementType);
            return Expression.AndAlso(
                Expression.Equal(
                    Expression.Property(
                        Expression.Constant(instance), typeInfo.GetDeclaredProperty("Session")),
                    Expression.Constant(session)),
                Expression.AndAlso(
                    Expression.Equal(
                        Expression.Property(
                            Expression.Constant(instance), typeInfo.GetDeclaredProperty("AppKey")),
                        Expression.Constant(this.AppKey)),
                    Expression.Equal(
                        Expression.Property(
                            Expression.Constant(instance), typeInfo.GetDeclaredProperty("Format")),
                            Expression.Convert(
                                Expression.Constant(this.Format), typeof(Nullable<Format>)))));
        }

    }

}
