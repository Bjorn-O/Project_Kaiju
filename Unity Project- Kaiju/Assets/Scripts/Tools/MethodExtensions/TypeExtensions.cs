using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Toolbox.MethodExtensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Checks if the type is a subclass of the given type.
        /// </summary>
        /// <param name="myType"></param>
        /// <param name="baseType"></param>
        /// <returns></returns>
        public static bool IsDerivedFrom(this Type myType, Type baseType)
        {
            var dict = myType.GetInheritedClassesDict();
            return dict.All(keyValuePair => keyValuePair.Value != myType);
        }

        /// <summary>
        /// Get list of all inherited classes 
        /// </summary>
        /// <param name="myType"></param>
        /// <returns></returns>
        public static List<Type> GetInheritedClassesList(this Type myType)
        {
            return Assembly.GetAssembly(myType).GetTypes()
                .Where(theType => theType.IsClass && !theType.IsAbstract && theType.IsSubclassOf(myType)).ToList();
        }

        /// <summary>
        /// Get list of all inherited classes and the base class
        /// </summary>
        /// <param name="myType"></param>
        /// <returns></returns>
        public static List<Type> GetInheritedClassesAndBaseClassList(this Type myType)
        {
            List<Type> list = new List<Type>();
            list.Add(myType);
            list.AddList(GetInheritedClassesList(myType));
            return list;
        }

        /// <summary>
        /// Get Dictonary of all inherited classes in the form (namespace + classname, type) 
        /// </summary>
        /// <param name="myType"></param>
        /// <returns>Dictonary (namespace + name, type)</returns>
        public static Dictionary<string, Type> GetInheritedClassesDict(this Type myType)
        {
            Dictionary<string, Type> typeDict = new Dictionary<string, Type>();
            List<Type> types = GetInheritedClassesList(myType);

            foreach (var type in types)
            {
                typeDict.Add(type.ToString(), type);
            }

            return typeDict;
        }

        /// <summary>
        /// Get Dictonary of all inherited classes and the base class in the form (namespace + classname, type) 
        /// </summary>
        /// <param name="myType"></param>
        /// <returns>Dictonary (namespace + name, type)</returns>
        public static Dictionary<string, Type> GetInheritedClassesAndBaseClassDict(this Type myType)
        {
            Dictionary<string, Type> typeDict = new Dictionary<string, Type>();
            List<Type> types = GetInheritedClassesAndBaseClassList(myType);

            foreach (var type in types)
            {
                typeDict.Add(type.ToString(), type);
            }

            return typeDict;
        }

        /// <summary>
        /// Get derrived classes from current class
        /// </summary>
        /// <param name="myType"></param>
        /// <returns></returns>
        public static List<Type> GetDerrivedClasses(this Type myType)
        {
            return (
                from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                from assemblyType in domainAssembly.GetTypes()
                where myType.IsAssignableFrom(assemblyType) && assemblyType != myType
                select assemblyType).ToList();
        }

        /// <summary>
        /// Returns a list of all types that inherent from the given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<string> GetInheritedClassesNames(this Type type)
        {
            List<Type> types = type.GetInheritedClassesList();
            List<string> names = new List<string>();
            foreach (var t in types)
            {
                names.Add(t.Name);
            }

            return names;
        }

        /// <summary>Checks if the type is serializable by Unity.</summary>
        /// <param name="type">The type to check.</param>
        /// <returns><see langword="true"/> if the type can be serialized by Unity.</returns>
        public static bool IsUnitySerializable(this Type type)
        {
            bool IsSystemType(Type typeToCheck) => typeToCheck.Namespace?.StartsWith("System") == true;

            bool IsCustomSerializableType(Type typeToCheck) =>
                typeToCheck.IsSerializable && typeToCheck.GetSerializedFields().Any() &&
                !IsSystemType(typeToCheck);

            if (type.IsAbstract) // static classes and interfaces are considered abstract too.
                return false;

            if (IsCustomSerializableType(type))
                return true;

            if (type.InheritsFrom(typeof(UnityEngine.Object)) && !type.IsGenericTypeDefinition)
                return true;

            if (type.IsEnum)
                return true;

            return UnitySerializablePrimitiveTypes.Contains(type) || UnitySerializableBuiltinTypes.Contains(type);
        }

        /// <summary>
        /// Collects all the serializable fields of a class: private ones with SerializeField attribute and public ones.
        /// </summary>
        /// <param name="type">Class type to collect the fields from.</param>
        /// <returns>Collection of the serializable fields of a class.</returns>
        /// <example><code>
        /// var fields = objectType.GetSerializedFields();
        /// foreach (var field in fields)
        /// {
        ///     string fieldLabel = ObjectNames.NicifyVariableName(field.Name);
        ///     object fieldValue = field.GetValue(serializedObject);
        ///     object newValue = DrawField(fieldLabel, fieldValue);
        ///     field.SetValue(serializedObject, newValue);
        /// }
        /// </code></example>
        private static IEnumerable<FieldInfo> GetSerializedFields(this Type type)
        {
            const BindingFlags instanceFilter = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            var instanceFields = type.GetFields(instanceFilter);
            return instanceFields.Where(field => field.IsPublic || field.GetCustomAttribute<SerializeField>() != null);
        }

        /// <summary>
        /// Checks whether the type is derivative of a generic class without specifying its type parameter.
        /// </summary>
        /// <param name="typeToCheck">The type to check.</param>
        /// <param name="generic">The generic class without type parameter.</param>
        /// <returns>True if the type is subclass of the generic class.</returns>
        /// <example><code>
        /// class Base&lt;T> { }
        /// class IntDerivative : Base&lt;int> { }
        /// class StringDerivative : Base&lt;string> { }
        ///
        /// bool intIsSubclass = typeof(IntDerivative).IsSubclassOfRawGeneric(typeof(Base&lt;>)); // true
        /// bool stringIsSubclass = typeof(StringDerivative).IsSubclassOfRawGeneric(typeof(Base&lt;>)); // true
        /// </code></example>
        private static bool IsSubclassOfRawGeneric(this Type typeToCheck, Type generic)
        {
            while (typeToCheck != null && typeToCheck != typeof(object))
            {
                Type cur = typeToCheck.IsGenericType ? typeToCheck.GetGenericTypeDefinition() : typeToCheck;

                if (generic == cur) return true;

                typeToCheck = typeToCheck.BaseType;
            }

            return false;
        }

        /// <summary>
        /// Checks whether the type inherits from the base type.
        /// </summary>
        /// <param name="typeToCheck">The type to check.</param>
        /// <param name="baseType">
        /// The base type to check inheritance from. It can be a generic type without the type parameter.
        /// </param>
        /// <returns>Whether <paramref name="typeToCheck"/>> inherits <paramref name="baseType"/>.</returns>
        /// <example><code>
        /// class Base&lt;T> { }
        /// class IntDerivative : Base&lt;int> { }
        ///
        /// bool isAssignableWithTypeParam = typeof(typeof(Base&lt;int>).IsAssignableFrom(IntDerivative)); // true
        /// bool isAssignableWithoutTypeParam = typeof(typeof(Base&lt;>)).IsAssignableFrom(IntDerivative); // false
        /// bool inherits = typeof(IntDerivative).Inherits(typeof(Base&lt;>)); // true
        /// </code></example>
        private static bool InheritsFrom(this Type typeToCheck, Type baseType)
        {
            bool subClassOfRawGeneric = false;
            if (baseType.IsGenericType)
                subClassOfRawGeneric = typeToCheck.IsSubclassOfRawGeneric(baseType);

            return baseType.IsAssignableFrom(typeToCheck) || subClassOfRawGeneric;
        }
        
        
        /// <summary>
        /// A hashset of all the types that can be serialized by Unity.
        /// </summary>
        private static readonly HashSet<Type> UnitySerializablePrimitiveTypes = new HashSet<Type>
        {
            typeof(bool), typeof(byte), typeof(sbyte), typeof(char), typeof(double), typeof(float), typeof(int),
            typeof(uint), typeof(long), typeof(ulong), typeof(short), typeof(ushort), typeof(string)
        };

        /// <summary>
        /// A hashset of all the types that can be serialized by Unity.
        /// </summary>
        private static readonly HashSet<Type> UnitySerializableBuiltinTypes = new HashSet<Type>
        {
            typeof(Vector2), typeof(Vector3), typeof(Vector4), typeof(Rect), typeof(Quaternion), typeof(Matrix4x4),
            typeof(Color), typeof(Color32), typeof(LayerMask), typeof(AnimationCurve), typeof(Gradient),
            typeof(RectOffset), typeof(GUIStyle)
        };
    }
}