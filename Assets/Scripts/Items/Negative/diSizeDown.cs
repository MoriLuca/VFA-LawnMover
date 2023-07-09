public class diSizeDown : DisposableItemBase
{
    public override string Name {get;set;} = "SizeDown";
    public override void Use()
    {
        _player.SizeDown();
    }
}