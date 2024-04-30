using System;

namespace Select.Rtti
{
    /// <summary>
    /// A BeanAttribute is used to declare beans to the context.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BeanAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the lifetime of the bean, by default it is transient.
        /// </summary>
        public Lifetime Lifetime { get; set; } = Lifetime.Prototype;

        /// <summary>
        /// Gets or sets the name of the bean. If this value is empty then the
        /// name of the type is used.
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// Gets or sets an optional registration type. For example, you
        /// might want to register as per the interface.
        /// </summary>
        public Type? RegisterAs { get; set; } = null;
    }
}