using Onix.Framework.Infra.Data.Interfaces;

namespace Onix.Framework.Infra.Data.Implementation
{
    public class Ordered : IOrdered
    {
        public string PropertyName { get; set; }
        public bool IsDescending { get; set; }

        protected Ordered() { }

        public Ordered(string propertyName, bool isDescending)
        {
            PropertyName = propertyName;
            IsDescending = isDescending;
        }
    }
}