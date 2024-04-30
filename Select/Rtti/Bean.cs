using System;

namespace Select.Rtti
{
    /// <summary>
    /// Represents a type managed by the context.
    /// </summary>
    public class Bean
    {
        /// <summary>
        /// Initializes a new instance of a Bean with the specified arguments.  
        /// </summary>
        public Bean(Lifetime lifetime, Type type, string name = "", Func<object>? factory = null)
        {
            Lifetime = lifetime;

            if (string.IsNullOrWhiteSpace(name))
            {
                name = type.Name;
            }

            Type = type;
            Name = name;
            Factory = factory;
        }

        /// <summary>
        /// Gets the context item lifetime.
        /// </summary>
        public Lifetime Lifetime { get; }
        
        /// <summary>
        /// Gets or sets the Bean's instance.
        /// </summary>
        public object? Instance { get; set; }

        /// <summary>
        /// A factory method to build an instance of the bean.
        /// </summary>
        public Func<object>? Factory { get; }
        
        /// <summary>
        /// Gets a value indicating whether the instance has been initialized
        /// </summary>
        public bool IsInitialized => Instance != null;
        
        /// <summary>
        /// Gets a value indicating whether this bean has a singleton lifetime.
        /// </summary>
        public bool IsSingleton => Lifetime == Lifetime.Singleton;

        /// <summary>
        /// Gets a value indicating whether this bean has a factory method.
        /// </summary>
        public bool IsFactory => Factory != null;
        
        /// <summary>
        /// The name of the bean.
        /// </summary>
        public string Name { get; }
        
        /// <summary>
        /// The type of the bean.
        /// </summary>
        public Type Type { get; }
    }
}