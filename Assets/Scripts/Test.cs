using Sources;
using Sources.Identification;
using Sources.Level;
using Sources.Level.Data;
using UnityEngine;

public class Test : MonoBehaviour {
    private void Start() {
        // Creates a new chunk data storage.
        var chunk = EditorData.World.GetOrCreateChunk(new Vector3Int(0, 0, 0));

        // Fills the storage with a floor of grass.
        for (var y = 0; y < 2; y++) {
            for (var x = 0; x < Chunk.ChunkLength; x++) {
                for (var z = 0; z < Chunk.ChunkLength; z++) {
                    chunk.PlaceBlock(new BlockData(Identifiers.Grass), new Vector3Int(x, y, z));
                }
            }
        }

        //// Writes the world into test.dat.
        //using (var binary = new BinaryWriter(File.OpenWrite("test.dat"))) {
        //    World.Write(binary);
        //}

        // Reads a the world from the file.
        //using (var binary = new BinaryReader(File.OpenRead("test.dat"))) {
        //    World.Read(binary);
        //}
    }
}