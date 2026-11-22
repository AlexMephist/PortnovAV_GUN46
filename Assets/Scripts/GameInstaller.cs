using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        // п. 11 ТЗ: создаём Controls и прокидываем в контейнер
        var controls = new Controls();

        Container.BindInstance(controls);
        Container.BindInstance(controls.Game);
    }
}
