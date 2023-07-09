public class diRandomShape : DisposableItemBase
{
    public override string Name {get;set;} = "Random shape";
    public override void Use()
    {
        _player.RandomizeSize();
    }
}