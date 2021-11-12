using System.Collections.Generic;
using System.Linq;
using Level.Entities;
using Level.Optimizers;
using Sources;
using Sources.Level;
using UnityEngine;

namespace Level.Generator {
    public class LevelGenerator : MonoBehaviour {
        public GameObject player;
        public GameObject triangle;
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
            var g = Instantiate(triangle, transform);
            var entity = g.GetComponent<Entity>();
            entity.InitPosition(new Vector3Int(3, 2, 3), World);
            World.AddEntity(entity);
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