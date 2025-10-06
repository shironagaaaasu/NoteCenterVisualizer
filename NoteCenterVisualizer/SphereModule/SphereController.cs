using System.Collections.Generic;
using System.Windows.Forms;
using UnityEngine;
using Zenject;

namespace NoteCenterVisualizer.SphereModule
{
    internal class SphereController : MonoBehaviour
    {
        public static SphereController Instance { get; set; }

        // 座標設定
        private readonly float[] xPositions = [-0.9f, -0.3f, 0.3f, 0.9f];
        private readonly float[] yBasePositions = [-0.05f, 0.5f, 1.0f];
        private readonly List<GameObject> spheres = new List<GameObject>();
        private GameObject frontPlane;
        private GameObject floorPlane;

        public float GameHeight { get; set; } = 0.0f;

        public void RefreshSpheres(bool isMenuScreen)
        {

            // すでに球体が存在していれば削除
            SphereDelete();
            
            if (!PluginConfig.Instance.Enabled) //MOD無効
                return;

            if (isMenuScreen && !PluginConfig.Instance.InMenu) //MOD有効、メニュー画面、メニュー画面で表示しない
                return;

            if (!isMenuScreen && !PluginConfig.Instance.InGame) //MOD有効、プレイ画面、、ゲーム画面で表示しない
                return;

            /*
             * 下記条件に当てはまったら球体表示
             * MOD有効、メニュー画面、メニュー画面で表示
             * MOD有効、プレイ画面、プレイ画面で表示
             */
            CreateSpheres();
        }

        public void RefreshPlane(bool isMenuScreen)
        {
            // 背景パネル削除
            if (frontPlane != null) Destroy(frontPlane);
            if (floorPlane != null) Destroy(floorPlane);

            if (PluginConfig.Instance.Enabled 
                && PluginConfig.Instance.InMenu 
                && PluginConfig.Instance.ShowPanel 
                && isMenuScreen)
            {
                /*
                 * MOD有効、メニュー画面、パネル表示のときのみ表示
                 */
                CreatePlane();
            }
        }

        public void CreateSpheres()
        {
            foreach (var x in xPositions)
            {
                foreach (var y in yBasePositions)
                {
                    var playerHeight = PluginConfig.Instance.AutoSetHeight ? GameHeight : PluginConfig.Instance.MyHeight;
                    float yPosition = y + (playerHeight / 2f);

                    var obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                    //コライダー削除
                    GameObject.Destroy(obj.GetComponent<Collider>()); 

                    /*球体半透明化
                    // マテリアル取得
                    var renderer = obj.GetComponent<Renderer>();

                    if (renderer != null)
                    {
                        // デフォルトだと Standard Shader が割り当てられている
                        renderer.material = new Material(renderer.material);

                        // 色を設定（RGBA）
                        Color color = renderer.material.color;
                        color.a = 0.5f; // 透明度 0.5 (50%)
                        renderer.material.color = color;

                        // レンダリングモードを透明対応にする
                        renderer.material.SetFloat("_Mode", 3); // 3 = Transparent
                        renderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        renderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        renderer.material.SetInt("_ZWrite", 0);
                        renderer.material.DisableKeyword("_ALPHATEST_ON");
                        renderer.material.EnableKeyword("_ALPHABLEND_ON");
                        renderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        renderer.material.renderQueue = 3000;
                    }
                    */

                    obj.transform.position = new Vector3(x, yPosition, PluginConfig.Instance.ZPosition);
                    obj.transform.localScale = Vector3.one * PluginConfig.Instance.SphereSize;
                    obj.SetActive(true);
                    spheres.Add(obj);
                }
            }
        }

        public void SphereDelete()
        {
            // 球体が存在していれば削除
            if (spheres.Count > 0)
            {
                foreach (var s in spheres)
                {
                    if (s != null) Destroy(s);
                }
                spheres.Clear();
            }
        }

        private void CreatePlane()
        {
            //正面パネル
            frontPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            //床パネル
            floorPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);

            // コライダー削除
            var frontcollider = frontPlane.GetComponent<Collider>();
            if (frontcollider != null) frontcollider.enabled = false;

            var floortcollider = frontPlane.GetComponent<Collider>();
            if (floortcollider != null) floortcollider.enabled = false;

            // 正面パネル座標・サイズ調整
            frontPlane.transform.position = new Vector3(0, 0f, 3.6f);
            frontPlane.transform.localScale = new Vector3(1f, 1f, 1f);

            // 回転を正面に
            frontPlane.transform.Rotate(270f, 0f, 0f, Space.World);


            // 床パネル座標・サイズ調整
            floorPlane.transform.position = new Vector3(0, 0.1f, 0);
            floorPlane.transform.localScale = new Vector3(1f, 1f, 1f);


            // 正面パネルマテリアル設定（黒・不透明）
            var frontrenderer = frontPlane.GetComponent<Renderer>();
            frontrenderer.material = new Material(Shader.Find("Standard"));
            frontrenderer.material.SetFloat("_Glossiness", 0f);  // 光沢なし
            frontrenderer.material.SetFloat("_Metallic", 0f);    // 金属光なし
            frontrenderer.material.EnableKeyword("_EMISSION");
            frontrenderer.material.SetColor("_EmissionColor", Color.black); // 自己発光色を黒
            frontrenderer.material.color = Color.black;
            frontrenderer.material.renderQueue = 3001; // 球体より奥に描画

            // 床パネルマテリアル設定（黒・不透明）
            var floorrenderer = floorPlane.GetComponent<Renderer>();
            floorrenderer.material = new Material(Shader.Find("Standard"));
            floorrenderer.material.SetFloat("_Glossiness", 0f);  // 光沢なし
            floorrenderer.material.SetFloat("_Metallic", 0f);    // 金属光なし
            floorrenderer.material.EnableKeyword("_EMISSION");
            floorrenderer.material.SetColor("_EmissionColor", Color.black); // 自己発光色を黒
            floorrenderer.material.color = Color.black;
            //floorrenderer.material.renderQueue = 3001; // 球体より奥に描画
        }
    }
}
