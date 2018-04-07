using System;
using System.Text;
using System.Threading.Tasks;

namespace AAS.Common
{
    public static class TypeExtensions
    {
        public static bool IsAssignableTo(this Type @this, Type other)
        {
            return other.IsAssignableFrom(@this);
        }

        public static bool IsAssignableTo<T>(this Type @this)
        {
            return @this.IsAssignableTo(typeof(T));
        }

        public static bool CanBeInstantiated(this Type @this)
        {
            return @this.IsClass && !@this.IsAbstract;
        }

        public static bool Is<TOther>(this Type @this)
        {
            return @this == typeof(TOther);
        }

        public static bool IsNot<TOther>(this Type @this)
        {
            return !@this.Is<TOther>();
        }
    }
}
