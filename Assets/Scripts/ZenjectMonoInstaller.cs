using Gameplay;
using Zenject;

public class ZenjectMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IThemeHolder>().To<ThemeHolder>().FromInstance(new ThemeHolder()).AsSingle();
        Container.Bind<ISaveManager>().To<SaveManager>().FromInstance(new SaveManager()).AsSingle();
    }
}