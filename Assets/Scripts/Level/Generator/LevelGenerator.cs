using System.Collections.Generic;
using System.Linq;
using Data;
using Level.Optimizers;
using Sources;
using Sources.Identification;
using Sources.Level;
using Sources.Registration;
using Sources.Skins;
using UnityEngine;

namespace Level.Generator {
    public class LevelGenerator : MonoBehaviour {
        public GameObject player;

        public GameObject triangle;
        public GameObject sphere;
        public GameObject shark;

        public MusicManager Music;
        public World World { get; private set; }


        public readonly Dictionary<Material, BlockRenderOptimizer> Optimizers =
            new Dictionary<Material, BlockRenderOptimizer>();

        private void Awake() {
            var editorData = FindObjectOfType<EditorData>();

            if (editorData == null || !editorData.editorPlaying) {
                World = new World(false);
                LevelData.LevelToLoad.Load(World);
                World.SpawnEntities();
                Optimize();
            }
            else {
                World = editorData.World;
                World.SpawnEntities();
            }

            // Create player
            var skinManager = Registry.Get<Skin>(Identifiers.ManagerSkin);

            print(PersistentDataContainer.PersistentData.skin);
            
            var skin = skinManager.Get(PersistentDataContainer.PersistentData.skin)
                       ?? skinManager.Get(Identifiers.SkinDefault);

            var prefab = Resources.Load<GameObject>(skin.PrefabPath);
            if (prefab == null) {
                skin = skinManager.Get(Identifiers.SkinDefault);
                prefab = Resources.Load<GameObject>(skin.PrefabPath);
            }

            Instantiate(prefab, transform);


            Music.Play(World.Skybox.MusicId);
        }


        private void Optimize() {
            var blocks = FindObjectsOfType<BlockView>().Where(it => it.staticBlock).ToList();
            blocks.ForEach(it => it.RefreshViewIfRequired());
            var materials = blocks.Select(it => it.Material).Distinct().ToList();
            foreach (var material in materials) {
                var opGameObject = new GameObject {
                    transform = {
                        parent = transform
                    }
                };
                var optimizer = opGameObject.AddComponent<BlockRenderOptimizer>();
                optimizer.material = material;
                optimizer.Init(blocks);
                Optimizers[material] = optimizer;
            }
        }
    }
}