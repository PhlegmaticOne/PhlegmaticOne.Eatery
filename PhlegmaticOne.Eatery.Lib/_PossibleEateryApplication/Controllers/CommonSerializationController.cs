namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents controller which is responsible for common serialization of applization containers
/// </summary>
public class CommonSerializationController : EateryApplicationControllerBase
{
    private readonly EateryApplication _eateryApplication;
    /// <summary>
    /// Initializes new CommonSerializationController instance
    /// </summary>
    public CommonSerializationController() { }
    /// <summary>
    /// Initializes new CommonSerializationController instance
    /// </summary>
    /// <param name="eateryApplication">Specified eatery application</param>
    public CommonSerializationController(EateryApplication eateryApplication) => _eateryApplication = eateryApplication;
    /// <summary>
    /// Serialize container in application or do nothing if container didn't finded
    /// </summary>
    /// <typeparam name="TContainer">Specified type of serializing container</typeparam>
    /// <typeparam name="TSerializer">Specified type of serialzier</typeparam>
    /// <param name="serializeContainerRequest">Request with path to file to serialize container</param>
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
    /// <summary>
    /// Serialize container asynchronously in application or do nothing if container didn't finded
    /// </summary>
    /// <typeparam name="TContainer">Specified type of serializing container</typeparam>
    /// <typeparam name="TSerializer">Specified type of serialzier</typeparam>
    /// <param name="serializeContainerAsyncRequest">Request with path to file to serialize container</param>
    public async Task SerializeContainerAsync<TContainer, TSerializer>(IApplicationRequest<string> serializeContainerAsyncRequest)
        where TContainer : class
        where TSerializer : IEateryApplicationSerializer<TContainer>, new()
    {
        if (IsInRole(serializeContainerAsyncRequest.Worker, nameof(SerializeContainerAsync)) == false)
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
            SavingPlacePath = serializeContainerAsyncRequest.RequestData1
        };
        await serializer.SaveAsync(containerToSerialize);
    }
}
