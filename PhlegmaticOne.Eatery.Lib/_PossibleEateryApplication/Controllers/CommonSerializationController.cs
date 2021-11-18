namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class CommonSerializationController : EateryApplicationControllerBase
{
    private readonly EateryApplication _eateryApplication;

    public CommonSerializationController()
    {
    }

    public CommonSerializationController(EateryApplication eateryApplication) => _eateryApplication = eateryApplication;
    public void SerializeContainer<TContainer, TSerializer>(IApplicationRequest<string> serializeContainerRequest)
        where TContainer : class
        where TSerializer : IEateryApplicationSerializer<TContainer>, new()
    {
        if (IsInRole(serializeContainerRequest.Worker, nameof(SerializeContainer)) == false)
        {
            return;
        }
        var containerToSerialize = _eateryApplication.GetContainer<TContainer>() as TContainer;
        if (containerToSerialize is null)
        {
            return;
        }
        var serializer = new TSerializer()
        {
            SavingPlacePath = serializeContainerRequest.RequestData1
        };
        serializer.Save(containerToSerialize);
    }

}
