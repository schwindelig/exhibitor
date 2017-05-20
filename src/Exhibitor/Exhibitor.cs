using System.Collections.Generic;
using System.Reflection;

namespace Exhibitor
{
    public static class Exhibitor
    {
        public static ExhibitorProxy<T> Load<T>()
        {
            return Load(default(T));
        }

        public static ExhibitorProxy<T> Load<T>(T instance)
        {
            var typeInfo = typeof(T).GetTypeInfo();

            return new ExhibitorProxy<T>(
                instance,
                LoadFields(typeInfo),
                LoadProperties(typeInfo),
                LoadMethods(typeInfo));
        }

        private static IEnumerable<FieldInfo> LoadFields(TypeInfo typeInfo)
        {
            return typeInfo.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        private static IEnumerable<PropertyInfo> LoadProperties(TypeInfo typeInfo)
        {
            return typeInfo.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }

        private static IEnumerable<MethodInfo> LoadMethods(TypeInfo typeInfo)
        {
            return typeInfo.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        }
    }
}
