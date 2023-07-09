public class diCameraZoomIn : DisposableItemBase
{
    public override string Name {get;set;} = "Zoom In";
    public override void Use() => _gameHandler.ChangeCameraZoom(-2);
    
}