namespace Nutrilab.Shared.Interfaces.EntityModels
{
    public interface IPermission
    {
        int Id { get; }

        string Name { get; }

        List<IRolePermission> Roles { get; }
    }
}
