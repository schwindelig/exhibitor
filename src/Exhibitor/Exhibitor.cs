using System.Collections.Generic;
using System.Reflection;

namespace ExhibitorLib
{
    public static class Exhibitor
    {
        private const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static;


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
            return typeInfo.GetFields(BINDING_FLAGS);
        }

        private static IEnumerable<PropertyInfo> LoadProperties(TypeInfo typeInfo)
        {
            return typeInfo.GetProperties(BINDING_FLAGS);
        }

        private static IEnumerable<MethodInfo> LoadMethods(TypeInfo typeInfo)
        {
            return typeInfo.GetMethods(BINDING_FLAGS);
        }
    }
}
