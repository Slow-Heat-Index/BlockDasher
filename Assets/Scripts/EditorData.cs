using System;
using System.Collections.Generic;
using Controller.GameEditor.Tool;
using Sources.Identification;
using Sources.Level;
using Sources.Level.Blocks;
using Sources.Level.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorData : MonoBehaviour {
    public static readonly Dictionary<EditorToolType, IEditorTool> Tools =
        new Dictionary<EditorToolType, IEditorTool> {
            { EditorToolType.PlaceBreak, new EditorToolPlaceBreak() },
            { EditorToolType.Selection, new EditorToolSelection() }
        };

    public Canvas canvas;
    public GameObject editorEventSystem;
    public Camera editorCamera;

    public bool editorPlaying = false;
    public EditorToolType selectedEditorTool = EditorToolType.PlaceBreak;


    public World World;
    private BlockType _selectedBlockType;
    private Dictionary<string, string> _metadata;


    public BlockType SelectedBlockType {
        get => _selectedBlockType;
        set {
            _selectedBlockType = value;
            OnSelectedBlockTypeChange?.Invoke(value);
        }
    }

    public Dictionary<string, string> Metadata {
        get => _metadata;
        set {
            _metadata = value;
            OnMetadataChange?.Invoke(value);
        }
    }

    private void Awake() {
        World = new World(true);
        _selectedBlockType = GrassBlock.GrassBlockType.Instance;
        _metadata = new Dictionary<string, string>();

        // Creates a new chunk data storage.
        var chunk = World.GetOrCreateChunk(new Vector3Int(0, 0, 0));

        // Fills the storage with a floor of grass.
        for (var y = 0; y < 2; y++) {
            for (var x = 0; x < Chunk.ChunkLength; x++) {
                for (var z = 0; z < Chunk.ChunkLength; z++) {
                    chunk.PlaceBlock(new BlockData(Identifiers.Grass), new Vector3Int(x, y, z), true);
                }
            }
        }
    }

    /**
     * Event called when the selected block type is changed.
     */
    public event Action<BlockType> OnSelectedBlockTypeChange;

    /**
     * Event called when the whole metadata map is changed.
     * This event doesn't fire if any of the values inside the dictionary is changed.
     */
    public event Action<Dictionary<string, string>> OnMetadataChange;

    public void PlayEditorLevel() {
        canvas.gameObject.SetActive(false);
        editorCamera.gameObject.SetActive(false);
        editorEventSystem.SetActive(false);

        editorPlaying = true;
        SceneManager.LoadScene("Level", LoadSceneMode.Additive);
    }

    public void StopPlayingEditorLevel() {
        canvas.gameObject.SetActive(true);
        editorCamera.gameObject.SetActive(true);
        editorEventSystem.SetActive(true);

        editorPlaying = false;
        World.ResetLevel();
        SceneManager.UnloadSceneAsync("Level");
    }
}