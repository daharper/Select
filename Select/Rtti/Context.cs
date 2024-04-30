using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Select.Rtti
{
    /// <summary>
    /// A basic application context responsible for identifying and managing beans.
    /// Provides minimal functionally, will be expanded if required.
    /// 
    /// A bean can be registered by its type or name.
    ///
    /// When resolving constructor arguments, if the argument name can be identified
    /// as a bean name, then that is used for resolution; otherwise the type is used.
    ///
    /// Use in combination with initializers for specialization. For example, in an
    /// initializer you can register many beans of the same type under different names.
    ///
    /// You can also register types by name and supply a factory method to create
    /// specific instances of the same type. And request instances by name.
    ///
    /// A lightweight approach towards Spring Context, but just enough for my needs.
    /// </summary>
    public static class Context
    {
        #region private
        
        // beans registered by name
        private static readonly Dictionary<string, Bean> BeanNameIndex = new(StringComparer.InvariantCultureIgnoreCase);

        // beans registered by type
        private static readonly Dictionary<string, Bean> BeanTypeIndex = new(StringComparer.InvariantCultureIgnoreCase);
        
        // used to locate types
        private static Locator _locator = new();

        // used to initialize singletons, and registrations
        private static readonly object SyncLock = new();
        
        #endregion
        
        /// <summary>
        /// Initializes the context with the specified assemblies which
        /// determine the scope in which types are located.
        /// </summary>
        public static void Initialize(params Assembly[] assemblies)
        {
            _locator = new Locator(assemblies);

            RegisterTypes<ServiceAttribute>();
            RegisterTypes<BeanAttribute>();

            Ready = true;
        }

        public static bool Ready = false;

        /// <summary>
        /// Registers the specified type argument as the provided for T
        /// </summary>
        public static Bean Register<T>(Type type, Func<object>? factory = null, Lifetime lifetime = Lifetime.Prototype)
            => RegisterType(type, lifetime, ToName(typeof(T)), factory);

        /// <summary>
        /// Registers the specified type, optionally with the specified name.
        /// </summary>
        public static Bean Register<T>(string name = "",  Func<object>? factory = null, Lifetime lifetime = Lifetime.Prototype)
            => RegisterType(typeof(T), lifetime, name, factory);
        
        /// <summary>
        /// Registers the specified type, optionally with the specified name
        /// </summary>
        public static Bean Register(Type type, string name = "", Func<object>? factory = null, Lifetime lifetime = Lifetime.Prototype)
            => RegisterType(type, lifetime, name, factory);

        /// <summary>
        /// Registers a singleton for type T
        /// </summary>
        public static Bean Register<T>(object instance)
        {
            var bean = RegisterType(typeof(T), Lifetime.Singleton, "");
            bean.Instance = instance;
            return bean;
        }

        /// <summary>
        /// Registers a singleton, optionally with the specified name
        /// </summary>
        public static Bean Register(object instance, string name = "")
        {
            var bean = RegisterType(instance.GetType(), Lifetime.Singleton, name);
            bean.Instance = instance;
            return bean;
        }

        /// <summary>
        /// Unregisters a singleton, disposes it if required.
        /// </summary>
        public static void Destroy<T>()
        {
            lock (SyncLock)
            {
                var name = ToName(typeof(T));

                if (!BeanTypeIndex.TryGetValue(name, out var bean)) return;

                if (bean.IsInitialized && bean.Instance is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                BeanTypeIndex.Remove(name);
            }
        }

        /// <summary>
        /// Gets an instance of type T
        /// </summary>
        public static T Get<T>() => (T) Get(typeof(T));

        /// <summary>
        /// Gets an instance of Type, returned a T
        /// </summary>
        public static T Get<T>(Type type) => (T) Get(type);

        /// <summary>
        /// Gets an instance of type TType, returned as T
        /// </summary>
        public static T Get<T, TType>() => (T) Get(typeof(TType));

        /// <summary>
        /// Gets an instance of type T, for the specified name
        /// </summary>
        public static T Get<T>(string name) => (T) Get(name);

        /// <summary>
        /// Gets an instance for the type with the specified name
        /// </summary>
        public static object Get(string name)
        {
            if (!BeanNameIndex.TryGetValue(name, out var b))
                throw new ArgumentException($"unregistered bean: {name}");
            
            return Resolve(b!);
        }
        
        /// <summary>
        /// Gets an instance of the specified type
        /// </summary>
        public static object Get(Type type)
        {
            var name = ToName(type);

            // try resolve by type.
            if (BeanTypeIndex.TryGetValue(name, out var b)) return Resolve(b);
            
            // register a new type
            var newBean = Register(type);
            return Resolve(newBean);
        }
        
        #region private methods

        // tries to find a bean for the specified type, if not found a new bean is registered
        private static Bean Find(Type type, string name)
        {
            // look for it in the names index.
            if (BeanNameIndex.TryGetValue(name, out var namedBean))
            {
                if (type.IsInstanceOfType(namedBean))
                {
                    return namedBean;
                }
            }

            // look for it in the type index
            if (BeanTypeIndex.TryGetValue(ToName(type), out var bean)) return bean;
            
            // register a new type
            return Register(type);
        }
        
        // tries to resolve an instance for the specified bean
        private static object Resolve(Bean bean)
        {
            if (bean is { IsSingleton: true, IsInitialized: true }) return bean.Instance!;

            object instance;

            if (bean.IsFactory)
            {
                // use the factory to create it
                instance = bean.Factory!();
            }
            else
            {
                // otherwise look for an appropriate constructor
                var type = bean.Type;
                var constructors = type.GetConstructors();

                // can't determine which constructor, let the activator try
                if (constructors.Length != 1)
                {
                    instance = Activator.CreateInstance(type);
                }
                else
                {
                    var constructor = constructors.First();
                    var parameters = constructor.GetParameters();

                    // if no parameters, just invoke the default
                    if (parameters.Length == 0)
                    {
                        instance = constructor.Invoke(null);
                    }
                    else
                    {
                        // otherwise recursively resolve arguments
                        var arguments = new object[parameters.Length];

                        for (var i = 0; i < parameters.Length; ++i)
                        {
                            var paramType = parameters[i].ParameterType;
                            var paramName = parameters[i].Name;

                            if (!(paramType.IsClass || paramType.IsInterface && paramType != typeof(string)))
                                throw new InvalidOperationException($"unable to create: {bean.Name}");
                            
                            if (type == paramType)
                                throw new InvalidOperationException($"circular dependency: {bean.Name}");

                            var dependencyBean = Find(paramType, paramName);
                            arguments[i] = Resolve(dependencyBean);
                        }

                        instance = constructor.Invoke(arguments);
                    }
                }
            }

            if (!bean.IsSingleton) return instance;
            
            lock (SyncLock)
            {
                if (!bean.IsInitialized)
                {
                    bean.Instance = instance;
                }
            }

            return bean.Instance!;
        }
        
        // registers a type
        private static Bean RegisterType(Type type, Lifetime lifetime, string name, Func<object>? factory = null)
        {
            var bean = new Bean(lifetime, type, name, factory);
            
            lock (SyncLock)
            {
                // the more general case where we use the type
                if (string.IsNullOrEmpty(name))
                {
                    name = ToName(type);
                    BeanTypeIndex.Add(name, bean);
                    return bean;
                }

                // the more specific case where a name has been provided
                BeanNameIndex.Add(name, bean);
            }

            return bean;
        }

        // registers types with the specified attribute
        private static void RegisterTypes<T>() where T : BeanAttribute
        {
            var items = _locator.FindTypesBy<T>();
            
            foreach (var (type, attribute) in items)
            {
                if (attribute.RegisterAs == null)
                {
                    RegisterType(type, attribute.Lifetime, attribute.Name);
                }
                else
                {
                    RegisterType(attribute.RegisterAs, attribute.Lifetime, attribute.Name, () => Get(type));
                }
            }
        }
        
        // uses the types fullname as an identifier
        private static string ToName(Type type) => type.FullName!;
        
        #endregion
    }
}