using System;

public interface IInfectible
{
    event Action<Obstacle> onInfected;
    bool IsInfected { get; }

    void Infect(float power);
}