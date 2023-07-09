public class diSizeUP : DisposableItemBase
{
    public override string Name {get;set;} = "SizeUP";
    public override void Use()
    {
        _player.SizeUp();
    }
}