namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public abstract class Worker
{
    protected Worker(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
