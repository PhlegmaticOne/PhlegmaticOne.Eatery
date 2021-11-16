namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class DishController
{
    //internal IApplicationRespond<TryToPrepareDishRespondType> BeginPrepare(IApplicationRequest<Recipe> beginPrepareDishRequest)
    //{
    //    var recipe = beginPrepareDishRequest.RequestData1;
    //    var neededIngredients = recipe.IngredientsTakesPartInPreparing;
    //    var neededCapacities = recipe.ProcessesQueueToPrepareDish.GroupBy(k => k.GetType());
    //    var preRespond = CheckForResouces(neededIngredients, neededCapacities);
    //    if(preRespond.RespondType != ApplicationRespondType.Success)
    //    {
    //        return preRespond;
    //    }
    //    foreach (var neededIngredient in neededIngredients)
    //    {
    //        var neededIngredientWeight = neededIngredient.Value;
    //        var neededIngredientType = neededIngredient.Key;
    //        var storagesContainingIngredient = _storageController.GetStoragesContainingIngredientType(neededIngredientType);
    //        for (int i = 0; i < storagesContainingIngredient.Count() && neededIngredientWeight > 0; ++i)
    //        {
    //            var storage = storagesContainingIngredient.ElementAt(i);
    //            var existingWeight = storage.GetExistingWeightOfIngredient(neededIngredientType);
    //            if (existingWeight <= neededIngredientWeight)
    //            {
    //                storage.RetrieveAllIngredient(neededIngredientType);
    //            }
    //            else
    //            {
    //                storage.RetrieveIngredientInWeight(neededIngredientType, neededIngredientWeight);
    //            }
    //            neededIngredientWeight -= existingWeight;
    //        }
    //    }
    //    foreach (var neededCapacity in neededCapacities)
    //    {
    //        _capacityContainer.DecreaseCapacity(neededCapacity.Key, neededCapacity.Count());
    //    }
    //    return new DefaultApplicationRespond<TryToPrepareDishRespondType>()
    //           .Update(TryToPrepareDishRespondType.PreparingBegan, ApplicationRespondType.Success,
    //                   $"You can prepare a dish");
    //}
    //private IApplicationRespond<TryToPrepareDishRespondType> CheckForResouces
    //    (IDictionary<Type, double> neededIngredients, IEnumerable<IGrouping<Type, DomainProductProcess>> neededCapacities)
    //{
    //    var respond = new DefaultApplicationRespond<TryToPrepareDishRespondType>();
    //    var existingIngredients = _storageController.GetAllExistingIngredients(EmptyApplicationRequest.Empty(null)).RespondResult1;
    //    foreach (var neededIngredient in neededIngredients)
    //    {
    //        var neededWeight = neededIngredient.Value;
    //        if (existingIngredients.TryGetValue(neededIngredient.Key, out double existingProductWeight) == false)
    //        {
    //            return respond.Update(TryToPrepareDishRespondType.NotEnoughIngredients,
    //                                  ApplicationRespondType.InternalError,
    //                                  $"Storages does not contain {neededIngredient.Key.Name} at all");
    //        }
    //        if (existingProductWeight < neededWeight)
    //        {
    //            return respond.Update(TryToPrepareDishRespondType.NotEnoughIngredients,
    //                                  ApplicationRespondType.InternalError,
    //                                  $"Storages contain {neededIngredient.Key.Name} in weight of" +
    //                                  $" {existingProductWeight} and you ask {neededWeight}");
    //        }
    //    }
    //    var existingCapacities = _capacityContainer.GetCurrentCapacities();
    //    var possibleCapacities = _capacityContainer.GetPossibleCapacities();
    //    foreach (var neededCapacity in neededCapacities)
    //    {
    //        if (existingCapacities.TryGetValue(neededCapacity.Key, out int existingCapacity) == false)
    //        {
    //            return respond.Update(TryToPrepareDishRespondType.NotEnoughCapacitiesAtAll,
    //                                  ApplicationRespondType.InternalError,
    //                                  $"Eatery does not have capacities for {neededCapacity.Key}");
    //        }
    //        var maximalCapacity = possibleCapacities[neededCapacity.Key];
    //        var neededCapacityCount = neededCapacity.Count();
    //        if (neededCapacityCount > maximalCapacity)
    //        {
    //            return respond.Update(TryToPrepareDishRespondType.NotEnoughCapacitiesAtAll,
    //                                  ApplicationRespondType.InternalError,
    //                                  $"Eatery has {maximalCapacity} capacities for {neededCapacity.Key}" +
    //                                  $" and you ask {neededCapacityCount}");
    //        }
    //        if (neededCapacityCount <= maximalCapacity && neededCapacityCount > existingCapacity)
    //        {
    //            return respond.Update(TryToPrepareDishRespondType.NotEnoghtCapacitiesAtThatMoment,
    //                                  ApplicationRespondType.InternalError,
    //                                  $"Eatery has {existingCapacities} out of {maximalCapacity} capacities" +
    //                                  $" at that moment for {neededCapacity.Key} and you ask {neededCapacity}");
    //        }
    //    }
    //    return respond.Update(TryToPrepareDishRespondType.PreparingBegan, ApplicationRespondType.Success, "");
    //}
}
