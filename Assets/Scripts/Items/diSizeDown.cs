public class diSizeDown : DisposableItemBase
{
    public string Name {get;set;} = "SizeDown";
    public override void Use()
    {
        _player.SizeDown();
        var item = gameObject.GetComponent<diSizeDown>();
        Destroy(item);
    }
}