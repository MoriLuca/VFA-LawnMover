public class diTeleport : DisposableItemBase
{
    public override string Name {get;set;} = "Teleport";
    public override void Use()
    {
        _player.Teleport();
    }
}