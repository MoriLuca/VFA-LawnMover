public class diSpeedUp : DisposableItemBase
{
    public override string Name {get;set;} = "Speed Up";
    public override void Use()
    {
        _player.SpeedUp();
    }
}