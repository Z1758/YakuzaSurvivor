

using System;
using System.Linq.Expressions;
using System.Collections.Generic;
public class EnumConvert
{
   
    protected static class Cache<From, To>
    {
        public static readonly Func<From, To> caster = Get();

        private static Func<From, To> Get()
        {
            var parameter = Expression.Parameter(typeof(From));
            var check = Expression.ConvertChecked(parameter, typeof(To));
            return Expression.Lambda<Func<From, To>>(check, parameter).Compile();
        }
    }
}
public class EnumConvert<To> : EnumConvert
{

    public static To Cast<From>(From from)
    {
        return Cache<From, To>.caster(from);
    }

}
