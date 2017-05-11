namespace FrameworkExtensions.ObjectExtensions
{
    public static class DynamicExtensions
    {

        public static dynamic AsDynamic(this object value)
        {
            return (dynamic) value;
        }
    }
}