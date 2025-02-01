namespace workingProject.WPAttribute
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class DisableUnitOfWorkAttribute: Attribute
    {
    }
}
