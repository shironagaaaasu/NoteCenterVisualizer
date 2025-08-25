using HarmonyLib;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Loader;
using NoteCenterVisualizer.Installers;
using NoteCenterVisualizer.SphereModule;
using SiraUtil.Zenject;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace NoteCenterVisualizer
{
    //[Plugin(RuntimeOptions.DynamicInit)]
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        // TODO: If using Harmony, uncomment and change YourGitHub to the name of your GitHub account, or use the form "com.company.project.product"
        //       You must also add a reference to the Harmony assembly in the Libs folder.
        // public const string HarmonyId = "com.github.YourGitHub.NoteCenterVisualizer";
        // internal static readonly HarmonyLib.Harmony harmony = new HarmonyLib.Harmony(HarmonyId);
        
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        //internal static NoteCenterVisualizerController PluginController { get { return NoteCenterVisualizerController.Instance; } }

        private readonly Harmony _harmony;

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public Plugin(IPALogger logger, Config config, PluginMetadata metadata, Zenjector zenjector)
        {
            
            Instance = this;
            Plugin.Log = logger;


            logger.Info($"{metadata.Name} {metadata.HVersion} {metadata.Author} initialized.");         

            zenjector.UseLogger(logger);
            _harmony = new Harmony("dev.shiro.notecentervisualizer");

            PluginConfig.Instance = config.Generated<PluginConfig>();

            GameObject spherecontroller = new GameObject("SphereController");
            SphereController.Instance = spherecontroller.AddComponent<SphereController>();

            //zenjector.Install<AppInstaller>(Location.App, PluginConfig.Instance);

            zenjector.Install<MenuInstaller>(Location.Menu);

            zenjector.Install<PlayerInstaller>(Location.Player);
        }

        #region BSIPA Config
        //Uncomment to use BSIPA's config
        /*
        [Init]
        public void InitWithConfig(Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Plugin.Log?.Debug("Config loaded");
        }
        */
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            //_harmony.PatchAll(Assembly.GetExecutingAssembly());

            _harmony.PatchAll();
            //Plugin.Log?.Debug($"{PluginConfig.Instance.Enabled} OnApplicationStart");
        }

        [OnExit]
        public void OnApplicationQuit()
        {
            PluginConfig.Instance.ShowPanel = false;
            _harmony.UnpatchSelf();
            //Plugin.Log?.Debug($"{PluginConfig.Instance.Enabled} OnApplicationQuit");
        }

        #region Harmony
        /*
        /// <summary>
        /// Attempts to apply all the Harmony patches in this assembly.
        /// </summary>
        internal static void ApplyHarmonyPatches()
        {
            try
            {
                Plugin.Log?.Debug("Applying Harmony patches.");
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Plugin.Log?.Error("Error applying Harmony patches: " + ex.Message);
                Plugin.Log?.Debug(ex);
            }
        }

        /// <summary>
        /// Attempts to remove all the Harmony patches that used our HarmonyId.
        /// </summary>
        internal static void RemoveHarmonyPatches()
        {
            try
            {
                // Removes all patches with this HarmonyId
                harmony.UnpatchAll(HarmonyId);
            }
            catch (Exception ex)
            {
                Plugin.Log?.Error("Error removing Harmony patches: " + ex.Message);
                Plugin.Log?.Debug(ex);
            }
        }
        */
        #endregion
    }
}
