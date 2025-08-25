using System;
using System.Runtime.CompilerServices;
using IPA.Config.Stores;

//BSMLでのUI生成に必要
[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace NoteCenterVisualizer
{
    internal class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public virtual bool Enabled { get; set; } = true;

        public virtual bool InGame { get; set; } = true;
                
        public virtual bool InMenu { get; set; } = true;

        public virtual float MyHeight { get; set; } = 170f;

        public virtual float ZPosition { get; set; } = 0.9f;

        public virtual float SphereSize { get; set; } = 0.5f;

        public virtual bool ShowPanel { get; set; } = false;
    }
}
