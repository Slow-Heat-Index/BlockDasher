using System.Collections.Generic;
using System.Linq;
using Level.Optimizers;
using Sources;
using Sources.Level;
using UnityEngine;

namespace Level.Generator {
    public class LevelGenerator : MonoBehaviour {
        public GameObject player;

        public GameObject triangle;
        public GameObject sphere;
        public GameObject shark;
        public World World { get; private set; }


        public readonly Dictionary<Material, BlockRenderOptimizer> Optimizers =
            new Dictionary<Material, BlockRenderOptimizer>();

        private void Awake() {
            var editorData = FindObjectOfType<EditorData>();

            if (editorData == null || !editorData.editorPlaying) {
                World = new World(false);
                LevelData.LevelToLoad.Load(World);
                Optimize();
            }
            else {
                World = editorData.World;
            }

            // Create player
            Instantiate(player, transform);
            World.SpawnEntities();
        }


        private void Optimize() {
            var blocks = FindObjectsOfType<BlockView>().Where(it => it.staticBlock).ToList();
            blocks.ForEach(it => it.RefreshViewIfRequired());
            var materials = blocks.Select(it => it.Material).Distinct().ToList();
            foreach (var material in materials) {
                var opGameObject = new GameObject();
                var optimizer = opGameObject.AddComponent<BlockRenderOptimizer>();
                optimizer.material = material;
                optimizer.Init(blocks);
                Optimizers[material] = optimizer;
            }
        }
    }
}