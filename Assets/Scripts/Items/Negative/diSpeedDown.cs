public class diSpeedDown : DisposableItemBase
{
    public override string Name {get;set;} = "Speed Down";
    public override void Use()
    {
        _player.SpeedDown();
    }
}