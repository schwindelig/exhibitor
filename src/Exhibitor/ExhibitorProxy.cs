using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Exhibitor
{
    public class ExhibitorProxy<T>
    {
        private readonly T instance;

        private readonly IEnumerable<FieldInfo> fields;

        private readonly IEnumerable<PropertyInfo> properties;

        private readonly IEnumerable<MethodInfo> methods;


        internal ExhibitorProxy(
            T instance,
            IEnumerable<FieldInfo> fields,
            IEnumerable<PropertyInfo> properties,
            IEnumerable<MethodInfo> methods)
        {
            this.instance = instance;
            this.fields = fields;
            this.properties = properties;
            this.methods = methods;
        }


        public TField GetFieldValue<TField>(string fieldName)
        {
            return (TField)this.GetFieldInfo(fieldName).GetValue(this.instance);
        }

        public void SetFieldValue<TField>(string fieldName, TField value)
        {
            this.GetFieldInfo(fieldName).SetValue(this.instance, value);
        }

        public TProperty GetPropertyValue<TProperty>(string propertyName)
        {
            return (TProperty)GetPropertyInfo(propertyName).GetValue(this.instance);
        }

        public void SetPropertyValue<TProperty>(string propertyName, TProperty value)
        {
            this.GetPropertyInfo(propertyName).SetValue(this.instance, value);
        }

        public void InvokeMethod(string methodName)
        {
            this.InvokeMethod(methodName, null);
        }

        public void InvokeMethod(string methodName, object[] parameters)
        {
            this.InvokeMethod<dynamic>(methodName, parameters);
        }

        public TReturn InvokeMethod<TReturn>(string methodName)
        {
            return this.InvokeMethod<TReturn>(methodName, null);
        }

        public TReturn InvokeMethod<TReturn>(string methodName, object[] parameters)
        {
            parameters = parameters ?? new object[0];

            return (TReturn)this.GetMethodInfo(methodName, parameters.Select(p => p.GetType()))
                .Invoke(this.instance, parameters);
        }

        private FieldInfo GetFieldInfo(string fieldName)
        {
            return this.fields.FirstOrDefault(f => f.Name == fieldName);
        }

        private PropertyInfo GetPropertyInfo(string propertyName)
        {
            return this.properties.FirstOrDefault(p => p.Name == propertyName);
        }

        private MethodInfo GetMethodInfo(string methodName, IEnumerable<Type> parameterTypes)
        {
            return this.methods.FirstOrDefault(m => 
                m.Name == methodName &&
                m.GetParameters()
                    .Select(p => p.ParameterType).SequenceEqual(parameterTypes));
        }
    }
}
