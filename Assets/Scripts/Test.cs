using System.IO;
using Sources.Identification;
using Sources.Level;
using UnityEngine;

public class Test : MonoBehaviour {
    private void Start() {
        // Creates a new chunk data storage.
        var chunkData = new ChunkData(new Vector3Int(0, 0, 0));

        // Fills the storage with a floor of grass.
        for (var x = 0; x < Chunk.ChunkLength; x++) {
            for (var z = 0; z < Chunk.ChunkLength; z++) {
                chunkData.Blocks[0, x, z] = new BlockData(Identifiers.GRASS);
            }
        }

        // Writes the chunk into test.dat.
        using (var binary = new BinaryWriter(File.OpenWrite("test.dat"))) {
            chunkData.Write(binary);
        }

        // Reads a copy of the chunk from the file.
        var copy = new ChunkData(new Vector3Int(0, 0, 0));
        using (var binary = new BinaryReader(File.OpenRead("test.dat"))) {
            copy.Read(binary);
        }

        // Is the block at (0, 10, 10) grass?
        Debug.Log(chunkData.Blocks[0, 10, 10].Identifier);
        Debug.Log(copy.Blocks[0, 10, 10].Identifier);
    }
}