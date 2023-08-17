using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class ShootingServiceInstaller : Installer
{
    [SerializeField] private Transform _gun;
    [SerializeField] private GunInfo _config;
    [SerializeField] private List<Transform> _targets;
    [SerializeField] private BulletHolderCell _cell;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<ShootingService>(Lifetime.Scoped);
        builder.Register<Reloading>(Lifetime.Scoped);
        builder.Register<Magazine>(Lifetime.Scoped);
        builder.RegisterComponent(_config);

        builder.Register(container => 
        {
            var config = container.Resolve<GunInfo>();
            
            return new Aiming(_gun, config); 
        }, Lifetime.Scoped);

        builder.Register(container =>
        {
            var config = container.Resolve<GunInfo>();
            var reloading = container.Resolve<Reloading>();
            var magazine = container.Resolve<Magazine>();
            var aiming = container.Resolve<Aiming>();

            return new Gun(_gun, config, reloading, magazine, aiming);
        builder.Register(container => { return new Aiming(_gun, _targets); }, Lifetime.Scoped);

        builder.Register<BulletHolder>(container =>
        {
            var magazine = container.Resolve<Magazine>();
            var mergeService = container.Resolve<MergeService>();

            return new BulletHolder(_cell, magazine, mergeService);
            
        }, Lifetime.Scoped);
    }
}
